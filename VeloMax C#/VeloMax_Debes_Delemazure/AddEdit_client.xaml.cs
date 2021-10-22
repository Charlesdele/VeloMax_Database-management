using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VeloMax_Debes_Delemazure
{
    /// <summary>
    /// Logique d'interaction pour AddEdit_client.xaml
    /// </summary>
    public partial class AddEdit_client : Window
    {
        public AddEdit_client()
        {
            InitializeComponent();
            menu_adresse.ItemsSource = Utilities.Loadadresse();
            menu_numprog.ItemsSource = Utilities.Loadnumprog();
        }

        //=======================================================================================================================
        // BOUTONS
        //=======================================================================================================================

        //---------------------------------------------------------------Création ou modification d'un client particulier

        #region Bouton enregistrer
        public void btnEnregistrer_click(object sender, RoutedEventArgs e)
        {
            //Vérification des textbox
            bool verif1 = Utilities.is_int(get_codepa.Text, 5);
            bool verif3 = Utilities.is_varchar(get_nompa.Text, 20);
            bool verif4 = Utilities.is_varchar(get_prenom.Text, 20);
            bool verif5 = Utilities.is_int(get_telephone.Text, 10);
            bool verif6 = Utilities.is_varchar(get_courriel.Text, 50);
            if (verif1 != true | verif3!=true | verif4!=true | verif5 != true | verif6!= true)
            {
                MessageBox.Show("Paramètres non conformes");
            }
            else
            {
                if (get_codepa.IsReadOnly == false) //Si c'est une creation le code client peut être changé
                {
                    Client newC = new Client();
                    newC.AdresseP = Convert.ToInt32(menu_adresse.SelectedValue);
                    newC.CodePa = Convert.ToInt32(get_codepa.Text);
                    newC.Courriel = get_courriel.Text;
                    newC.DateA = DateTime.Now;
                    newC.NomPa = get_nompa.Text;
                    newC.NumProg = Convert.ToInt32(menu_numprog.SelectedValue);
                    newC.Prenom = get_prenom.Text;
                    newC.Telephone = get_telephone.Text;
                    newC.CreateEditDelete("create");
                    this.Close();
                }
                else //Sinon on prend celui affiché
                {
                    Client newC = new Client();
                    newC.AdresseP = Convert.ToInt32(menu_adresse.SelectedValue);
                    newC.CodePa = Convert.ToInt32(get_codepa.Text);
                    newC.Courriel = get_courriel.Text;
                    newC.DateA = Convert.ToDateTime(get_datea.Text);
                    newC.NomPa = get_nompa.Text;
                    newC.NumProg = Convert.ToInt32(menu_numprog.SelectedValue);
                    newC.Prenom = get_prenom.Text;
                    newC.Telephone = get_telephone.Text;
                    newC.CreateEditDelete("edit");
                    this.Close();
                }
            }
            
        }
        #endregion

        //--------------------------------------------------------Carnet d'adresse
        //(si besoin d'en rajouter une ou d'en vérifier une)

        #region Carnet d'adresses
        private void btn_adresse(object sender, RoutedEventArgs e)
        {
            AddEdit_adresse add = new AddEdit_adresse();
            add.ShowDialog();
            while (add.IsVisible == true) { }
            menu_adresse.ItemsSource = Utilities.Loadadresse();
            add.clear();
        }
        #endregion

        //--------------------------------------------------------------Fermer la fenetre

        #region Bouton annuler
        private void btnAnnuler_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        //-----------------Réinitialiser les textbox

        #region Reinitialiser
        public void clear()
        {
            get_codepa.Text = "";
            get_codepa.IsReadOnly = false;
            get_courriel.Text = "";
            get_courriel.IsReadOnly = false;
            get_datea.Text = "";
            get_datea.IsReadOnly = false;
            get_nompa.Text = "";
            get_nompa.IsReadOnly = false;
            get_prenom.Text = "";
            get_prenom.IsReadOnly = false;
            get_telephone.Text = "";
            get_telephone.IsReadOnly = false;
            menu_adresse.Text = "";
            menu_numprog.Text = "";
        }
        #endregion


    }
}
