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
using System.Globalization;

namespace G_B
{
    public partial class Fdashboard : Form
    {
        private TableLayoutPanel oTabMed = null;
        private TableLayoutPanel TabVisiteurs = null;
        private List<ComboBox> oListChoosedMedecins = new List<ComboBox>();
        private List<CheckedListBox> oListChoosedMedicaments = new List<CheckedListBox>();
        private Cvisiteurs ovisiteurs = Cvisiteurs.getInstance();
        private Cmedecins omedecins = Cmedecins.getInstance();
        private Cmedicaments omedicaments = Cmedicaments.getInstance();
        private Cpresenters opresenters = Cpresenters.getInstance();
        private CligneFFs oligneFFs = CligneFFs.getInstance();
        private CligneFHFs oligneFHFs = CligneFHFs.getInstance();
        private List<Cvisiteur> oListVisiteurs = null;
        private CcompteRendus ocompterendus = CcompteRendus.getInstance();

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
            oListVisiteurs = ovisiteurs.GetVisiteursByRegion(Fconnexion.oCurrentChefRegion.Region);
            foreach (Cvisiteur oUnVisiteur in oListVisiteurs)
            {
                Dashboard_ListVisiteur.Items.Add($"{oUnVisiteur.Nom} {oUnVisiteur.Prenom}");
            }

            InitTabVisiteursAccueil();

            FillTabVisiteurs();
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
            Dashboard_lblChooseMonth.Text = "Choisir un mois";
            Dashboard_lblTitreTabVisiteurs.Text = $"Données concernant les visiteurs de votre région, pour le mois de {DateTime.Now.ToString("MMMM", new CultureInfo("fr-FR"))}";
        }

        private void InitTabVisiteursAccueil()
        {
            TabVisiteurs = new TableLayoutPanel();
            TabVisiteurs.Visible = true;
            TabVisiteurs.Parent = Panel_Accueil;
            TabVisiteurs.BackColor = Color.White;
            TabVisiteurs.Name = "Dahsboard_TabVisiteurs";
            TabVisiteurs.AutoSize = true;
            TabVisiteurs.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            TabVisiteurs.ColumnCount = 6;
            TabVisiteurs.RowCount = oListVisiteurs.Count() + 1;

            SetHeaderTabVisiteurs();

            TabVisiteurs.Location = new Point((Panel_Accueil.Width - TabVisiteurs.Width) / 2, (Panel_Accueil.Height - TabVisiteurs.Height) / 2);
        }

        private void SetHeaderTabVisiteurs()
        {
            Label oLabelNom = new Label();
            oLabelNom.Text = "Nom";
            oLabelNom.Name = "Dashboard_lblTabVisNom";
            oLabelNom.AutoSize = true;
            oLabelNom.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            Label oLabelPrenom = new Label();
            oLabelPrenom.Text = "Prénom";
            oLabelPrenom.Name = "Dashboard_lblTabVisPrenom";
            oLabelPrenom.AutoSize = true;
            oLabelPrenom.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            Label oLabelMed = new Label();
            oLabelMed.Text = "Médecins visités";
            oLabelMed.Name = "Dashboard_lblTabVisMed";
            oLabelMed.AutoSize = true;
            oLabelMed.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            Label oLabelFF = new Label();
            oLabelFF.Text = "Frais Forfaits";
            oLabelFF.Name = "Dashboard_lblTabVisFF";
            oLabelFF.AutoSize = true;
            oLabelFF.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            Label oLabelFHF = new Label();
            oLabelFHF.Text = "Frais Hors Forfaits";
            oLabelFHF.Name = "Dashboard_lblTabVisFHF";
            oLabelFHF.AutoSize = true;
            oLabelFHF.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            Label oLabelCompteRendu = new Label();
            oLabelCompteRendu.Text = "Compte Rendu Déposé";
            oLabelCompteRendu.Name = "Dashboard_lblTabVisCompteRendu";
            oLabelCompteRendu.AutoSize = true;
            oLabelCompteRendu.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            TabVisiteurs.Controls.Add(oLabelNom, 0, 0);
            TabVisiteurs.Controls.Add(oLabelPrenom, 1, 0);
            TabVisiteurs.Controls.Add(oLabelMed, 2, 0);
            TabVisiteurs.Controls.Add(oLabelFF, 3, 0);
            TabVisiteurs.Controls.Add(oLabelFHF, 4, 0);
            TabVisiteurs.Controls.Add(oLabelCompteRendu, 5, 0);
        }

