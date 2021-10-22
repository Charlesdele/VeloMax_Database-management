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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.IO;


namespace VeloMax_Debes_Delemazure
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //==================================================================================================================================================================================================================================================
        // INITIALISATION
        //==================================================================================================================================================================================================================================================

        #region Initialisation
        public MainWindow()
        {
            InitializeComponent();
            Connexion Login = new Connexion();
            Login.ShowDialog();
            while (Login.IsVisible == true) { }
            this.ShowDialog();
        }

        public string variable; // pour identifier quoi editer, modifier ou supprimer

        public int i1=1; // pour changer de page dans l'affichage des stocks
        #endregion

        //==================================================================================================================================================================================================================================================
        // AFFICHAGES
        //==================================================================================================================================================================================================================================================

        //-----------------------------------------------------------Affichage des pièces

        #region Affichage pièce
        public void Affichage_piece(object sender, RoutedEventArgs e)
        {
            variable = "Piece";
            titre.Text = "Affichage des pièces";

            //On initialise notre liste de pièces
            List<Piece> ListOfPiece = new List<Piece>();

            //Definition de la requete
            string requete = "select * from Piece";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Piece new_piece = new Piece(); //On initialise une pièce à chaque ligne

                //Lecture des colonnes
                string numP = (string)reader["numP"];
                string description = (string)reader["description"];
                int prixU = Convert.ToInt32(reader["prixU"]);
                DateTime dateI = (DateTime)reader["dateI"];
                DateTime dateD = (DateTime)reader["dateD"];
                int quantite = Convert.ToInt32(reader["quantitéEnStockPiece"]);

                //Attribution des valeurs des colonnes au reader
                new_piece.NumP = numP; ;
                new_piece.Description = description;
                new_piece.PrixU = prixU;
                new_piece.DateI = dateI;
                new_piece.DateD = dateD;
                new_piece.Quantite = quantite;
                ListOfPiece.Add(new_piece);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste de piece
            datagrid.ItemsSource = ListOfPiece;


            //Creation des colonnes de la listview
            GridViewColumn NumP = new GridViewColumn();
            NumP.Width = 50;
            NumP.DisplayMemberBinding = new Binding("NumP");
            NumP.Header = "Numero produit";

            GridViewColumn Description = new GridViewColumn();
            Description.Width = 150;
            Description.DisplayMemberBinding = new Binding("Description");
            Description.Header = "Description";

            GridViewColumn PrixU = new GridViewColumn();
            PrixU.Width = 100;
            PrixU.DisplayMemberBinding = new Binding("PrixU");
            PrixU.Header = "Prix unitaire";

            GridViewColumn DateI = new GridViewColumn();
            DateI.Width = 200;
            DateI.DisplayMemberBinding = new Binding("DateI");
            DateI.Header = "Date intro marché";

            GridViewColumn DateD = new GridViewColumn();
            DateD.Width = 200;
            DateD.DisplayMemberBinding = new Binding("DateD");
            DateD.Header = "Date discontinuation prod";

            GridViewColumn Quantite = new GridViewColumn();
            Quantite.Width = 60;
            Quantite.DisplayMemberBinding = new Binding("Quantite");
            Quantite.Header = "Quantité en stock";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(NumP);
            maingrid.Columns.Add(Description);
            maingrid.Columns.Add(PrixU);
            maingrid.Columns.Add(Quantite);
            maingrid.Columns.Add(DateI);
            maingrid.Columns.Add(DateD);

            datagrid.View = maingrid;
        }
        #endregion

        //-----------------------------------------------------------Affichage des clients

        #region Affichage client
        public void Affichage_client(object sender, RoutedEventArgs e)
        {
            variable = "Client";
            titre.Text = "Affichage des clients";

            //On initialise notre liste de clients
            List<Client> ListOfClient = new List<Client>();

            //Definition de la requete
            string requete = "select * from particulier";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Client new_client = new Client(); //On initialise un client a chaque ligne

                //Lecture des colonnes
                int codePa = Convert.ToInt32(reader["codePa"]);
                int adresseP = Convert.ToInt32(reader["adresseP"]);
                string nomPa = (string)reader["nomPa"];
                string prenom = (string)reader["prenom"];
                string telephone = (string)reader["telephone"];
                string courriel = (string)reader["courriel"];
                DateTime dateA = (DateTime)reader["dateA"];
                int numProg = Convert.ToInt32(reader["numProg"]);

                //Attribution des valeurs des colonnes au reader
                new_client.CodePa = codePa;
                new_client.AdresseP = adresseP;
                new_client.NomPa = nomPa;
                new_client.Prenom = prenom;
                new_client.Telephone = telephone;
                new_client.Courriel = courriel;
                new_client.DateA = dateA;
                new_client.NumProg = numProg;
                ListOfClient.Add(new_client);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste de clients
            datagrid.ItemsSource = ListOfClient;

            //Creation des colonnes de la listview
            GridViewColumn CodePa = new GridViewColumn();
            CodePa.Width = 50;
            CodePa.DisplayMemberBinding = new Binding("CodePa");
            CodePa.Header = "Id";

            GridViewColumn AdresseP = new GridViewColumn();
            AdresseP.Width = 80;
            AdresseP.DisplayMemberBinding = new Binding("AdresseP");
            AdresseP.Header = "Adresse";

            GridViewColumn NomPa = new GridViewColumn();
            NomPa.Width = 100;
            NomPa.DisplayMemberBinding = new Binding("NomPa");
            NomPa.Header = "Nom";

            GridViewColumn Prenom = new GridViewColumn();
            Prenom.Width = 100;
            Prenom.DisplayMemberBinding = new Binding("Prenom");
            Prenom.Header = "Prenom";

            GridViewColumn Telephone = new GridViewColumn();
            Telephone.Width = 115;
            Telephone.DisplayMemberBinding = new Binding("Telephone");
            Telephone.Header = "Telephone";

            GridViewColumn Courriel = new GridViewColumn();
            Courriel.Width = 130;
            Courriel.DisplayMemberBinding = new Binding("Courriel");
            Courriel.Header = "Email";

            GridViewColumn DateA = new GridViewColumn();
            DateA.Width = 170;
            DateA.DisplayMemberBinding = new Binding("DateA");
            DateA.Header = "Date d'adhésion";

            GridViewColumn NumProg = new GridViewColumn();
            NumProg.Width = 100;
            NumProg.DisplayMemberBinding = new Binding("NumProg");
            NumProg.Header = "Prog fidélité";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(CodePa);
            maingrid.Columns.Add(NomPa);
            maingrid.Columns.Add(Prenom);
            maingrid.Columns.Add(Courriel);
            maingrid.Columns.Add(Telephone);
            maingrid.Columns.Add(AdresseP);
            maingrid.Columns.Add(NumProg);
            maingrid.Columns.Add(DateA);

            datagrid.View = maingrid;
        }
        #endregion

        //----------------------------------------------------------------Affichage des fournisseurs

        #region Affichage fournisseur
        public void Affichage_fournisseur(object sender, RoutedEventArgs e)
        {
            variable = "Fournisseur";
            titre.Text = "Affichage des fournisseurs";

            //On initialise notre liste de fournisseurs
            List<Fournisseur> ListOfFournisseur = new List<Fournisseur>();

            //Definition de la requete
            string requete = "select * from fournisseur";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande 
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Fournisseur new_fournisseur = new Fournisseur(); //On initialise un fournisseur à chaque ligne

                //Lecture des colonnes
                string siret = (string)reader["siret"];
                string nomf = (string)reader["nomf"];
                string contact = (string)reader["contact"];
                int adresseF = Convert.ToInt32(reader["adresseF"]);
                int libelle = Utilities.get_libelle(siret);

                //Attribution des valeurs des colonnes au reader
                new_fournisseur.Siret = siret;
                new_fournisseur.Nomf = nomf;
                new_fournisseur.Contact = contact;
                new_fournisseur.AdresseF = adresseF;
                new_fournisseur.Libelle = libelle;
                ListOfFournisseur.Add(new_fournisseur);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste de fournisseurs
            datagrid.ItemsSource = ListOfFournisseur;

            //Creation des colonnes de la listview
            GridViewColumn Siret = new GridViewColumn();
            Siret.Width = 160;
            Siret.DisplayMemberBinding = new Binding("Siret");
            Siret.Header = "Siret";

            GridViewColumn Nomf = new GridViewColumn();
            Nomf.Width = 140;
            Nomf.DisplayMemberBinding = new Binding("Nomf");
            Nomf.Header = "Nom";

            GridViewColumn Contact = new GridViewColumn();
            Contact.Width = 120;
            Contact.DisplayMemberBinding = new Binding("Contact");
            Contact.Header = "Contact";

            GridViewColumn AdresseF = new GridViewColumn();
            AdresseF.Width = 80;
            AdresseF.DisplayMemberBinding = new Binding("AdresseF");
            AdresseF.Header = "Adresse";

            GridViewColumn Libelle = new GridViewColumn();
            Libelle.Width = 80;
            Libelle.DisplayMemberBinding = new Binding("Libelle");
            Libelle.Header = "Libelle";


            GridView maingrid = new GridView();
            maingrid.Columns.Add(Nomf);
            maingrid.Columns.Add(Siret);
            maingrid.Columns.Add(Contact);
            maingrid.Columns.Add(AdresseF);
            maingrid.Columns.Add(Libelle);

            datagrid.View = maingrid;
        }
        #endregion

        //--------------------------------------------------------------Affichage des commandes

        #region Affichage commande
        public void Affichage_commande(object sender, RoutedEventArgs e)
        {
            variable = "Commande";
            titre.Text = "Affichage des commandes";

            //On initialise notre liste de commandes
            List<Commande> ListOfCommande = new List<Commande>();

            //Definition de la requete
            string requete = "select * from Commande";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Commande new_commande = new Commande(); //On initialise une commande à chaque ligne

                //Lecture des colonnes
                int numC = Convert.ToInt32(reader["numC"]);
                DateTime dateC = (DateTime)reader["dateC"];
                int adresseL = Convert.ToInt32(reader["adresseL"]);
                DateTime dateL = (DateTime)reader["dateL"];
                int codePa;
                if (reader["codePa"] == System.DBNull.Value ) //Si le code client particulier est une valeur nulle on lui attribue 0
                {
                    codePa = 0;
                }
                else
                {
                    codePa = Convert.ToInt32(reader["codePa"]);
                }
                string nomB;
                if (reader["nomB"] == System.DBNull.Value) //Si le nom de la boutique est une valeur nulle on lui attribue le nom Null
                {
                    nomB = "Null";
                }
                else
                {
                    nomB = (string)reader["nomB"];
                }

                //Attribution des valeurs des colonnes au reader
                new_commande.NumC = numC;
                new_commande.DateC = dateC;
                new_commande.AdresseL = adresseL;
                new_commande.DateL= dateL;
                new_commande.CodePa = codePa;
                new_commande.NomB = nomB;
                ListOfCommande.Add(new_commande);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste de commandes
            datagrid.ItemsSource = ListOfCommande;

            //Creation des colonnes de la listview
            GridViewColumn NumC = new GridViewColumn();
            NumC.Width = 80;
            NumC.DisplayMemberBinding = new Binding("NumC");
            NumC.Header = "Numéro";

            GridViewColumn DateC = new GridViewColumn();
            DateC.Width = 180;
            DateC.DisplayMemberBinding = new Binding("DateC");
            DateC.Header = "Date";

            GridViewColumn AdresseL = new GridViewColumn();
            AdresseL.Width = 120;
            AdresseL.DisplayMemberBinding = new Binding("AdresseL");
            AdresseL.Header = "Adresse Livraison";

            GridViewColumn DateL = new GridViewColumn();
            DateL.Width = 200;
            DateL.DisplayMemberBinding = new Binding("DateL");
            DateL.Header = "Date livraison";

            GridViewColumn CodePa = new GridViewColumn();
            CodePa.Width = 135;
            CodePa.DisplayMemberBinding = new Binding("CodePa");
            CodePa.Header = "Code client";

            GridViewColumn NomB = new GridViewColumn();
            NomB.Width = 150;
            NomB.DisplayMemberBinding = new Binding("NomB");
            NomB.Header = "Nom Boutique";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(NumC);
            maingrid.Columns.Add(DateC);
            maingrid.Columns.Add(NomB);
            maingrid.Columns.Add(CodePa);
            maingrid.Columns.Add(AdresseL);
            maingrid.Columns.Add(DateL);

            datagrid.View = maingrid;
        }
        #endregion

        //--------------------------------------------------------------Affichage des boutiques

        #region Affichage boutique
        public void Affichage_boutique(object sender, RoutedEventArgs e)
        {
            variable = "Boutique";
            titre.Text = "Affichage des boutiques";

            //On initialise notre liste de boutiques
            List<Boutique> ListOfBoutique = new List<Boutique>();

            //Definition de la requete
            string requete = "select * from boutique";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Boutique new_boutique = new Boutique(); //On initialise une boutique à chaque ligne

                //Lecture des colonnes
                string nomB = (string)reader["nomB"];
                int adresseB = Convert.ToInt32(reader["adresseB"]);
                string telephone = (string)reader["telephone"];
                string courriel = (string)reader["courriel"];
                string contact = (string)reader["contact"];
                int remise = Convert.ToInt32(reader["remise"]);

                //Attribution des valeurs des colonnes au reader
                new_boutique.NomB = nomB;
                new_boutique.AdresseB = adresseB;
                new_boutique.Telephone = telephone;
                new_boutique.Courriel = courriel;
                new_boutique.Contact = contact;
                new_boutique.Remise = remise;
                ListOfBoutique.Add(new_boutique);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste de boutiques
            datagrid.ItemsSource = ListOfBoutique;

            //Creation des colonnes de la listview
            GridViewColumn NomB = new GridViewColumn();
            NomB.Width = 160;
            NomB.DisplayMemberBinding = new Binding("NomB");
            NomB.Header = "Nom";

            GridViewColumn AdresseB = new GridViewColumn();
            AdresseB.Width = 100;
            AdresseB.DisplayMemberBinding = new Binding("AdresseB");
            AdresseB.Header = "Adresse";

            GridViewColumn Telephone = new GridViewColumn();
            Telephone.Width = 135;
            Telephone.DisplayMemberBinding = new Binding("Telephone");
            Telephone.Header = "Telephone";

            GridViewColumn Courriel = new GridViewColumn();
            Courriel.Width = 200;
            Courriel.DisplayMemberBinding = new Binding("Courriel");
            Courriel.Header = "Courriel";

            GridViewColumn Contact = new GridViewColumn();
            Contact.Width = 135;
            Contact.DisplayMemberBinding = new Binding("Contact");
            Contact.Header = "Contact";

            GridViewColumn Remise = new GridViewColumn();
            Remise.Width = 80;
            Remise.DisplayMemberBinding = new Binding("Remise");
            Remise.Header = "Remise";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(NomB);
            maingrid.Columns.Add(Courriel);
            maingrid.Columns.Add(Telephone);
            maingrid.Columns.Add(Contact);
            maingrid.Columns.Add(AdresseB);
            maingrid.Columns.Add(Remise);

            datagrid.View = maingrid;
        }
        #endregion

        //-----------------------------------------------------------Affichage des velos

        #region Affichage velo
        public void Affichage_Velo (object sender, RoutedEventArgs e)
        {
            variable = "Velo";
            titre.Text = "Affichage des velos";

            //On initialise notre liste de velos
            List<Velo> ListOfVelo = new List<Velo>();

            //Definition de la requete
            string requete = "select * from velo";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Velo new_velo = new Velo(); //On initialise un velo à chaque ligne

                //Lecture des colonnes
                int numV = Convert.ToInt32(reader["numv"]);
                string nom = (string)reader["nom"];
                string grandeur = (string)reader["grandeur"];
                int prix = Convert.ToInt32(reader["prix"]);
                string ligne = (string)reader["ligne"];
                DateTime dateD = (DateTime)reader["dateD"];
                DateTime dateF = (DateTime)reader["dateF"];
                int numA = Convert.ToInt32(reader["numA"]);
                int quantitéEnStockVelo = Convert.ToInt32(reader["quantitéEnStockVelo"]);

                //Attribution des valeurs des colonnes au reader
                new_velo.NumV = numV;
                new_velo.Nom = nom;
                new_velo.Grandeur = grandeur;
                new_velo.Prix = prix;
                new_velo.Ligne = ligne;
                new_velo.DateD = dateD;
                new_velo.DateF = dateF;
                new_velo.NumA = numA;
                new_velo.QuantitéEnStockVelo = quantitéEnStockVelo;
                ListOfVelo.Add(new_velo);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste de velos
            datagrid.ItemsSource = ListOfVelo;

            //Creation des colonnes de la listview
            GridViewColumn NumV = new GridViewColumn();
            NumV.Width = 70;
            NumV.DisplayMemberBinding = new Binding("NumV");
            NumV.Header = "Numéro";

            GridViewColumn Nom = new GridViewColumn();
            Nom.Width = 135;
            Nom.DisplayMemberBinding = new Binding("Nom");
            Nom.Header = "Nom";

            GridViewColumn Grandeur = new GridViewColumn();
            Grandeur.Width = 100;
            Grandeur.DisplayMemberBinding = new Binding("Grandeur");
            Grandeur.Header = "Grandeur";

            GridViewColumn Prix = new GridViewColumn();
            Prix.Width = 70;
            Prix.DisplayMemberBinding = new Binding("Prix");
            Prix.Header = "Prix";

            GridViewColumn Ligne = new GridViewColumn();
            Ligne.Width = 135;
            Ligne.DisplayMemberBinding = new Binding("Ligne");
            Ligne.Header = "Ligne";

            GridViewColumn DateD = new GridViewColumn();
            DateD.Width = 200;
            DateD.DisplayMemberBinding = new Binding("DateD");
            DateD.Header = "Date intro marché";

            GridViewColumn DateF = new GridViewColumn();
            DateF.Width = 200;
            DateF.DisplayMemberBinding = new Binding("DateF");
            DateF.Header = "Date discontinuation prod";

            GridViewColumn NumA = new GridViewColumn();
            NumA.Width = 100;
            NumA.DisplayMemberBinding = new Binding("NumA");
            NumA.Header = "Numéro Assemblage";

            GridViewColumn QuantitéEnStockVelo = new GridViewColumn();
            QuantitéEnStockVelo.Width = 100;
            QuantitéEnStockVelo.DisplayMemberBinding = new Binding("QuantitéEnStockVelo");
            QuantitéEnStockVelo.Header = "Quantité";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(NumV);
            maingrid.Columns.Add(Nom);
            maingrid.Columns.Add(Grandeur);
            maingrid.Columns.Add(Prix);
            maingrid.Columns.Add(Ligne);
            maingrid.Columns.Add(NumA);
            maingrid.Columns.Add(QuantitéEnStockVelo);
            maingrid.Columns.Add(DateD);
            maingrid.Columns.Add(DateF);

            datagrid.View = maingrid;
        }
        #endregion

        //-----------------------------------------------------------Affichage des stocks

        #region Affichage stock
        public void Affichage_Stock (object sender, RoutedEventArgs e)
        {
            variable = "Stock";
            if (i1 == 1) //Stocks par pièce
            {
                titre.Text = "Aperçu des stocks par pièce";
             
                //On initialise notre liste des stocks par pièces
                List<Reader> ListOfPiece = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT numP,quantitéEnStockPiece FROM piece ;";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newP = new Reader(); //On initialise une pièce à chaque ligne

                    //Lecture des colonnes
                    string numP = (string)reader["numP"];
                    int quantite = Convert.ToInt32(reader["quantitéEnStockPiece"]);

                    //Attribution des valeurs des colonnes au reader
                    newP.string1 = numP;
                    newP.int1 = quantite;
                    ListOfPiece.Add(newP);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfPiece;

                //Creation des colonnes de la listview
                GridViewColumn Nump = new GridViewColumn();
                Nump.Width = 170;
                Nump.DisplayMemberBinding = new Binding("string1");
                Nump.Header = "Numéro pièce";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 170;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantite";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nump);
                maingrid.Columns.Add(Quantite);
                datagrid.View = maingrid;
            }
            else if (i1 == 2) //Stocks par fournisseurs
            {
                titre.Text = "Aperçu des stocks par fournisseur";

                //On initialise une liste des pieces par fournisseur
                List<Reader> ListOfPiece = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nomf,GROUP_CONCAT(DISTINCT numP) FROM livre,fournisseur WHERE fournisseur.siret=livre.siret GROUP BY nomf;";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newS = new Reader(); //On initialise un nouveau fournisseur par ligne

                    //Lecture des colonnes
                    string nomfournisseur = (string)reader["nomf"];
                    string piece = reader.GetString(1);

                    //Attribution des valeurs des colonnes au reader
                    newS.string1 = nomfournisseur;
                    newS.string2 = piece;
                    ListOfPiece.Add(newS);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste des pièces par fournisseur
                datagrid.ItemsSource = ListOfPiece;

                //Creation des colonnes de la listview
                GridViewColumn Nomf = new GridViewColumn();
                Nomf.Width = 170;
                Nomf.DisplayMemberBinding = new Binding("string1");
                Nomf.Header = "Nom fournisseur";

                GridViewColumn Piece = new GridViewColumn();
                Piece.Width = 200;
                Piece.DisplayMemberBinding = new Binding("string2");
                Piece.Header = "Piece";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nomf);
                maingrid.Columns.Add(Piece);
                datagrid.View = maingrid;
            }
            else if (i1 == 3) //Stocks par vélo
            {
                titre.Text = "Aperçu des stocks par vélo";

                //On initialise une liste des stocks de velo
                List<Reader> ListOfVelo = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nom,grandeur,quantitéEnStockVelo FROM velo ORDER BY quantitéEnStockVelo DESC;";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newV = new Reader(); //On initialise un nouveau velo à chaque ligne

                    //Lecture des colonnes
                    string nom = (string)reader["nom"];
                    string grandeur = (string)reader["grandeur"];
                    int quantite = Convert.ToInt32(reader["quantitéEnStockVelo"]);

                    //Attribution des valeurs des colonnes au reader
                    newV.string1 = nom;
                    newV.string2 = grandeur;
                    newV.int1= quantite;
                    ListOfVelo.Add(newV);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de velo
                datagrid.ItemsSource = ListOfVelo;

                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 170;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Grandeur = new GridViewColumn();
                Grandeur.Width = 170;
                Grandeur.DisplayMemberBinding = new Binding("string2");
                Grandeur.Header = "Grandeur";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 135;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantité";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Grandeur);
                maingrid.Columns.Add(Quantite);
                datagrid.View = maingrid;
            }
            else if (i1 == 4) //Stocks par grandeur
            {
                titre.Text = "Aperçu des stocks par grandeur";

                //On initialise notre liste du stocks de velo par grandeur
                List<Reader> ListOfVelo = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT grandeur,SUM(quantitéEnStockVelo) FROM velo GROUP BY grandeur ORDER BY SUM(quantitéEnStockVelo) DESC;";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newV = new Reader(); //On  initialise une grandeur par ligne

                    //Lecture des colonnes
                    string grandeur = (string)reader["grandeur"];
                    int quantite = Convert.ToInt32(reader["SUM(quantitéEnStockVelo)"]);

                    //Attribution des valeurs des colonnes au reader
                    newV.string1 = grandeur;
                    newV.int1 = quantite;
                    ListOfVelo.Add(newV);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfVelo;

                //Creation des colonnes de la listview
                GridViewColumn Grandeur = new GridViewColumn();
                Grandeur.Width = 170;
                Grandeur.DisplayMemberBinding = new Binding("string1");
                Grandeur.Header = "Grandeur";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 135;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantité";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Grandeur);
                maingrid.Columns.Add(Quantite);
                datagrid.View = maingrid;
            }
            else if (i1 == 5) //Stocks par ligne
            {
                titre.Text = "Aperçu des stocks par ligne";

                //On initialise notre liste du stocks de velo par ligne
                List<Reader> ListOfVelo = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT ligne,SUM(quantitéEnStockVelo) FROM velo GROUP BY ligne ORDER BY SUM(quantitéEnStockVelo) DESC;";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newV = new Reader(); //On initialise une ligne par ligne

                    //Lecture des colonnes
                    string ligne = (string)reader["ligne"];
                    int quantite = Convert.ToInt32(reader["SUM(quantitéEnStockVelo)"]);

                    //Attribution des valeurs des colonnes au reader
                    newV.string1 = ligne;
                    newV.int1 = quantite;
                    ListOfVelo.Add(newV);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de velo
                datagrid.ItemsSource = ListOfVelo;

                GridViewColumn Ligne = new GridViewColumn();
                Ligne.Width = 170;
                Ligne.DisplayMemberBinding = new Binding("string1");
                Ligne.Header = "Grandeur";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 135;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantité";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Ligne);
                maingrid.Columns.Add(Quantite);
                datagrid.View = maingrid;
            }         
        }
        #endregion


        //==================================================================================================================================================================================================================================================
        // PASSER UNE COMMANDE, MODE DEMO, MODULE STATISTIQUES
        //==================================================================================================================================================================================================================================================

        //---------------------------------------------------------------Affichage de la fenetre du module statistique

        #region Module statistiques
        public void Module_statistiques(object sender, RoutedEventArgs e)
        {
            Module_stat stat = new Module_stat(); //Creation de la fenetre du module statistiques
            stat.ShowDialog(); //Affichage de la fenetre
        }
        #endregion

        //-----------------------------------------------------------Affichage de la fenetre pour passer une commande

        #region Passer une commande
        public void passer_commande(object sender, RoutedEventArgs e)
        {
            Passer_commande commande = new Passer_commande(); //Creation de la fenetre pour passer une commande
            commande.menu_boutique.ItemsSource = Utilities.Loadnomb(); //Affichage des pièces dans la listview dediée au pièces de la fenetre commande
            commande.menu_client.ItemsSource = Utilities.Loadcodepa(); //Affichage des vélos dans la listview dédiée au vélos de la fenetre commande
            commande.ShowDialog(); //Affichage de la fenetre
        }
        #endregion

        //-----------------------------------------------------Affichage de la fenetre du mode demo

        #region Mode DEMO
        public void Mode_demo(object sender, RoutedEventArgs e)
        {
            Demo demo = new Demo(); //Creation de la fenetre du mode demo
            demo.ShowDialog(); //Affichage de la fenetre
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // BOUTONS
        //==================================================================================================================================================================================================================================================

        //-----------------------------------------------------------------Affichage du nombre de pièce en rupture de stock et lesquelles

        #region Bouton rupture de stock
        public void update_RuptudeDeStock(object sender, RoutedEventArgs e)
        {
            //Initialisation
            int x = 0; //On initialise le nombre de pièce en rupture de stock
            string Message = ""; //On initialise les pièces qui sont en rupture de stock
            List<Reader> pieces = new List<Reader>(); //On initialise notre liste de pièces

            //Definition de la requete
            string requete = "SELECT numP FROM piece where quantitéEnStockPiece=0;";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Reader newP = new Reader(); //On initialise une pièce à chaque ligne

                //Attribution des valeurs des colonnes au reader
                newP.string1 = (string)reader["numP"];

                Message += newP.string1 + "  "; //Les pièces en rupture de stock s'ajoute au message
                pieces.Add(newP);
            }
            reader.Close();
            command.Dispose();

            //Affichage du nombre de pièce en rupture de stock
            x = pieces.Count;
            rupturedestock.Content = "Pièce en rupture de stock : " + Convert.ToString(x);

            //Affichage du message des numero des pièces en rupture de stock
            MessageBox.Show("Pièces en rupture de stock : " + Message);
        }
        #endregion

        //-------------------------------------------------------------Suppression d'un ou plusieurs tuples d'une des tables de la base de données VeloMax

        #region Bouton supprimer
        public void btnsupprimer_click(object sender, RoutedEventArgs e)
        {
            if (variable == "Piece")
            {
                List<Piece> Listdatagrid = (List<Piece>)datagrid.ItemsSource; //On recupere les pieces selectionnes dans une liste
                for (int i = 0; i < datagrid.SelectedItems.Count; i++) //On parcours la liste
                {
                    Piece suppr = datagrid.SelectedItems[i] as Piece;
                    suppr.CreateEditDelete("delete"); //Suppression dans la base de données
                    Listdatagrid.Remove(suppr);  //Suppression dans la liste
                }
                Clear_datagrid(); //Reinitialiser la listview
                datagrid.ItemsSource = Listdatagrid; //Mise à jour de l'affichage
            }
            else if (variable == "Client")
            {
                List<Client> Listdatagrid = (List<Client>)datagrid.ItemsSource; //On recupere les clients selectionnes dans une liste
                for (int i = 0; i < datagrid.SelectedItems.Count; i++) //On parcours la liste
                {
                    Client suppr = datagrid.SelectedItems[i] as Client;
                    suppr.CreateEditDelete("delete"); //Suppression dans la base de données
                    Listdatagrid.Remove(suppr); //Suppression dans la liste
                }
                Clear_datagrid(); //Reinitialiser la listview
                datagrid.ItemsSource = Listdatagrid; //Mise à jour de l'affichage
            }
            else if (variable == "Fournisseur")
            {
                List<Fournisseur> Listdatagrid = (List<Fournisseur>)datagrid.ItemsSource; //On recupere les fournisseurs selectionnes dans une liste
                for (int i = 0; i < datagrid.SelectedItems.Count; i++) //On parcours la liste
                {
                    Fournisseur suppr = datagrid.SelectedItems[i] as Fournisseur;
                    suppr.CreateEditDelete("delete"); //Suppression dans la base de données
                    Listdatagrid.Remove(suppr); //Suppression dans la liste
                }
                Clear_datagrid(); //Reinitialiser la listview
                datagrid.ItemsSource = Listdatagrid; //Mise à jour de l'affichage
            }
            else if (variable == "Commande")
            {
                List<Commande> Listdatagrid = (List<Commande>)datagrid.ItemsSource; //On recupere les commandes selectionnes dans une liste
                for (int i = 0; i < datagrid.SelectedItems.Count; i++) //On parcours la liste
                {
                    Commande suppr = datagrid.SelectedItems[i] as Commande;
                    suppr.CreateEditDelete("delete"); //Suppression dans la base de données
                    Listdatagrid.Remove(suppr); //Suppression dans la liste
                }
                Clear_datagrid(); //Reinitialiser la listview
                datagrid.ItemsSource = Listdatagrid; //Mise à jour de l'affichage
            } 
            else if (variable == "Boutique")
            {
                List<Boutique> Listdatagrid = (List<Boutique>)datagrid.ItemsSource; //On recupere les boutiques selectionnes dans une liste
                for (int i = 0; i < datagrid.SelectedItems.Count; i++) //On parcours la liste
                {
                    Boutique suppr = datagrid.SelectedItems[i] as Boutique;
                    suppr.CreateEditDelete("delete"); //Suppression dans la base de données
                    Listdatagrid.Remove(suppr); //Suppression dans la liste
                }
                Clear_datagrid(); //Reinitialiser la listview
                datagrid.ItemsSource = Listdatagrid; //Mise à jour de l'affichage
            }
            else if (variable == "Velo")
            {
                List<Velo> Listdatagrid = (List<Velo>)datagrid.ItemsSource; //On recupere les velos selectionnes dans une liste
                for (int i = 0; i < datagrid.SelectedItems.Count; i++) //On parcours la liste
                { 
                    Velo suppr = datagrid.SelectedItems[i] as Velo;
                    suppr.CreateEditDelete("delete"); //Suppression dans la base de données
                    Listdatagrid.Remove(suppr); //Suppression dans la liste
                } 
                Clear_datagrid();  //Reinitialiser la listview
                datagrid.ItemsSource = Listdatagrid; //Mise à jour de l'affichage
            }
        }
        #endregion

        //-----------------------------------------------------------Creation ou modification d'un tuple d'une des tables de la base de données VeloMax

        #region Bouton Ajouter/Editer
        public void btnediter_click(object sender, RoutedEventArgs e)
        {  
            if (variable == "Adresse")
            {
                AddEdit_adresse add = new AddEdit_adresse();
                if (datagrid.SelectedItems.Count == 0)
                {
                    add.clear();
                    add.ShowDialog();
                }
                else
                {
                    List<Adresse> Listdatagrid = (List<Adresse>)datagrid.ItemsSource;
                    Adresse selec = datagrid.SelectedItem as Adresse;
                    add.get_codepostal.Text = Convert.ToString(selec.CodePostal);
                    add.get_numero.Text = Convert.ToString(selec.Nombre);
                    add.get_province.Text = selec.Province;
                    add.get_rue.Text = selec.Rue;
                    add.get_ville.Text = selec.Ville;
                    add.get_numad.Text = Convert.ToString(selec.NumAd);
                    add.get_numad.IsReadOnly = true;
                    add.ShowDialog();
                }
                while (add.IsVisible == true) { }
                add.clear();
            }
            if (variable == "Commande")
            {
                AddEdit_commande add = new AddEdit_commande();
                add.menu_adresse.ItemsSource = Utilities.Loadadresse();
                add.menu_codepa.ItemsSource = Utilities.Loadcodepa();
                add.menu_nomb.ItemsSource = Utilities.Loadnomb();
                if (datagrid.SelectedItems.Count == 0)
                {
                    add.clear();
                    add.ShowDialog();
                }
                else
                {
                    List<Commande> Listdatagrid = (List<Commande>)datagrid.ItemsSource;
                    Commande selec = datagrid.SelectedItem as Commande;
                    add.menu_adresse.Text = Convert.ToString(selec.AdresseL);
                    add.get_datec.Text = Convert.ToString(selec.DateC);
                    add.get_datel.Text = Convert.ToString(selec.DateL);
                    add.get_numc.Text = Convert.ToString(selec.NumC);
                    add.menu_codepa.Text = Convert.ToString(selec.CodePa);
                    add.menu_nomb.Text = selec.NomB;
                    add.get_numc.IsReadOnly = true;
                    add.ShowDialog();                    
                }
                while (add.IsVisible == true) { }
                add.clear();
                Affichage_commande(sender, e);
            }
            else if (variable == "Fournisseur")
            {
                AddEdit_fournisseur add = new AddEdit_fournisseur();
                add.menu_adresse.ItemsSource = Utilities.Loadadresse();
                if (datagrid.SelectedItems.Count == 0)
                {
                    add.clear();
                    add.ShowDialog();                
                }
                else
                {
                    List<Fournisseur> Listdatagrid = (List<Fournisseur>)datagrid.ItemsSource;
                    Fournisseur selec = datagrid.SelectedItem as Fournisseur;
                    add.get_siret.Text = selec.Siret;
                    add.get_siret.IsReadOnly = true;
                    add.get_nom.Text = selec.Nomf;
                    add.get_contact.Text = selec.Contact;
                    add.menu_adresse.Text = Convert.ToString(selec.AdresseF);
                    add.get_libelle.Text = Convert.ToString(selec.Libelle);
                    add.ShowDialog();
                }
                while (add.IsVisible == true){}
                add.clear();
                Affichage_fournisseur(sender, e);
            }
            else if (variable == "Boutique")
            {
                AddEdit_boutique add = new AddEdit_boutique();
                add.menu_adresse.ItemsSource = Utilities.Loadadresse();
                if (datagrid.SelectedItems.Count == 0)
                {
                    add.clear();
                    add.ShowDialog();
                }
                else
                {
                    List<Boutique> Listdatagrid = (List<Boutique>)datagrid.ItemsSource;
                    Boutique selec = datagrid.SelectedItem as Boutique;
                    add.get_contact.Text = selec.Contact;
                    add.get_courriel.Text = selec.Courriel;
                    add.get_nomb.Text = selec.NomB;
                    add.get_remise.Text = Convert.ToString(selec.Remise);
                    add.get_telephone.Text = selec.Telephone;
                    add.get_nomb.IsReadOnly = true;
                    add.menu_adresse.Text = Convert.ToString(selec.AdresseB);
                    add.ShowDialog();
                }
                while (add.IsVisible == true) { }
                add.clear();
                Affichage_boutique(sender, e);
            }
            else if (variable == "Client")
            {
                AddEdit_client add = new AddEdit_client();
                add.menu_adresse.ItemsSource = Utilities.Loadadresse();
                add.menu_numprog.ItemsSource = Utilities.Loadnumprog();                
                if (datagrid.SelectedItems.Count == 0)
                {
                    add.clear();
                    add.get_datea.Text = DateTime.Now.ToString();
                    add.get_datea.IsReadOnly = true;                  
                    add.ShowDialog();
                }
                else
                {
                    List<Client> Listdatagrid = (List<Client>)datagrid.ItemsSource;
                    Client selec = datagrid.SelectedItem as Client;
                    add.menu_adresse.Text = Convert.ToString(selec.AdresseP);
                    add.get_codepa.Text = Convert.ToString(selec.CodePa);
                    add.get_courriel.Text = selec.Courriel;
                    add.get_datea.Text = Convert.ToString(selec.DateA);
                    add.get_nompa.Text = selec.NomPa;
                    add.menu_numprog.Text = Convert.ToString(selec.NumProg);
                    add.get_prenom.Text = selec.Prenom;
                    add.get_telephone.Text = selec.Telephone;
                    add.get_codepa.IsReadOnly = true;
                    add.ShowDialog();
                }
                while (add.IsVisible == true) { }
                add.clear();
                Affichage_client(sender, e);
            }
            else if (variable == "Piece")
            {
                AddEdit_piece add = new AddEdit_piece();
                if (datagrid.SelectedItems.Count == 0)
                {
                    add.clear();
                    add.ShowDialog();
                }
                else
                {
                    List<Piece> Listdatagrid = (List<Piece>)datagrid.ItemsSource;
                    Piece selec = datagrid.SelectedItem as Piece;
                    add.get_dated.Text = Convert.ToString(selec.DateD);
                    add.get_datei.Text = Convert.ToString(selec.DateI);
                    add.get_description.Text = selec.Description;
                    add.get_nump.Text = selec.NumP;
                    add.get_prixu.Text = Convert.ToString(selec.PrixU);
                    add.get_quantite.Text = Convert.ToString(selec.Quantite);
                    add.get_nump.IsReadOnly = true;
                    add.ShowDialog();
                }
                while (add.IsVisible == true) { }
                add.clear();
                Affichage_piece(sender, e);
            }
            else if (variable == "Velo")
            {
                AddEdit_velo add = new AddEdit_velo();
                add.get_numAbox.ItemsSource = Utilities.Loadnuma();                
                if (datagrid.SelectedItems.Count == 0)
                {
                    add.clear();
                    add.ShowDialog();
                }
                else
                {
                    List<Velo> Listdatagrid = (List<Velo>)datagrid.ItemsSource;
                    Velo selec = datagrid.SelectedItem as Velo;
                    add.get_dated.Text = Convert.ToString(selec.DateD);
                    add.get_datef.Text = Convert.ToString(selec.DateF);
                    add.get_grandeur.Text = selec.Grandeur;
                    add.get_ligne.Text = selec.Ligne;
                    add.get_nom.Text = selec.Nom;
                    add.get_numv.Text = Convert.ToString(selec.NumV);
                    add.get_prix.Text = Convert.ToString(selec.Prix);
                    add.get_stock.Text = Convert.ToString(selec.QuantitéEnStockVelo);
                    add.get_numv.IsReadOnly = true;
                    add.get_numAbox.Text = Convert.ToString(selec.NumA);
                    add.ShowDialog();
                }
                while (add.IsVisible == true) { }
                add.clear();
                Affichage_Velo(sender, e);
            }       
        }
        #endregion

        //---------------------------------------------------Acceder à la page suivante pour la gestion des stocks

        #region Bouton suivant
        public void Suivant(object sender, RoutedEventArgs e)
        {
            //Stock ne possède que 5 pages
            if (variable == "Stock")
            {
                i1++; //On augmente l'index
                if (i1 == 6) //Si l'index est à 6
                {
                    i1 = 1; //On retourne à la première page
                }
                Affichage_Stock(sender, e); //Mise à jour de l'affichage
            }
        }
        #endregion

        //----------------------------------------------------Acceder à la page précédente pour la gestion des stocks

        #region Bouton précédent
        public void Precedent(object sender, RoutedEventArgs e)
        {
            //Stock ne possède que 5 pages
            if (variable == "Stock")
            {
                i1--; //On diminue l'index
                if (i1 == 0) //Si l'index est à 0
                {
                    i1 = 5; //On retourne à la dernière page
                }
                Affichage_Stock(sender, e); //Mise à jour de l'affichage
            }
        }
        #endregion

        //----------------------------------------------------------Fermer le logiciel

        #region Deconnexion
        public void fermer_session (object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        //======================================================================================================================
        // AUTRES FONCTIONS
        //======================================================================================================================

        //---------------------------Mise à jour de l'affichage

        #region Réinitialiser
        private void Clear_datagrid()
        {
            datagrid.ClearValue(ItemsControl.ItemsSourceProperty);
        }
        #endregion
    }
}
