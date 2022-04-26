using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace G_B.mesClasses
{
    public class CchefRegion : Cemploye
    {
        public CchefRegion(string id, string nom, string prenom, string login, string mdp, string adresse, int cp, string ville, DateTime dateEmbauche, string mdp_hash)
           : base(id, nom, prenom, login, mdp, adresse, cp, ville, dateEmbauche, mdp_hash)
        {

        }
    }

    public class CchefRegions
    {
        public List<CchefRegion> oListChefs { get; set; } = new List<CchefRegion>();
        private CchefRegions()
        {
            string AllChefs = GetAllChefs();
            oListChefs = JsonSerializer.Deserialize<List<CchefRegion>>(AllChefs);
        }

        private static CchefRegions Instance = null;
        public static CchefRegions getInstance()
        {
            if (Instance == null)
            {
                Instance = new CchefRegions();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public string GetAllChefs()
        {
            string Chefs = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/employe/getallchefs/";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    Chefs = message.Result;
                }
            }

            return Chefs;
        }

        public string VerifInfoConnexion(string sLogin, string sPassword)
        {
            string ChefRegionInfo = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/authentification/verification_objChef/" + sLogin + "/" + sPassword;
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    ChefRegionInfo = message.Result;
                }
            }

            return ChefRegionInfo;
        }

        public bool isRealChef(string sFullName)
        {
            foreach (CchefRegion oUnChef in oListChefs)
            {
                if ($"{oUnChef.Nom} {oUnChef.Prenom}".ToLower() == sFullName.ToLower()) //Pour gérer les majuscules
                {
                    return true;
                }
            }
            return false;
        }
    }
}
