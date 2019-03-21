using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformer_Assignment_01
{
    static class Checker
    {
        /// <summary>
        /// Checks for correct input when user inputs Model Year.
        /// </summary>
        /// <returns>Correct model year of the car</returns>
        public static int CheckYear()
        {
            while (true)
            {
                Console.WriteLine("What is the year of production?");
                string stringYear = Console.ReadLine();
                int intYear;
                if (int.TryParse(stringYear, out intYear))
                {
                    if ((intYear >= 1870) && (intYear <= DateTime.Now.Year))
                    {
                        return intYear;
                    }
                    else
                    {
                        Console.WriteLine($"Wrong input. Please input a number between 1870 and {DateTime.Now.Year}. Press any key to continue");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input. Please put in a number. Press any key to continue.");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        /// <summary>
        /// Checks for correct input when user inputs kilometers.
        /// </summary>
        /// <returns>Correct number of kilometers of the car</returns>
        public static int CheckKms()
        {
            while (true)
            {
                Console.WriteLine("How many kilometers has the car driven?");
                string stringKms = Console.ReadLine();
                int intKms;
                if (int.TryParse(stringKms, out intKms))
                {
                    if (intKms >= 0)
                    {
                        return intKms;
                    }
                    else
                    {
                        Console.WriteLine($"Wrong input. Please input an integer larger than or equal to 0. Press any key to continue");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input. Please put in an integer. Press any key to continue");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        /// <summary>
        /// Checks for correct input when user inputs type of fuel.
        /// </summary>
        /// <returns>Correct type of fuel of the car</returns>
        public static Car.FuelEnum CheckFuel()
        {
            while (true)
            {
                Console.WriteLine($"What fuel does the car take? Select one of the following:{Car.getFuelTypes()}");
                string stringFuel = Console.ReadLine();
                Car.FuelEnum enumFuel;
                if (Enum.TryParse(stringFuel, out enumFuel))
                {
                    int intFuel;
                    if (int.TryParse(stringFuel, out intFuel))
                    {
                        Console.WriteLine($"Wrong input. Please put in one of the following options:{Car.getFuelTypes()} \nPress any key to continue.");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        return enumFuel;
                    }
                }
                else
                {
                    Console.WriteLine($"Wrong input. Please put in one of the following options:{Car.getFuelTypes()} \nPress any key to continue.");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        /// <summary>
        /// Checks for correct input when user inputs price.
        /// </summary>
        /// <returns>Correct price of the car</returns>
        public static decimal CheckPrice()
        {
            while (true)
            {
                Console.WriteLine("What is the car's price?");
                string stringPrice = Console.ReadLine();
                decimal decimalPrice;
                if (decimal.TryParse(stringPrice, out decimalPrice))
                {
                    if (decimalPrice >= 0)
                    {
                        return decimalPrice;
                    }
                    else
                    {
                        Console.WriteLine($"Wrong input. Please input a number larger than or equal to 0. Press any key to continue");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input. Please put in a number. Press any key to continue");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        /// <summary>
        /// Checks for correct input when user inputs number of doors.
        /// </summary>
        /// <returns>Correct number of doors of the car</returns>
        public static int CheckDoors()
        {
            while (true)
            {
                Console.WriteLine("Doors?");
                string stringDoors = Console.ReadLine();
                int intDoors;
                if (int.TryParse(stringDoors, out intDoors))
                {
                    if ((intDoors == 3) || (intDoors == 5))
                    {
                        return intDoors;
                    }
                    else
                    {
                        Console.WriteLine($"Wrong input. Please input 3 or 5 as number of doors. Press any key to continue");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input. Please put in an integer. Press any key to continue");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        /// <summary>
        /// Checks for correct input when user inputs crashed flag.
        /// </summary>
        /// <returns>Correct crashed flag of the car</returns>
        internal static bool CheckCrashed()
        {
            while (true)
            {
                Console.WriteLine("Crashed?");
                string stringCrashed = Console.ReadLine();
                bool boolCrashed;
                if (bool.TryParse(stringCrashed, out boolCrashed))
                {
                    return boolCrashed;
                }
                else
                {
                    Console.WriteLine("Wrong input. Please put in a boolean value - \"True\" or \"False\". Press any key to continue");
                    Console.ReadKey();
                    continue;
                }
            }
        }
    }
}
