using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);   //Git veri kaynağından, benim gönderdiğim Add(Car entity) nesnesinin referansını yakala
                addedEntity.State = EntityState.Added;     //O aslında eklenecek bir nesne
                context.SaveChanges();                     //Ve şimdi ekle(bu işlemleri yap)
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                // contextdeki car tablosunu ele al ve linq sorgusu olarak singleordefault sorgusunu uygula
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //Recapdbcontext DbSet deki bütün car tablosunu listeye çevir ve bana ver(arka planda select * from car calıstırıyor ve listeye ceviriyor)                
                return filter == null                               //<<< filtre null mı ?
                    ? context.Set<TEntity>().ToList()                  //<<< evetse bunu çalıştır 
                    : context.Set<TEntity>().Where(filter).ToList();   //<<< filtre null değil filtre varsa filtreleyip ver
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}
