﻿using DataAccsess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
            new Car { Id = 1, BrandId = 1, ColorId = 4, DailyPrice = 100, ModelYear = 2023, Description = "TOG" },
            new Car { Id = 2, BrandId = 2, ColorId = 5, DailyPrice = 300, ModelYear = 2020, Description = "Tesla" },
            new Car { Id = 3, BrandId = 3, ColorId = 2, DailyPrice = 500, ModelYear = 2015, Description = "Ford" },
            new Car { Id = 4, BrandId = 4, ColorId = 4, DailyPrice = 400, ModelYear = 2020, Description = "Mercedes" },
            new Car { Id = 5, BrandId = 5, ColorId = 1, DailyPrice = 600, ModelYear = 2018, Description = "Fiat" },

            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetById(int byId)
        {
            return _cars.Where(c=> c.Id == byId).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.FirstOrDefault(c => c.Id == car.Id);
            carToUpdate.Id= car.Id;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId= car.ColorId;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;

        }
    }
}