using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace G_B.mesClasses
{
    public class Cpresenter
    {
        public Cpresenter(string id_med, string id_visit, int id_medecin, string anneeMois, bool isVisite)
        {
            Id_med = id_med;
            Id_visit = id_visit;
            Id_medecin = id_medecin;
            AnneeMois = anneeMois;
            IsVisite = isVisite;
        }

        public string Id_med { get; set; }
        public string Id_visit { get; set; }
        public int Id_medecin { get; set; }
        public string AnneeMois { get; set; }
        public bool IsVisite { get; set; }
    }

    public class Cpresenters
    {
        public List<Cpresenter> oListPresenters { get; set; } = new List<Cpresenter>();
        public Cpresenters()
        {
            string AllPresenters = GetAllPresenters();
            oListPresenters = JsonSerializer.Deserialize<List<Cpresenter>>(AllPresenters);
        }

        public string GetAllPresenters()
        {
            string Presenters = null;

            HttpClient client = new HttpClient();
            string url = "http://localhost:59906/api/medicament/getallpresenters";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    Presenters = message.Result;
                }
            }

            return Presenters;
        }

        private static Cpresenters Instance = null;
        public static Cpresenters getInstance()
        {
            if (Instance == null)
            {
                Instance = new Cpresenters();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public string AjoutPresenter(string sId_med, string sId_visit, int sId_medecin, string ChoosedMonth)
        {
            string Result = null;

            HttpClient client = new HttpClient();
            string url = $"http://localhost:59906/api/medicament/CreatePresenter/{sId_med}/{sId_visit}/{sId_medecin}/{ChoosedMonth}";
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    Result = message.Result;
                }
            }

            return Result;
        }

        public int GetNbMedecinVisiteByIdVisitAndMonth(string sIdVisiteur)
        {
            int nbMedecinVisite = 0;
            string AnneeMois = DateTime.Now.ToString("yyyyM");

            foreach(Cpresenter unPresenter in oListPresenters)
            {
                if(unPresenter.Id_visit == sIdVisiteur && unPresenter.AnneeMois == AnneeMois && unPresenter.IsVisite)
                {
                    nbMedecinVisite++;
                }
            }

            return nbMedecinVisite;
        }
    }
}
