using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modell
{
    public abstract class Feed 
    {
        public string Url { get; set; }

        public string Namn { get; set; }
        
        public Kategori Kategori { get; set; }

        public int AntalAvsnitt { get; set; }

        public List<Avsnitt> listaAvsnitt = new List<Avsnitt>();

        public Feed(string url, string namn, Kategori kategori)
        {
            Url = url;
            Namn = namn;
            Kategori = kategori;
            AntalAvsnitt = 0;
            
        }

        public Feed()
        {

        }
    }
}
