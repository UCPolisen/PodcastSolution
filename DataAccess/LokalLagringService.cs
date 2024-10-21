using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using Modell;

namespace DataAccess
{
    public class LokalLagringService
    {
        private readonly string saveFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "podcasts.xml");

        public async Task SparaAvsnittAsync(List<Avsnitt> avsnitt)
        {
            string json = JsonSerializer.Serialize(avsnitt);
            await File.WriteAllTextAsync(saveFilePath, json);
        }

        public async Task<List<Avsnitt>> LaddaAvsnittAsync()
        {
            if (!File.Exists(saveFilePath))
                return new List<Avsnitt>();

            string json = await File.ReadAllTextAsync(saveFilePath);
            return JsonSerializer.Deserialize<List<Avsnitt>>(json);
        }
    }
}
