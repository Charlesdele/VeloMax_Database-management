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
    public partial class Module_stat : Window
    {
        //==================================================================================================================================================================================================================================================
        // INITIALISATION
        //==================================================================================================================================================================================================================================================

        #region Initialisation
        public Module_stat()
        {
            InitializeComponent();
            Clear_datagrid();
        }

        public string categorie = ""; // pour savoir sur quelle affichage on se positionne

        public int int_programme = 1; //pour parcourir la liste des clients en fonction de leur programme

        public int int_client = 1; // pour changer les pages et montrer les analyses faites sur les clients (bouton client)

        public int int_boutique =1; // pour changer les pages et montrer les analyses faites sur les boutiques (bouton boutique)

        public int int_commande = 1; // pour changer les pages et montrer les analyses faites sur les commandes (bouton commande)

        public int int_quantitevendu = 0; // pour alterner entre les quantités vendues de vélos et de pièces
        #endregion

        //==================================================================================================================================================================================================================================================
        // ANALYSES
        //=================================================================================================================================================================================================================================================

        //-------------------------------------------------------------------Affichage des abonnements des clients avec leur date d'adhésion et d'expiration       

        #region Analyse expiration adhésion
        public void btn_expirationadhesion (object sender, RoutedEventArgs e)
        {
            Clear_datagrid();
            text.Text = "";
            text2.Text = "Expiration adhésion";

            //Initialisation de la liste des clients
            List<Reader> ListOfClient = new List<Reader>();

            //Definition de la requete, Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT nomPa,prenom,programme.description,dateA,duree FROM particulier,programme WHERE programme.numProg = particulier.numProg ORDER BY programme.numProg;";

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Reader newC = new Reader(); //On initialise un nouveau client à chaque ligne

                //Lecture des colonnes
                string nom = (string)reader["nomPa"];
                string prenom = (string)reader["prenom"];
                string description = (string)reader["description"];
                DateTime date = Convert.ToDateTime(reader["dateA"]);
                int duree = Convert.ToInt32(reader["duree"]);
                DateTime dateF = date.AddYears(duree);

                //Attribution des valeurs des colonnes au reader
                newC.date1 = date;
                newC.date2 = dateF;
                newC.string1 = nom;
                newC.int1 = duree;
                newC.string2 = prenom;
                newC.string3 = description;
                ListOfClient.Add(newC);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste des clients
            datagrid.ItemsSource = ListOfClient;

            //Creation des colonnes de la listview
            GridViewColumn Nom = new GridViewColumn();
            Nom.Width = 80;
            Nom.DisplayMemberBinding = new Binding("string1");
            Nom.Header = "Nom";

            GridViewColumn Prenom = new GridViewColumn();
            Prenom.Width = 80;
            Prenom.DisplayMemberBinding = new Binding("string2");
            Prenom.Header = "Prenom";

            GridViewColumn Description = new GridViewColumn();
            Description.Width = 100;
            Description.DisplayMemberBinding = new Binding("string3");
            Description.Header = "Description";

            GridViewColumn Duree = new GridViewColumn();
            Duree.Width = 100;
            Duree.DisplayMemberBinding = new Binding("int1");
            Duree.Header = "Duree";

            GridViewColumn Date = new GridViewColumn();
            Date.Width = 82;
            Date.DisplayMemberBinding = new Binding("date1");
            Date.Header = "Adhésion";

            GridViewColumn DateF = new GridViewColumn();
            DateF.Width = 82;
            DateF.DisplayMemberBinding = new Binding("date2");
            DateF.Header = "Expiration";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(Nom);
            maingrid.Columns.Add(Prenom);
            maingrid.Columns.Add(Description);
            maingrid.Columns.Add(Date);
            maingrid.Columns.Add(Duree);
            maingrid.Columns.Add(DateF);
            datagrid.View = maingrid;
        }
        #endregion

        //---------------------------------------------------------------Affichage des quantites vendues de pièces et de vélos

        #region Analyse quantité vendue
        public void btn_quantitevendue (object sender, RoutedEventArgs e)
        {
            Clear_datagrid();
            int_quantitevendu++; //Changement de page
            if (int_quantitevendu == 1) //Page 1 (quantité vendue de pièce)
            {
                text.Text = "";
                text2.Text = "Quantité vendue de pièce";

                //On initialise notre liste de pièces vendues
                List<Reader> ListOfPiece = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT piece.numP,piece.description,SUM(contientp.quantite) AS Pieces_vendues,piece.prixU*SUM(contientp.quantite) " +
                "AS Total FROM commande,contientp,piece WHERE commande.numC = contientp.numC AND piece.numP = contientp.numP GROUP BY numP ORDER BY Total DESC; ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des resultats
                while (reader.Read())
                {
                    Reader new_piece = new Reader(); //Initialisation d'une pièce à chaque ligne

                    //Lecture des colonnes
                    string numP = (string)reader["numP"];
                    int revenu = reader.GetInt32(3);
                    string description = (string)reader["description"];
                    int quantite = Convert.ToInt32(reader["Pieces_vendues"]);

                    //Attribution des valeurs des colonnes au reader
                    new_piece.string1 = numP; ;
                    new_piece.int1 = quantite;
                    new_piece.string2 = description;
                    new_piece.int2 = revenu;
                    ListOfPiece.Add(new_piece);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfPiece;

                //Creation des colonnes de la listview
                GridViewColumn NumP = new GridViewColumn();
                NumP.Width = 120;
                NumP.DisplayMemberBinding = new Binding("string1");
                NumP.Header = "Numero produit";

                GridViewColumn Description = new GridViewColumn();
                Description.Width = 120;
                Description.DisplayMemberBinding = new Binding("string2");
                Description.Header = "Description";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 120;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantité vendue";

                GridViewColumn Revenu = new GridViewColumn();
                Revenu.Width = 90;
                Revenu.DisplayMemberBinding = new Binding("int2");
                Revenu.Header = "Revenu";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(NumP);
                maingrid.Columns.Add(Description);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Revenu);
                datagrid.View = maingrid;
            }
            else if (int_quantitevendu == 2) //Page 2 (quantité vendue de vélo)
            {
                int_quantitevendu = 0;
                text.Text = "";
                text2.Text = "Quantité vendue de vélo";

                //On initialise notre liste de vélo
                List<Reader> ListOfVelo = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nom,SUM(contientv.quantite),velo.prix*SUM(contientv.quantite) FROM velo,contientv WHERE" +
                    " velo.numV=contientv.numV GROUP BY nom ORDER BY velo.prix*SUM(contientv.quantite) DESC; ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader new_velo = new Reader(); //On initialise un velo a chaque ligne

                    //Lecture des colonnes
                    string nom = (string)reader["nom"];
                    int quantite = reader.GetInt32(1);
                    int revenu = reader.GetInt32(2);

                    //Attribution des valeurs des colonnes au reader
                    new_velo.string1 = nom;
                    new_velo.int1 = quantite;
                    new_velo.int2 = revenu;
                    ListOfVelo.Add(new_velo);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfVelo;

                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 120;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 120;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantite vendue";

                GridViewColumn Revenu = new GridViewColumn();
                Revenu.Width = 100;
                Revenu.DisplayMemberBinding = new Binding("int2");
                Revenu.Header = "Revenu";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Revenu);
                datagrid.View = maingrid;
            }                    
        }
        #endregion

        //-------------------------------------------------------------Affichage des clients en fonction de leur programme de fidélité

        #region Analyse programme
        public void btn_programme (object sender, RoutedEventArgs e)
        {
            Clear_datagrid();
            int_programme++;

            //On initialise notre liste de client en fonction du programme
            List<Reader> ListOfClient = new List<Reader>();

            //Définition de la requête
            string requete = "";
            if (int_programme == 1) //Programme classique
            {
                text.Text = "";
                text2.Text = "Programme classique";
                requete = "SELECT prenom,nomPa,programme.description FROM particulier,programme WHERE programme.numProg = particulier.numProg AND programme.numProg=0;";
            }
            else if (int_programme == 2) //Programme Fidelio
            {
                text.Text = "";
                text2.Text = "Programme Fidelio";
                requete = "SELECT prenom,nomPa,programme.description FROM particulier,programme WHERE programme.numProg = particulier.numProg AND programme.numProg=1;";
            }
            else if (int_programme == 3) //Programme Fidelio Or
            {
                text.Text = "";
                text2.Text = "Programme Fidelio Or";
                requete = "SELECT prenom,nomPa,programme.description FROM particulier,programme WHERE programme.numProg = particulier.numProg AND programme.numProg=2;";
            }
            else if (int_programme == 4) //Programme Fidelio Platine
            {
                text.Text = "";
                text2.Text = "Programme Fidelio Platine";
                requete = "SELECT prenom,nomPa,programme.description FROM particulier,programme WHERE programme.numProg = particulier.numProg AND programme.numProg=3;";
            }
            else if (int_programme == 5) //Programme Fidelio Max
            {
                text.Text = "";
                text2.Text = "Programme Fidelio Max";
                int_programme = 0;
                requete = "SELECT prenom,nomPa,programme.description FROM particulier,programme WHERE programme.numProg = particulier.numProg AND programme.numProg=4;";
            }

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                Reader newC = new Reader(); //On initialise un client a chaque ligne

                //Lecture des colonnes
                string nom = (string)reader["nomPa"];
                string prenom = (string)reader["prenom"];
                string description = (string)reader["description"];

                //Attribution des valeurs des colonnes au reader
                newC.string1 = nom;
                newC.string2 = prenom;
                newC.string3 = description;
                ListOfClient.Add(newC);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste de piece
            datagrid.ItemsSource = ListOfClient;

            //Creation des colonnes de la listview
            GridViewColumn Nom = new GridViewColumn();
            Nom.Width = 80;
            Nom.DisplayMemberBinding = new Binding("string1");
            Nom.Header = "Nom";

            GridViewColumn Prenom = new GridViewColumn();
            Prenom.Width = 80;
            Prenom.DisplayMemberBinding = new Binding("string2");
            Prenom.Header = "Prenom";

            GridViewColumn Description = new GridViewColumn();
            Description.Width = 100;
            Description.DisplayMemberBinding = new Binding("string3");
            Description.Header = "Description";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(Nom);
            maingrid.Columns.Add(Prenom);
            maingrid.Columns.Add(Description);
            datagrid.View = maingrid;
        }
        #endregion

        //-------------------------------------------------------Affichage des analyses faites sur les clients particuliers

        #region Analyse client
        public void btn_client (object sender, RoutedEventArgs e)
        {
            //Prepartion de l'affichage
            set_page(int_client); //On update les titres des pages suivantes et précédentes en fonction de la page actuelle
            boutonSuivant.Opacity = 100; //Si les boutons étaient devenus invisibles (pas affichés dans certaine partie du module statistique)
            boutonPrecedent.Opacity = 100;
            categorie = "client";
            ////////////////////
            if (int_client == 1) //Revenus générés par les pièces avec les clients
            {
                text.Text = "Revenus générés par les";
                text2.Text = "pièces avec les clients";

                //On initialise notre liste des clients avec les revenus générés par ces derniers
                List<Reader> ListOfClient = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nomPa,prenom,GROUP_CONCAT(piece.numP),GROUP_CONCAT(quantite),GROUP_CONCAT(prixU),SUM(quantite*prixU) " +
                    "FROM commande, particulier, contientp, piece WHERE particulier.codePa = commande.codePa AND " +
                    "commande.numC = contientp.numC AND contientp.numP = piece.numP GROUP BY nomPa ORDER BY SUM(quantite* prixU) DESC; ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newC = new Reader(); //On initialise un client a chaque ligne

                    //Lecture des colonnes
                    string nom = (string)reader["nomPa"];
                    string prenom = (string)reader["prenom"];
                    string piece = (string)reader["GROUP_CONCAT(piece.numP)"];                 
                    string prix = (string)reader["GROUP_CONCAT(prixU)"];
                    string quantite = (string)reader["GROUP_CONCAT(quantite)"];
                    int sum = Convert.ToInt32(reader["SUM(quantite*prixU)"]);


                    //Attribution des valeurs des colonnes au reader
                    newC.string1 = nom;
                    newC.string2 = prenom;
                    newC.string3 = piece;
                    newC.string4 = prix;
                    newC.string5 = quantite;
                    newC.int1 = sum;
                    ListOfClient.Add(newC);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfClient;

                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 80;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Prenom = new GridViewColumn();
                Prenom.Width = 80;
                Prenom.DisplayMemberBinding = new Binding("string2");
                Prenom.Header = "Prenom";

                GridViewColumn Piece = new GridViewColumn();
                Piece.Width = 100;
                Piece.DisplayMemberBinding = new Binding("string3");
                Piece.Header = "Pièce";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 100;
                Quantite.DisplayMemberBinding = new Binding("string5");
                Quantite.Header = "Quantité";

                GridViewColumn Prix = new GridViewColumn();
                Prix.Width = 100;
                Prix.DisplayMemberBinding = new Binding("string4");
                Prix.Header = "Prix";

                GridViewColumn Sum = new GridViewColumn();
                Sum.Width = 100;
                Sum.DisplayMemberBinding = new Binding("int1");
                Sum.Header = "Total (en €)";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Prenom);
                maingrid.Columns.Add(Piece);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Prix);
                maingrid.Columns.Add(Sum);
                datagrid.View = maingrid;
            }
            else if (int_client == 2) //Revenues générés par les vélos avec les clients
            {
                text.Text = "Revenus générés par les";
                text2.Text = "vélos avec les clients";

                //On initialise notre liste de client avec les revenus générés par les vélos avec les clients
                List<Reader> ListOfClient = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nomPa,prenom,GROUP_CONCAT(velo.nom),GROUP_CONCAT(quantite),GROUP_CONCAT(prix),SUM(quantite*prix) " +
                    "FROM commande, particulier, contientv, velo WHERE particulier.codePa = commande.codePa AND " +
                    "commande.numC = contientv.numC AND contientv.numV = velo.numV GROUP BY velo.numV ORDER BY SUM(quantite* prix) DESC; ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des resultats
                while (reader.Read())
                {
                    Reader newC = new Reader(); //On initialise un client à chaque ligne

                    //Lecture des colonnes
                    string nom = (string)reader["nomPa"];
                    string prenom = (string)reader["prenom"];
                    string velo = (string)reader["GROUP_CONCAT(velo.nom)"];
                    string quantite = (string)reader["GROUP_CONCAT(quantite)"];
                    string prix = (string)reader["GROUP_CONCAT(prix)"];
                    int sum = Convert.ToInt32(reader["SUM(quantite*prix)"]);

                    //Attribution des valeurs des colonnes au reader
                    newC.string1 = nom;
                    newC.string2 = prenom;
                    newC.string3 = velo;
                    newC.string4 = quantite;
                    newC.string5 = prix;
                    newC.int3 = sum;
                    ListOfClient.Add(newC);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfClient;

                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 80;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Prenom = new GridViewColumn();
                Prenom.Width = 80;
                Prenom.DisplayMemberBinding = new Binding("string2");
                Prenom.Header = "Prenom";

                GridViewColumn Velo = new GridViewColumn();
                Velo.Width = 100;
                Velo.DisplayMemberBinding = new Binding("string3");
                Velo.Header = "Velo";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 100;
                Quantite.DisplayMemberBinding = new Binding("string4");
                Quantite.Header = "Quantité";

                GridViewColumn Prix = new GridViewColumn();
                Prix.Width = 100;
                Prix.DisplayMemberBinding = new Binding("string5");
                Prix.Header = "Prix";

                GridViewColumn Sum = new GridViewColumn();
                Sum.Width = 100;
                Sum.DisplayMemberBinding = new Binding("int3");
                Sum.Header = "Total(en €)";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Prenom);
                maingrid.Columns.Add(Velo);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Prix);
                maingrid.Columns.Add(Sum);
                datagrid.View = maingrid;
            }
            else if (int_client == 3) //Quantité de pièces achetées avec les clients
            {
                text.Text = "Quantité de pièces achetées";
                text2.Text = "avec les clients";

                //On initialise notre liste de cliens avec la quantité de pièces achetées
                List<Reader> ListOfClient = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nomPa,prenom,GROUP_CONCAT(piece.numP),GROUP_CONCAT(prixU), SUM(quantite) FROM commande, particulier, contientp, piece WHERE" +
                    " particulier.codePa = commande.codePa AND commande.numC = contientp.numC AND contientp.numP = piece.numP GROUP BY nomPa ORDER BY SUM(quantite) DESC; ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newC = new Reader(); //On initialise un client à chaque ligne

                    //Lecture des colonnes 
                    string nom = (string)reader["nomPa"];
                    string prenom = (string)reader["prenom"];
                    string piece = (string)reader["GROUP_CONCAT(piece.numP)"];
                    int quantite = Convert.ToInt32(reader["SUM(quantite)"]);
                    string prix = (string)reader["GROUP_CONCAT(prixU)"];

                    //Attribution des valeurs des colonnes au reader
                    newC.string1 = nom;
                    newC.string2 = prenom;
                    newC.string3 = piece;
                    newC.int1 = quantite;
                    newC.string4 = prix;
                    ListOfClient.Add(newC);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfClient;

                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 80;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Prenom = new GridViewColumn();
                Prenom.Width = 80;
                Prenom.DisplayMemberBinding = new Binding("string2");
                Prenom.Header = "Prenom";

                GridViewColumn Piece = new GridViewColumn();
                Piece.Width = 100;
                Piece.DisplayMemberBinding = new Binding("string3");
                Piece.Header = "Pièce";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 100;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantité";

                GridViewColumn Prix = new GridViewColumn();
                Prix.Width = 100;
                Prix.DisplayMemberBinding = new Binding("string4");
                Prix.Header = "Prix";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Prenom);
                maingrid.Columns.Add(Piece);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Prix);
                datagrid.View = maingrid;
            }
            else if (int_client == 4) //Quantité de vélos achetés avec les clients
            {
                text.Text = "Quantités de vélos achetés";
                text2.Text = "avec les clients";

                //On initialise notre liste de client ave les quantités de vélos achetés
                List<Reader> ListOfClient = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nomPa,prenom,GROUP_CONCAT(velo.nom),GROUP_CONCAT(prix), SUM(quantite) FROM commande, particulier, contientv, velo WHERE " +
                    "particulier.codePa = commande.codePa AND commande.numC = contientv.numC AND contientv.numV = velo.numV GROUP BY nomPa ORDER BY SUM(quantite) DESC; ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newC = new Reader(); //On initialise un client à chaque ligne

                    //Lecture des colonnes
                    string nom = (string)reader["nomPa"];
                    string prenom = (string)reader["prenom"];
                    string velo = (string)reader["GROUP_CONCAT(velo.nom)"];
                    int quantite = Convert.ToInt32(reader["SUM(quantite)"]);
                    string prix = (string)reader["GROUP_CONCAT(prix)"];

                    //Attribution des valeurs des colonnes au reader
                    newC.string1 = nom;
                    newC.string2 = prenom;
                    newC.string3 = velo;
                    newC.int1 = quantite;
                    newC.string4 = prix;
                    ListOfClient.Add(newC);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfClient;

                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 80;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Prenom = new GridViewColumn();
                Prenom.Width = 80;
                Prenom.DisplayMemberBinding = new Binding("string2");
                Prenom.Header = "Prenom";

                GridViewColumn Velo = new GridViewColumn();
                Velo.Width = 150;
                Velo.DisplayMemberBinding = new Binding("string3");
                Velo.Header = "Velo";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 100;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantité";

                GridViewColumn Prix = new GridViewColumn();
                Prix.Width = 100;
                Prix.DisplayMemberBinding = new Binding("string4");
                Prix.Header = "Prix";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Prenom);
                maingrid.Columns.Add(Velo);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Prix);
                datagrid.View = maingrid;
            }
            else if (int_client == 5) //Revenues générés avec les clients en général
            {
                text.Text = "Revenus générés";
                text2.Text = "avec les clients";
                List<string[]> result = new List<string[]>();
                MySqlConnection maConnexion = Utilities.Connexion();
                //On récupère les commandes de pièces 
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nomPa,prenom,SUM(quantite*prixU) " +
                    "FROM commande, particulier, contientp, piece WHERE particulier.codePa = commande.codePa AND " +
                    "commande.numC = contientp.numC AND contientp.numP = piece.numP GROUP BY nomPa ORDER BY SUM(quantite* prixU) DESC; ";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string[] particulier = new string[3];
                    for (int i = 0; i <= particulier.Length - 1; i++)
                    {
                        particulier[i] = reader.GetString(i);
                    }
                    result.Add(particulier);
                }
                reader.Close();
                command.Dispose();
                //On récupère les commandes de vélo
                MySqlConnection maConnexion2 = Utilities.Connexion();
                MySqlCommand command2 = maConnexion2.CreateCommand();
                command2.CommandText = "SELECT nomPa,prenom,SUM(quantite*prix) " +
                    "FROM commande, particulier, contientv, velo WHERE particulier.codePa = commande.codePa AND " +
                    "commande.numC = contientv.numC AND contientv.numV = velo.numV GROUP BY velo.numV ORDER BY SUM(quantite* prix) DESC; ";
                MySqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    string[] particulier = new string[3];
                    for (int i = 0; i <= particulier.Length - 1; i++)
                    {
                        particulier[i] = reader2.GetString(i);
                    }
                    result.Add(particulier);
                }
                reader2.Close();
                command2.Dispose();
                Dictionary<int, string> clientMontant = new Dictionary<int, string>();
                //On récupère les noms de chaque particuliers
                string[] nom = new string[result.Count];
                for (int i = 0; i <= result.Count - 1; i++)
                {
                    if (nom.Contains(result[i][0]) == false)
                    {
                        nom[i] = result[i][0];
                    }
                }
                //Pour chaque client on lui attribue le montant de l'ensemble de ses commandes
                //On construit notre dictionnaire (montant, client)
                for (int i = 0; i <= result.Count - 1; i++)
                {
                    int total = 0;
                    for (int j = 0; j <= result.Count - 1; j++)
                    {
                        if (result[i][0] == result[j][0] && nom.Contains(result[i][0]) == true)
                        {
                            int commande = Convert.ToInt32(result[j][2]);
                            total = total + commande;
                        }
                    }
                    if (total != 0)
                    {
                        string a = result[i][0] + " " + result[i][1];
                        clientMontant[total] = a;
                    }
                    nom[i] = "done";

                }

                int[] montant = new int[clientMontant.Count];
                //On trie les montants grâce au tri fusion et on affiche les particuliers dans l'ordre
                Dictionary<int, string>.KeyCollection keys = clientMontant.Keys;
                int compteur = 0;
                foreach (int key in keys)
                {
                    montant[compteur] = key;
                    compteur++;
                }
                montant = Utilities.TriFusion(montant, 0, montant.Length - 1);


                List<Reader> ListOfClient = new List<Reader>();
                for (int i = montant.Length - 1; i >= 0; i--)
                {
                    Reader newC = new Reader();
                    string clientmontant_tab = clientMontant[montant[i]];
                    int montant_tab = montant[i];
                    newC.string1 = clientmontant_tab;
                    newC.int1 = montant_tab;
                    ListOfClient.Add(newC);
                    Console.WriteLine(clientMontant[montant[i]] + " " + montant[i] + "euros");                  
                }

                datagrid.ItemsSource = ListOfClient;

                GridViewColumn Client = new GridViewColumn();
                Client.Width = 180;
                Client.DisplayMemberBinding = new Binding("string1");
                Client.Header = "Client";

                GridViewColumn Montant = new GridViewColumn();
                Montant.Width = 100;
                Montant.DisplayMemberBinding = new Binding("int1");
                Montant.Header = "Montant";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Client);
                maingrid.Columns.Add(Montant);
                datagrid.View = maingrid;
            }       
        }
        #endregion

        //---------------------------------------------------------Affichage des analyses faites sur les clients boutiques

        #region Analyse boutique
        public void btn_boutique (object sender, RoutedEventArgs e)
        {
            //Prepartion de l'affichage
            set_page(int_boutique); //On update les titres des pages suivantes et précédentes en fonction de la page actuelle            
            boutonSuivant.Opacity = 100; //Si les boutons étaient devenus invisibles (pas affichés dans certaine partie du module statistique)
            boutonPrecedent.Opacity = 100;
            categorie = "boutique";
            //////////////////////
            if (int_boutique == 1) //Revenus générés par les pièces avec les boutiques
            {
                text.Text = "Revenus générés par les";
                text2.Text = "pièces avec les boutiques";

                //On initialise notre liste de boutique avec les revenus générés par les pièces
                List<Reader> ListOfBoutique = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT boutique.nomB,GROUP_CONCAT(piece.numP),GROUP_CONCAT(quantite),GROUP_CONCAT(prixU),SUM(quantite*prixU) FROM commande, boutique, contientp," +
                    " piece WHERE boutique.nomB = commande.nomB AND commande.numC = contientp.numC AND contientp.numP = piece.numP GROUP BY nomB ORDER BY SUM(quantite* prixU) DESC; ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newB = new Reader(); //On initialise une boutique à chaque ligne

                    //On initialise une boutique à chaque ligne
                    string nom = (string)reader["nomB"];
                    string piece = (string)reader["GROUP_CONCAT(piece.numP)"];
                    string quantite = (string)reader["GROUP_CONCAT(quantite)"];
                    string prix = (string)reader["GROUP_CONCAT(prixU)"];
                    int sum = Convert.ToInt32(reader["SUM(quantite*prixU)"]);

                    //Attribution des valeurs des colonnes au reader
                    newB.string1 = nom;
                    newB.string2 = piece;
                    newB.string3 = quantite;
                    newB.string4 = prix;
                    newB.int1 = sum;
                    ListOfBoutique.Add(newB);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de boutique
                datagrid.ItemsSource = ListOfBoutique;


                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 180;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Piece = new GridViewColumn();
                Piece.Width = 80;
                Piece.DisplayMemberBinding = new Binding("string2");
                Piece.Header = "Piece";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 100;
                Quantite.DisplayMemberBinding = new Binding("string3");
                Quantite.Header = "Quantité";

                GridViewColumn Prix = new GridViewColumn();
                Prix.Width = 100;
                Prix.DisplayMemberBinding = new Binding("string4");
                Prix.Header = "Prix";

                GridViewColumn Sum = new GridViewColumn();
                Sum.Width = 100;
                Sum.DisplayMemberBinding = new Binding("int1");
                Sum.Header = "Total (en €)";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Piece);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Prix);
                maingrid.Columns.Add(Sum);
                datagrid.View = maingrid;
            }
            else if (int_boutique == 2) //Revenus générés par les vélos avec les boutiques
            {
                text.Text = "Revenus générés par les";
                text2.Text = "vélos avec les boutiques";

                //On initialise notre liste de boutique avec les revenus générés par les vélos
                List<Reader> ListOfBoutique = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT boutique.nomB,GROUP_CONCAT(velo.nom),GROUP_CONCAT(quantite),GROUP_CONCAT(prix),SUM(quantite*prix) FROM commande, boutique, contientv, " +
                    "velo WHERE boutique.nomB = commande.nomB AND commande.numC = contientv.numC AND contientv.numv = velo.numV GROUP BY nomB ORDER BY SUM(quantite* prix) DESC; ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newB = new Reader(); //On initialise une boutique à chaque ligne

                    //Lecture des colonnes
                    string nom = (string)reader["nomB"];
                    string velo = (string)reader["GROUP_CONCAT(velo.nom)"];
                    string quantite = (string)reader["GROUP_CONCAT(quantite)"];
                    string prix = (string)reader["GROUP_CONCAT(prix)"];
                    int sum = Convert.ToInt32(reader["SUM(quantite*prix)"]);

                    //Attribution des valeurs des colonnes au reader
                    newB.string1 = nom;
                    newB.string2 = velo;
                    newB.string3 = quantite;
                    newB.string4 = prix;
                    newB.int1 = sum;
                    ListOfBoutique.Add(newB);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste des boutiques
                datagrid.ItemsSource = ListOfBoutique;

                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 180;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Velo = new GridViewColumn();
                Velo.Width = 180;
                Velo.DisplayMemberBinding = new Binding("string2");
                Velo.Header = "Velo";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 100;
                Quantite.DisplayMemberBinding = new Binding("string3");
                Quantite.Header = "Quantité";

                GridViewColumn Prix = new GridViewColumn();
                Prix.Width = 100;
                Prix.DisplayMemberBinding = new Binding("string4");
                Prix.Header = "Prix";

                GridViewColumn Sum = new GridViewColumn();
                Sum.Width = 100;
                Sum.DisplayMemberBinding = new Binding("int1");
                Sum.Header = "Total (en €)";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Velo);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Prix);
                maingrid.Columns.Add(Sum);
                datagrid.View = maingrid;
            }
            else if (int_boutique == 3) //Quantité de pièces achetées avec les boutiques
            {
                text.Text = "Quantité de pièces achetées";
                text2.Text = "avec les boutiques";

                //On initialise notre liste de boutique avec la quantité de pièces achetées
                List<Reader> ListOfBoutique = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT boutique.nomB,GROUP_CONCAT(piece.numP),GROUP_CONCAT(prixU), SUM(quantite) FROM commande, boutique, contientp, piece WHERE " +
                    "boutique.nomB = commande.nomB AND commande.numC = contientp.numC AND contientp.numP = piece.numP GROUP BY nomB ORDER BY SUM(quantite) DESC ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newB = new Reader(); //On initialise une boutique à chaque ligne
                    string nom = (string)reader["nomB"];
                    string piece = (string)reader["GROUP_CONCAT(piece.numP)"];
                    int quantite = Convert.ToInt32(reader["SUM(quantite)"]);
                    string prix = (string)reader["GROUP_CONCAT(prixU)"];


                    //Attribution des valeurs des colonnes au reader
                    newB.string1 = nom;
                    newB.string2 = piece;
                    newB.string3 = prix;
                    newB.int1 = quantite;
                    ListOfBoutique.Add(newB);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfBoutique;

                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 180;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Piece = new GridViewColumn();
                Piece.Width = 80;
                Piece.DisplayMemberBinding = new Binding("string2");
                Piece.Header = "Piece";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 100;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantite";

                GridViewColumn Prix = new GridViewColumn();
                Prix.Width = 100;
                Prix.DisplayMemberBinding = new Binding("string3");
                Prix.Header = "Prix";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Piece);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Prix);
                datagrid.View = maingrid;
            }
            else if (int_boutique == 4) //Quantités de vélos achetés avec les boutiques
            {
                text.Text = "Quantités de vélos achetés";
                text2.Text = "avec les boutiques";

                //On initialise notre liste de boutique avec les quantités d evélos achetés avec les boutiques
                List<Reader> ListOfBoutique = new List<Reader>();

                //Connexion, creation de la commande et définition de la requête
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT boutique.nomB,GROUP_CONCAT(velo.nom),GROUP_CONCAT(prix),SUM(quantite) FROM commande, boutique, contientv, velo WHERE " +
                    "boutique.nomB = commande.nomB AND commande.numC = contientv.numC AND contientv.numv = velo.numV GROUP BY nomB ORDER BY SUM(quantite) DESC; ";

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newB = new Reader();//On initialise une boutique à chaque ligne

                    //Lecture des colonnes
                    string nom = (string)reader["nomB"];
                    string velo = (string)reader["GROUP_CONCAT(velo.nom)"];
                    int quantite = Convert.ToInt32(reader["SUM(quantite)"]);
                    string prix = (string)reader["GROUP_CONCAT(prix)"];

                    //Attribution des valeurs des colonnes au reader
                    newB.string1 = nom;
                    newB.string2 = velo;
                    newB.int1 = quantite;
                    newB.string3 = prix;
                    ListOfBoutique.Add(newB);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de boutique
                datagrid.ItemsSource = ListOfBoutique;

                //Creation des colonnes de la listview
                GridViewColumn Nom = new GridViewColumn();
                Nom.Width = 180;
                Nom.DisplayMemberBinding = new Binding("string1");
                Nom.Header = "Nom";

                GridViewColumn Velo = new GridViewColumn();
                Velo.Width = 180;
                Velo.DisplayMemberBinding = new Binding("string2");
                Velo.Header = "Velo";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 100;
                Quantite.DisplayMemberBinding = new Binding("int1");
                Quantite.Header = "Quantité";

                GridViewColumn Prix = new GridViewColumn();
                Prix.Width = 100;
                Prix.DisplayMemberBinding = new Binding("string3");
                Prix.Header = "Prix";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Nom);
                maingrid.Columns.Add(Velo);
                maingrid.Columns.Add(Quantite);
                maingrid.Columns.Add(Prix);
                datagrid.View = maingrid;
            }
            else if (int_boutique == 5) //Revenus généras par les boutiques en général
            {
                text.Text = "Revenus générés";
                text2.Text = "par les boutiques";
                List<string[]> result = new List<string[]>();
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT boutique.nomB, SUM(quantite*prixU) FROM commande, boutique, contientp," +
                    " piece WHERE boutique.nomB = commande.nomB AND commande.numC = contientp.numC AND contientp.numP = piece.numP GROUP BY nomB ORDER BY SUM(quantite* prixU) DESC; ";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string[] boutique = new string[2];
                    for (int i = 0; i <= boutique.Length - 1; i++)
                    {
                        boutique[i] = reader.GetString(i);
                    }
                    result.Add(boutique);
                }
                reader.Close();
                command.Dispose();
                MySqlConnection maConnexion2 = Utilities.Connexion();
                MySqlCommand command2 = maConnexion2.CreateCommand();
                command2.CommandText = "SELECT boutique.nomB,SUM(quantite*prix) FROM commande, boutique, contientv, " +
                    "velo WHERE boutique.nomB = commande.nomB AND commande.numC = contientv.numC AND contientv.numv = velo.numV GROUP BY nomB ORDER BY SUM(quantite* prix) DESC;  ";
                MySqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    string[] boutique = new string[2];
                    for (int i = 0; i <= boutique.Length - 1; i++)
                    {
                        boutique[i] = reader2.GetString(i);
                    }
                    result.Add(boutique);
                }
                reader2.Close();
                command2.Dispose();
                Dictionary<int, string> boutiqueMontant = new Dictionary<int, string>();
                string[] nomBoutique = new string[result.Count];
                for (int i = 0; i <= result.Count - 1; i++)
                {
                    if (nomBoutique.Contains(result[i][0]) == false)
                    {
                        nomBoutique[i] = result[i][0];
                    }

                }
                for (int i = 0; i <= result.Count - 1; i++)
                {
                    int total = 0;
                    for (int j = 0; j <= result.Count - 1; j++)
                    {
                        if (result[i][0] == result[j][0] && nomBoutique.Contains(result[i][0]) == true)
                        {
                            int commande = Convert.ToInt32(result[j][1]);
                            total = total + commande;
                        }
                    }
                    if (total != 0)
                    {
                        string a = result[i][0];
                        boutiqueMontant[total] = a;
                    }
                    nomBoutique[i] = "done";

                }
                int[] montant = new int[boutiqueMontant.Count];
                Dictionary<int, string>.KeyCollection keys = boutiqueMontant.Keys;
                int compteur = 0;
                foreach (int key in keys)
                {
                    montant[compteur] = key;
                    compteur++;
                }
                montant = Utilities.TriFusion(montant, 0, montant.Length - 1);

                List<Reader> ListOfClient = new List<Reader>();
                for (int i = montant.Length - 1; i >= 0; i--)
                {
                    Reader newC = new Reader();
                    string clientmontant_tab = boutiqueMontant[montant[i]];
                    int montant_tab = montant[i];
                    newC.string1 = clientmontant_tab;
                    newC.int1 = montant_tab;
                    ListOfClient.Add(newC);
                    Console.WriteLine(boutiqueMontant[montant[i]] + " " + montant[i] + "euros");
                }

                datagrid.ItemsSource = ListOfClient;

                GridViewColumn Boutique = new GridViewColumn();
                Boutique.Width = 200;
                Boutique.DisplayMemberBinding = new Binding("string1");
                Boutique.Header = "Client";

                GridViewColumn Montant = new GridViewColumn();
                Montant.Width = 100;
                Montant.DisplayMemberBinding = new Binding("int1");
                Montant.Header = "Montant";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Boutique);
                maingrid.Columns.Add(Montant);
                datagrid.View = maingrid;
            }
        }
        #endregion

        //--------------------------------------------------------Affichage des analyses faites sur les commandes

        #region Analyse commande
        public void btn_commande (object sender, RoutedEventArgs e)
        {
            //Prepartion de l'affichage
            set_page_commande(int_commande); //On update les titres des pages suivantes et précédentes en fonction de la page actuelle                 
            boutonSuivant.Opacity = 100; //Si les boutons étaient devenus invisibles (pas affichés dans certaine partie du module statistique)
            boutonPrecedent.Opacity = 100;
            categorie = "commande";
            //////////////////////
            if (int_commande == 1) //Moyenne du nombre de pièces
            {
                text.Text = "Moyenne du";
                text2.Text = "nombre de pièces";

                float moyennePa = 0;
                float nbCommandePa = 0;
                float moyenneB = 0;
                float nbCommandeB = 0;
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT  AVG(quantite),COUNT(*) FROM commande, particulier, contientp, piece WHERE particulier.codePa = commande.codePa AND " +
                    "commande.numC = contientp.numC AND contientp.numP = piece.numP; ";

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    moyennePa = reader.GetFloat(0);
                    nbCommandePa = reader.GetFloat(1);
                }
                reader.Close();
                command.Dispose();
                MySqlConnection maConnexion2 = Utilities.Connexion();
                MySqlCommand command2 = maConnexion2.CreateCommand();
                command2.CommandText = "SELECT AVG(quantite),COUNT(*) FROM commande, boutique, contientp, piece WHERE boutique.nomB = commande.nomB AND " +
                    "commande.numC = contientp.numC AND contientp.numP = piece.numP ";
                MySqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    moyenneB = reader2.GetFloat(0);
                    nbCommandeB = reader2.GetFloat(1);
                }
                reader2.Close();
                command2.Dispose();

                List<Reader> Classe = new List<Reader>();

                string particulier = "Particulier";
                float moyenneP = moyennePa;
                Reader MP = new Reader();
                MP.string1 = particulier;
                MP.float1 = moyenneP;
                Classe.Add(MP);

                string boutique = "Boutique";
                float moyenneBoutique = moyenneB;
                Reader MB = new Reader();
                MB.string1 = boutique;
                MB.float1 = moyenneBoutique;
                Classe.Add(MB);

                string Mtotal = "Totale";
                float moyenne = (moyennePa * nbCommandePa + moyenneB * nbCommandeB) / (nbCommandeB + nbCommandePa);
                Reader MT = new Reader();
                MT.string1 = Mtotal;
                MT.float1 = moyenne;
                Classe.Add(MT);

                datagrid.ItemsSource = Classe;

                GridViewColumn Moyenne_titre = new GridViewColumn();
                Moyenne_titre.Width = 200;
                Moyenne_titre.DisplayMemberBinding = new Binding("string1");
                Moyenne_titre.Header = "Moyenne";

                GridViewColumn Moyenne = new GridViewColumn();
                Moyenne.Width = 100;
                Moyenne.DisplayMemberBinding = new Binding("float1");
                Moyenne.Header = "Valeur";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Moyenne_titre);
                maingrid.Columns.Add(Moyenne);
                datagrid.View = maingrid;

                Console.WriteLine("Moyenne du nombre de pièces achetées");
                Console.WriteLine("Moyenne Particulier : " + moyennePa + " pieces");
                Console.WriteLine("Moyenne Boutique : " + moyenneB + " pieces");
                Console.WriteLine("Moyenne totale sur l'ensemble des commandes : " + (moyennePa * nbCommandePa + moyenneB * nbCommandeB) / (nbCommandeB + nbCommandePa) + " pieces");
            }
            else if (int_commande == 2) //Moyenne du nombre de vélos
            {
                text.Text = "Moyenne du";
                text2.Text = "nombre de vélos";

                float moyennePa = 0;
                float nbCommandePa = 0;
                float moyenneB = 0;
                float nbCommandeB = 0;
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT AVG(quantite),COUNT(*) FROM commande, particulier, contientv, velo WHERE particulier.codePa = commande.codePa AND " +
                    "commande.numC = contientv.numC AND contientv.numV = velo.numV; ";

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    moyennePa = reader.GetFloat(0);
                    nbCommandePa = reader.GetFloat(1);
                }
                reader.Close();
                command.Dispose();
                MySqlConnection maConnexion2 = Utilities.Connexion();
                MySqlCommand command2 = maConnexion2.CreateCommand();
                command2.CommandText = "SELECT AVG(quantite),COUNT(*) FROM commande, boutique, contientv, velo WHERE boutique.nomB = commande.nomB AND " +
                    "commande.numC = contientv.numC AND contientv.numv = velo.numV; ";
                MySqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    moyenneB = reader2.GetFloat(0);
                    nbCommandeB = reader2.GetFloat(1);
                }
                reader2.Close();
                command2.Dispose();

                List<Reader> Classe = new List<Reader>();

                string particulier = "Particulier";
                float moyenneP = moyennePa;
                Reader MP = new Reader();
                MP.string1 = particulier;
                MP.float1 = moyenneP;
                Classe.Add(MP);

                string boutique = "Boutique";
                float moyenneBoutique = moyenneB;
                Reader MB = new Reader();
                MB.string1 = boutique;
                MB.float1 = moyenneBoutique;
                Classe.Add(MB);

                string Mtotal = "Totale";
                float moyenne = (moyennePa * nbCommandePa + moyenneB * nbCommandeB) / (nbCommandeB + nbCommandePa);
                Reader MT = new Reader();
                MT.string1 = Mtotal;
                MT.float1 = moyenne;
                Classe.Add(MT);

                datagrid.ItemsSource = Classe;

                GridViewColumn Moyenne_titre = new GridViewColumn();
                Moyenne_titre.Width = 200;
                Moyenne_titre.DisplayMemberBinding = new Binding("string1");
                Moyenne_titre.Header = "Moyenne";

                GridViewColumn Moyenne = new GridViewColumn();
                Moyenne.Width = 100;
                Moyenne.DisplayMemberBinding = new Binding("float1");
                Moyenne.Header = "Valeur";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Moyenne_titre);
                maingrid.Columns.Add(Moyenne);
                datagrid.View = maingrid;

                Console.WriteLine("Moyenne du nombre de vélos achetées");
                Console.WriteLine("Moyenne Particulier : " + moyennePa + " vélos");
                Console.WriteLine("Moyenne Boutique : " + moyenneB + " vélos");
                Console.WriteLine("Moyenne totale sur l'ensemble des commandes : " + (moyennePa * nbCommandePa + moyenneB * nbCommandeB) / (nbCommandeB + nbCommandePa) + " vélos");
            }
            else if (int_commande == 3) //Moynne du nombre de commandes
            {
                text.Text = "Moyenne du";
                text2.Text = "nombre de commandes";

                List<int> indexCommande = Utilities.getListCommande();
                int total = 0;
                for (int i = 0; i <= indexCommande.Count - 1; i++)
                {
                    total = total + Utilities.prixCommande(indexCommande[i], indexCommande);
                }

                List<Reader> Classe = new List<Reader>();

                string benef = "Benefice Total";
                int benef_total = total;
                Reader Benefice = new Reader();
                Benefice.string1 = benef;
                Benefice.int1 = benef_total;
                Classe.Add(Benefice);

                string nbr = "Nombre de commandes";
                int count = indexCommande.Count;
                Reader NombreC = new Reader();
                NombreC.string1 = nbr;
                NombreC.int1 = indexCommande.Count;
                Classe.Add(NombreC);

                string MoyenneCommande = "Moyenne du prix d'une commande";
                int moyenne = (total / indexCommande.Count);
                Reader MoyennePrixCommande = new Reader();
                MoyennePrixCommande.string1 = MoyenneCommande;
                MoyennePrixCommande.int1 = moyenne;
                Classe.Add(MoyennePrixCommande);

                datagrid.ItemsSource = Classe;

                GridViewColumn Moyenne_titre = new GridViewColumn();
                Moyenne_titre.Width = 230;
                Moyenne_titre.DisplayMemberBinding = new Binding("string1");
                Moyenne_titre.Header = "Moyenne";

                GridViewColumn Moyenne = new GridViewColumn();
                Moyenne.Width = 100;
                Moyenne.DisplayMemberBinding = new Binding("int1");
                Moyenne.Header = "Valeur";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(Moyenne_titre);
                maingrid.Columns.Add(Moyenne);
                datagrid.View = maingrid;

                Console.WriteLine("Benefice total : " + total + " euros");
                Console.WriteLine("Nombre de commandes : " + indexCommande.Count);
                Console.WriteLine("Moyenne du prix d'une commande " + (total / indexCommande.Count) + " euros");
            }
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // FONCTIONS
        //==================================================================================================================================================================================================================================================

        //-----------------------------Définir les titres des pages suivantes et précédentes pour les analyses sur les clients et boutiques (en dessous des boutons)

        #region Titre des pages suivantes/précédentes des pages client et boutique
        private void set_page (int now)
        {
            if (now == 1)
            {
                text_precedent.Text = "Revenus générés total";
                text_suivant.Text = "Revenus générés par les vélos";
            }
            else if (now == 2)
            {
                text_precedent.Text = "Revenus générés par les pièces";
                text_suivant.Text = "Quantité de pièces achetées";
            }
            else if (now == 3)
            {
                text_precedent.Text = "Revenus générés par les vélos";
                text_suivant.Text = "Quantité de vélos achetés";
            }
            else if (now == 4)
            {
                text_precedent.Text = "Quantité de pièces achetées";
                text_suivant.Text = "Revenus générés total";
            }
            else if (now == 5)
            {
                text_precedent.Text = "Quantité de vélos achetés";
                text_suivant.Text = "Revenus générés par les pièces";
            }
        }
        #endregion

        //--------------------------------------Définir les titres des pages suivantes et précédentes pour les analyse sur les commandes (en dessous des boutons)
        //(les titres ne sont pas les mêmes pour les analyses des commandes)

        #region Titre des pages suivantes/précédentes de la page commande
        private void set_page_commande (int now)
        {
            if (now == 1)
            {
                text_precedent.Text = "Moyenne du prix des commandes";
                text_suivant.Text = "Moyenne du nombre de vélos";
            }
            else if (now == 2)
            {
                text_precedent.Text = "Moyenne du nombre de pièces";
                text_suivant.Text = "Moyenne du prix des commandes";
            }
            else if (now == 3)
            {
                text_precedent.Text = "Moyenne du nombre de vélos";
                text_suivant.Text = "Moyenne du nombre de pièces";
            }
        }
        #endregion

        //---------------------------Réinitialiser la listview et les titres

        #region Réinitialiser
        private void Clear_datagrid()
        {
            text_precedent.Text = "";
            text_suivant.Text = "";
            boutonPrecedent.Opacity = 0;
            boutonSuivant.Opacity = 0;
            text.Text = "";
            text2.Text = "";
            GridView maingrid = new GridView();
            datagrid.View = maingrid;
            datagrid.ClearValue(ItemsControl.ItemsSourceProperty);
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // BOUTONS
        //==================================================================================================================================================================================================================================================

        //------------------------------------------------------Fermer la fenetre

        #region Bouton quitter
        public void btn_quitter(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        //------------------------------------------------------Accéder à la page suivante

        #region Bouton suivant
        public void btn_suivant(object sender, RoutedEventArgs e)
        {
            if (categorie == "client")
            {
                int_client++; //On augmente l'index
                if (int_client == 6) //Pour les analyses des clients il n'y a que 5 pages
                {
                    int_client = 1; //On retourne à la première page
                }
                set_page(int_client); //Update des titres des boutons "pages suivantes et précédentes"
                btn_client(sender, e); //Actualisation de l'affichage d'une des pages client
            }
            else if (categorie == "boutique")
            {
                int_boutique++; //On augmente l'index
                if (int_boutique == 6) //Pour les analyses des boutiques il n'y a que 5 pages
                {
                    int_boutique = 1; //On retourne à la première page
                }
                set_page(int_boutique); //Update des titres des boutons "pages suivantes et précédentes"
                btn_boutique(sender, e); //Actualisation de l'affichage d'une des pages boutique
            }
            else if (categorie == "commande")
            {
                int_commande++; //On augmente l'index
                if (int_commande == 4) //Pour les analyses des commandes il n'y a que 3 pages
                {
                    int_commande = 1; //On retourne à la première page
                }
                set_page_commande(int_commande); //Update des titres des boutons "pages suivantes et précédentes"
                btn_commande(sender, e); //Actualisation de l'affichage d'une des pages commande
            }
        }
        #endregion

        //------------------------------------------------------Accéder à la page précédente

        #region Bouton précédent
        public void btn_precedent(object sender, RoutedEventArgs e)
        {
            if (categorie == "client")
            {
                int_client--; //On diminue l'index
                if (int_client == 0) //Pour les analyses des clients il n'y a que 5 pages
                {
                    int_client = 5; //On retourne à la dernière page
                }
                set_page(int_client); //Update des titres des boutons "pages suivantes et précédentes"
                btn_client(sender, e); //Actualisation de l'affichage d'une des pages client
            }
            else if (categorie == "boutique")
            {
                int_boutique--; //On diminue l'index
                if (int_boutique == 0) //Pour les analyses des boutiques il n'y a que 5 pages
                { 
                    int_boutique = 5; //On retourne à la dernière page
                }
                set_page(int_boutique); //Update des titres des boutons "pages suivantes et précédentes"
                btn_boutique(sender, e); //Actualisation de l'affichage d'une des pages boutique
            }
            else if (categorie == "commande")
            {
                int_commande--; //On diminue l'index
                if (int_commande == 0) //Pour les analyses des commandes il n'y a que 3 pages
                {
                    int_commande = 3; //On retourne à la dernière page
                }
                set_page_commande(int_commande); //Update des titres des boutons "pages suivantes et précédentes"
                btn_commande(sender, e); //Actualisation de l'affichage d'une des pages commandes
            } 
        }
        #endregion
    }
}
