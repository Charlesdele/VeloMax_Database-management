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

namespace VeloMax_Debes_Delemazure
{
    /// <summary>
    /// Logique d'interaction pour AddEdit_adresse.xaml
    /// </summary>
    public partial class AddEdit_adresse : Window
    {

        //=======================================================================================================================
        // INITIALISATION
        //=======================================================================================================================

        //-----------------------Affichage des adresses au lancement de la fenetre

        #region AddEdit_Adresse
        public AddEdit_adresse()
        {
            InitializeComponent();

            //ON AFFICHE TOUTES LES ADRESSES EXISTANTES DANS LA LISTVIEW//

            //Initialisation de la liste des adresses
            List<Adresse> ListOfAdresse = new List<Adresse>();

            //Definition de la requete
            string requete = "select * from adresse";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Adresse newA = new Adresse(); //On crée un nouvelle adresse

                //Lecture des colonnes
                int numAd = Convert.ToInt32(reader["numAd"]);
                int numero = Convert.ToInt32(reader["nombre"]);
                string rue = (string)reader["rue"];
                int codePostal = Convert.ToInt32(reader["codePostal"]);
                string ville = (string)reader["ville"];
                string province = (string)reader["province"];

                //Attribution des valeurs des colonnes à la nouvelle adresse
                newA.NumAd = numAd; ;
                newA.Nombre = numero;
                newA.Rue = rue;
                newA.Province = province;
                newA.Ville = ville;
                newA.CodePostal = codePostal;
                ListOfAdresse.Add(newA);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste d'adresse
            datagrid.ItemsSource = ListOfAdresse;

            //Creation des colonnes de la listview
            GridViewColumn NumAd = new GridViewColumn();
            NumAd.Width = 70;
            NumAd.DisplayMemberBinding = new Binding("NumAd");
            NumAd.Header = "Numero";

            GridViewColumn Numero = new GridViewColumn();
            Numero.Width = 110;
            Numero.DisplayMemberBinding = new Binding("Nombre");
            Numero.Header = "Numero rue";

            GridViewColumn Rue = new GridViewColumn();
            Rue.Width = 200;
            Rue.DisplayMemberBinding = new Binding("Rue");
            Rue.Header = "Rue";

            GridViewColumn Ville = new GridViewColumn();
            Ville.Width = 200;
            Ville.DisplayMemberBinding = new Binding("Ville");
            Ville.Header = "Ville";

            GridViewColumn CodePostal = new GridViewColumn();
            CodePostal.Width = 120;
            CodePostal.DisplayMemberBinding = new Binding("CodePostal");
            CodePostal.Header = "Code Postal";

            GridViewColumn Province = new GridViewColumn();
            Province.Width = 135;
            Province.DisplayMemberBinding = new Binding("Province");
            Province.Header = "Province";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(NumAd);
            maingrid.Columns.Add(Numero);
            maingrid.Columns.Add(Rue);
            maingrid.Columns.Add(Ville);
            maingrid.Columns.Add(CodePostal);
            maingrid.Columns.Add(Province);

            datagrid.View = maingrid;
        }
        #endregion

        //=======================================================================================================================
        // BOUTONS
        //=======================================================================================================================

        //----------------------------------------------------------------Creation ou modification d'une adresse

