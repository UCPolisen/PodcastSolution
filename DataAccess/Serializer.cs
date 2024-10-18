using Modell;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication; // Glöm inte att lägga till referens till System.ServiceModel
using System.Xml;
using System.Xml.Serialization;

namespace DataAccess
{
    internal class Serializer
    {
        // Laddar podcast från en angiven URL (t.ex. RSS-feed)
        public Podcast LoadPodcastFromUrl(Podcast podcast, string feedUrl)
        {
            // Skapar en XmlReader för att hämta XML-data från URL:en
            using (XmlReader reader = XmlReader.Create(feedUrl))
            {
                // Laddar RSS-feed data
                SyndicationFeed feed = SyndicationFeed.Load(reader);

                // Loopar igenom varje avsnitt i flödet
                foreach (SyndicationItem item in feed.Items)
                {
                    Avsnitt episode = new Avsnitt
                    {
                        // Sätter avsnittets titel
                        Titel = item.Title.Text,
                        // Sätter avsnittets beskrivning
                        Beskrivning = item.Summary?.Text ?? "Saknar beskrivning"
                    };

                    // Lägger till avsnittet i podcastens avsnittlista
                    podcast.AvsnittLista.Add(episode);
                    podcast.AntalAvsnitt++;
                }
            }

            // Returnerar podcast-objektet med alla avsnitt
            return podcast;
        }

        // Sparar en lista av podcasts i en XML-fil
        public void SavePodcastsToFile(List<Podcast> podcasts)
        {
            SaveToXmlFile("podcasts.xml", podcasts);
        }

        // Sparar en lista av kategorier i en XML-fil
        public void SaveCategoriesToFile(List<Kategori> categories)
        {
            SaveToXmlFile("kategori.xml", categories);
        }

        // Allmän metod för att spara en lista som XML
        private void SaveToXmlFile<T>(string fileName, List<T> items)
        {
            try
            {
                // Skapar en XmlSerializer för den angivna listan
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

                // Skapar en ny fil eller öppnar en befintlig fil för skrivning
                using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    // Serialiserar listan och sparar den i XML-format
                    xmlSerializer.Serialize(stream, items);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}"); // Loggar felmeddelandet
                throw; // Kasta vidare undantaget
            }
        }

        // Hämtar kategorier från en XML-fil och returnerar dem som en lista
        public List<Kategori> LoadCategoriesFromFile()
        {
            return LoadFromXmlFile<Kategori>("kategori.xml");
        }

        // Hämtar podcasts från en XML-fil och returnerar dem som en lista
        public List<Podcast> LoadPodcastsFromFile()
        {
            return LoadFromXmlFile<Podcast>("podcasts.xml");
        }

        // Generell metod för att läsa en lista från en XML-fil
        private List<T> LoadFromXmlFile<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Filen {fileName} finns inte."); // Meddelande om att filen saknas
                return new List<T>(); // Returnerar en tom lista
            }

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    // Deserialiserar listan från filen
                    return (List<T>)xmlSerializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}"); // Loggar felmeddelandet
                throw; // Kasta vidare undantaget
            }
        }
    }
}
