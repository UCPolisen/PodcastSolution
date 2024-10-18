using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Kontroller;
using Modell;

namespace BusinessLayer
{
    public class Validering
    {

        KategoriKontroller kategoriKontroller = new();
        MediaFeedKontroller mediaKontroller = new();

        public void ValideraURL(string input)
        {
            if (Uri.IsWellFormedUriString(input, UriKind.Absolute) == false)
            {
                throw new ValideringEx("Ange giltig RSS-länk");
            }
        }

        public void ComboBoxValidering(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ValideringEx("Välj ett av följande val");
            }
        }

        public void TextBoxValidering(string input)
        {
            if (input.Length <= 0)
            {
                throw new ValideringEx("Fyll i rutan");
            }
        }

        public void CheckIfSelected(string namn)
        {
            if (namn == null)
            {
                throw new ValideringEx("Du måste välja ett värde");
            }
        }

        public void TextBoxKategoriValidering(string input)
        {
            if (input.Length <= 0)
            {
                throw new ValideringEx("Fyll i rutan");
            }
        }
        public void CheckIfExistsKategori(string namn)
        {
            Kategori kategori = kategoriKontroller.GetAll().FirstOrDefault(p => p.Namn == namn)!;
            if (kategori != null)
            {
                throw new ValideringEx("Kategorin finns redan");
            }
        }
        public void CheckIfExistsPod(string url)
        {
            Podcast podcast = mediaKontroller.GetAllMediaFeed().FirstOrDefault(p => p.Url == url)!;
            if (podcast != null)
            {
                throw new ValideringEx("Podcasten finns redan");
            }

        }

    }
}