        #region Bouton enregistrer
        public void btnEnregistrer_click(object sender, RoutedEventArgs e)
        {
            //Verification des textbox
            bool verif1 = Utilities.is_int(get_numad.Text,3);
            bool verif2 = Utilities.is_varchar(get_province.Text, 30);
            bool verif3 = Utilities.is_int(get_numero.Text,3);
            bool verif4 = Utilities.is_varchar(get_rue.Text, 50);
            bool verif5 = Utilities.is_int(get_codepostal.Text, 8);
            bool verif6 = Utilities.is_varchar(get_ville.Text, 40);
            if (verif1 != true | verif3 != true | verif4 != true | verif5 != true | verif6 != true | verif2!=true)
            {
                MessageBox.Show("Paramètres non conformes");
            }
            else
            {
                if (get_numad.IsReadOnly == false) //Si c'est une creation le numero d'adresse peut etre modifie dans la textbox
                {
                    Adresse newA = new Adresse();
                    newA.CodePostal= Convert.ToInt32(get_codepostal.Text);
                    newA.Nombre = Convert.ToInt32(get_numero.Text);
                    newA.NumAd = Convert.ToInt32(get_numad.Text);
                    newA.Province = get_province.Text;
                    newA.Rue = get_rue.Text;
                    newA.Ville = get_ville.Text;
                    newA.CreateEdit("create");
                    
                }
                else //Sinon on prend celui affiché
                {
                    Adresse newA = new Adresse();
                    newA.CodePostal = Convert.ToInt32(get_codepostal.Text);
                    newA.Nombre = Convert.ToInt32(get_numero.Text);
                    newA.NumAd = Convert.ToInt32(get_numad.Text);
                    newA.Province = get_province.Text;
                    newA.Rue = get_rue.Text;
                    newA.Ville = get_ville.Text;
                    newA.CreateEdit("edit");
                }
            }
            this.clear();
            Affichage_adresse(sender, e); //On actualise l'affichage des adresses
        }
        #endregion

        //-------------------------------------------------------------Update de l'affichage des adresses

        #region Affichage adresse
        public void Affichage_adresse(object sender, RoutedEventArgs e)
        {
            //Initialisation de la liste des adresses
            List<Adresse> ListOfAdresse = new List<Adresse>();

            //Definition de la requete
            string requete = "select * from adresse";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Adresse newA = new Adresse(); // On créé une nouvelle adresse

                //Lecture des colonnes
                int numAd = Convert.ToInt32(reader["numAd"]);
                int numero = Convert.ToInt32(reader["nombre"]);
                string rue = (string)reader["rue"];
                int codePostal = Convert.ToInt32(reader["codePostal"]);
                string ville = (string)reader["ville"];
                string province = (string)reader["province"];

                //Attribution des valeurs des colonnes à la nouvelle adresse
                newA.NumAd = numAd; ;
                newA.Nombre = numero;
                newA.Rue = rue;
                newA.Province = province;
                newA.Ville = ville;
                newA.CodePostal = codePostal;
                ListOfAdresse.Add(newA);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste d'adresse
            datagrid.ItemsSource = ListOfAdresse;

            //Creation des colonnes de la listview
            GridViewColumn NumAd = new GridViewColumn();
            NumAd.Width = 100;
            NumAd.DisplayMemberBinding = new Binding("NumAd");
            NumAd.Header = "Numero adresse";

            GridViewColumn Numero = new GridViewColumn();
            Numero.Width = 100;
            Numero.DisplayMemberBinding = new Binding("Nombre");
            Numero.Header = "Numero rue";

            GridViewColumn Rue = new GridViewColumn();
            Rue.Width = 100;
            Rue.DisplayMemberBinding = new Binding("Rue");
            Rue.Header = "Rue";

            GridViewColumn Ville = new GridViewColumn();
            Ville.Width = 200;
            Ville.DisplayMemberBinding = new Binding("Ville");
            Ville.Header = "Ville";

            GridViewColumn CodePostal = new GridViewColumn();
            CodePostal.Width = 200;
            CodePostal.DisplayMemberBinding = new Binding("CodePostal");
            CodePostal.Header = "Code Postal";

            GridViewColumn Province = new GridViewColumn();
            Province.Width = 120;
            Province.DisplayMemberBinding = new Binding("Province");
            Province.Header = "Province";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(NumAd);
            maingrid.Columns.Add(Numero);
            maingrid.Columns.Add(Rue);
            maingrid.Columns.Add(Ville);
            maingrid.Columns.Add(CodePostal);
            maingrid.Columns.Add(Province);

            datagrid.View = maingrid;
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
            get_codepostal.Text = "";
            get_codepostal.IsReadOnly = false;
            get_numad.Text = "";
            get_numad.IsReadOnly = false;
            get_numero.Text = "";
            get_numero.IsReadOnly = false;
            get_province.Text = "";
            get_province.IsReadOnly = false;
            get_rue.Text = "";
            get_rue.IsReadOnly = false;
            get_ville.Text = "";
            get_ville.IsReadOnly = false;
        }
        #endregion
    }
}
