using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformer_Assignment_01
{
    class CarRepository
    {
        public static string ServerName { get; set; } = @"TRANSFORMER10\SQLEXPRESS2017";
        public static string DatabaseName { get; set; } = "CarlotDB";
        public string ConnString { get; set; } = $"Server={ServerName}; Database = {DatabaseName}; Trusted_Connection = True";

        public List<Car> GetAll()
        {
            List<Car> carsFromDB = new List<Car>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM [Car] LEFT JOIN [Fuel] ON Car.ID_Fuel = Fuel.ID_Fuel";

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Car.FuelEnum thisFuel = (Car.FuelEnum)Enum.Parse(typeof(Car.FuelEnum), reader.GetString(11));
                                Car loadedCar = new Car(reader.GetInt32(0),reader.GetInt32(2), reader.GetInt32(3),reader.GetString(4),
                                    reader.GetString(5),thisFuel,reader.GetDecimal(6),reader.GetString(7),reader.GetInt32(8),reader.GetBoolean(9));
                                carsFromDB.Add(loadedCar);
                            }
                        }
                    }
                }
                return carsFromDB;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return carsFromDB;
            }
        }

        public Car GetByID(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM [Car] LEFT JOIN [Fuel] ON Car.ID_Fuel = Fuel.ID_Fuel WHERE Car.ID_Car = @idInput";
                        command.Parameters.Add("@idInput", SqlDbType.Int).Value = id;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Car.FuelEnum thisFuel = (Car.FuelEnum)Enum.Parse(typeof(Car.FuelEnum), reader.GetString(11));
                                Car loadedCar = new Car(reader.GetInt32(0), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4),
                                    reader.GetString(5), thisFuel, reader.GetDecimal(6), reader.GetString(7), reader.GetInt32(8), reader.GetBoolean(9));
                                return loadedCar;
                            }
                        }
                    }
                }
                return new Car();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return new Car();
            }
        }

        public int AddNewCar(Car car)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"INSERT INTO Car (ID_Car,ID_Fuel,ModelYear,Mileage,Brand,Model,Price,City,Doors,Crashed) " +
                            $"VALUES (@ID_Car,@ID_Fuel,@ModelYear,@Mileage,@Brand,@Model,@Price,@City,@Doors,@Crashed)";
                        command.Parameters.Add("@ID_Car", SqlDbType.Int).Value = car.ID;
                        command.Parameters.Add("@ID_Fuel", SqlDbType.Int).Value = Convert.ToInt32(car.Fuel);
                        command.Parameters.Add("@ModelYear", SqlDbType.Int).Value = car.ModelYear;
                        command.Parameters.Add("@Mileage", SqlDbType.Int).Value = car.KMs;
                        command.Parameters.Add("@Brand", SqlDbType.NVarChar).Value = car.Brand;
                        command.Parameters.Add("@Model", SqlDbType.NVarChar).Value = car.Model;
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = car.Price;
                        command.Parameters.Add("@City", SqlDbType.NVarChar).Value = car.City;
                        command.Parameters.Add("@Doors", SqlDbType.Int).Value = car.Doors;
                        command.Parameters.Add("@Crashed", SqlDbType.Bit).Value = car.Crashed ? 1 : 0;
                        command.ExecuteNonQuery();
                    }
                }
                return car.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 0;
            }
        }

        public bool UpdateCar(Car car)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Car SET ID_Fuel= ID_Fuel, ModelYear=@ModelYear, Mileage= @Mileage, " +
                            "Brand=@Brand, Model=@Model, Price=@Price,City= @City, Doors=@Doors, Crashed= @Crashed " +
                            "WHERE ID_Car = @ID_Car";
                        command.Parameters.Add("@ID_Car", SqlDbType.Int).Value = car.ID;
                        command.Parameters.Add("@ID_Fuel", SqlDbType.Int).Value = Convert.ToInt32(car.Fuel);
                        command.Parameters.Add("@ModelYear", SqlDbType.Int).Value = car.ModelYear;
                        command.Parameters.Add("@Mileage", SqlDbType.Int).Value = car.KMs;
                        command.Parameters.Add("@Brand", SqlDbType.NVarChar).Value = car.Brand;
                        command.Parameters.Add("@Model", SqlDbType.NVarChar).Value = car.Model;
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = car.Price;
                        command.Parameters.Add("@City", SqlDbType.NVarChar).Value = car.City;
                        command.Parameters.Add("@Doors", SqlDbType.Int).Value = car.Doors;
                        command.Parameters.Add("@Crashed", SqlDbType.Bit).Value = car.Crashed ? 1 : 0;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            Console.WriteLine("Entry editted");
                        }
                        else
                        {
                            Console.WriteLine("shit's all fucked up");
                        }
                    }

                }
                return true;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }


        }

        public bool DeleteCar(Car car)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM Car WHERE ID_Car = @idToDelete";
                        command.Parameters.Add("@idToDelete", SqlDbType.Int).Value = car.ID;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            Console.WriteLine("Entry successfully deleted from Person");
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }


        }

    }
}
