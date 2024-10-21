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

                dataGridView1.Rows[rowIndex].Cells["URL"].Value = item.Url;
                dataGridView1.Rows[rowIndex].Cells["Namn"].Value = item.Namn;
                dataGridView1.Rows[rowIndex].Cells["Kategori"].Value = item.Kategori.Namn;
                dataGridView1.Rows[rowIndex].Cells["Avsnitt"].Value = item.AntalAvsnitt;

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

        private void button1_Click_1(object sender, EventArgs e)
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

            catch (Exception ex)
            {
                ValideringEgenEx.CustomException(ex);
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

        private async void button4_Click(object sender, EventArgs e)
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
                await Task.Delay(1000);


            }

            catch (Exception)
            {
                throw;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                validering.CheckIfSelected(senastePodcast.Namn);
                string podNamn = senastePodcast.Namn;
                mediaKontroller.DeleteMediaFeed(podNamn);

                fyllPodcastGridView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void fyllAvsnitt(Podcast pod)
        {
            dataGridView2.Rows.Clear();

            foreach (Avsnitt avsnitt in pod.AvsnittLista)
            {
                dataGridView2.Rows.Add(avsnitt.Titel);
            }

        }

        private List<Podcast> getAllPodcast()
        {

            List<Podcast> listaPodcasts = new List<Podcast>();
            listaPodcasts = mediaKontroller.GetAllMediaFeed();
            return listaPodcasts;

        }

        private async void button8_Click(object sender, EventArgs e)
        {
            try
            {
                validering.CheckIfSelected(senastePodcast.Namn);
                validering.ComboBoxValidering(comboBox1.Text);
                string namn = textBox2.Text;
                Object kategoriTextObj = comboBox1.SelectedItem;
                string kategoriText = kategoriTextObj.ToString();
                Kategori kategori = new Kategori(kategoriText);
                string url = textBox1.Text;
                int index = mediaKontroller.GetIndexMediaFeed(senastePodcast.Namn);
                mediaKontroller.UpdateMediaFeed(index, url, namn, kategori);

                dataGridView1.Rows.Clear();

                fyllPodcastGridView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];


            if (row.Cells[0].Value == null)
            {
                MessageBox.Show("Välj en podcast");
            }
            else
            {
                string senastValdaPod = row.Cells[0].Value.ToString();

                List<Podcast> podLista = getAllPodcast();
                int indexPod = mediaKontroller.GetIndexMediaFeed(senastValdaPod);
                senastePodcast = podLista[indexPod];
                textBox1.Text = senastePodcast.Url;
                textBox2.Text = senastePodcast.Namn;

                fyllAvsnitt(senastePodcast);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[index];

            if (row.Cells[0].Value == null)
            {
                MessageBox.Show("Välj ett avsnitt");
            }
            else
            {
                string avsnittNamn = row.Cells[0].Value.ToString();
                fyllAvsnitt(avsnittNamn);
            }
        }

        private void fyllAvsnitt(string namn)
        {


            int index = mediaKontroller.GetIndexMediaFeed(textBox2.Text);
            List<Podcast> podcastLista = getAllPodcast();
            Podcast pod = podcastLista[index];


            Avsnitt beskrivningAvsnitt = pod.AvsnittLista.FirstOrDefault(avsnitt => avsnitt.Titel == namn);

            if (beskrivningAvsnitt != null)
            {
                listBox4.Items.Clear();
                listBox4.Items.Add(beskrivningAvsnitt.Beskrivning);
            }
            else
            {
                listBox4.Items.Clear();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Podcast> podcastLista = mediaKontroller.GetAllMediaFeed();
            dataGridView1.Rows.Clear();

            foreach (Podcast pod in podcastLista)
            {
                mediaKontroller.DeleteMediaFeed(pod.Namn);
                mediaKontroller.CreateMediaFeed(pod.Url, pod.Namn, pod.Kategori);
            }
            fyllPodcastGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                validering.CheckIfSelected(senasteKategori.Namn);
                string nyttKategoriNamn = textBox3.Text;
                kategoriKontroller.Update(nyttKategoriNamn, senasteKategori.Namn);

                listBox3.Items.Clear();

                foreach (var item in from Kategori item in kategoriKontroller.GetAll()
                                     where kategoriKontroller.GetAll().Contains(item) == false
                                     select item)
                {

                    listBox3.Items.Add(item.Namn);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string valdKategori = "";
                if (listBox3.SelectedItem != null)
                {
                    valdKategori = listBox3.SelectedItem.ToString();
                }

                int index = kategoriKontroller.GetIndex(valdKategori);
                List<Kategori> listaKategori = kategoriKontroller.GetAll();
                senasteKategori = listaKategori[index];
                validering.CheckIfSelected(senasteKategori.Namn);
                textBox3.Text = senasteKategori.Namn;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            fyllPodcastGridView();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                validering.CheckIfSelected(senasteKategori.Namn);

                List<Podcast> sorteradPodcast = mediaKontroller.GetAllMediaFeed().Where(p => p.Kategori.Namn == senasteKategori.Namn).ToList();

                dataGridView1.Rows.Clear();

                foreach (Podcast podcast in sorteradPodcast)
                {
                    int rowIndex = dataGridView1.Rows.Add();

                    dataGridView1.Rows[rowIndex].Cells["Namn"].Value = podcast.Namn;
                    dataGridView1.Rows[rowIndex].Cells["Avsnitt"].Value = podcast.AntalAvsnitt;
                    dataGridView1.Rows[rowIndex].Cells["Kategori"].Value = podcast.Kategori.Namn;
                }
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
