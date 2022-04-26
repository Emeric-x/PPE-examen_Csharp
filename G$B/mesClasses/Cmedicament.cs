using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace G_B.mesClasses
{
    public class Cmedicament
    {
        public Cmedicament(string id, string nom, string photo, string description, string categorie)
        {
            Id = id;
            Nom = nom;
            Photo = photo;
            Description = description;
            Categorie = categorie;
        }

        public string Id { get; set; }
        public string Nom { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Categorie { get; set; }
    }

    public class Cmedicaments
    {
        public List<Cmedicament> oListMedicaments { get; set; } = new List<Cmedicament>();
        private Cmedicaments()
        {
            string AllMedicaments = GetAllMedicaments();
            oListMedicaments = JsonSerializer.Deserialize<List<Cmedicament>>(AllMedicaments);
        }

        private static Cmedicaments Instance = null;
        public static Cmedicaments getInstance()
        {
            if (Instance == null)
            {
                Instance = new Cmedicaments();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public string GetAllMedicaments()
        {
            string Medicaments = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/medicament/getallmedicaments/";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    Medicaments = message.Result;
                }
            }

            return Medicaments;
        }

        public string GetIdMedByName(string sFullName)
        {
            foreach (Cmedicament oUnMedicament in oListMedicaments)
            {
                if ($"{oUnMedicament.Nom}".ToLower() == sFullName.ToLower()) //Pour gérer les majuscules
                {
                    return oUnMedicament.Id;
                }
            }
            return null;
        }
    }
}
