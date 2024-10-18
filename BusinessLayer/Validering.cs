using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modell;

namespace BusinessLayer
{
    public static class Validering
    {
        public static Boolean vardeFinns(TextBox textboxAttValidera)
        {
            Boolean resultat = true;
            if (textboxAttValidera.Text == "")
            {
                MessageBox.Show("Textrutan saknar värde!");
                resultat = false;
                textboxAttValidera.Focus();
            }
            return resultat;
        }

        public static Boolean vardeFinns(ComboBox comboBoxAttValidera, string typ)
        {
            Boolean resultat = true;
            if (comboBoxAttValidera.SelectedIndex < 0)
            {
                MessageBox.Show($"Vänligen välj {typ}!");
                resultat = false;
            }
            return resultat;
        }

        public static Boolean vardeFinns(ListBox listBoxAttValidera, string typ)
        {
            Boolean resultat = true;
            if (listBoxAttValidera.SelectedIndex < 0)
            {
                MessageBox.Show($"Vänligen välj {typ}!");
                resultat = false;
            }
            return resultat;
        }


        public static Boolean KontrolleraTeckenAntal(int antalTeckenInskrivna, int minTecken, int maxTecken, string typ)
        {
            Boolean resultat = true;
            if (antalTeckenInskrivna < minTecken || antalTeckenInskrivna > maxTecken)
            {
                MessageBox.Show($"Vänligen använd mellan {minTecken} och {maxTecken} tecken i rutan för {typ}!");
                resultat = false;
            }

            return resultat;
        }

        //Denna privata metod kommer att användas i metoden precis under
        private static bool PodcastUrlExisterar(string url, List<Podcast> befintligaPodcasts)
        {
            return befintligaPodcasts.Any(podcast => podcast.Url == url);
        }

        public static Boolean ValideraURL(string url, List<Podcast> befintligaPodcasts)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Textrutan saknar värde!");
                return false;
            }

            if (PodcastUrlExisterar(url, befintligaPodcasts))
            {
                MessageBox.Show("Podcasten med denna URL finns redan i listan.");
                return false;
            }

            return true;
        }
    }
}
