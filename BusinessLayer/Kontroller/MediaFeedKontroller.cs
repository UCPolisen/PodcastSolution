using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Modell;
using DataAccess;

namespace BusinessLayer.Kontroller
{
    // Kontrollerklass för att hantera MediaFeed, specifikt podcasts
    public class MediaFeedKontroller
    {
        // Skapar en referens till repositoryt för mediafeeds (här podcasts)
        IRepositoryMedia<Podcast> reposi;

        public MediaFeedKontroller()
        {
            reposi = new MediaRepository();
        }

        public void CreateMediaFeed(string url, string namn, Kategori kategori)
        {
            Podcast newPodcast = new Podcast(url, namn, kategori);
            reposi.Insert(newPodcast); 
        }

        public void DeleteMediaFeed(string namn)
        {
            int index = reposi.GetIndex(namn); 
            reposi.Delete(index); 
        }

        public void UpdateMediaFeed(int index, string url, string namn, Kategori kategori)
        {
            Podcast pod = new Podcast(url, namn, kategori); 
            reposi.Update(index, pod);
        }

        public List<Podcast> GetAllMediaFeed()
        {
            List<Podcast> podListan = new List<Podcast>();
            podListan = reposi.GetAll();
            return podListan;
        }

        public int GetIndexMediaFeed(string podcastNamn)
        {
            List<Podcast> podLista = GetAllMediaFeed(); 

            for (int i = 0; i < podLista.Count; i++)
            {
                if (podLista[i]?.Namn?.Equals(podcastNamn, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return i; 
                }
            }
            return -1;
        }


        public int GetPodcastIndex(string podcastName)
        {
            return reposi.GetIndex(podcastName);
        }

        public List<Podcast> GetByKategoriMediaFeed(string Kategori)
        {
            List<Podcast> podLista = new List<Podcast>();
            podLista = reposi.GetByKategori(Kategori);
            return podLista; 
        }

        public List<Avsnitt> GetAvsnittForPodcast(Podcast podcast)
        {
            if (podcast != null && podcast.AvsnittLista != null)
            {
                return podcast.AvsnittLista; 
            }
            else
            {
                return new List<Avsnitt>(); 
            }
        }

    }
}
