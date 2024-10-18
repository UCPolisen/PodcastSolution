using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Modell;
using DataAccess;

namespace Business.Controller
{
    // Kontrollerklass för att hantera MediaFeed, specifikt podcasts
    public class MediaFeedController
    {
        // Skapar en referens till repositoryt för mediafeeds (här podcasts)
        IRepositoryMedia<Podcast> reposi;

        // Konstruktor som initierar repository för podcasts
        public MediaFeedController()
        {
            reposi = new MediaRepository(); // Initialiserar ett repository-objekt
        }

        // Skapar en ny podcast och lägger till den i repository
        public void CreateMediaFeed(string url, string namn, Kategori kategori)
        {
            Podcast newPodcast = new Podcast(url, namn, kategori); // Skapar en ny Podcast-objekt
            reposi.Insert(newPodcast); // Lägger till den nya podcasten i repository
        }

        // Tar bort en podcast från repository baserat på dess namn
        public void DeleteMediaFeed(string namn)
        {
            int index = reposi.GetIndex(namn); // Hämtar indexet för podcasten med det angivna namnet
            reposi.Delete(index); // Tar bort podcasten vid det indexet
        }

        // Uppdaterar en podcast i repository på en viss plats (index)
        public void UpdateMediaFeed(int index, string url, string namn, Kategori kategori)
        {
            Podcast pod = new Podcast(url, namn, kategori); // Skapar en ny podcast med de nya värdena
            reposi.Update(index, pod); // Uppdaterar podcasten på den angivna indexpositionen
        }

        // Hämtar en lista med alla podcasts i repository
        public List<Podcast> GetAllMediaFeed()
        {
            List<Podcast> podListan = new List<Podcast>(); // Skapar en tom lista för podcasts
            podListan = reposi.GetAll(); // Hämtar alla podcasts från repository
            return podListan; // Returnerar listan med podcasts
        }

        // Hämtar index för en podcast baserat på dess namn
        public int GetIndexMediaFeed(string namn)
        {
            return reposi.GetIndex(namn); // Returnerar indexet för podcasten med det angivna namnet
        }

        // Hämtar en lista med podcasts som tillhör en specifik kategori
        public List<Podcast> GetByKategoriMediaFeed(string Kategori)
        {
            List<Podcast> podLista = new List<Podcast>(); // Skapar en tom lista för podcasts
            podLista = reposi.GetByKategori(Kategori); // Hämtar podcasts som matchar kategorin
            return podLista; // Returnerar listan med podcasts för den valda kategorin
        }
    }
}
