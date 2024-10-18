using Modell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
        public interface IRepositoryMedia<T> : IRepository<Podcast>
        {
            List<T> GetByKategori(string kategori);
        }
}
