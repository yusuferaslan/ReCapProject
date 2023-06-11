using DataAccsess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Concrete.EntityFramework
{
    public class EfCarDal : ICarDal
    {
        public void Add(Car entity)
        {
            using (ReCapDbContext context = new ReCapDbContext())
            {
                var addedEntity = context.Entry(entity);   //Git veri kaynağından, benim gönderdiğim Add(Car entity) nesnesinin referansını yakala
                addedEntity.State = EntityState.Added;     //O aslında eklenecek bir nesne
                context.SaveChanges();                     //Ve şimdi ekle(bu işlemi yap)
            }
        }

        public void Delete(Car entity)
        {
            using (ReCapDbContext context = new ReCapDbContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            using (ReCapDbContext context = new ReCapDbContext())
            {
                // contextdeki car tablosunu ele al ve linq sorgusu olarak singleordefault sorgusunu uygula
                return context.Set<Car>().SingleOrDefault(filter);
            }
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapDbContext context = new ReCapDbContext())
            {
                //Recapdbcontext DbSet deki bütün car tablosunu listeye çevir ve bana ver(arka planda select * from car calıstırıyor ve listeye ceviriyor)                
                return filter == null                               //<<< filtre null mı ?
                    ? context.Set<Car>().ToList()                  //<<< evetse bunu çalıştır 
                    : context.Set<Car>().Where(filter).ToList();   //<<< filtre null değil filtre varsa filtreleyip ver
            }
        }

        public void Update(Car entity)
        {
            using (ReCapDbContext context = new ReCapDbContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}
