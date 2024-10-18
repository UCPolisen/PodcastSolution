using System;
using Modell;
using BusinessLayer;
using DataAccess;
using BusinessLayer.Kontroller;

namespace PodcastProjekt
{
    public partial class Form1 : Form
    {
        MediaFeedKontroller mediaKontroller;
        KategoriKontroller kategoriKontroller;
        Validering  Validering;
        Kategori senasteKategori;
        Podcast senastePodcast;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
