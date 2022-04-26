using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace G_B.mesClasses
{
    public class Cmedecin
    {
        public Cmedecin(int id, string nom, string prenom, string adresse, int cp, string ville)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Cp = cp;
            Ville = ville;
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public int Cp { get; set; }
        public string Ville { get; set; }
    }

    public class Cmedecins
    {
        public List<Cmedecin> oListMedecins { get; set; } = new List<Cmedecin>();
        private Cmedecins()
        {
            string AllMedecins = GetAllMedecins();
            oListMedecins = JsonSerializer.Deserialize<List<Cmedecin>>(AllMedecins);
        }

        private static Cmedecins Instance = null;
        public static Cmedecins getInstance()
        {
            if (Instance == null)
            {
                Instance = new Cmedecins();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public string GetAllMedecins()
        {
            string Medecins = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/medecin/getallmedecins/";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    Medecins = message.Result;
                }
            }

            return Medecins;
        }

        public bool isRealMedecin(string sFullName)
        {
            foreach (Cmedecin oUnMedecin in oListMedecins)
            {
                if ($"{oUnMedecin.Nom} {oUnMedecin.Prenom}".ToLower() == sFullName.ToLower()) //Pour gérer les majuscules
                {
                    return true;
                }
            }
            return false;
        }

        public int GetIdMedecinByName(string sFullName)
        {
            foreach (Cmedecin oUnMedecin in oListMedecins)
            {
                if ($"{oUnMedecin.Nom} {oUnMedecin.Prenom}".ToLower() == sFullName.ToLower()) //Pour gérer les majuscules
                {
                    return oUnMedecin.Id;
                }
            }
            return 0;
        }
    }
}
