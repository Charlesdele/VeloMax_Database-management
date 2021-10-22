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
    public partial class AddEdit_boutique : Window
    {

        public AddEdit_boutique()
        {           
            InitializeComponent();
            menu_adresse.ItemsSource = Utilities.Loadadresse();
        }

        //=======================================================================================================================
        // BOUTONS
        //=======================================================================================================================

        //---------------------------------------------------------------Creation ou modification d'une boutique

        #region Bouton enregistrer
        public void btnEnregistrer_click(object sender, RoutedEventArgs e)
        {
            //Vérification des textbox
            bool verif1 = Utilities.is_varchar(get_nomb.Text, 30);
            bool verif2 = Utilities.is_int(get_remise.Text, 100);
            bool verif3 = Utilities.is_int(get_telephone.Text, 10);
            bool verif4 = Utilities.is_varchar(get_contact.Text,20);
            bool verif5 = Utilities.is_varchar(get_courriel.Text, 50);
            if (verif1 != true | verif2 != true | verif3 != true | verif4 != true | verif5 != true)
            {
                MessageBox.Show("Paramètres non conformes");
            }
            else
            {
                if (get_nomb.IsReadOnly == false) //Si il s'agit d'une creation (Nomb de la boutique peut être modifié dans la textbox)
                {
                    Boutique newB = new Boutique();
                    newB.NomB = get_nomb.Text;
                    newB.Remise = Convert.ToInt32(get_remise.Text);
                    newB.Telephone = Convert.ToString(get_telephone.Text);
                    newB.Contact = get_contact.Text;
                    newB.AdresseB = Convert.ToInt32(menu_adresse.SelectedValue);
                    newB.Courriel = get_courriel.Text;
                    newB.CreateEditDelete("create");
                    this.Close();
                }
                else  //Sinon on prend celui affiché
                {
                    Boutique newB = new Boutique();
                    newB.NomB = get_nomb.Text;
                    newB.Remise = Convert.ToInt32(get_remise.Text);
                    newB.Telephone = get_telephone.Text;
                    newB.Contact = get_contact.Text;
                    newB.AdresseB = Convert.ToInt32(menu_adresse.SelectedValue);
                    newB.Courriel = get_courriel.Text;
                    newB.CreateEditDelete("edit");
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

        //------------------------------------------------------------Fermer la fenetre

        #region Bouton annuler
        private void btnAnnuler_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        //-----------------Reinitialiser les textbox

        #region Reinitialiser
        public void clear()
        {
            get_nomb.Text = "";
            get_nomb.IsReadOnly = false;
            get_remise.Text = "";
            get_remise.IsReadOnly = false;
            get_telephone.Text = "";
            get_telephone.IsReadOnly = false;
            get_contact.Text = "";
            get_contact.IsReadOnly = false;
            get_courriel.Text = "";
            get_courriel.IsReadOnly = false;
            menu_adresse.Text = "";
        }
        #endregion


    }
}
