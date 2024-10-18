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
        public Podcast DeSerializeURL(Podcast pod, string url)
        {
            XmlReader serializer = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(serializer);

            foreach (SyndicationItem item in feed.Items)
            {
                Avsnitt avsnitt = new Avsnitt();
                avsnitt.Titel = item.Title.Text;
                if (item.Summary == null)
                {
                    avsnitt.Beskrivning = "Saknar beskrivning";
                }
                else
                {
                    avsnitt.Beskrivning = item.Summary.Text;
                }

                pod.AvsnittLista.Add(avsnitt);
                pod.AntalAvsnitt++;
            }
            return pod;

        }

        public void Serialize(List<Podcast> podcastList)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Podcast>));
                using (FileStream utStream = new FileStream("podcasts.xml", FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(utStream, podcastList);
                }

            }
            catch
            {
                throw;
            }


        }

        public void SerializeKategori(List<Kategori> kategoriList)
        {

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Kategori>));
                using (FileStream utStream = new FileStream("kategori.xml", FileMode.Create, FileAccess.Write))
                {

                    serializer.Serialize(utStream, kategoriList);
                }

            }
            catch
            {
                throw;
            }

        }


        public List<Kategori> GetKategoriList()
        {
            List<Kategori> kategoriLista = new List<Kategori>();
            try
            {
                if (!File.Exists("kategori.xml"))
                {
                    return new List<Kategori>();
                }

                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Kategori>));
                    using (FileStream streamIn = new FileStream("kategori.xml", FileMode.Open, FileAccess.Read))
                    {
                        kategoriLista = (List<Kategori>)serializer.Deserialize(streamIn);
                    }

                    return kategoriLista;
                }
            }

            catch
            {
                throw;
            }
        }




        public List<Podcast> DeSerializePodcast()
        {
            List<Podcast> returnLista = new List<Podcast>();
            try
            {
                if (!File.Exists("podcasts.xml"))
                {
                    Console.WriteLine("filen finns inte");
                    return new List<Podcast>();

                }

                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Podcast>));
                    using (FileStream streamIn = new FileStream("podcasts.xml", FileMode.Open, FileAccess.Read))
                    {
                        returnLista = (List<Podcast>)serializer.Deserialize(streamIn);
                    }

                    return returnLista;
                }

            }
            catch
            {
                throw;
            }
        }


    }
}
