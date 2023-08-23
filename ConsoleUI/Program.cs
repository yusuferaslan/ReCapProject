using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
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

            //GetAllTest();

            //GetCarsByBrandIdTest();

            //GetCarsByColorIdTest();

            //GetCarDetailsTest();

            //BrandGetAllTest();

            //UserAddTest();

            //CustomerAddTest();

            //RentalManagerAddTest();



        }

        private static void RentalManagerAddTest()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result = rentalManager.Add(new Rental() { Id = 3, CarId = 1, CustomerId = 2, RentDate = DateTime.Now });
            Console.WriteLine(result.Message);
        }

        private static void CustomerAddTest()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            customerManager.Add(new Customer() { Id = 1, UserId = 1, CompanyName = "Aselsan" });
            customerManager.Add(new Customer() { Id = 2, UserId = 2, CompanyName = "Roketsan" });
            customerManager.Add(new Customer() { Id = 3, UserId = 3, CompanyName = "Havelsan" });
        }

        private static void UserAddTest()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            userManager.Add(new User() { Id = 1, FirstName = "Yusuf", LastName = "Eraslan", Email = "yusuferaslan@hotmail.com",  });
            userManager.Add(new User() { Id = 2, FirstName = "Fatih", LastName = "Mehmet", Email = "FSM@hotmail.com",  });
            userManager.Add(new User() { Id = 3, FirstName = "Kemal", LastName = "Mustafa", Email = "MKA@hotmail.com",  });
            userManager.Add(new User() { Id = 4, FirstName = "Engin", LastName = "Demiroğ", Email = "engindemirog@hotmail.com",  });
        }

        private static void BrandGetAllTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            var result = brandManager.GetAll();
            foreach (var brand in result.Data)
            {
                Console.WriteLine(brand.Name + brand.Id);
            }
        }

        private static void CarAddTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());

            carManager.Add(new Car() { BrandId = 1, ColorId = 1, Description = "T10x", DailyPrice = 100, ModelYear = 2023 });
            carManager.Add(new Car() { BrandId = 2, ColorId = 2, Description = "3008", DailyPrice = 50, ModelYear = 2021 });
            carManager.Add(new Car() { BrandId = 3, ColorId = 3, Description = "Civic", DailyPrice = 60, ModelYear = 2022 });
            carManager.Add(new Car() { BrandId = 1, ColorId = 4, Description = "T10x 4WD", DailyPrice = 60, ModelYear = 2022 });
            carManager.Add(new Car() { BrandId = 3, ColorId = 2, Description = "Accord", DailyPrice = 10, ModelYear = 2022 });
            carManager.Add(new Car() { BrandId = 3, ColorId = 2, Description = "Type R", DailyPrice = 10, ModelYear = 2022 });
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
                Console.WriteLine("Marka: {0} Model: {1} Renk: {2} Fiyat: {3}", cardetails.BrandName, cardetails.CarName, cardetails.ColorName, cardetails.DailyPrice);
            }
        }
    }
}