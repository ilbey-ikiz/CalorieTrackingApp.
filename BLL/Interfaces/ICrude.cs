using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    
    public interface ICrud<T>  where T : class
    {

        public List<T> GetAll();
        public bool Add(T item);
        public bool Update(T item);
        public bool Remove(int id);
        public T GetById(int id);
    }
}
