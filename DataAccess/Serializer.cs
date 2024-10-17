using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace DataAccess
{
    internal class Serializer
    {
        // Deserialisera podcast från en URL (t.ex. en RSS-feed)
        public Podcast DeSerializeURL(Podcast pod, string url)
        {
            // Skapar en XmlReader för att läsa XML från URL:en
            XmlReader serializer = XmlReader.Create(url);
            // Laddar RSS-feed data
            SyndicationFeed feed = SyndicationFeed.Load(serializer);

            // Går igenom varje objekt (avsnitt) i feeden
            foreach (SyndicationItem item in feed.Items)
            {
                Avsnitt avsnitt = new Avsnitt();
                // Sätter avsnittets titel
                avsnitt.Titel = item.Title.Text;

                // Om ingen beskrivning finns, sätt standardtext
                if (item.Summary == null)
                {
                    avsnitt.Beskrivning = "Saknar beskrivning";
                }
                else
                {
                    // Sätter beskrivningen om den finns
                    avsnitt.Beskrivning = item.Summary.Text;
                }

                // Lägg till avsnittet i podcastens avsnittlista
                pod.AvsnittLista.Add(avsnitt);
                pod.AntalAvsnitt++;
            }

            // Returnerar podcast-objektet med alla avsnitt inlagda
            return pod;
        }

        // Serialisera en lista av podcasts till en XML-fil
        public void Serialize(List<Podcast> podcastList)
        {
            // Anropar en metod för att spara podcast-listan i en fil
            SerializeToFile("podcasts.xml", podcastList);
        }

        // Serialisera en lista av kategorier till en XML-fil
        public void SerializeKategori(List<Kategori> kategoriList)
        {
            // Anropar en metod för att spara kategori-listan i en fil
            SerializeToFile("kategori.xml", kategoriList);
        }

        // Allmän metod för att spara en lista till en XML-fil
        private void SerializeToFile<T>(string fileName, List<T> list)
        {
            try
            {
                // Skapar en XmlSerializer för att hantera den angivna listtypen
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

                // Skapar eller öppnar filen för skrivning
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    // Serialiserar listan och sparar den i XML-format i filen
                    serializer.Serialize(fileStream, list);
                }
            }
            catch (Exception ex) // Fångar specifika fel
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}"); // Loggar felmeddelandet
                throw; // Kasta vidare undantaget
            }
        }

        // Hämta kategorier från en XML-fil och returnera dem som en lista
        public List<Kategori> GetKategoriList()
        {
            // Anropar en metod för att läsa in kategori-listan från en fil
            return DeserializeFromFile<Kategori>("kategori.xml");
        }

        // Hämta podcasts från en XML-fil och returnera dem som en lista
        public List<Podcast> DeSerializePodcast()
        {
            // Anropar en metod för att läsa in podcast-listan från en fil
            return DeserializeFromFile<Podcast>("podcasts.xml");
        }

        // Generell metod för att läsa en lista från en XML-fil
        private List<T> DeserializeFromFile<T>(string fileName)
        {
            // Kontrollera om filen finns
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Filen {fileName} finns inte"); // Meddelande om att filen saknas
                return new List<T>(); // Returnera en tom lista om filen inte finns
            }

            try
            {
                // Skapar en serializer för den typ av lista som anges
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

                // Öppnar filen för att läsa data
                using (FileStream streamIn = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    // Läser in och returnerar listan från filen
                    return (List<T>)serializer.Deserialize(streamIn);
                }
            }
            catch
            {
                throw; // Om något går fel, kasta ett undantag
            }
        }
    }

}