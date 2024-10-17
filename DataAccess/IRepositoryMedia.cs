using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class IRepositoryMedia
    {
        public interface IRepositoryMedia : IRepository<Podcast>
        {
            List<T> GetByKategori(string kategori);
        }
    }
}
