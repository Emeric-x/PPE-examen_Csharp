using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using G_B.mesClasses;
using System.Net.Http;

namespace G_B
{
    public partial class Fdashboard : Form
    {
        private TableLayoutPanel oTabMed = null;
        private List<ComboBox> oListChoosedMedecins = new List<ComboBox>();
        private List<CheckedListBox> oListChoosedMedicaments = new List<CheckedListBox>();
        private Cvisiteurs ovisiteurs = Cvisiteurs.getInstance();
        private CchefRegions ochefRegions = CchefRegions.getInstance();
        private Cmedecins omedecins = Cmedecins.getInstance();
        private Cmedicaments omedicaments = Cmedicaments.getInstance();

        public Fdashboard()
        {
            InitializeComponent();

            InitializeText();

            //Liste des mois
            Dashboard_ListMonths.Items.Add("Janvier");
            Dashboard_ListMonths.Items.Add("Février");
            Dashboard_ListMonths.Items.Add("Mars");
            Dashboard_ListMonths.Items.Add("Avril");
            Dashboard_ListMonths.Items.Add("Mai");
            Dashboard_ListMonths.Items.Add("Juin");
            Dashboard_ListMonths.Items.Add("Juillet");
            Dashboard_ListMonths.Items.Add("Août");
            Dashboard_ListMonths.Items.Add("Septembre");
            Dashboard_ListMonths.Items.Add("Octobre");
            Dashboard_ListMonths.Items.Add("Novembre");
            Dashboard_ListMonths.Items.Add("Décembre");

            //Liste des visiteurs
            Cvisiteurs ovisiteurs = Cvisiteurs.getInstance();
            List<Cvisiteur> oListVisiteurs = ovisiteurs.GetVisiteursByRegion(Fconnexion.oCurrentChefRegion.Region);
            foreach (Cvisiteur oUnVisiteur in oListVisiteurs)
            {
                Dashboard_ListVisiteur.Items.Add($"{oUnVisiteur.Nom} {oUnVisiteur.Prenom}");
            }
        }

        private void InitializeText()
        {
            Dashboard_lblTitle.Text = "Accueil";
            Dashboard_lblTitle.Left = (Dashboard_enTete.Width - Dashboard_lblTitle.Width) / 2;
            Dashboard_lblTitle.Top = (Dashboard_enTete.Height - Dashboard_lblTitle.Height) / 2;
            Dashboard_lblNom.Text = Fconnexion.oCurrentChefRegion.Nom;
            Dashboard_lblPrenom.Text = Fconnexion.oCurrentChefRegion.Prenom;
            Dashboard_lblProfil.Text = "Profil";
            Dashboard_btnAcceuil.Text = "Accueil";
            Dashboard_btnForm.Text = "Formulaire";
            Dashboard_btnLogout.Text = "↩️ Déconnexion";
            Dashboard_lblChooseVisit.Text = "Choisir un visiteur";
            Dashboard_lblNbMedecin.Text = "Choisir le nombre de médecin à visiter";
            Dashboard_ValideForm.Text = "Valider";
            Dashboard_ValideNb.Text = "Confirmer";
        }

        #region Events Hover Btn

        private void Dashboard_btnAcceuil_MouseHover(object sender, EventArgs e)
        {
            Dashboard_btnAcceuil.ForeColor = Color.Blue;
        }

        private void Dashboard_btnAcceuil_MouseLeave(object sender, EventArgs e)
        {
            Dashboard_btnAcceuil.ForeColor = Color.DeepSkyBlue;
        }

        private void Dashboard_btnComptes_MouseHover(object sender, EventArgs e)
        {
            Dashboard_btnForm.ForeColor = Color.Blue;
        }

        private void Dashboard_btnComptes_MouseLeave(object sender, EventArgs e)
        {
            Dashboard_btnForm.ForeColor = Color.DeepSkyBlue;
        }

        private void Dashboard_btnLogout_MouseHover(object sender, EventArgs e)
        {
            Dashboard_btnLogout.ForeColor = Color.Black;
            Dashboard_btnLogout.BackColor = Color.Blue;
        }

        private void Dashboard_btnLogout_MouseLeave(object sender, EventArgs e)
        {
            Dashboard_btnLogout.ForeColor = Color.White;
            Dashboard_btnLogout.BackColor = Color.DeepSkyBlue;
        }

        #endregion

        private void Dashboard_btnForm_Click(object sender, EventArgs e)
        {
            Dashboard_lblTitle.Text = "Formulaire";
            Panel_form.Visible = true;
        }

        private void Dashboard_btnAcceuil_Click(object sender, EventArgs e)
        {
            Dashboard_lblTitle.Text = "Accueil";
            Panel_form.Visible = false;
        }

        private void Dashboard_btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Fconnexion fconnexion = new Fconnexion();
            fconnexion.Show();
        }

