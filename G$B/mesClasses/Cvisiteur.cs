using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace G_B.mesClasses
{
    public class Cvisiteur : Cemploye
    {
        public Cvisiteur(string id, string nom, string prenom, string login, string mdp, string adresse, int cp, string ville, DateTime dateEmbauche, string mdp_hash, char region)
            : base(id, nom, prenom, login, mdp, adresse, cp, ville, dateEmbauche, mdp_hash, region)
        {

        }
    }

    public class Cvisiteurs
    {
        public List<Cvisiteur> oListVisiteurs { get; set; } = new List<Cvisiteur>();
        private Cvisiteurs()
        {
            string AllVisiteurs = GetAllVisiteurs();
            oListVisiteurs = JsonSerializer.Deserialize<List<Cvisiteur>>(AllVisiteurs);
        }

        private static Cvisiteurs Instance = null;
        public static Cvisiteurs getInstance()
        {
            if (Instance == null)
            {
                Instance = new Cvisiteurs();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public string GetAllVisiteurs()
        {
            string Visiteurs = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/employe/getallvisiteurs/";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    Visiteurs = message.Result;
                }
            }

            return Visiteurs;
        }

        public bool isRealVisiteur(string sFullName)
        {
            foreach(Cvisiteur oUnVisiteur in oListVisiteurs)
            {
                if($"{oUnVisiteur.Nom} {oUnVisiteur.Prenom}".ToLower() == sFullName.ToLower()) //Pour gérer les majuscules
                {
                    return true;
                }
            }
            return false;
        }

        public string GetIdVisitByName(string sFullName)
        {
            foreach (Cvisiteur oUnVisiteur in oListVisiteurs)
            {
                if ($"{oUnVisiteur.Nom} {oUnVisiteur.Prenom}".ToLower() == sFullName.ToLower()) //Pour gérer les majuscules
                {
                    return oUnVisiteur.Id;
                }
            }
            return null;
        }

        public char GetRegionVisitByName(string sFullName)
        {
            foreach (Cvisiteur oUnVisiteur in oListVisiteurs)
            {
                if ($"{oUnVisiteur.Nom} {oUnVisiteur.Prenom}".ToLower() == sFullName.ToLower())
                {
                    return oUnVisiteur.Region;
                }
            }
            return Convert.ToChar("n"); //obligé de mettre un return ici + impo de return null donc j'ai mis ca meme si c inutile
        }

        public List<Cvisiteur> GetVisiteursByRegion(char sRegion)
        {
            List<Cvisiteur> oListRetour = new List<Cvisiteur>();

            foreach(Cvisiteur oUnVisiteur in oListVisiteurs)
            {
                if(oUnVisiteur.Region == sRegion)
                {
                    oListRetour.Add(oUnVisiteur);
                }
            }

            return oListRetour;
        }
    }
}
