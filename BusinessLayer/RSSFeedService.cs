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
            List<Avsnitt> avsnitt = new List<Avsnitt>();
            try
            {
                // Hämta RSS-flödet
                HttpResponseMessage response = await client.GetAsync(rssUrl);
                response.EnsureSuccessStatusCode();
                string rssContent = await response.Content.ReadAsStringAsync();

                // Parsar RSS-flödet
                XDocument rssXml = XDocument.Parse(rssContent);

                // Extrahera relevant information från RSS-flödet
                var items = from item in rssXml.Descendants("item")
                            select new Avsnitt
                            {
                                Titel = item.Element("title")?.Value,
                                Beskrivning = item.Element("description")?.Value
                            };

                avsnitt = items.ToList();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Fel vid hämtning: {e.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }

            return avsnitt;
        }
    }
}
