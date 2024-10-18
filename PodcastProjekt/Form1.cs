using System;
using Modell;
using BusinessLayer;
using DataAccess;
using BusinessLayer.Kontroller;
using System.Diagnostics;
using System.Windows.Forms;

namespace PodcastProjekt
{
    public partial class Form1 : Form
    {
        MediaFeedKontroller mediaKontroller;
        KategoriKontroller kategoriKontroller;
        Validering validering;
        Kategori senasteKategori;
        Podcast senastePodcast;

        public Form1()
        {
            InitializeComponent();
            mediaKontroller = new MediaFeedKontroller();
            kategoriKontroller = new KategoriKontroller();
            senasteKategori = new Kategori();
            senastePodcast = new Podcast();
            StartInfo();

        }

        private void fyllPodcastGridView()
        {
            dataGridView1.Rows.Clear();

            foreach (var item in from Podcast item in mediaKontroller.GetAllMediaFeed()
                                 where mediaKontroller.GetAllMediaFeed().Contains(item) == false
                                 select item)
            {

                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells["Namn"].Value = item.Namn;
                dataGridView1.Rows[rowIndex].Cells["Avsnitt"].Value = item.AntalAvsnitt;
                dataGridView1.Rows[rowIndex].Cells["Kategori"].Value = item.Kategori.Namn;

            }
        }

        private void StartInfo()
        {
            foreach (var item in kategoriKontroller.GetAll())
            {
                listBox3.Items.Add(item.Namn);
                comboBox1.Items.Add(item.Namn);
            }

            fyllPodcastGridView();
        }

        private void LaggTillKategori_Click(object sender, EventArgs e)
        {
            try
            {
                validering.CheckIfExistsKategori(textBox3.Text);
                validering.TextBoxValidering(textBox3.Text);
                listBox3.Items.Clear();
                string kategoriText = textBox3.Text;

                // Skapa en ny kategori med texten

                kategoriKontroller.CreateKategori(kategoriText);
                foreach (var item in from Kategori item in kategoriKontroller.GetAll()
                                     where kategoriKontroller.GetAll().Contains(item) == false
                                     select item)
                {

                    listBox3.Items.Add(item.Namn);
                    comboBox1.Items.Add(item.Namn);

                }

                textBox3.Clear();
            }

            catch (Exception)
            {
                throw;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                validering.CheckIfSelected(senasteKategori.Namn);
                DialogResult result = MessageBox.Show("Är du säker?", "Bekräfta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    kategoriKontroller.Delete(senasteKategori.Namn);

                    List<Podcast> podcastsToRemove = mediaKontroller.GetAllMediaFeed().Where(p => p.Kategori.Namn == senasteKategori.Namn).ToList();

                    // Ta bort de relevanta Podcasts
                    foreach (Podcast podcast in podcastsToRemove)
                    {
                        mediaKontroller.DeleteMediaFeed(podcast.Namn);
                    }


                    listBox3.Items.Clear();
                    dataGridView1.Rows.Clear();
                    foreach (var item in from Kategori item in kategoriKontroller.GetAll()
                                         where kategoriKontroller.GetAll().Contains(item) == false
                                         select item)
                    {

                        listBox3.Items.Add(item.Namn);
                        comboBox1.Items.Remove(item.Namn);

                    }
                    fyllPodcastGridView();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                validering.ComboBoxValidering((string)comboBox1.SelectedItem);
                validering.TextBoxValidering(textBox2.Text);
                validering.ValideraURL(textBox1.Text);
                validering.CheckIfExistsPod(textBox1.Text);


                string url = textBox1.Text;
                string namn = textBox2.Text;
                Object kategoriTextObj = comboBox1.SelectedItem;
                string kategoriText = kategoriTextObj.ToString();



                Kategori kategori = new Kategori(kategoriText);

                mediaKontroller.CreateMediaFeed(url, namn, kategori);

                fyllPodcastGridView();

            }

            catch (Exception)
            {
                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
