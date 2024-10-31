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

        // Länk för RSS
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

                // Null-kontroll för item.Url
                dataGridView1.Rows[rowIndex].Cells["Column2"].Value = item.Url ?? "Ingen URL";

                // Null-kontroll för item.Namn
                dataGridView1.Rows[rowIndex].Cells["Column1"].Value = item.Namn ?? "Ingen namn";

                // Null-kontroll för item.Kategori och item.Kategori.Namn
                if (item.Kategori != null)
                {
                    dataGridView1.Rows[rowIndex].Cells["Column3"].Value = item.Kategori.Namn ?? "Ingen kategori";
                }
                else
                {
                    dataGridView1.Rows[rowIndex].Cells["Column3"].Value = "Ingen kategori";
                }

                // Null-kontroll för item.AntalAvsnitt
                dataGridView1.Rows[rowIndex].Cells["Column4"].Value = item.AntalAvsnitt.ToString();
            }
        }


        private void StartInfo()
        {
            foreach (var item in kategoriKontroller.GetAll())
            {
                // Null-kontroll för item.Namn
                var kategoriNamn = item.Namn ?? "Okänd kategori";

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
                    // Null-kontroll för item.Namn
                    var kategoriNamn = item.Namn ?? "Okänd kategori";

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
                // Null-kontroll för senasteKategori.Namn
                if (senasteKategori?.Namn == null)
                {
                    MessageBox.Show("Ingen kategori vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                validering.CheckIfSelected(senasteKategori.Namn);

                DialogResult result = MessageBox.Show("Är du säker?", "Bekräfta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    kategoriKontroller.Delete(senasteKategori.Namn);

                    List<Podcast> podcastsToRemove = mediaKontroller.GetAllMediaFeed()
                        .Where(p => p.Kategori?.Namn == senasteKategori.Namn)
                        .ToList();

                    // Ta bort de relevanta Podcasts
                    foreach (Podcast podcast in podcastsToRemove)
                    {
                        // Null-kontroll för podcast.Namn
                        if (string.IsNullOrEmpty(podcast.Namn))
                        {
                            MessageBox.Show("Podcast utan namn hittad. Hoppar över borttagning.", "Varning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        // Null-kontroll för item.Namn
                        var kategoriNamn = item.Namn ?? "Okänd kategori";

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
                // Null-kontroll för comboBox1.SelectedItem
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Ingen kategori vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validera användarens inmatning
                validering.ComboBoxValidering((string)comboBox1.SelectedItem);
                validering.TextBoxValidering(textBox2.Text);
                validering.ValideraURL(textBox1.Text);
                validering.CheckIfExistsPod(textBox1.Text);

                // Hämta data från input
                string url = textBox1.Text;
                string namn = textBox2.Text;

                // Null-kontroll för kategoriTextObj innan konvertering
                Object kategoriTextObj = comboBox1.SelectedItem;
                string kategoriText = kategoriTextObj?.ToString() ?? "Okänd kategori";

                Kategori kategori = new Kategori(kategoriText);

                // Skapa podcast och hämta avsnitten från RSS
                LokalLagringService lagringService = new LokalLagringService();
                Podcast podcast = lagringService.HämtaPodcastFrånRSS(url, namn, kategori);

                // Kontrollera att podcast inte är null och att dess egenskaper inte är null
                if (podcast != null && !string.IsNullOrEmpty(podcast.Url) && !string.IsNullOrEmpty(podcast.Namn) && podcast.Kategori != null)
                {
                    // Skicka podcasten till mediaKontroller för att hantera skapande
                    mediaKontroller.CreateMediaFeed(podcast.Url, podcast.Namn, podcast.Kategori);
                }
                else
                {
                    MessageBox.Show("Podcast-data är inte fullständig.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Fyll grid view med uppdaterad information
                fyllPodcastGridView();

                // Vänta på uppdatering av UI
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
            // Check if a row is selected in dataGridView1
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row index
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Safely get the podcast name from the selected row (assuming it's in Column1)
                string podcastName = dataGridView1.Rows[selectedIndex].Cells["Column1"].Value?.ToString() ?? string.Empty;

                if (!string.IsNullOrEmpty(podcastName))
                {
                    // Remove the podcast from the media controller using the DeleteMediaFeed method
                    mediaKontroller.DeleteMediaFeed(podcastName);

                    // Update the DataGridView by refilling it
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

                // Make sure the row index is valid
                if (index >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[index];

                    // Check if the name (Column1) or URL (Column0) is empty
                    if (row.Cells["Column1"].Value == null)
                    {
                        MessageBox.Show("Välj en podcast");
                        return;
                    }

                    // Retrieve the podcast name from Column1 (this should be the name, not the URL)
                    string senastValdaNamn = row.Cells["Column1"].Value?.ToString() ?? "Okänt namn";

                    // Retrieve the podcast URL from Column0 (assuming it's the URL column)
                    string senastValdaPod = row.Cells["Column2"].Value?.ToString() ?? "Okänd podcast";

                    // Fetch all podcasts
                    List<Podcast> podLista = getAllPodcast();

                    // Find the selected podcast either by URL or Name
                    Podcast? valdPod = podLista.FirstOrDefault(p => p.Url == senastValdaPod);
                    Podcast? valdPodnamn = podLista.FirstOrDefault(p => p.Namn == senastValdaNamn);

                    // If either match, we proceed with filling episodes
                    if (valdPod != null || valdPodnamn != null)
                    {
                        Podcast? selectedPodcast = valdPod ?? valdPodnamn;

                        // Call the method to fill the episodes for the selected podcast
                        if (selectedPodcast != null)
                        {
                            // Call the method to fill the episodes for the selected podcast
                            fyllAvsnitt(selectedPodcast);

                            // Clear and set the name in textBox2
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

            // Null-check för pod.AvsnittLista
            if (pod.AvsnittLista == null || pod.AvsnittLista.Count == 0)
            {
                MessageBox.Show("Inga avsnitt tillgängliga för denna podcast.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Loop through the episodes in AvsnittLista and populate listBox1
            foreach (var avsnitt in pod.AvsnittLista)
            {
                if (!string.IsNullOrEmpty(avsnitt?.Titel)) // Check if avsnitt and Titel are not null or empty
                {
                    listBox1.Items.Add(avsnitt.Titel);
                }
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kontrollera att något är valt och att senastePodcast inte är null
            if (listBox1.SelectedIndex == -1 || senastePodcast == null || senastePodcast.AvsnittLista == null)
                return;

            // Kontrollera att indexet är giltigt
            if (listBox1.SelectedIndex < senastePodcast.AvsnittLista.Count)
            {
                // Hämta det valda avsnittet baserat på indexet från listBox1
                var valtAvsnitt = senastePodcast.AvsnittLista[listBox1.SelectedIndex];

                // Kontrollera att valtAvsnitt inte är null
                if (valtAvsnitt != null)
                {
                    // Använd fyllBeskrivningAvsnitt-metoden för att fylla beskrivningen i textBox4
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
                    // Null-kontroll för pod.Namn, pod.Url och pod.Kategori
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
                    // Använd null-coalescing operator för att säkerställa att valdKategori inte blir null
                    valdKategori = listBox3.SelectedItem.ToString() ?? "Okänd kategori";
                }


                // Kontrollera att valdKategori inte är tomt
                if (string.IsNullOrEmpty(valdKategori))
                {
                    MessageBox.Show("Ingen kategori vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int index = kategoriKontroller.GetIndex(valdKategori);

                // Kontrollera att indexet är giltigt
                List<Kategori> listaKategori = kategoriKontroller.GetAll();
                if (index < 0 || index >= listaKategori.Count)
                {
                    MessageBox.Show("Kategorin hittades inte.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                senasteKategori = listaKategori[index];

                // Kontrollera att senasteKategori.Namn inte är null innan validering
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
                MessageBox.Show("Ett fel inträffade: " + ex.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // boolean till sorteringen i button10_click nedan
        private bool isSortedAscending = true;

        private void button10_Click(object sender, EventArgs e)
        {
            // Hämta alla objekt från listBox1
            var items = listBox1.Items.Cast<string>().ToList();

            if (isSortedAscending)
            {
                // Sortera i stigande ordning: numeriska först, sedan alfanumeriska
                items = items
                    .OrderBy(item => ExtractLeadingNumber(item) ?? int.MaxValue) // Sorterar numeriskt om möjligt, annars ger högsta värde för att sortera efter
                    .ThenBy(item => ExtractLeadingNumber(item) != null ? "" : item, StringComparer.Ordinal) // Sorterar alfanumeriska efter numeriska
                    .ToList();
            }
            else
            {
                // Sortera i fallande ordning: alfanumeriska först, sedan numeriska i omvänd ordning
                items = items
                    .OrderByDescending(item => ExtractLeadingNumber(item) ?? int.MinValue) // Sorterar numeriskt om möjligt i omvänd ordning, annars ger lägsta värde för att sortera först
                    .ThenByDescending(item => ExtractLeadingNumber(item) != null ? "" : item, StringComparer.Ordinal) // Sorterar alfanumeriska efter numeriska i omvänd ordning
                    .ToList();
            }

            // Töm listan och fyll på med den sorterade listan
            listBox1.Items.Clear();
            listBox1.Items.AddRange(items.ToArray());

            // Växla sorteringsordningen för nästa klick
            isSortedAscending = !isSortedAscending;
        }

        // Extrahera ledande nummer från en sträng, returnerar null om inget nummer hittas
        private int? ExtractLeadingNumber(string input)
        {
            var match = System.Text.RegularExpressions.Regex.Match(input, @"^\d+");
            return match.Success ? int.Parse(match.Value) : (int?)null;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Null-kontroll för senasteKategori.Namn
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
                    // Null-kontroll för item.Namn
                    if (!string.IsNullOrEmpty(item?.Namn))
                    {
                        listBox3.Items.Add(item.Namn);
                    }
                    else
                    {
                        listBox3.Items.Add("Okänd kategori");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett fel inträffade: " + ex.Message, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                // Null check for the latest podcast
                if (string.IsNullOrEmpty(senastePodcast?.Namn))
                {
                    MessageBox.Show("Ingen podcast vald.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate that a new name has been entered
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Skriv in ett nytt namn för podcasten.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the new podcast name from textBox2
                string nyttPodcastNamn = textBox2.Text;

                // Handle possible null reference for URL
                string podcastUrl = senastePodcast.Url ?? "Ingen URL";  // Provide a default URL if it's null

                // Handle possible null reference for Kategori
                Kategori podcastKategori = senastePodcast.Kategori ?? new Kategori { Namn = "Ingen kategori" };  // Provide a default category if it's null

                // Find the index of the selected podcast by its current name
                int podcastIndex = mediaKontroller.GetPodcastIndex(senastePodcast.Namn);

                // Update the podcast with the new name, and handle the URL and category
                mediaKontroller.UpdateMediaFeed(podcastIndex, podcastUrl, nyttPodcastNamn, podcastKategori);

                // Refresh the podcast list in the DataGridView
                fyllPodcastGridView();

                // Notify the user of the success
                MessageBox.Show("Podcastens namn har uppdaterats.", "Framgång", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
