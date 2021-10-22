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
    /// Logique d'interaction pour AddEdit_fournisseur.xaml
    /// </summary>
    public partial class AddEdit_fournisseur : Window
    {
        public AddEdit_fournisseur()
        {
            InitializeComponent();
        }

        //=======================================================================================================================
        // BOUTONS
        //=======================================================================================================================

        //---------------------------------------------------------------Creation ou modification d'un fournisseur

        #region Bouton enregistrer
        public void btnEnregistrer_click(object sender, RoutedEventArgs e)
        {
            //Verification des textbox
            bool verif1 = Utilities.is_int(get_siret.Text, 14);
            bool verif2 = Utilities.is_varchar(get_nom.Text, 20);
            bool verif3 = Utilities.is_varchar(get_contact.Text, 20);
            bool verif4 = Utilities.is_int(get_libelle.Text, 3);
            if (verif1!= true | verif2!=true | verif3 != true | verif4!=true)
            {
                MessageBox.Show("Paramètres non conformes");
            }
            else
            {
                if (get_siret.IsReadOnly == false) //Si c'est une creation le siret peut etre modifie dans la textbox
                {
                    Fournisseur newF = new Fournisseur();
                    newF.Siret = get_siret.Text;
                    newF.Nomf = get_nom.Text;
                    newF.Contact = get_contact.Text;
                    newF.AdresseF = Convert.ToInt32(menu_adresse.SelectedValue);
                    newF.Libelle = Convert.ToInt32(get_libelle.Text);
                    newF.CreateEditDelete("create");
                    this.Close();
                }
                else //Sinon on prend celui affiché
                {
                    Fournisseur newF = new Fournisseur();
                    newF.Siret = get_siret.Text;
                    newF.Nomf = get_nom.Text;
                    newF.Contact = get_contact.Text;
                    newF.AdresseF = Convert.ToInt32(menu_adresse.SelectedValue);
                    newF.Libelle = Convert.ToInt32(get_libelle.Text);
                    newF.CreateEditDelete("edit");
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

        //----------------Réinitialiser les textbox

        #region Reinitialiser
        public void clear()
        {
            menu_adresse.Text = "";
            get_contact.Text = "";
            get_contact.IsReadOnly = false;
            get_libelle.Text = "";
            get_libelle.IsReadOnly = false;
            get_nom.Text = "";
            get_nom.IsReadOnly = false;
            get_siret.Text = "";
            get_siret.IsReadOnly = false;
        }
        #endregion

    }
}
