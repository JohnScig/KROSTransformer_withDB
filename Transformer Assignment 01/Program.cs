using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformer_Assignment_01
{
    class Program
    {
        static CarLot carLot = StartCarLot();

        static void Main(string[] args)
        {

            //Filtrator filtrator = new Filtrator(carLot.carDatabase);
            //LoadCars should ask for text file address, clean carDatabase then load cars from new textfile and work with only this text file

            MenuController();

        }

        /// <summary>
        /// Method which guides the user through various functions of the application.
        /// 
        /// </summary>
        static void MenuController()
        {
            bool quit = true;

            while (quit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to your car lot database program");
                Console.WriteLine("What would you like to do? Please press the number associated with a menu item");

                Console.WriteLine("1 - Load cars into the database from a text file");
                Console.WriteLine("2 - Save cars from the database to a text file");
                Console.WriteLine("3 - Show all cars currently in the database");
                Console.WriteLine("4 - Add a new car to the database");
                Console.WriteLine("5 - Remove a car from the database");
                Console.WriteLine("6 - Change a property of a car from the database");
                Console.WriteLine("7 - Filter tool");
                Console.WriteLine("\nPress any other number to quit");

                int controller;
                if (int.TryParse(Console.ReadLine(), out controller)) { }
                else
                {
                    Console.WriteLine("Wrong input. Please put in a number associated with your choice. Press any key to continue.");
                    Console.ReadKey();
                    continue;
                }

                switch (controller)
                {
                    case (1): { carLot.LoadCars(); break; }
                    case (2): { carLot.SaveCars(); break; }
                    case (3): { carLot.ShowCars(); break; }
                    case (4): { carLot.AddCar(); break; }
                    case (5): { carLot.RemoveCar(); break; }
                    case (6): { carLot.ChangeCar(); break; }
                    case (7): { Filtrator filtrator = new Filtrator(carLot.carDatabase); filtrator.FilterCars(); break; }
                    default: { quit = false; break; }
                }

            }

            Console.WriteLine("Thank you for using our application");
            Console.ReadKey();




        }

        /// <summary>
        /// This method creates the car lot. It asks for input from the user - the user can define a path to his .txt file with an already
        /// existing database. If such file does not exist, user doesn't have one, or it is corrupted, the car lot is created
        /// with a default empty .txt file with no cars in it.
        /// </summary>
        /// <returns>Car lot</returns>
        static CarLot StartCarLot()
        {
            Console.WriteLine("Welcome to the Car Lot Database Program. \nPlease type in the name of the txt file which holds your data. Leave empty for default database.");
            Console.WriteLine("These are the .txt files currently in the default folder:");
            DirectoryInfo dinfo = new DirectoryInfo(@"C: \Users\transformer10\source\repos\Transformer Assignment 01\Transformer Assignment 01\bin\Debug");
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            foreach (FileInfo file in Files)
            {
                Console.WriteLine(file.Name);
            }

            string databasePath = Console.ReadLine();

            if (databasePath.Equals(""))
            {
                return new CarLot();
            }
            else
            {
                return new CarLot(databasePath);
            }
        }
    }
}
