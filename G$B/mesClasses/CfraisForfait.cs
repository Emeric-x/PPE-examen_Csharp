using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace G_B.mesClasses
{
    public class CfraisForfait
    {
        public CfraisForfait(string id, string libelle, double montant)
        {
            Id = id;
            Libelle = libelle;
            Montant = montant;
        }

        public string Id { get; set; }
        public string Libelle { get; set; }
        public double Montant { get; set; }
    }

    public class CfraisForfaits
    {
        public List<CfraisForfait> oListFraisForfaits { get; set; } = new List<CfraisForfait>();
        private CfraisForfaits()
        {
            string AllFraisForfait = GetAllFraisForfait();
            oListFraisForfaits = JsonSerializer.Deserialize<List<CfraisForfait>>(AllFraisForfait);
        }

        public string GetAllFraisForfait()
        {
            string FraisForfait = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/fichefrais/GetAllFraisForfaits";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    FraisForfait = message.Result;
                }
            }

            return FraisForfait;
        }

        private static CfraisForfaits Instance = null;
        public static CfraisForfaits getInstance()
        {
            if (Instance == null)
            {
                Instance = new CfraisForfaits();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public double GetMontantByIdFraisForfait(string sIdFraisForfait)
        {
            double MtRetour = 0;

            foreach(CfraisForfait unFraisForfait in oListFraisForfaits)
            {
                if(unFraisForfait.Id == sIdFraisForfait)
                {
                    MtRetour = unFraisForfait.Montant;
                }
            }

            return MtRetour;
        }
    }
}
