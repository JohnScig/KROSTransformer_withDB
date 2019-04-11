﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformer_Assignment_01
{
    class CarLot
    {
        #region Properties

        /// <summary>
        /// ArrayList that serves as the on-RAM list of cars in the car lot
        /// </summary>
        public List<Car> carDatabase { get; set; } = new List<Car>();

        /// <summary>
        /// Holds the string value of the path to the .txt that serves as HDD database
        /// </summary>
        public string FilePath { get; set; } = "defaultCarLot.txt";

        /// <summary>
        /// Property necessary for creating unique IDs 
        /// </summary>
        public int Incrementor { get; set; } = 0;

        private CarRepository carRepository = new CarRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor - loads the default .txt file as the database.
        /// </summary>
        public CarLot() { LoadCars(); }

        /// <summary>
        /// 
        /// Constructor. String textDatabase rewrites the detault filePath, which allows for loading custom .txt files into the program.
        /// </summary>
        /// <param name="textDatabase"></param>
        public CarLot(string textDatabase)
        {
            FilePath = textDatabase;
            LoadCars();
        }

        #endregion

        #region Methods



        /// <summary>
        /// Saves the entire ArrayList to the .txt file. To avoid issues, the .txt file is deleted first, then created anew and ArrayList is written into it.
        /// </summary>
        public void SaveCars()
        {
            File.Delete(FilePath);
            Console.WriteLine("The following car has been successfully added to the database:");
            foreach (Car car in carDatabase)
            {
                File.AppendAllText(FilePath, car.ToString() + "\n");
                Console.WriteLine(car.ToString());
            }
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Loads cars into an ArrayList. The .txt file with cars is set at startup. If user doesn't specify, it's defaultCarLot.txt.
        /// </summary>
        public void LoadCars()
        {
            List<Car> carList = new List<Car>();
            try
            {
                string[] lines = File.ReadAllLines(FilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('\t');
                    Car.FuelEnum thisFuel = (Car.FuelEnum)Enum.Parse(typeof(Car.FuelEnum), parts[5]);
                    carList.Add(new Car(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), parts[3], parts[4], thisFuel,
                        decimal.Parse(parts[6]), parts[7], int.Parse(parts[8]), bool.Parse(parts[9])));
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file you tried to load is missing.\n" +
                    "A new empty database file will be created.\n" +
                    "Press any key to continue");
                Console.ReadKey();
                if (!FilePath.Contains(".txt"))
                {
                    FilePath += ".txt";
                }
            }
            catch (Exception)
            {
                FilePath = CreateFilePath();
                Console.WriteLine("The file you tried to load is corrupted or the data in it doesn't fit the required format.\n" +
                    $"A new empty database file was created named {FilePath}\n" +
                    "Press any key to continue");
                Console.ReadKey();
                FilePath = CreateFilePath();
            }


            if (carList.Count != 0)
            {
                Console.WriteLine("The following cars have been successfully loaded from the database:");
                foreach (var car in carList)
                {
                    Console.WriteLine(car.ToString());
                }
            }
            else
            {
                Console.WriteLine("Your text file is empty.");
            }

            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();
            carDatabase = carList;
            Incrementor = getHighestID();
        }

        /// <summary>
        /// Loads cars into an ArrayList from an SQL Database
        /// </summary>
        public void LoadCarsSQL()
        {
            carDatabase = carRepository.GetAll();

            if (carDatabase.Count != 0)
            {
                Console.WriteLine("The following cars have been successfully loaded from the database:");
                foreach (var car in carDatabase)
                {
                    Console.WriteLine(car.ToString());
                }
            }
            else
            {
                Console.WriteLine("Your text file is empty.");
            }

            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();
            Incrementor = getHighestID();
        }

        /// <summary>
        /// Prints out information about a single car from the SQL Database
        /// </summary>
        /// <returns></returns>
        public bool GetCarByIDSQL()
        {
            int idToSelect;

            if (carDatabase.Count == 0)
            {
                Console.WriteLine("The database is empty. Press any key to continue");
                Console.ReadKey();
                return false;
            }

            Console.Clear();
            ShowCars();
            Console.WriteLine("Which car would you like to change? Please type in its ID");
            if (int.TryParse(Console.ReadLine(), out idToSelect)) { } else { Console.WriteLine("Wrong input. Please put in an integer ID"); Console.ReadKey(); }

            Console.Clear();
            Console.WriteLine(carRepository.GetByID(idToSelect).ToString());

            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
            return true;
        }


        /// <summary>
        /// Prints Car.ToString() for each car in the ArrayList. Prints a message in case the ArrayList is empty.
        /// </summary>
        public void ShowCars()
        {
            if (carDatabase.Count == 0)
            {
                Console.WriteLine("Your database is empty. Load cars from a text file or add cars manually");
            }
            else
            {
                Console.WriteLine("These are the cars currently stored in your car lot");
                foreach (Car car in carDatabase)
                {
                    Console.WriteLine(car.ToString());
                }
            }
            Console.WriteLine("\nPress any key to return to continue");
            Console.ReadKey();
        }

        /// <summary>
        /// Adds a car to the ArrayList. The method asks for user's input in defining the car, then calls the Car constructor. 
        /// After the car is created, it's added to the ArrayList and also written into the .txt file.
        /// This method is used when inputing cars manually
        /// </summary>
        public void AddCar()
        {
            Console.WriteLine("Please define the specific parameters of your car:");

            int modelYear = Checker.CheckYear("What is the year of production?");

            int kms = Checker.CheckKms("How many kilometers has the car driven?");

            string brand = Checker.CheckString("What is the brand of this car? If there are 2 words, please join them: AlfaRomeo");

            string model = Checker.CheckString("What is the model of this car?");

            Car.FuelEnum fuel = Checker.CheckFuel($"What fuel does the car take? Select one of the following:{Car.getFuelTypes()}");

            decimal price = Checker.CheckPrice("What is the car's price?");

            string city = Checker.CheckString("Where is the car being sold?");

            int doors = Checker.CheckDoors("How many doors does the car have?");

            bool crashed = Checker.CheckCrashed("Has the car been crashed?");

            Car newCar = new Car(++Incrementor, modelYear, kms, brand, model, fuel, price, city, doors, crashed);

            AddCarSQL(newCar);
            carDatabase.Add(newCar);
            File.AppendAllText(FilePath, newCar.ToString() + "\n");
            Console.WriteLine("The following car has been successfully added to the database:");
            Console.WriteLine(newCar.ToString());
            Console.WriteLine("Press any key to continue to the main menu");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Adds a car to the ArrayList. Also saves this car to the .txt file
        /// This method is used in loading cars from textfile.
        /// </summary>
        /// <param name="car"> The instance of class Car to be added to the ArrayList</param>
        public void AddCar(Car car)
        {
            carDatabase.Add(car);
            File.AppendAllText(FilePath, (car.ToString() + "\n"));
            Console.WriteLine("The following car has been successfully added to the database:");
            Console.Write(car.ToString());
        }

        /// <summary>
        /// Adds the car to the SQL Database
        /// </summary>
        /// <param name="car"></param>
        public void AddCarSQL(Car car)
        {
            carDatabase.Add(car);
            if (carRepository.AddNewCar(car) != 0)
            {
                Console.WriteLine("Car successfully added to SQL Database");
            }
            else
            {
                Console.WriteLine("Car not added to the SQL Database");
            }
        }

        /// <summary>
        /// Asks user for ID of the car, then removes said car from the ArrayList
        /// </summary>
        public bool RemoveCar()
        {
            int idToRemove;
            bool carRemoved = false;

            if (carDatabase.Count == 0)
            {
                Console.WriteLine("The database is empty. Press any key to continue");
                Console.ReadKey();
                return false;
            }

            Console.Clear();
            ShowCars();
            Console.WriteLine("Which car would you like to remove? Please type in its ID");
            if (int.TryParse(Console.ReadLine(), out idToRemove)) { } else { Console.WriteLine("Wrong input. Please put in an integer ID"); Console.ReadKey(); }

            foreach (Car car in carDatabase)
            {
                if (car.ID == idToRemove)
                {
                    Console.Clear();
                    Console.WriteLine(car.ToString());
                    carDatabase.Remove(car);
                    carRepository.DeleteCar(car);
                    carRemoved = true;
                    break;
                }
            }

            if (carRemoved)
            {
                Console.WriteLine("This car was removed successfully.");
            }
            else
            {
                Console.WriteLine("Car with such ID was not found in the database.");
            }

            Console.ReadKey();
            SaveCars();

            return true;
        }

        /// <summary>
        /// Asks user for ID of the car, then calls the change function on the car.
        /// </summary>
        public bool ChangeCar()
        {
            int idToChange;
            bool carChanged = false;

            if (carDatabase.Count == 0)
            {
                Console.WriteLine("The database is empty. Press any key to continue");
                Console.ReadKey();
                return false;
            }

            Console.Clear();
            ShowCars();
            Console.WriteLine("Which car would you like to change? Please type in its ID");
            if (int.TryParse(Console.ReadLine(), out idToChange)) { } else { Console.WriteLine("Wrong input. Please put in an integer ID"); Console.ReadKey(); }

            foreach (Car car in carDatabase)
            {
                if (car.ID == idToChange)
                {
                    Console.Clear();
                    Console.WriteLine(car.ToString());
                    car.ChangeProperty();
                    if (carRepository.UpdateCar(car))
                    {
                        Console.WriteLine("Car updated in database");
                    }
                    carChanged = true;
                    break;
                }
            }

            if (carChanged)
            {
                Console.WriteLine("This car was changed successfully.");
            }
            else
            {
                Console.WriteLine("Car with such ID was not found in the database.");
            }

            Console.ReadKey();
            SaveCars();

            return true;
        }

        /// <summary>
        /// Runs through the ArrayList, checks each car's ID, returns the highest one.
        /// </summary>
        /// <returns> Highest car ID in the ArrayList </returns>
        public int getHighestID()
        {
            foreach (var car in carDatabase)
            {
                if (car.ID > Incrementor)
                {
                    Incrementor = car.ID;
                }
            }
            return Incrementor;
        }

        /// <summary>
        /// Creates a new string representing current day and time of day to help create a unique database file which also shows when the database was created.
        /// </summary>
        /// <returns>String in the following format: defaultCarLot_day_(DayOfYear)_time_(time of day).txt </returns>
        public string CreateFilePath()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("defaultCarLot_");
            sb.Append("day_" + DateTime.Now.DayOfYear);
            sb.Append("_time_" + DateTime.Now.Hour);
            sb.Append(DateTime.Now.Minute);
            sb.Append(DateTime.Now.Second);
            sb.Append(".txt");
            return sb.ToString();
        }
        #endregion

    }
}
