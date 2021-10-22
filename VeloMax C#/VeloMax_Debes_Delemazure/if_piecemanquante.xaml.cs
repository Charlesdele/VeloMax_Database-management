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
using MySql.Data.MySqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace VeloMax_Debes_Delemazure
{
    /// <summary>
    /// Logique d'interaction pour if_piecemanquante.xaml
    /// </summary>
    public partial class if_piecemanquante : Window
    {
        //==================================================================================================================================================================================================================================================
        // INITIALISATION
        //==================================================================================================================================================================================================================================================

        #region Initialisation
        public if_piecemanquante()
        {
            InitializeComponent();
            initialize_piece();
        }

        public Dictionary<string, List<Reader>> Piece_FD = new Dictionary<string, List<Reader>>(); //Dictionnaire des pièces_fournisseurs_delai
        //Ce dictionnaire a comme clé les pièces manquantes et comme valeurs chaque pièce est associé à une list<Reader> ayant comme paramètres un string (different fournisseurs 
        //disponibles) et d'un int (delais associés à ces fournisseurs)

        public List<string> PiecesManquantes = new List<string>(); //Liste des pièces manquantes que l'on affiche dans le premier menu

        public List<string> FournisseurDispo = new List<string>(); //Liste des fournisseurs dispos que l'on affiche dans le deuxième menu

        public int delai = 0; //Plus grand delai de la livraison des pièces manquantes

        public void initialize_piece() //Affiche les pieces manquantes dans le menu
        {
            foreach (string piece in Piece_FD.Keys)
            {
                PiecesManquantes.Add(piece);
            }
            menu_piece.ItemsSource = PiecesManquantes;
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // BOUTONS
        //==================================================================================================================================================================================================================================================

        //----------------------------------------------------Valider la commande de la pièce manquante par le fournisseur selectionné

        #region Bouton valider
        public void valider (object sender, RoutedEventArgs e)
        {
            //Initialisation de la nouvelle liste de pièces manquantes
            List<string> update = new List<string>();

            //On supprime la piece manquante que l'on vient d'attribuer à une fournisseur disponibles
            PiecesManquantes.Remove(Convert.ToString(menu_piece.SelectedValue));

            //On vérifie si la livraison de cette pièce est la plus grande
            if (get_delai(Convert.ToString(menu_fournisseur.SelectedValue)) > delai)
            {
                delai = get_delai(Convert.ToString(menu_fournisseur.SelectedValue)); //Dans ce cas le client devra attendre au minimum ce delai
            }

            //Si toutes les pièces ont été commandes
            if (PiecesManquantes.Count == 0)
            {
                MessageBox.Show("Temps de livraison des pièces + " + delai);
                this.Close(); //On ferme la fenetre
            }
            else //Sinon on s'occupe des autres pièces manquantes
            {
                for (int i = 0; i < PiecesManquantes.Count;i++)
                {
                    update.Add(PiecesManquantes[i]); 
                }
            }

            //Association de la liste des pièces manquantes au premier menu déroulant
            menu_piece.ItemsSource = update;

            //Réinitialisation du menu des fournisseurs
            menu_fournisseur.ItemsSource = new List<string>();
            menu_fournisseur.Text = String.Empty;
            menu_fournisseur.SelectedIndex= -1;                   
        }
        #endregion

        //-------------------------------------------------------------Mise a jour du menu déroulant des fournisseurs disponibles en fonction de la pièce sélectionné

        #region Update le menu déroulant des fournisseurs disponibles
        public void update_fournisseur(object sender, RoutedEventArgs e)
        {
            //Réinitialisation du menu fournisseur
            menu_fournisseur.Text = String.Empty;
            menu_fournisseur.SelectedIndex = -1;

            //Initialisation de la nouvelle liste
            List<string> update = new List<string>();

            //On recupère la liste des fournisseurs (chacun associé à un délai) en fonction de la pièce sélectionné dans le premier menu déroulant
            List<Reader> FeDe = Piece_FD[Convert.ToString(menu_piece.SelectedValue)];
            for (int i = 0; i < FeDe.Count;i++)
            {
                string info = FeDe[i].string1 + "(delai " + Convert.ToString(FeDe[i].int1) + " J)"; //On affiche dans le menu les fournisseurs avec leur delai
                update.Add(info); 
            }

            //Association de la liste des fournisseurs avec leur delai au deuxième menu déroulant
            menu_fournisseur.ItemsSource = update;
        }
        #endregion

        //-------------------------------------------Quitter

        #region Fermer fenetre
        public void Quitter(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // INITIALISATION
        //==================================================================================================================================================================================================================================================

        //--------------------------------------------Retourne le delai le plus grand pour la livraison des pièces que le client devra attendre

        #region Delai des fournisseurs disponibles
        public int get_delai (string menu_fournisseur)
        {
            int delai = 0;
            string[] numbers = Regex.Split(menu_fournisseur, @"\D+"); //Divise le string
            foreach (string nbr in numbers)
            {
                if(int.TryParse(nbr,out delai)) //Test pour chaque partie du string si c'est un nombre
                {
                    delai = Convert.ToInt32(nbr);
                }
                if (delai != 0) //Si c'est un nombre on arrête la boucle
                {
                    break;
                }
            }
            return delai;
        }
        #endregion

        

    }
}