        private void Dashboard_ValideNb_Click(object sender, EventArgs e)
        {
            int NbMedecins = Convert.ToInt16(Math.Round(Convert.ToDouble(Dashboard_NumNbMedecin.Value)));
            oListChoosedMedecins.Clear();
            oListChoosedMedicaments.Clear();

            //Si le tableau existe deja on le vide, sinon on le créer
            if (oTabMed == null)
            {
                oTabMed = new TableLayoutPanel();
                oTabMed.Name = "Dashboard_tabMed";
            }
            else
            {
                oTabMed.Controls.Clear();
                oTabMed.RowStyles.Clear();
            }

            if (NbMedecins > 0)
            {
                // Configuration tableau
                oTabMed.Visible = true;
                oTabMed.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                oTabMed.AutoSize = true;
                oTabMed.Location = new Point(250, 270);
                oTabMed.RowCount = 3;
                oTabMed.ColumnCount = NbMedecins + 1; // +1 pour laisser une première colonne pour les lbl

                // Ajout en-tete gauche tableau
                AddLeftHeaderTab();

                for (int i = 0; i < NbMedecins; i++)
                {
                    // Ajout en-tete haut tableau
                    AddTopHeaderTab(i);

                    // Ajout liste medecins
                    AddListMedecinsTab(i);

                    // Ajout liste medicaments
                    AddListMedicamentsTab(i);
                }
                
                Panel_form.Controls.Add(oTabMed);
            }
            else
            {
                oTabMed.Visible = false;
            }
        }

        private void AddLeftHeaderTab()
        {
            Label oLabelChoixMedecin = new Label();
            oLabelChoixMedecin.Text = "Choix du médecin";
            oLabelChoixMedecin.Name = "Dashboard_lblChoixMedecin";
            oLabelChoixMedecin.AutoSize = true;
            oLabelChoixMedecin.Font = new Font("Microsoft Sans Serif", 10);
            oTabMed.Controls.Add(oLabelChoixMedecin, 0, 1);
            Label oLabelMedicament = new Label();
            oLabelMedicament.Text = "Médicaments à présenter";
            oLabelMedicament.Name = "Dashboard_lblMedicament";
            oLabelMedicament.AutoSize = true;
            oLabelMedicament.Font = new Font("Microsoft Sans Serif", 10);
            oTabMed.Controls.Add(oLabelMedicament, 0, 2);
        }

        private void AddTopHeaderTab(int i)
        {
            Label oLabelNbMedecin = new Label();
            if (i == 0)
            {
                oLabelNbMedecin.Text = $"{i + 1}er médecin";
            }
            else
            {
                oLabelNbMedecin.Text = $"{i + 1}ème médecin";
            }
            oLabelNbMedecin.Name = $"Dashboard_lblNbMedecin{i}";
            oLabelNbMedecin.AutoSize = true;
            oLabelNbMedecin.Font = new Font("Microsoft Sans Serif", 10);
            oTabMed.Controls.Add(oLabelNbMedecin, i + 1, 0);
        }

        private void AddListMedecinsTab(int i)
        {
            ComboBox oComboBox = new ComboBox();
            oComboBox.Name = $"Dashboard_CBmedecin{i}";
            foreach (Cmedecin oUnMedecin in omedecins.oListMedecins)
            {
                oComboBox.Items.Add($"{oUnMedecin.Nom} {oUnMedecin.Prenom}");
            }
            oTabMed.Controls.Add(oComboBox, i + 1, 1);
            oListChoosedMedecins.Add(oComboBox);
        }

        private void AddListMedicamentsTab(int i)
        {
            CheckedListBox oListMed = new CheckedListBox();
            oListMed.Name = $"Dashboard_ListMed{i}";
            foreach (Cmedicament oUnMedicament in omedicaments.oListMedicaments)
            {
                oListMed.Items.Add($"{oUnMedicament.Nom}");
            }
            oTabMed.Controls.Add(oListMed, i + 1, 2);
            oListChoosedMedicaments.Add(oListMed);
        }

        private void Dashboard_ValideForm_Click(object sender, EventArgs e)
        {
            if (GestionErreur()) // si cette condition est vraie ca veut dire que toutes les info du form ont été vérifiées et validées
            {
                // envoie des données du formulaire
                string idVisiteur = ovisiteurs.GetIdVisitByName(Dashboard_ListVisiteur.Text);
                int i = 0;
                string ChoosedMonth = Dashboard_ListMonths.Text;

                foreach (ComboBox oUnMedecin in oListChoosedMedecins)
                {
                    int idMedecin = omedecins.GetIdMedecinByName($"{oUnMedecin.Text}");

                    for (int j = 0; j < oListChoosedMedicaments[i].Items.Count; j++)
                    {
                        if (oListChoosedMedicaments[i].GetItemChecked(j) == true)
                        {
                            string idMedicament = omedicaments.GetIdMedByName(oListChoosedMedicaments[i].Items[j].ToString());
                            AjoutPresenter(idMedicament, idVisiteur, idMedecin, ChoosedMonth);
                        }
                    }
                }

                MessageBox.Show("Validé !");
            }
        }

        private bool GestionErreur()
        {
            if (Convert.ToInt16(Dashboard_NumNbMedecin.Value) == 0)
            {
                MessageBox.Show("Il faut attribuer au moins un médecin à visiter.");
                return false;
            }
            
            if (!ovisiteurs.isRealVisiteur(Dashboard_ListVisiteur.Text))
            {
                MessageBox.Show("Le visiteur choisi n'existe pas.");
                return false;
            }
            else
            {
                char RegionVisit = ovisiteurs.GetRegionVisitByName(Dashboard_ListVisiteur.Text);
                if(RegionVisit != Fconnexion.oCurrentChefRegion.Region)
                {
                    MessageBox.Show("Le visiteur choisi n'appartient pas à votre région.");
                    return false;
                }
            }

            foreach (ComboBox oUnMedecin in oListChoosedMedecins)
            {
                if (!omedecins.isRealMedecin(oUnMedecin.Text))
                {
                    MessageBox.Show("Un médecin choisi n'existe pas.");
                    return false;
                }
            }

            return true;
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
    }
}
