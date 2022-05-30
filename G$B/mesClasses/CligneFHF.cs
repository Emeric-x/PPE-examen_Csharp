using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace G_B.mesClasses
{
    public class CligneFHF
    {
        public CligneFHF(int id, string idVisiteur, string mois, string libelle, double montant)
        {
            Id = id;
            IdVisiteur = idVisiteur;
            Mois = mois;
            Libelle = libelle;
            Montant = montant;
        }

        public int Id { get; set; }
        public string IdVisiteur { get; set; }
        public string Mois { get; set; }
        public string Libelle { get; set; }
        public double Montant { get; set; }
    }

    public class CligneFHFs
    {
        public List<CligneFHF> oListLigneFHFs { get; set; } = new List<CligneFHF>();
        private CligneFHFs()
        {
            string AllLigneFHF = GetAllLigneFHF();
            oListLigneFHFs = JsonSerializer.Deserialize<List<CligneFHF>>(AllLigneFHF);
        }

        public string GetAllLigneFHF()
        {
            string LigneFHF = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/fichefrais/getAllLigneFHF";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    LigneFHF = message.Result;
                }
            }

            return LigneFHF;
        }

        private static CligneFHFs Instance = null;
        public static CligneFHFs getInstance()
        {
            if (Instance == null)
            {
                Instance = new CligneFHFs();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public double GetTotalFHFByIdVisitAndMonth(string sIdVisiteur)
        {
            double totalFHF = 0;
            string AnneeMois = Fdashboard.GetCurrentAnneeMois();

            foreach (CligneFHF uneLigneFHF in oListLigneFHFs)
            {
                if (uneLigneFHF.IdVisiteur == sIdVisiteur && uneLigneFHF.Mois == AnneeMois)
                {
                    totalFHF += uneLigneFHF.Montant;
                }
            }

            return totalFHF;
        }
    }
}
