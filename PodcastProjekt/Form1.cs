using System;
using Modell;
using BusinessLayer;
using DataAccess;
using BusinessLayer.Kontroller;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        // L�nk f�r RSS
        // https://rss.podplaystudio.com/1477.xml
        // https://feed.pod.space/alexosigge
        // https://access.acast.com/rss/62d1f29df280fb0013f8a8a5/

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
                comboBox1.Items.Clear();

                kategoriKontroller.CreateKategori(kategoriText);

                foreach (var item in from Kategori item in kategoriKontroller.GetAll()
                                     where !kategoriKontroller.GetAll().Contains(item)
                                     select item)
                {
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
                        if (string.IsNullOrEmpty(podcast.Namn))
                        {
                            MessageBox.Show("Podcast utan namn hittad. Hoppar �ver borttagning.", "Varning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        mediaKontroller.DeleteMediaFeed(podcast.Namn);
                    }

                    listBox3.Items.Clear();
                    dataGridView1.Rows.Clear();
                    textBox3.Clear();
                    comboBox1.Items.Clear();

                    foreach (var item in from Kategori item in kategoriKontroller.GetAll()
                                         where !kategoriKontroller.GetAll().Contains(item)
                                         select item)
                    {
                        var kategoriNamn = item.Namn ?? "Ok�nd kategori";

                        listBox3.Items.Add(kategoriNamn);
                        comboBox1.Items.Remove(kategoriNamn);
                        comboBox1.Items.Add(kategoriNamn);
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                string podcastName = dataGridView1.Rows[selectedIndex].Cells["Column1"].Value?.ToString() ?? string.Empty;

                if (!string.IsNullOrEmpty(podcastName))
                {
                    mediaKontroller.DeleteMediaFeed(podcastName);

                    fyllPodcastGridView();
                }
                else
                {
                    MessageBox.Show("The selected podcast has no valid name.");
                }
            }
            else
            {
                MessageBox.Show("Please select a podcast to remove.");
            }
        }

        private List<Podcast> getAllPodcast()
        {

            List<Podcast> listaPodcasts = new List<Podcast>();
            listaPodcasts = mediaKontroller.GetAllMediaFeed();
            return listaPodcasts;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;

                if (index >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[index];

                    if (row.Cells["Column1"].Value == null)
                    {
                        MessageBox.Show("V�lj en podcast");
                        return;
                    }

                    string senastValdaNamn = row.Cells["Column1"].Value?.ToString() ?? "Ok�nt namn";

                    string senastValdaPod = row.Cells["Column2"].Value?.ToString() ?? "Ok�nd podcast";

                    List<Podcast> podLista = getAllPodcast();

                    Podcast? valdPod = podLista.FirstOrDefault(p => p.Url == senastValdaPod);
                    Podcast? valdPodnamn = podLista.FirstOrDefault(p => p.Namn == senastValdaNamn);

                    if (valdPod != null || valdPodnamn != null)
                    {
                        Podcast? selectedPodcast = valdPod ?? valdPodnamn;

                        if (selectedPodcast != null)
                        {
                            fyllAvsnitt(selectedPodcast);

                            textBox2.Clear();
                            textBox2.Text = senastValdaNamn;
                        }
                        else
                        {
                            MessageBox.Show("Podcast hittades inte.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Podcast hittades inte.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel uppstod: {ex.Message}", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void fyllAvsnitt(Podcast pod)
        {
            listBox1.Items.Clear();
            senastePodcast = pod;

            // Null-check f�r pod.AvsnittLista
            if (pod.AvsnittLista == null || pod.AvsnittLista.Count == 0)
            {
                MessageBox.Show("Inga avsnitt tillg�ngliga f�r denna podcast.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var avsnitt in pod.AvsnittLista)
            {
                if (!string.IsNullOrEmpty(avsnitt?.Titel)) 
                {
                    listBox1.Items.Add(avsnitt.Titel);
                }
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kontrollera att n�got �r valt och att senastePodcast inte �r null
            if (listBox1.SelectedIndex == -1 || senastePodcast == null || senastePodcast.AvsnittLista == null)
                return;

            if (listBox1.SelectedIndex < senastePodcast.AvsnittLista.Count)
            {
                // H�mta det valda avsnittet baserat p� indexet fr�n listBox1
                var valtAvsnitt = senastePodcast.AvsnittLista[listBox1.SelectedIndex];

                // Kontrollera att valtAvsnitt inte �r null
                if (valtAvsnitt != null)
                {
                    // Anv�nd fyllBeskrivningAvsnitt-metoden f�r att fylla beskrivningen i textBox4
                    fyllBeskrivningAvsnitt(valtAvsnitt);
                }
            }
        }



        private void fyllBeskrivningAvsnitt(Avsnitt avsnitt)
        {
            textBox4.Text = avsnitt.Beskrivning;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                List<Podcast> podcastLista = mediaKontroller.GetAllMediaFeed();
                dataGridView1.Rows.Clear();

                foreach (Podcast pod in podcastLista)
                {
                    if (!string.IsNullOrEmpty(pod.Namn) && !string.IsNullOrEmpty(pod.Url) && pod.Kategori != null)
                    {
                        mediaKontroller.DeleteMediaFeed(pod.Namn);
                        mediaKontroller.CreateMediaFeed(pod.Url, pod.Namn, pod.Kategori);
                    }
                    else
                    {
                        MessageBox.Show("En podcast saknar namn, URL eller kategori och kunde inte uppdateras.", "Varning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    comboBox1.Items.Clear();

                    foreach (var item in kategoriKontroller.GetAll())
                    {
                        // Null-kontroll f�r item.Namn
                        var kategoriNamn = item.Namn ?? "Ok�nd kategori";

                        comboBox1.Items.Add(kategoriNamn);
                    }

                }

                fyllPodcastGridView();
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
                    // Anv�nd null operator f�r att s�kerst�lla att valdKategori inte blir null
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


        // boolean till sorteringen i button10_click nedan
        private bool isSortedAscending = true;

        private void button10_Click(object sender, EventArgs e)
        {
            // H�mta alla objekt fr�n listBox1
            var items = listBox1.Items.Cast<string>().ToList();

            if (isSortedAscending)
            {
                // Sortera i stigande ordning: numeriska f�rst, sedan alfanumeriska
                items = items
                    .OrderBy(item => ExtractLeadingNumber(item) ?? int.MaxValue)
                    .ThenBy(item => ExtractLeadingNumber(item) != null ? "" : item, StringComparer.Ordinal)
                    .ToList();
            }
            else
            {
                // Sortera i fallande ordning: alfanumeriska f�rst, sedan numeriska i omv�nd ordning
                items = items
                    .OrderByDescending(item => ExtractLeadingNumber(item) ?? int.MinValue) 
                    .ThenByDescending(item => ExtractLeadingNumber(item) != null ? "" : item, StringComparer.Ordinal) 
                    .ToList();
            }

            listBox1.Items.Clear();
            listBox1.Items.AddRange(items.ToArray());

            // V�xla sorteringsordningen f�r n�sta klick
            isSortedAscending = !isSortedAscending;
        }

        // Extrahera ledande nummer fr�n en str�ng, returnerar null om inget nummer hittas
        private int? ExtractLeadingNumber(string input)
        {
            var match = System.Text.RegularExpressions.Regex.Match(input, @"^\d+");
            return match.Success ? int.Parse(match.Value) : (int?)null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
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

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(senastePodcast?.Namn))
                {
                    MessageBox.Show("Ingen podcast vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Skriv in ett nytt namn f�r podcasten.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nyttPodcastNamn = textBox2.Text;

                string podcastUrl = senastePodcast.Url ?? "Ingen URL"; 

                Kategori podcastKategori = senastePodcast.Kategori ?? new Kategori { Namn = "Ingen kategori" }; 

                int podcastIndex = mediaKontroller.GetPodcastIndex(senastePodcast.Namn);

                mediaKontroller.UpdateMediaFeed(podcastIndex, podcastUrl, nyttPodcastNamn, podcastKategori);

                fyllPodcastGridView();

                MessageBox.Show("Podcastens namn har uppdaterats.", "Framg�ng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett fel uppstod: " + ex.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
