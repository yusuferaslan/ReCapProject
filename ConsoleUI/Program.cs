using Business.Abstract;
using Business.Concrete;
using DataAccsess.Concrete.EntityFramework;
using DataAccsess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());

            carManager.Add(new Car() { BrandId = 1, ColorId = 3, Description = "TOGG", DailyPrice = 100, ModelYear = 2023 });
            carManager.Add(new Car() { BrandId = 2, ColorId = 4, Description = "Peugeot", DailyPrice = 50, ModelYear = 2021 });
            carManager.Add(new Car() { BrandId = 3, ColorId = 5, Description = "Honda", DailyPrice = 60, ModelYear = 2022 });
            carManager.Add(new Car() { BrandId = 3, ColorId = 5, Description = "H", DailyPrice = 60, ModelYear = 2022 });
            carManager.Add(new Car() { BrandId = 4, ColorId = 2, Description = "Honda", DailyPrice = 0, ModelYear = 2022 });

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
        }
    }
}