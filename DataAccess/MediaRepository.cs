using Modell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MediaRepository : IRepositoryMediaFeed<Podcast>
    {
        Serializer serializer;
        List<Podcast> podcastLista;

        // Konstruktor för att initiera serializer och hämta alla podcasts
        public MediaRepository()
        {
            serializer = new Serializer();
            podcastLista = new List<Podcast>();
            podcastLista = GetAll();
        }

        // Hämtar podcasts baserat på kategori
        public List<Podcast> GetByKategori(string Kategori)
        {
            List<Podcast> podcastByKategori = new List<Podcast>();
            foreach (Podcast item in podcastLista)
            {
                if (string.Equals(item.Kategori.Namn, Kategori, StringComparison.OrdinalIgnoreCase))
                {
                    podcastByKategori.Add(item);
                }
            }

            return podcastByKategori;
        }

        // Lägger till en ny podcast till listan och sparar ändringarna
        public void Insert(Podcast pod)
        {
            try
            {
                pod = serializer.DeSerializeURL(pod, pod.Url);
                podcastLista.Add(pod);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Uppdaterar en podcast genom att ta bort den gamla och lägga till den nya
        public void Update(int index, Podcast pod)
        {
            Delete(index);
            Insert(pod);
        }

        // Hämtar indexet för en podcast baserat på dess namn
        public int GetIndex(string namn)
        {
            int index = -1; // Börja med att sätta index till -1 (ingen matchning).

            for (int i = 0; i < podcastLista.Count; i++)
            {
                if (string.Equals(namn, podcastLista[i].Namn, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        // Hämtar alla podcasts från serializer
        public List<Podcast> GetAll()
        {
            List<Podcast> listaMedAllaPodcats = new List<Podcast>();

            try
            {
                listaMedAllaPodcats = serializer.DeSerializePodcast();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaMedAllaPodcats;
        }

        // Tar bort en podcast från listan baserat på index och sparar ändringarna
        public void Delete(int index)
        {
            if (index >= 0 && index < podcastLista.Count)
            {
                podcastLista.RemoveAt(index);
                SaveChanges();
            }
            else
            {
                throw new IndexOutOfRangeException("Indexet du har angett finns ej i listan");
            }
        }

        // Sparar alla ändringar till listan med podcasts
        public void SaveChanges()
        {
            try
            {
                serializer.Serialize(podcastLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

