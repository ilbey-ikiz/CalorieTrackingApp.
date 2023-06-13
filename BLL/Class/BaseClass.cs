using DAL;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BaseClass<T> : ICrud<T> where T : BaseClass
    {
        public BaseClass()
        {
            db = new CalorieTrackingDbContext();
        }
        public CalorieTrackingDbContext db;
        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public bool Add(T item)
        {
            db.Set<T>().Add(item);
            return db.SaveChanges() > 0;
        }

        public bool Update(T item)
        {
            db.Set<T>().Update(item);

            return db.SaveChanges() > 0;
        }

        public bool Remove(int id)
        {
            T entity = db.Set<T>().Find(id);
            db.Set<T>().Remove(entity);
            return db.SaveChanges() > 0;
        }
        public T GetById(int id)
        {
            T entity = db.Set<T>().Find(id);
            return entity;
        }

        


    }
}
