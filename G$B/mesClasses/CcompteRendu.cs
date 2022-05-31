using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace G_B.mesClasses
{
    public class CcompteRendu
    {
        public CcompteRendu(int id, string id_visit, string lienFichier, string anneeMois)
        {
            Id = id;
            Id_visit = id_visit;
            LienFichier = lienFichier;
            AnneeMois = anneeMois;
        }

        public int Id { get; set; }
        public string Id_visit { get; set; }
        public string LienFichier { get; set; }
        public string AnneeMois { get; set; }
    }

    public class CcompteRendus
    {
        public List<CcompteRendu> oListCompteRendus { get; set; } = new List<CcompteRendu>();
        private CcompteRendus()
        {
            string AllCompteRendus = GetAllCompteRendus();
            oListCompteRendus = JsonSerializer.Deserialize<List<CcompteRendu>>(AllCompteRendus);
        }

        public string GetAllCompteRendus()
        {
            string CompteRendus = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/compterendu/getallcompterendu";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    CompteRendus = message.Result;
                }
            }

            return CompteRendus;
        }

        private static CcompteRendus Instance = null;
        public static CcompteRendus getInstance()
        {
            if (Instance == null)
            {
                Instance = new CcompteRendus();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public string isCompteRenduDepose(string sIdVisiteur)
        {
            string isDepose = "Non";
            string AnneeMois = Fdashboard.GetCurrentAnneeMois();

            foreach (CcompteRendu oUnCompteRendu in oListCompteRendus)
            {
                if(oUnCompteRendu.Id_visit == sIdVisiteur && oUnCompteRendu.AnneeMois == AnneeMois)
                {
                    isDepose = "Oui";
                }
            }

            return isDepose;
        }
    }
}
