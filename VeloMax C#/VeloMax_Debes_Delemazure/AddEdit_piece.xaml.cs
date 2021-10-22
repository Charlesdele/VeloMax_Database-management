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
    /// Logique d'interaction pour AddEdit_piece.xaml
    /// </summary>
    public partial class AddEdit_piece : Window
    {
        public AddEdit_piece()
        {
            InitializeComponent();
        }

        //=======================================================================================================================
        // BOUTONS
        //=======================================================================================================================

        //--------------------------------------------------------------Création ou modifcation d'une pièce

        #region Bouton enregistrer
        public void btnEnregistrer_click(object sender, RoutedEventArgs e)
        {
            //Vérification des Textbox
            bool verif1 = Utilities.is_varchar(get_nump.Text, 10);
            bool verif2 = Utilities.is_varchar(get_description.Text, 50);
            bool verif3 = Utilities.is_int(get_prixu.Text, 10);
            bool verif4 = Utilities.is_int(get_quantite.Text, 10);
            if (verif1!=true | verif2!=true | verif3!=true | verif4!=true)
            {
                MessageBox.Show("Paramètres non conformes");
            }
            else
            {
                if (get_nump.IsReadOnly == false) //Si c'est une creation le numero de piece peut etre modifie dans la textbox
                {
                    Piece newP = new Piece();
                    newP.DateD = Convert.ToDateTime(get_dated.Text);
                    newP.DateI = Convert.ToDateTime(get_datei.Text);
                    newP.Description = get_description.Text;
                    newP.NumP = get_nump.Text;
                    newP.PrixU = Convert.ToInt32(get_prixu.Text);
                    newP.Quantite = Convert.ToInt32(get_quantite);
                    newP.CreateEditDelete("create");
                    this.Close();
                }
                else //Sinon on prend celui affiché
                {
                    Piece newP = new Piece();
                    newP.DateD = Convert.ToDateTime(get_dated.Text);
                    newP.DateI = Convert.ToDateTime(get_datei.Text);
                    newP.Description = get_description.Text;
                    newP.NumP = get_nump.Text;
                    newP.PrixU = Convert.ToInt32(get_prixu.Text);
                    newP.Quantite = Convert.ToInt32(get_quantite.Text);
                    newP.CreateEditDelete("edit");
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
            get_datei.Text = "";
            get_datei.IsReadOnly = false;
            get_description.Text = "";
            get_description.IsReadOnly = false;
            get_nump.Text = "";
            get_nump.IsReadOnly = false;
            get_prixu.Text = "";
            get_prixu.IsReadOnly = false;
            get_quantite.Text = "";
            get_quantite.IsReadOnly = false;
        }
        #endregion
    }
}
