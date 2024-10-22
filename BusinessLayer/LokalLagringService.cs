using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Modell;

namespace BusinessLayer
{
    public class LokalLagringService
    {
        // Metod för att hämta och fylla på en Podcast med avsnitt från en RSS-länk
        public Podcast HämtaPodcastFrånRSS(string rssUrl, string namn, Kategori kategori)
        {
            Podcast podcast = new Podcast(rssUrl, namn, kategori);

            try
            {
                using (XmlReader reader = XmlReader.Create(rssUrl))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(reader);

                    if (feed != null)
                    {
                        podcast.AvsnittLista = new List<Avsnitt>();

                        foreach (SyndicationItem item in feed.Items)
                        {
                            Avsnitt avsnitt = new Avsnitt
                            {
                                Titel = item.Title.Text,
                                Beskrivning = item.Summary.Text
                            };
                            podcast.AvsnittLista.Add(avsnitt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hantera fel
                Console.WriteLine($"Fel vid hämtning av RSS: {ex.Message}");
            }

            return podcast;
        }
    }
}