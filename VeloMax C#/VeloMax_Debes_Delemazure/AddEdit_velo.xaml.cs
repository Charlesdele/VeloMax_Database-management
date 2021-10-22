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
    /// Logique d'interaction pour AddEdit_velo.xaml
    /// </summary>
    public partial class AddEdit_velo : Window
    {
        public AddEdit_velo()
        {
            InitializeComponent();
        }

        //=======================================================================================================================
        // BOUTONS
        //=======================================================================================================================

        //----------------------------------------------------------------Creation ou modification d'un velo

        #region Bouton enregistrer
        public void btnEnregistrer_click(object sender, RoutedEventArgs e)
        {
            //Verification des textbox
            bool verif1 = Utilities.is_int(get_numv.Text, 5);
            bool verif2 = Utilities.is_varchar(get_nom.Text, 30);
            bool verif3 = Utilities.is_varchar(get_grandeur.Text, 30);
            bool verif4 = Utilities.is_int(get_prix.Text, 6);
            bool verif5 = Utilities.is_varchar(get_ligne.Text, 30);
            bool verif6 = Utilities.is_int(get_stock.Text, 6);
            if (verif1!=true | verif2!=true | verif3!=true | verif4!=true | verif5!=true |verif6!=true)
            {
                MessageBox.Show("Paramètres non conformes");
            }
            else
            {
                if (get_numv.IsReadOnly == false) //Si c'est une creation le numero du velo peut etre change dans la textbox
                {
                    Velo newV = new Velo();
                    newV.DateD = Convert.ToDateTime(get_dated.Text);
                    newV.DateF = Convert.ToDateTime(get_datef.Text);
                    newV.Grandeur = get_grandeur.Text;
                    newV.Ligne = get_ligne.Text;
                    newV.Nom = get_nom.Text;
                    newV.NumA = Convert.ToInt32(get_numAbox.SelectedValue);
                    newV.NumV = Convert.ToInt32(get_numv.Text);
                    newV.Prix = Convert.ToInt32(get_prix.Text);
                    newV.QuantitéEnStockVelo = Convert.ToInt32(get_stock.Text);
                    newV.CreateEditDelete("create");
                    this.Close();
                }
                else //Sinon on prend celui affiché
                {
                    Velo newV = new Velo();
                    newV.DateD = Convert.ToDateTime(get_dated.Text);
                    newV.DateF = Convert.ToDateTime(get_datef.Text);
                    newV.Grandeur = get_grandeur.Text;
                    newV.Ligne = get_ligne.Text;
                    newV.Nom = get_nom.Text;
                    newV.NumA = Convert.ToInt32(get_numAbox.SelectedValue);
                    newV.NumV = Convert.ToInt32(get_numv.Text);
                    newV.Prix = Convert.ToInt32(get_prix.Text);
                    newV.QuantitéEnStockVelo = Convert.ToInt32(get_stock.Text);
                    newV.CreateEditDelete("edit");
                    this.Close();
                }
            }           
        }
        #endregion

        //-------------------------------------------------------------Fermer la fenetre

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
            get_dated.Text = "";
            get_dated.IsReadOnly = false;
            get_datef.Text = "";
            get_datef.IsReadOnly = false;
            get_grandeur.Text = "";
            get_grandeur.IsReadOnly = false;
            get_ligne.Text = "";
            get_ligne.IsReadOnly = false;
            get_nom.Text = "";
            get_nom.IsReadOnly = false;
            get_numv.Text = "";
            get_numv.IsReadOnly = false;
            get_prix.Text = "";
            get_prix.IsReadOnly = false;
            get_stock.Text = "";
            get_stock.IsReadOnly = false;
            get_numAbox.Text = "";
        }
        #endregion
    }
}
