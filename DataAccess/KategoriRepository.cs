using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class KategoriRepository
    {
        Serializer serializer;
        List<Kategori> kategoriLista;

        public KategoriRepository()
        {
            serializer = new Serializer();
            kategoriLista = new List<Kategori>();
            kategoriLista = GetAll();
        }

        public void delete (int index)
        {
            try
            {
                kategoriLista.RemoveAt(index);
                SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public List<Kategori> GetAll()
        {
            List<Kategori> allaKategorier = new List<Kategori>();

            try 
            {
                allaKategorier = serializer.GetKategoriList();
            }
            catch 
            {
                throw;
            }
            return allaKategorier;
        }

        public int GetIndex(String name) 
        {
            int index = -1;

            for (int i = 0; i < kategoriLista.Count; i++) 
            {
                if (string.Equals(namn, kategoriLista[i].Namn, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public void insert(Kategori entity)
        {
            kategoriLista.Add(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            try
            {
                serializer.SerializeKategori(kategoriLista);
            }
            catch
            {
                throw;
            }
        }

        public void Update(int index, Kategori entity)
        {
            delete(index);
            insert(entity);
            SaveChanges();
        }
    }
}
