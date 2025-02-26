﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        //[TransactionScopeAspect] //>> Hata yönetimi
        //[PerformanceAspect(5)] //>> Bu metodun çalışması 5 sn geçerse beni uyar. Sadece bu metodu kontrol eder. Ama AspectInterceptorSelector içine yazılırsa tüm metodları kontrol eder.
        public IResult Add(Car car)
        {
            if (car.Description.Length > 2 && car.DailyPrice > 0)
            {

                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);

            }
            else
            {
                return new ErrorResult(Messages.CarNotAdded);
            }

        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheRemoveAspect("ICarService.Get")] //>>Bellekte ICarService'deki bütün Get'leri sil demek
        //[CacheRemoveAspect("Get")] //>> Bellekte içerisinde Get olan tüm keyleri sil demek
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 4)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == carId));
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == colorId));
        }
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsId(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(p => p.Id == carId));
        }
    }
}
