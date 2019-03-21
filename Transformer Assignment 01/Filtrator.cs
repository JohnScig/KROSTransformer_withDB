using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformer_Assignment_01
{
    class Filtrator
    {
        public List<Car> CarDatabase { get; set; }
        public List<Car> FilteredCars { get; set; } = new List<Car>();
        bool selecting = true;
        bool byYear; int minYear = 0; int maxYear = 9999;
        bool byKms; int minKms = 0; int maxKms = int.MaxValue;
        bool byBrand; List<string> brands = new List<string>();
        bool byFuel;
        bool byPrice;
        bool byCity;
        bool byDoors;
        bool byCrashed;

        public Filtrator(List<Car> carLotDatabase)
        {
            CarDatabase = carLotDatabase;
        }

        public void filterCars()
        {
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
                    $"8 - check if you're also looking for crashed vehicles - {byCrashed}\n");

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
                Console.WriteLine("What is the lowest model year?");
                minYear = int.Parse(Console.ReadLine());
                Console.WriteLine("What is the highest model year?");
                maxYear = int.Parse(Console.ReadLine());
            }
            if (byKms)
            {
                Console.WriteLine("What is the lowest kms?");
                minKms = int.Parse(Console.ReadLine());
                Console.WriteLine("What is the highest kms?");
                maxKms = int.Parse(Console.ReadLine());
            }
            if (byBrand)
            {
                Console.WriteLine("Please put in all the brands. Separate them with a white space. Brands containing 2 words written as \"AlfaRomeo\"");
                string userInput = Console.ReadLine();

                char[] separator = new char[] { ' ' };
                brands = userInput.Split(separator).ToList();
                foreach (var brand in brands)
                {
                    Console.WriteLine(brand);
                }
            }

            foreach (Car car in CarDatabase)
            {
                if ((car.ModelYear <= maxYear) && (car.ModelYear >= minYear) && (car.KMs <= maxKms) && (car.KMs >= minKms))
                {
                    if (brands.Count != 0)
                    {
                        if (brands.Contains(car.Brand))
                        { FilteredCars.Add(car); }
                        else
                        { }
                    }
                    else
                    {
                        FilteredCars.Add(car);
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
            Console.Read();
            Console.Clear();


        }



    }



    /*
    public void FilterCarsByYear()
    {
        Console.Clear();
        Console.WriteLine("What is the lowest model year?");
        int minYear = int.Parse(Console.ReadLine());
        Console.WriteLine("What is the highest model year?");
        int maxYear = int.Parse(Console.ReadLine());

        List<Car> filteredCars = new List<Car>();
        foreach (Car car in CarDatabase)
        {
            if ((car.ModelYear <= maxYear) && (car.ModelYear >= minYear))
            {
                filteredCars.Add(car);
            }
        }

        if (filteredCars.Count > 0)
        {
            Console.WriteLine("The following cars fit your search criteria");
            foreach (Car car in filteredCars)
            {
                Console.WriteLine(car.ToString());
            }
        }
        else
        {
            Console.WriteLine("No cars fit your search criteria");
        }

        Console.WriteLine("Press any key to get back to the menu");
        Console.Read();
        Console.Clear();
    }
    */
}

