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
    /// Logique d'interaction pour AddEdit_commande.xaml
    /// </summary>
    public partial class AddEdit_commande : Window
    {
        public AddEdit_commande()
        {
            InitializeComponent();
        }

        //=======================================================================================================================
        // BOUTONS
        //=======================================================================================================================

        //----------------------------------------------------------------Création ou modification d'une commande

        #region Bouton enregistrer
        public void btnEnregistrer_click(object sender, RoutedEventArgs e)
        {
            //Vérification des textbox
            bool verif1 = Utilities.is_int(get_numc.Text, 3);
            if (verif1!=true)
            {
                MessageBox.Show("Paramètres non conformes");
            }
            else
            {
                if (get_numc.IsReadOnly == false) //Si c'est une creation le numéro de commande peut être modifié dans la textbox
                {
                    Commande newC = new Commande();
                    newC.AdresseL = Convert.ToInt32(menu_adresse.SelectedValue);
                    newC.DateC = Convert.ToDateTime(get_datec.Text);
                    newC.DateL = Convert.ToDateTime(get_datel.Text);
                    newC.NumC = Convert.ToInt32(get_numc.Text);
                    newC.CodePa = Convert.ToInt32(menu_codepa.SelectedValue);
                    newC.NomB = Convert.ToString(menu_nomb.SelectedValue);
                    newC.CreateEditDelete("create");
                    this.Close();
                }
                else //Sinon on prend celui affiché
                {
                    Commande newC = new Commande();
                    newC.AdresseL = Convert.ToInt32(menu_adresse.SelectedValue);
                    newC.DateC = Convert.ToDateTime(get_datec.Text);
                    newC.DateL = Convert.ToDateTime(get_datel.Text);
                    newC.NumC = Convert.ToInt32(get_numc.Text);
                    newC.CodePa = Convert.ToInt32(menu_codepa.SelectedValue);
                    newC.NomB = Convert.ToString(menu_nomb.SelectedValue);
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

        //-------------------------------------------------------------Fermer la fenetre

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
            get_datec.Text = "";
            get_datec.IsReadOnly = false;
            get_datel.Text = "";
            get_datel.IsReadOnly = false;
            get_numc.Text = "";
            get_numc.IsReadOnly = false;
            menu_adresse.Text = "";
            menu_codepa.Text = "";
            menu_nomb.Text = "";
        }
        #endregion


    }
}
