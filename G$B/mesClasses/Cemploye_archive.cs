using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Windows.Forms;

namespace G_B.mesClasses
{
   /* public class Cemploye
    {
        public string Id { get; set; }
        public string nom_employe { get; set; }
        public string prenom_employe { get; set; }
        public string login_employe { get; set; }
        public string mdp_employe { get; set; }
        public string adresse_employe { get; set; }
        public string cp_employe { get; set; }
        public string ville_employe { get; set; }
        public CcarteBancaire Carte { get; set; }
        public List<Ccompte> oListComptes { get; set; } = new List<Ccompte>();
        public Cregion Region { get; set; }

        public Cemploye(string Id, string nom_employe, string prenom_employe, string login_employe, string mdp_employe, string adresse_employe, string cp_employe, string ville_employe)
        {
            this.Id = Id;
            this.nom_employe = nom_employe;
            this.prenom_employe = prenom_employe;
            this.login_employe = login_employe;
            this.mdp_employe = mdp_employe;
            this.adresse_employe = adresse_employe;
            this.cp_employe = cp_employe;
            this.ville_employe = ville_employe;
        }
    }*/

    /*public class Cemployes
    {
        public List<Cemployes> oListEmployes { get; set; } = new List<Cemployes>();

        public Cemployes()
        {
            Cdao oDao = new Cdao();
            oDao.OpenConnexion();
            MySqlDataReader oReader = oDao.ReadData("SELECT * FROM employe");

            while (oReader.Read())
            {
                Cemploye oEmploye = new Cemploye(oReader["id"].ToString(), oReader["nom"].ToString(), oReader["prenom"].ToString(), oReader["login"].ToString(), oReader["mdp"].ToString());
                oListEmployes.Add(oEmploye);
            }

            oDao.CloseConnexion();
        }

        public string VerifInfoConnexion(string sLogin, string sPassword)
        {
            string EmployeInfo = null;

            HttpClient client = new HttpClient();
            string url = "http://172.29.0.118:82/api/authentification/verification_obj/" + sLogin + "/" + sPassword;
            var responsetask = client.GetAsync(url);
            responsetask.Wait();

            if (responsetask.IsCompleted)
            {
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var message = result.Content.ReadAsStringAsync();
                    message.Wait();
                    EmployeInfo = message.Result;
                }
            }

            return EmployeInfo;
        }
    }*/
}
