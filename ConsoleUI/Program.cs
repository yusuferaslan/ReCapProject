using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());

            //carManager.Add(new Car() { BrandId = 1, ColorId = 1, Description = "Elektrik", DailyPrice = 100, ModelYear = 2023 });
            //carManager.Add(new Car() { BrandId = 2, ColorId = 2, Description = "Dizel", DailyPrice = 50, ModelYear = 2021 });
            //carManager.Add(new Car() { BrandId = 3, ColorId = 3, Description = "Benzin", DailyPrice = 60, ModelYear = 2022 });
            //carManager.Add(new Car() { BrandId = 1, ColorId = 4, Description = "Hibrit", DailyPrice = 60, ModelYear = 2022 });
            //carManager.Add(new Car() { BrandId = 3, ColorId = 2, Description = "LPG", DailyPrice = 10, ModelYear = 2022 });

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }


            foreach (var car in carManager.GetCarsByBrandId(1))
            {
                Console.WriteLine(car.BrandId +" "+ car.Description);
            }


            foreach (var car in carManager.GetCarsByColorId(5))
            {
                Console.WriteLine(car.ColorId +" "+ car.Description);
            }


            foreach (var cardetails in carManager.GetCarDetails())
            {
                Console.WriteLine("Yakıt Tipi: {0} Marka: {1} Renk: {2} Fiyat: {3}", cardetails.CarName, cardetails.BrandName, cardetails.ColorName, cardetails.DailyPrice);
            }

        }
    }
}