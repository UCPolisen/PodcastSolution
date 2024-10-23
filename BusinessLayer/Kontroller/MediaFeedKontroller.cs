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

        // Konstruktor som initierar repository för podcasts
        public MediaFeedKontroller()
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
        public int GetIndexMediaFeed(string podcastNamn)
        {
            List<Podcast> podLista = GetAllMediaFeed(); // Hämta alla podcasts

            for (int i = 0; i < podLista.Count; i++)
            {
                // Null-conditional operator används för att hantera podLista[i] och podLista[i].Namn
                if (podLista[i]?.Namn?.Equals(podcastNamn, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return i; // Returnera index om podcasten hittas
                }
            }
            return -1; // Returnera -1 om podcasten inte hittas
        }


        public int GetPodcastIndex(string podcastName)
        {
            return reposi.GetIndex(podcastName);
        }


        //a
        // Hämtar en lista med podcasts som tillhör en specifik kategori
        public List<Podcast> GetByKategoriMediaFeed(string Kategori)
        {
            List<Podcast> podLista = new List<Podcast>(); // Skapar en tom lista för podcasts
            podLista = reposi.GetByKategori(Kategori); // Hämtar podcasts som matchar kategorin
            return podLista; // Returnerar listan med podcasts för den valda kategorin
        }

        public List<Avsnitt> GetAvsnittForPodcast(Podcast podcast)
        {
            // Kontrollera om podcasten inte är null och innehåller avsnitt
            if (podcast != null && podcast.AvsnittLista != null)
            {
                return podcast.AvsnittLista; // Returnera listan med avsnitt
            }
            else
            {
                return new List<Avsnitt>(); // Returnera en tom lista om inga avsnitt hittades
            }
        }

    }
}
