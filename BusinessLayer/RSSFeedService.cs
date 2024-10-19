using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;
using Modell;

namespace BusinessLayer
{
    public class RSSFeedService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<Avsnitt>> HamtaRSSFlodeAsync(string rssUrl)
        {
            if (string.IsNullOrEmpty(rssUrl))
            {
                throw new ArgumentException("RSS URL kan inte vara tom.");
            }

            List<Avsnitt> avsnitt = new List<Avsnitt>();

            try
            {
                // Hämta RSS-flödet
                HttpResponseMessage response = await client.GetAsync(rssUrl);

                // Kontrollera om svaret är framgångsrikt
                response.EnsureSuccessStatusCode();

                // Hämta innehållet som en sträng
                string rssContent = await response.Content.ReadAsStringAsync();

                // Kontrollera om RSS-innehållet är tomt
                if (string.IsNullOrEmpty(rssContent))
                {
                    throw new Exception("Inget innehåll hittades i RSS-flödet.");
                }

                // Parsar RSS-flödet
                XDocument rssXml = XDocument.Parse(rssContent);

                // Extrahera relevant information från RSS-flödet
                var items = from item in rssXml.Descendants("item")
                            select new Avsnitt
                            {
                                Titel = item.Element("title")?.Value ?? "Titel saknas",
                                Beskrivning = item.Element("description")?.Value ?? "Beskrivning saknas"
                            };

                avsnitt = items.ToList();
            }
            catch (HttpRequestException e)
            {
                // Fel vid hämtning av RSS-flödet
                Console.WriteLine($"Fel vid hämtning av RSS-flödet: {e.Message}");
                throw new Exception($"Kunde inte hämta RSS-flödet: {e.Message}");
            }
            catch (Exception ex)
            {
                // Generellt fel vid behandling av RSS-flödet
                Console.WriteLine($"Fel vid behandling av RSS-flödet: {ex.Message}");
                throw new Exception($"Fel vid behandling av RSS-flödet: {ex.Message}");
            }

            return avsnitt;
        }
    }
}
