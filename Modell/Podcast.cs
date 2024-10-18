using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modell
{
    public class Podcast : Feed
    {
        public List<Avsnitt> AvsnittLista { get; set; }


        public Podcast(string url, string namn,Kategori kategori) : base(url, namn, kategori) 
        {
            Url = url;
            Namn = namn;
            Kategori = kategori;
            AvsnittLista = new List<Avsnitt>();
        }

        public Podcast() 
        {

        }
    }
}
