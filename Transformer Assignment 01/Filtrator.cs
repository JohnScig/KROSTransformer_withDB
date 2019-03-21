using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformer_Assignment_01
{
    class Filtrator
    {
        public static void FilterCars(List<Car> carLotDatabase)
        {
            List<Car> CarsToFilter = new List<Car>(carLotDatabase);
            List<Car> FilteredCars = new List<Car>(CarsToFilter);
            
            bool selecting = true;
            bool byYear = false;
            bool byKms = false;
            bool byBrand = false;
            bool byFuel = false;
            bool byPrice = false;
            bool byCity = false;
            bool byDoors = false;
            bool byCrashed = false;


            Console.Clear();
            Console.WriteLine("Welcome to the filtration tool");
            while (selecting)
            {
                Console.Clear();
                Console.WriteLine("Please select the filtration criteria. Press the number of filter you'd like to use and press Enter. When done, type in a 0");
                Console.WriteLine($"1 - filter by model year - {byYear}\n" +
                    $"2 - filter by km's driven - {byKms}\n" +
                    $"3 - filter by carmaker - {byBrand}\n" +
                    $"4 - filter by fuel - {byFuel}\n" +
                    $"5 - filter by price - {byPrice}\n" +
                    $"6 - filter by city - {byCity}\n" +
                    $"7 - filter by doors - {byDoors}\n" +
                    $"8 - check if you'd like to include crashed vehicles - {byCrashed}\n");

                int selection = int.Parse(Console.ReadLine());

                switch (selection)
                {
                    case (0): { selecting = false; break; }
                    case (1): { byYear = !byYear; break; }
                    case (2): { byKms = !byKms; break; }
                    case (3): { byBrand = !byBrand; break; }
                    case (4): { byFuel = !byFuel; break; }
                    case (5): { byPrice = !byPrice; break; }
                    case (6): { byCity = !byCity; break; }
                    case (7): { byDoors = !byDoors; break; }
                    case (8): { byCrashed = !byCrashed; break; }
                    default: { Console.WriteLine("Wrong input"); break; }
                }
            }

            if (byYear)
            {
                int minYear = Checker.CheckYear("What is the lowest model year?");
                int maxYear = Checker.CheckYear("What is the highest model year?");
                foreach (Car car in CarsToFilter)
                {
                    if ((car.ModelYear <= minYear) || (car.ModelYear >= maxYear))
                    {
                        if (FilteredCars.Contains(car))
                        {
                            FilteredCars.Remove(car);
                        }
                    }
                }
            }

            if (byKms)
            {
                int minKms = Checker.CheckKms("What is the lowest kms?");
                int maxKms = Checker.CheckKms("What is the highest kms?");

                foreach (Car car in CarsToFilter)
                {
                    if ((car.KMs <= minKms) || (car.KMs >= maxKms))
                    {
                        if (FilteredCars.Contains(car))
                        {
                            FilteredCars.Remove(car);
                        }
                    }
                }

            }

            if (byBrand)
            {
                List<string> brands = new List<string>();
                bool moreBrands = true;
                do
                {
                    string userInput = Checker.CheckString("Type in a brand. Brands containing 2 words written as \"AlfaRomeo\"");
                    brands.Add(userInput);

                    Console.WriteLine("Would you like to add another brand to the filter? Type \"yes\" if so, any other input will mean this was the last brand");
                    string userAnswer = Console.ReadLine();
                    if (userAnswer.Equals("yes") || userAnswer.Equals("Yes") || userAnswer.Equals("YES"))
                    {
                        moreBrands = true;
                    }
                    else
                    {
                        moreBrands = false;
                    }
                } while (moreBrands);

                foreach (Car car in CarsToFilter)
                {
                    bool carToKeep = false;
                    foreach (string brand in brands)
                    {
                        if (car.Brand.Equals(brand))
                        {
                            carToKeep = true;
                        }
                    }
                    if (!carToKeep)
                    {
                        if (FilteredCars.Contains(car))
                        {
                            FilteredCars.Remove(car);
                        }
                    }
                }
            }

            if (byFuel)
            {
                List<Car.FuelEnum> fuels = new List<Car.FuelEnum>();
                bool moreFuels = true;
                do
                {
                    Car.FuelEnum userInput = Checker.CheckFuel($"Type in a fuel type. Select one of the following:{Car.getFuelTypes()}");
                    fuels.Add(userInput);

                    Console.WriteLine("Would you like to add another fuel to the filter? Type \"yes\" if so, any other input will mean this was the last brand");
                    string userAnswer = Console.ReadLine();
                    if (userAnswer.Equals("yes") || userAnswer.Equals("Yes") || userAnswer.Equals("YES"))
                    {
                        moreFuels = true;
                    }
                    else
                    {
                        moreFuels = false;
                    }
                } while (moreFuels);

                foreach (Car car in CarsToFilter)
                {
                    bool carToKeep = false;
                    foreach (Car.FuelEnum fuel in fuels)
                    {
                        if (car.Fuel.Equals(fuel))
                        {
                            carToKeep = true;
                        }
                    }
                    if (!carToKeep)
                    {
                        if (FilteredCars.Contains(car))
                        {
                            FilteredCars.Remove(car);
                        }
                    }
                }
            }

            if (byPrice)
            {

                decimal minPrice = Checker.CheckPrice("What is the lowest price?");
                decimal maxPrice = Checker.CheckPrice("What is the highest price?");

                foreach (Car car in CarsToFilter)
                {
                    if ((car.Price <= minPrice) || (car.Price >= maxPrice))
                    {
                        if (FilteredCars.Contains(car))
                        {
                            FilteredCars.Remove(car);
                        }
                    }
                }

            }

            if (byCity)
            {
                List<string> cities = new List<string>();
                bool moreCities = true;
                do
                {
                    string userInput = Checker.CheckString("Type in a city.");
                    cities.Add(userInput);

                    Console.WriteLine("Would you like to add another city to the filter? Type \"yes\" if so, any other input will mean this was the last brand");
                    string userAnswer = Console.ReadLine();
                    if (userAnswer.Equals("yes") || userAnswer.Equals("Yes") || userAnswer.Equals("YES"))
                    {
                        moreCities = true;
                    }
                    else
                    {
                        moreCities = false;
                    }
                } while (moreCities);

                foreach (Car car in CarsToFilter)
                {
                    bool carToKeep = false;
                    foreach (string city in cities)
                    {
                        if (car.City.Equals(city))
                        {
                            carToKeep = true;
                        }
                    }
                    if (!carToKeep)
                    {
                        if (FilteredCars.Contains(car))
                        {
                            FilteredCars.Remove(car);
                        }
                    }
                }
            }

            if (byDoors)
            {
                int doorValue = Checker.CheckKms("What is the required number of doors?");

                foreach (Car car in CarsToFilter)
                {
                    if (!(car.Doors == doorValue))
                    {
                        if (FilteredCars.Contains(car))
                        {
                            FilteredCars.Remove(car);
                        }
                    }
                }

            }

            if (!byCrashed)
            {
                foreach (Car car in CarsToFilter)
                {
                    if ((car.Crashed == true))
                    {
                        if (FilteredCars.Contains(car))
                        {
                            FilteredCars.Remove(car);
                        }
                    }
                }
            }

            if (FilteredCars.Count > 0)
            {
                Console.WriteLine("The following cars fit your search criteria");
                foreach (Car car in FilteredCars)
                {
                    Console.WriteLine(car.ToString());
                }
            }
            else
            {
                Console.WriteLine("No cars fit your search criteria");
            }

            Console.WriteLine("Press any key to get back to the menu");
            Console.ReadLine();
            Console.Clear();
        }
    }
}

