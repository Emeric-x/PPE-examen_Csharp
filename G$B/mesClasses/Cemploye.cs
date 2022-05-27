using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_B.mesClasses
{
    public abstract class Cemploye
    {
        protected Cemploye(string id, string nom, string prenom, string login, string mdp, string adresse, int cp, string ville, DateTime dateEmbauche, string mdp_hash, char region)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Login = login;
            Mdp = mdp;
            Adresse = adresse;
            Cp = cp;
            Ville = ville;
            DateEmbauche = dateEmbauche;
            Mdp_hash = mdp_hash;
            Region = region;
        }

        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Mdp { get; set; }
        public string Adresse { get; set; }
        public int Cp { get; set; }
        public string Ville { get; set; }
        public DateTime DateEmbauche { get; set; }
        public string Mdp_hash { get; set; }
        public char Region { get; set; }
    }
}