        private void FillTabVisiteurs()
        {
            int i = 1;

            foreach(Cvisiteur unVisiteur in oListVisiteurs)
            {
                // obligé de créer un label car Add prend uniquement un Control en paramètre
                Label oLabelNom = new Label();
                oLabelNom.Text = $"{unVisiteur.Nom}";
                oLabelNom.Name = $"{unVisiteur.Nom}";
                oLabelNom.AutoSize = true;
                oLabelNom.Font = new Font("Microsoft Sans Serif", 10);

                Label oLabelPrenom = new Label();
                oLabelPrenom.Text = $"{unVisiteur.Prenom}";
                oLabelPrenom.Name = $"{unVisiteur.Prenom}";
                oLabelPrenom.AutoSize = true;
                oLabelPrenom.Font = new Font("Microsoft Sans Serif", 10);

                Label oLabelMed = new Label();
                oLabelMed.Text = $"{opresenters.GetNbMedecinVisiteByIdVisitAndMonth(unVisiteur.Id)}";
                oLabelMed.Name = $"lblMedVisit_{unVisiteur.Nom}";
                oLabelMed.AutoSize = true;
                oLabelMed.Font = new Font("Microsoft Sans Serif", 10);

                Label oLabelFF = new Label();
                oLabelFF.Text = $"{oligneFFs.GetTotalFFByIdVisitAndMonth(unVisiteur.Id)}";
                oLabelFF.Name = $"lblFF_{unVisiteur.Nom}";
                oLabelFF.AutoSize = true;
                oLabelFF.Font = new Font("Microsoft Sans Serif", 10);

                Label oLabelFHF = new Label();
                oLabelFHF.Text = $"{oligneFHFs.GetTotalFHFByIdVisitAndMonth(unVisiteur.Id)}";
                oLabelFHF.Name = $"lblFHF_{unVisiteur.Nom}";
                oLabelFHF.AutoSize = true;
                oLabelFHF.Font = new Font("Microsoft Sans Serif", 10);

                Label oLabelCompteRendu = new Label();
                oLabelCompteRendu.Text = $"{ocompterendus.isCompteRenduDepose(unVisiteur.Id)}";
                oLabelCompteRendu.Name = $"lblCompteRendu_{unVisiteur.Nom}";
                oLabelCompteRendu.AutoSize = true;
                oLabelCompteRendu.Font = new Font("Microsoft Sans Serif", 10);

                TabVisiteurs.Controls.Add(oLabelNom, 0, i);
                TabVisiteurs.Controls.Add(oLabelPrenom, 1, i);
                TabVisiteurs.Controls.Add(oLabelMed, 2, i);
                TabVisiteurs.Controls.Add(oLabelFF, 3, i);
                TabVisiteurs.Controls.Add(oLabelFHF, 4, i);
                TabVisiteurs.Controls.Add(oLabelCompteRendu, 5, i);

                i++;
            }
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
            Panel_Accueil.Visible = false;
        }

        private void Dashboard_btnAcceuil_Click(object sender, EventArgs e)
        {
            Dashboard_lblTitle.Text = "Accueil";
            Panel_form.Visible = false;
            Panel_Accueil.Visible = true;
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
                oTabMed.Width = 0; // pour éviter les problèmes de taille lorsqu'on passe d'un grand à un plus petit tableau
            }

            if (NbMedecins > 0)
            {
                // Configuration tableau
                oTabMed.Visible = true;
                oTabMed.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                oTabMed.AutoSize = true;
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
                oTabMed.Location = new Point((Panel_form.Width - oTabMed.Width) / 2, 270); //après avoir ajouté le tableau au panel, pour que width soit bien pris en compte
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
                            opresenters.AjoutPresenter(idMedicament, idVisiteur, idMedecin, ChoosedMonth);
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



        #region Fonctions utiles

        public static string GetCurrentAnneeMois()
        {
            string Annee = DateTime.Now.ToString("yyyy");
            string mois = DateTime.Now.ToString("MM");
            string AnneeMois = "";

            AnneeMois = $"{Annee}{mois}";

            return AnneeMois;
        }

        #endregion
    }
}
