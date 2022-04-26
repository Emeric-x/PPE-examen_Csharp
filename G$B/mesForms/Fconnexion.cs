using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G_B.mesClasses;
using System.Text.Json;

namespace G_B
{
    public partial class Fconnexion : Form
    {
        public static CchefRegion oCurrentChefRegion = null;

        public Fconnexion()
        {
            InitializeComponent();

            InitializeText();
        }

        private void InitializeText()
        {
            LoginTitle.Text = "G$B";
            LoginTitle.Left = (FormLogin.Width - LoginTitle.Width) / 2; // pour centrer horizontalement
            Login_lblLogin.Text = "Identifiant : ";
            Login_lblPwd.Text = "Mot de passe : ";
            Login_btnLogin.Text = "Connexion";
            Login_btnLogin.Left = (FormLogin.Width - Login_btnLogin.Width) / 2;
        }

        private void Login_btnLogin_MouseHover(object sender, EventArgs e)
        {
            Login_btnLogin.BackColor = Color.White;
        }

        private void Login_btnLogin_MouseLeave(object sender, EventArgs e)
        {
            Login_btnLogin.BackColor = Color.FromArgb(108, 205, 255);
        }

        private void Login_btnLogin_Click(object sender, EventArgs e)
        {
            string Login;
            string Password;
            string ChefRegionJson;

            Login = Login_tbLogin.Text;
            Password = Login_tbPwd.Text;

            CchefRegions oChefRegion = CchefRegions.getInstance();
            ChefRegionJson = oChefRegion.VerifInfoConnexion(Login, Password); // récupère l'objet en string

            if (ChefRegionJson != null)
            {
                oCurrentChefRegion = JsonSerializer.Deserialize<CchefRegion>(ChefRegionJson); // le transforme en objet

                this.Hide();
                Fdashboard fdashboard = new Fdashboard();
                fdashboard.Show();
            }
            else
            {
                Login_lblLogin.ForeColor = Color.Red;
                Login_lblPwd.ForeColor = Color.Red;
                Login_tbPwd.Text = "";
                Login_tbLogin.Text = "";
                MessageBox.Show("Le login ou le password est incorrecte !");
            }
        }
    }
}
