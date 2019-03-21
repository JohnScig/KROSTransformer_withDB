using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformer_Assignment_01
{
    class Car
    {

        #region FuelEnum

        public enum FuelEnum
        {
            Diesel, Petrol, Biodiesel, Electric, Hybrid, Hydrogen
        }



        #endregion

        #region Properties
        public int ID { get; }
        public int ModelYear { get; set; }
        public int KMs { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public FuelEnum Fuel { get; set; }
        //public string Fuel { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public int Doors { get; set; }
        public bool Crashed { get; set; }

        #endregion

        #region Constructors
        public Car(int id, int modelYear, int kMs, string brand, string model, Car.FuelEnum fuel, decimal price, string city, int doors, bool crashed)
        {
            ID = id;
            ModelYear = modelYear;
            KMs = kMs;
            Brand = brand;
            Model = model;
            Fuel = fuel;
            Price = price;
            City = city;
            Doors = doors;
            Crashed = crashed;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method describing the car in specific format.
        /// </summary>
        /// <returns> String representation of the car, properties divided by \t, readable by CatLor </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{ID}\t{ModelYear}\t{KMs}\t{Brand}\t{Model}\t{Fuel}\t{Price}\t{City}\t{Doors}\t{Crashed}");

            return sb.ToString();
        }

        /// <summary>
        /// Method changes specific property. User will specify, which property he wants changed, then inputs new value for the selected property.
        /// </summary>
        public void ChangeProperty()
        {
            int propertyToChange = 0;
            string newValue;
            Console.WriteLine("Which property would you like to change? Type in one of the following: " +
                "\n1 - Year \n2 - Kilometers \n3 - Brand \n4 - Model \n5 - Fuel \n6 - Price \n7 - City \n8 - Doors \n9 - Crashed");
            if (int.TryParse(Console.ReadLine(), out propertyToChange))
            {

            }
            else { propertyToChange = 99; newValue = "0"; }

            switch (propertyToChange)
            {

                case (1): { ModelYear = Checker.CheckYear(); IndicateChange(); break; }
                case (2): { KMs = Checker.CheckKms(); IndicateChange(); break; }
                case (3):
                    {
                        Console.WriteLine("What is the new brand?");
                        newValue = Console.ReadLine();
                        Brand = newValue; IndicateChange(); break;
                    }
                case (4):
                    {
                        Console.WriteLine("What is the new model?");
                        newValue = Console.ReadLine();
                        Model = newValue; IndicateChange(); break;
                    }
                case (5): { Fuel = Checker.CheckFuel(); IndicateChange(); break; }
                case (6): { Price = Checker.CheckPrice(); IndicateChange(); break; }
                case (7):
                    {
                        Console.WriteLine("What is the new city?");
                        newValue = Console.ReadLine();
                        City = (newValue); IndicateChange(); break;
                    }
                case (8): { Doors = Checker.CheckDoors(); IndicateChange(); break; }
                case (9): { Crashed = Checker.CheckCrashed(); IndicateChange(); break; }
                default: { Console.WriteLine("Wrong input, nothing was changed in the database"); this.ToString(); break; }
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Short method that indicates if a change has happened. Used in ChangeProperty() to save time writing.
        /// </summary>
        void IndicateChange()
        {
            Console.Clear();
            Console.WriteLine("Value has been changed.");
            Console.WriteLine(this.ToString());
        }

        /// <summary>
        /// Method that returns selection of fuels in our Fuel Enum. Saves time writing.
        /// </summary>
        /// <returns></returns>
        public static string getFuelTypes()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var fuelType in (Car.FuelEnum[])Enum.GetValues(typeof(Car.FuelEnum)))
            {
                sb.Append($" {fuelType};");
                //Console.Write($" {fuelType};");
            }
            return sb.ToString();
        }
        #endregion

    }
}

//carDatabase = AddCar(new Car(carDatabase.Count, 2018, 150, "Skoda", "Fabia", "Petrol", 10000, "Nove Mesto", 5, false), carDatabase, "car.txt");
//carDatabase = AddCar(new Car(carDatabase.Count, 2017, 785, "Skoda", "Octavia", "Petrol", 10000, "Zilina", 5, false), carDatabase, "car.txt");
//carDatabase = AddCar(new Car(carDatabase.Count, 2016, 999, "Skoda", "Felicia", "Diesel", 10000, "Partizanske", 3, false), carDatabase, "car.txt");
//carDatabase = AddCar(new Car(carDatabase.Count, 2014, 123, "Skoda", "Superb", "Petrol", 10000, "Bratislava", 5, true), carDatabase, "car.txt");
//carDatabase = AddCar(new Car(carDatabase.Count, 2015, 151, "Skoda", "Citygo", "Electric", 10000, "Bratislava", 3, false), carDatabase, "car.txt");



