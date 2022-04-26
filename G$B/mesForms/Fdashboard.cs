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

            Cvisiteurs ovisiteurs = Cvisiteurs.getInstance();
            foreach(Cvisiteur oUnVisiteur in ovisiteurs.oListVisiteurs)
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
            Cmedicaments omedicaments = Cmedicaments.getInstance();
            Cmedecins omedecins = Cmedecins.getInstance();
            this.oListChoosedMedecins.Clear();
            this.oListChoosedMedicaments.Clear();

            //Si le tableau existe deja on le vide, sinon on le créer
            if (this.oTabMed == null)
            {
                this.oTabMed = new TableLayoutPanel();
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
                this.oTabMed.Visible = true;
                oTabMed.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                oTabMed.AutoSize = true;
                oTabMed.Location = new Point(250, 270);
                oTabMed.RowCount = 3;
                oTabMed.ColumnCount = NbMedecins + 1; // +1 pour laisser une première colonne pour les lbl

                // Ajout en-tete gauche tableau
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

                for (int i = 0; i < NbMedecins; i++)
                {
                    // Ajout en-tete haut tableau
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

                    // Ajout liste medecins
                    ComboBox oComboBox = new ComboBox();
                    oComboBox.Name = $"Dashboard_CBmedecin{i}";
                    foreach (Cmedecin oUnMedecin in omedecins.oListMedecins)
                    {
                        oComboBox.Items.Add($"{oUnMedecin.Nom} {oUnMedecin.Prenom}");
                    }
                    oTabMed.Controls.Add(oComboBox, i + 1, 1);
                    this.oListChoosedMedecins.Add(oComboBox);

                    // Ajout liste medicaments
                    CheckedListBox oListMed = new CheckedListBox();
                    oListMed.Name = $"Dashboard_ListMed{i}";
                    foreach (Cmedicament oUnMedicament in omedicaments.oListMedicaments)
                    {
                        oListMed.Items.Add($"{oUnMedicament.Nom}");
                    }
                    oTabMed.Controls.Add(oListMed, i + 1, 2);
                    this.oListChoosedMedicaments.Add(oListMed);
                }
                
                Panel_form.Controls.Add(oTabMed);
            }
            else
            {
                this.oTabMed.Visible = false;
            }
        }

        private void Dashboard_ValideForm_Click(object sender, EventArgs e)
        {
            if (GestionErreur()) // si cette condition est vraie ca veut dire que toutes les info du form ont été vérifiées et validées
            {
                // envoie des données du formulaire
                string idVisiteur = ovisiteurs.GetIdVisitByName(Dashboard_ListVisiteur.Text);
                int i = 0;

                foreach (ComboBox oUnMedecin in this.oListChoosedMedecins)
                {
                    int idMedecin = omedecins.GetIdMedecinByName($"{oUnMedecin.Text}");

                    for (int j = 0; j < this.oListChoosedMedicaments[i].Items.Count; j++)
                    {
                        if (this.oListChoosedMedicaments[i].GetItemChecked(j) == true)
                        {
                            string idMedicament = omedicaments.GetIdMedByName(this.oListChoosedMedicaments[i].Items[j].ToString());
                            AjoutPresenter(idMedicament, idVisiteur, idMedecin);
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

            foreach (ComboBox oUnMedecin in this.oListChoosedMedecins)
            {
                if (!omedecins.isRealMedecin(oUnMedecin.Text))
                {
                    MessageBox.Show("Un médecin choisi n'existe pas.");
                    return false;
                }
            }

            return true;
        }

        public string AjoutPresenter(string sId_med, string sId_visit, int sId_medecin)
        {
            string Result = null;

            HttpClient client = new HttpClient();
            string url = $"http://localhost:59906/api/medicament/CreatePresenter/{sId_med}/{sId_visit}/{sId_medecin}";
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
