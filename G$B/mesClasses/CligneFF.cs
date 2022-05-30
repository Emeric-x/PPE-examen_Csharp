using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace G_B.mesClasses
{
    public class CligneFF
    {
        public CligneFF(string idVisiteur, string mois, string idFraisForfait, int quantite)
        {
            IdVisiteur = idVisiteur;
            Mois = mois;
            IdFraisForfait = idFraisForfait;
            Quantite = quantite;
        }

        public string IdVisiteur { get; set; }
        public string Mois { get; set; }
        public string IdFraisForfait { get; set; }
        public int Quantite { get; set; }
    }

    public class CligneFFs
    {
        public List<CligneFF> oListLigneFFs { get; set; } = new List<CligneFF>();
        private CligneFFs()
        {
            string AllLigneFF = GetAllLigneFF();
            oListLigneFFs = JsonSerializer.Deserialize<List<CligneFF>>(AllLigneFF);
        }

        public string GetAllLigneFF()
        {
            string LigneFF = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/fichefrais/getAllLigneFF";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    LigneFF = message.Result;
                }
            }

            return LigneFF;
        }

        private static CligneFFs Instance = null;
        public static CligneFFs getInstance()
        {
            if (Instance == null)
            {
                Instance = new CligneFFs();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public double GetTotalFFByIdVisitAndMonth(string sIdVisiteur)
        {
            double totalFF = 0;
            string AnneeMois = DateTime.Now.ToString("yyyyM");
            CfraisForfaits ofraisForfait = CfraisForfaits.getInstance();

            foreach (CligneFF uneLigneFF in oListLigneFFs)
            {
                if(uneLigneFF.IdVisiteur == sIdVisiteur && uneLigneFF.Mois == AnneeMois)
                {
                    totalFF += uneLigneFF.Quantite * ofraisForfait.GetMontantByIdFraisForfait(uneLigneFF.IdFraisForfait);
                }
            }

            return totalFF;
        }
    }
}
