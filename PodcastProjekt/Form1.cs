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
            validering = new Validering();

            StartInfo();

        }

        private void fyllPodcastGridView()
        {
            dataGridView1.Rows.Clear();

            foreach (var item in from Podcast item in mediaKontroller.GetAllMediaFeed()
                                 where !mediaKontroller.GetAllMediaFeed().Contains(item)
                                 select item)
            {
                int rowIndex = dataGridView1.Rows.Add();

                // Null-kontroll f�r item.Url
                dataGridView1.Rows[rowIndex].Cells["Column2"].Value = item.Url ?? "Ingen URL";

                // Null-kontroll f�r item.Namn
                dataGridView1.Rows[rowIndex].Cells["Column1"].Value = item.Namn ?? "Ingen namn";

                // Null-kontroll f�r item.Kategori och item.Kategori.Namn
                if (item.Kategori != null)
                {
                    dataGridView1.Rows[rowIndex].Cells["Column3"].Value = item.Kategori.Namn ?? "Ingen kategori";
                }
                else
                {
                    dataGridView1.Rows[rowIndex].Cells["Column3"].Value = "Ingen kategori";
                }

                // Null-kontroll f�r item.AntalAvsnitt
                dataGridView1.Rows[rowIndex].Cells["Column4"].Value = item.AntalAvsnitt.ToString();
            }
        }


        private void StartInfo()
        {
            foreach (var item in kategoriKontroller.GetAll())
            {
                // Null-kontroll f�r item.Namn
                var kategoriNamn = item.Namn ?? "Ok�nd kategori";

                listBox3.Items.Add(kategoriNamn);
                comboBox1.Items.Add(kategoriNamn);
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
                                     where !kategoriKontroller.GetAll().Contains(item)
                                     select item)
                {
                    // Null-kontroll f�r item.Namn
                    var kategoriNamn = item.Namn ?? "Ok�nd kategori";

                    listBox3.Items.Add(kategoriNamn);
                    comboBox1.Items.Add(kategoriNamn);
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
                // Null-kontroll f�r senasteKategori.Namn
                if (senasteKategori?.Namn == null)
                {
                    MessageBox.Show("Ingen kategori vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                validering.CheckIfSelected(senasteKategori.Namn);

                DialogResult result = MessageBox.Show("�r du s�ker?", "Bekr�fta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    kategoriKontroller.Delete(senasteKategori.Namn);

                    List<Podcast> podcastsToRemove = mediaKontroller.GetAllMediaFeed()
                        .Where(p => p.Kategori?.Namn == senasteKategori.Namn)
                        .ToList();

                    // Ta bort de relevanta Podcasts
                    foreach (Podcast podcast in podcastsToRemove)
                    {
                        // Null-kontroll f�r podcast.Namn
                        if (string.IsNullOrEmpty(podcast.Namn))
                        {
                            MessageBox.Show("Podcast utan namn hittad. Hoppar �ver borttagning.", "Varning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        mediaKontroller.DeleteMediaFeed(podcast.Namn);
                    }

                    listBox3.Items.Clear();
                    dataGridView1.Rows.Clear();

                    foreach (var item in from Kategori item in kategoriKontroller.GetAll()
                                         where !kategoriKontroller.GetAll().Contains(item)
                                         select item)
                    {
                        // Null-kontroll f�r item.Namn
                        var kategoriNamn = item.Namn ?? "Ok�nd kategori";

                        listBox3.Items.Add(kategoriNamn);
                        comboBox1.Items.Remove(kategoriNamn);
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
                // Null-kontroll f�r comboBox1.SelectedItem
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Ingen kategori vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validera anv�ndarens inmatning
                validering.ComboBoxValidering((string)comboBox1.SelectedItem);
                validering.TextBoxValidering(textBox2.Text);
                validering.ValideraURL(textBox1.Text);
                validering.CheckIfExistsPod(textBox1.Text);

                // H�mta data fr�n input
                string url = textBox1.Text;
                string namn = textBox2.Text;

                // Null-kontroll f�r kategoriTextObj innan konvertering
                Object kategoriTextObj = comboBox1.SelectedItem;
                string kategoriText = kategoriTextObj?.ToString() ?? "Ok�nd kategori";

                Kategori kategori = new Kategori(kategoriText);

                // Skapa podcast och h�mta avsnitten fr�n RSS
                LokalLagringService lagringService = new LokalLagringService();
                Podcast podcast = lagringService.H�mtaPodcastFr�nRSS(url, namn, kategori);

                // Kontrollera att podcast inte �r null och att dess egenskaper inte �r null
                if (podcast != null && !string.IsNullOrEmpty(podcast.Url) && !string.IsNullOrEmpty(podcast.Namn) && podcast.Kategori != null)
                {
                    // Skicka podcasten till mediaKontroller f�r att hantera skapande
                    mediaKontroller.CreateMediaFeed(podcast.Url, podcast.Namn, podcast.Kategori);
                }
                else
                {
                    MessageBox.Show("Podcast-data �r inte fullst�ndig.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Fyll grid view med uppdaterad information
                fyllPodcastGridView();

                // V�nta p� uppdatering av UI
                await Task.Delay(1000);

                // Rensa textrutorna
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel uppstod: {ex.Message}", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // H�mtar den valda podcasten fr�n gridPodcasts, men var medveten om att den kan vara null.
                    var dataBoundItem = dataGridView1.SelectedRows[0].DataBoundItem;
                    if (dataBoundItem is Podcast selectedPodcast) // s�kerst�ller att objektet �r en Podcast
                    {
                        // Nu �r vi s�kra p� att selectedPodcast inte �r null och kan s�kert komma �t dess ID.
                        mediaKontroller?.DeleteMediaFeed(selectedPodcast.Namn);

                        // Uppdatera gridPodcasts efter radering
                        dataGridView1.DataSource = null; // Ta bort datak�llan
                        dataGridView1.DataSource = mediaKontroller?.GetAllMediaFeed(); // Fyll p� igen med uppdaterad data

                        MessageBox.Show("Den valda podcasten har nu raderats!");

                        // Rensa informationen p� startsidan s� att den borttagna podcastens information inte visas
                        dataGridView1.Rows.Clear();

                        //Uppdatera datagridview i denna form
                        fyllPodcastGridView();
                    }
                    else
                    {
                        MessageBox.Show("Det valda objektet �r inte en giltig podcast.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("V�nligen v�lj en podcast att radera.", "Ingen podcast vald", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) // H�r f�ngas eventuella undantag som uppst�r i try-blocket.
            {
                MessageBox.Show(ex.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void fyllAvsnitt(Podcast pod)
        {
            dataGridView2.Rows.Clear();

            // Null-check for pod.AvsnittLista
            if (pod.AvsnittLista == null || pod.AvsnittLista.Count == 0)
            {
                MessageBox.Show("Inga avsnitt tillg�ngliga f�r denna podcast.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Loop through the episodes in the AvsnittLista and populate dataGridView2
            foreach (var avsnitt in pod.AvsnittLista)
            {
                int rowIndex = dataGridView2.Rows.Add();
                dataGridView2.Rows[rowIndex].Cells["Column5"].Value = avsnitt.Titel; 
            }
        }



        private List<Podcast> getAllPodcast()
        {

            List<Podcast> listaPodcasts = new List<Podcast>();
            listaPodcasts = mediaKontroller.GetAllMediaFeed();
            return listaPodcasts;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                // Null-kontroll f�r senastePodcast.Namn
                if (string.IsNullOrEmpty(senastePodcast?.Namn))
                {
                    MessageBox.Show("Ingen podcast vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                validering.CheckIfSelected(senastePodcast.Namn);

                // Kontrollera att en kategori har valts i comboBox1
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Ingen kategori vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                validering.ComboBoxValidering(comboBox1.Text);

                // Null-kontroll f�r textBox2.Text (namn)
                string namn = string.IsNullOrEmpty(textBox2.Text) ? "Ok�nt namn" : textBox2.Text;

                // H�mta och konvertera SelectedItem fr�n comboBox1
                Object kategoriTextObj = comboBox1.SelectedItem;
                string kategoriText = kategoriTextObj?.ToString() ?? "Ok�nd kategori";

                Kategori kategori = new Kategori(kategoriText);

                // Null-kontroll f�r textBox1.Text (url)
                string url = string.IsNullOrEmpty(textBox1.Text) ? "Ingen URL" : textBox1.Text;

                int index = mediaKontroller.GetIndexMediaFeed(senastePodcast.Namn);
                if (index < 0)
                {
                    MessageBox.Show("Podcasten hittades inte i feeden.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                mediaKontroller.UpdateMediaFeed(index, url, namn, kategori);

                dataGridView1.Rows.Clear();
                fyllPodcastGridView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];

            if (row.Cells[0].Value == null)
            {
                MessageBox.Show("V�lj en podcast");
            }
            else
            {
                string senastValdaPod = row.Cells[0].Value?.ToString() ?? "Ok�nd podcast";

                // H�mta alla podcasts med hj�lp av getAllPodcast
                List<Podcast> podLista = getAllPodcast();


                // Hitta vald podcast och fyll avsnitt
                Podcast? valdPod = podLista.FirstOrDefault(p => p.Url == senastValdaPod);
                if (valdPod != null)
                {
                    fyllAvsnitt(valdPod);
                }
                else
                {
                    MessageBox.Show("Podcast hittades inte.");
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            // Ensure the index is valid
            if (index < 0 || index >= dataGridView2.Rows.Count)
            {
                MessageBox.Show("Ogiltig rad!");
                return;
            }

            DataGridViewRow row = dataGridView2.Rows[index];

            // Check if the cell value is null
            var cellValue = row.Cells[0].Value;

            // Use the null-coalescing operator to safely get the string value
            string avsnittNamn = cellValue?.ToString() ?? string.Empty;

            // Check if avsnittNamn is not null or empty before calling the method
            if (!string.IsNullOrEmpty(avsnittNamn))
            {
                fyllBeskrivningAvsnitt(avsnittNamn); // Now it's guaranteed not to be null or empty
            }
            else
            {
                MessageBox.Show("V�lj ett avsnitt med v�rde!");
            }
        }




        private void fyllBeskrivningAvsnitt(string namn)
        {
            // Retrieve the index of the media feed based on the textBox2 content
            int index = mediaKontroller.GetIndexMediaFeed(textBox2.Text);
            List<Podcast> podcastLista = getAllPodcast(); // Corrected to fetch the correct podcast list

            // Ensure the index is valid within the podcast list
            if (index >= 0 && index < podcastLista.Count)
            {
                Podcast pod = podcastLista[index];

                // Ensure the episode list (AvsnittLista) is not null or empty
                if (pod.AvsnittLista != null && pod.AvsnittLista.Count > 0)
                {
                    // Fetch the first episode matching the provided name or return null if not found
                    Avsnitt? beskrivningAvsnitt = pod.AvsnittLista.FirstOrDefault(avsnitt => avsnitt?.Titel == namn);

                    if (beskrivningAvsnitt != null)
                    {
                        listBox4.Items.Clear();

                        // Ensure description exists before adding to listBox4
                        string beskrivning = beskrivningAvsnitt.Beskrivning ?? "Ingen beskrivning tillg�nglig";
                        listBox4.Items.Add(beskrivning);
                    }
                    else
                    {
                        MessageBox.Show("Inget avsnitt matchar namnet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Inga avsnitt tillg�ngliga f�r denna podcast.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Podcast kunde inte hittas.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void button5_Click(object sender, EventArgs e)
        {
            List<Podcast> podcastLista = mediaKontroller.GetAllMediaFeed();
            dataGridView1.Rows.Clear();

            foreach (Podcast pod in podcastLista)
            {
                // Null-kontroll f�r pod.Namn, pod.Url och pod.Kategori
                if (!string.IsNullOrEmpty(pod.Namn) && !string.IsNullOrEmpty(pod.Url) && pod.Kategori != null)
                {
                    mediaKontroller.DeleteMediaFeed(pod.Namn);
                    mediaKontroller.CreateMediaFeed(pod.Url, pod.Namn, pod.Kategori);
                }
                else
                {
                    MessageBox.Show("En podcast saknar namn, URL eller kategori och kunde inte uppdateras.", "Varning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            fyllPodcastGridView();
        }




        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Null-kontroll f�r senasteKategori.Namn
                if (string.IsNullOrEmpty(senasteKategori?.Namn))
                {
                    MessageBox.Show("Ingen kategori vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                validering.CheckIfSelected(senasteKategori.Namn);

                string nyttKategoriNamn = textBox3.Text;
                kategoriKontroller.Update(nyttKategoriNamn, senasteKategori.Namn);

                listBox3.Items.Clear();

                foreach (var item in from Kategori item in kategoriKontroller.GetAll()
                                     where !kategoriKontroller.GetAll().Contains(item)
                                     select item)
                {
                    // Null-kontroll f�r item.Namn
                    if (!string.IsNullOrEmpty(item?.Namn))
                    {
                        listBox3.Items.Add(item.Namn);
                    }
                    else
                    {
                        listBox3.Items.Add("Ok�nd kategori");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett fel intr�ffade: " + ex.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //ingen  referens!!!!!!
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string valdKategori = "";
                if (listBox3.SelectedItem != null)
                {
                    // Anv�nd null-coalescing operator f�r att s�kerst�lla att valdKategori inte blir null
                    valdKategori = listBox3.SelectedItem.ToString() ?? "Ok�nd kategori";
                }


                // Kontrollera att valdKategori inte �r tomt
                if (string.IsNullOrEmpty(valdKategori))
                {
                    MessageBox.Show("Ingen kategori vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int index = kategoriKontroller.GetIndex(valdKategori);

                // Kontrollera att indexet �r giltigt
                List<Kategori> listaKategori = kategoriKontroller.GetAll();
                if (index < 0 || index >= listaKategori.Count)
                {
                    MessageBox.Show("Kategorin hittades inte.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                senasteKategori = listaKategori[index];

                // Kontrollera att senasteKategori.Namn inte �r null innan validering
                if (!string.IsNullOrEmpty(senasteKategori?.Namn))
                {
                    validering.CheckIfSelected(senasteKategori.Namn);
                    textBox3.Text = senasteKategori.Namn;
                }
                else
                {
                    MessageBox.Show("Kategorinamnet saknas.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett fel intr�ffade: " + ex.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Null-kontroll f�r senasteKategori.Namn
                if (string.IsNullOrEmpty(senasteKategori?.Namn))
                {
                    MessageBox.Show("Ingen kategori vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                validering.CheckIfSelected(senasteKategori.Namn);

                List<Podcast> sorteradPodcast = mediaKontroller.GetAllMediaFeed()
                    .Where(p => p.Kategori?.Namn == senasteKategori.Namn)
                    .ToList();

                dataGridView1.Rows.Clear();

                foreach (Podcast podcast in sorteradPodcast)
                {
                    int rowIndex = dataGridView1.Rows.Add();

                    dataGridView1.Rows[rowIndex].Cells["Column1"].Value = podcast.Namn;
                    dataGridView1.Rows[rowIndex].Cells["Column4"].Value = podcast.AntalAvsnitt;

                    // Null-kontroll f�r podcast.Kategori och podcast.Kategori.Namn
                    dataGridView1.Rows[rowIndex].Cells["Column3"].Value = podcast.Kategori?.Namn ?? "Ok�nd kategori";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett fel intr�ffade: " + ex.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
