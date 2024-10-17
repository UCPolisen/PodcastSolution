using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        void Insert(T entity);
        void Update(int index, T entity);
        void Delete(int index);
        void SaveChanges();

        int GetIndex(string namn);

    }
}

