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
            //CarAddTest();

            GetAllTest();

            //GetCarsByBrandIdTest();

            //GetCarsByColorIdTest();

            //GetCarDetailsTest();

        }

        private static void CarAddTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());

            carManager.Add(new Car() { BrandId = 1, ColorId = 1, Description = "Elektrik", DailyPrice = 100, ModelYear = 2023 });
            carManager.Add(new Car() { BrandId = 2, ColorId = 2, Description = "Dizel", DailyPrice = 50, ModelYear = 2021 });
            carManager.Add(new Car() { BrandId = 3, ColorId = 3, Description = "Benzin", DailyPrice = 60, ModelYear = 2022 });
            carManager.Add(new Car() { BrandId = 1, ColorId = 4, Description = "Hibrit", DailyPrice = 60, ModelYear = 2022 });
            carManager.Add(new Car() { BrandId = 3, ColorId = 2, Description = "LPG", DailyPrice = 10, ModelYear = 2022 });
        }

        private static void GetAllTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());

            var result = carManager.GetAll();
            if (result.Success == true)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine(car.Description);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void GetCarsByBrandIdTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarsByBrandId(1).Data)
            {
                Console.WriteLine(car.BrandId + " " + car.Description);
            }
        }

        private static void GetCarsByColorIdTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarsByColorId(5).Data)
            {
                Console.WriteLine(car.ColorId + " " + car.Description);
            }
        }

        private static void GetCarDetailsTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());

            var result = carManager.GetCarDetails();

            foreach (var cardetails in result.Data)
            {
                Console.WriteLine("Yakıt Tipi: {0} Marka: {1} Renk: {2} Fiyat: {3}", cardetails.CarName, cardetails.BrandName, cardetails.ColorName, cardetails.DailyPrice);
            }
        }
    }
}