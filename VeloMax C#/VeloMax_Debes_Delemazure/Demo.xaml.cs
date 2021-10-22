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
using System.Data;
using Newtonsoft.Json;

namespace VeloMax_Debes_Delemazure
{
    /// <summary>
    /// Logique d'interaction pour Demo.xaml
    /// </summary>
    public partial class Demo : Window
    {
        //==================================================================================================================================================================================================================================================
        // INITIALISATION
        //==================================================================================================================================================================================================================================================

        #region Initialisation
        public Demo()
        {
            InitializeComponent();
            text_demo.Text = "Appuyer sur suivant";
            text_demo2.Text = "pour commencer";
        }

        public int i = 1; //int qui va permettre de changer de page en fonction de sa valeur que l'on va incrémenter et décrementer
        #endregion

        //==================================================================================================================================================================================================================================================
        // BOUTONS
        //==================================================================================================================================================================================================================================================

        //--------------------------------------------------------Passer à la page suivante

        #region Bouton suivant
        public void btn_suivant (object sender, RoutedEventArgs e)
        {        
            if (i == 1) //Page 1 (affichage du nombre de client particulier et client boutique
            {
                Clear_datagrid();

                //Initialisation du nombre de client
                int nbr_client = 0;

                //Definition de la requete
                string requete = "SELECT count(*) FROM particulier;";

                //Connexion et creation de la commande Mysql
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = requete;

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    nbr_client = Convert.ToInt32(reader["count(*)"]);
                }
                reader.Close();
                command.Dispose();
                text_demo.Text = "Nombre de particuliers : " + Convert.ToString(nbr_client);

                /////////////////////

                //Initialisation du nombre de boutique
                int nbr_boutique = 0;

                //Definition de la requete
                string requete2 = "SELECT count(*) FROM boutique;";

                //Connexion et creation de la commande Mysql
                MySqlConnection maConnexion2 = Utilities.Connexion();
                MySqlCommand command2 = maConnexion2.CreateCommand();
                command2.CommandText = requete2;

                //Execution de la commande
                MySqlDataReader reader2 = command2.ExecuteReader();

                //Lecture des résultats
                while (reader2.Read())
                {
                    nbr_boutique = Convert.ToInt32(reader2["count(*)"]);
                }
                reader2.Close();
                command2.Dispose();
                text_demo2.Text = "Nombre de boutiques : " + Convert.ToString(nbr_boutique);

            }
            else if (i == 2) //Page 2 (affichage du noms des clients avec le cumul de toutes ses commandes en euros)
            {
                Clear_datagrid();
                text_demo.Text = "Noms des clients avec le cumul";
                text_demo2.Text = "de toutes ses commandes en euros";

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
            else if (i == 3) //Page 3 (liste des produits ayant une quantité en stock <=2
            {
                Clear_datagrid();
                text_demo.Text = "Liste des produits ayant ";
                text_demo2.Text = "une quantité en stock <= 2";

                //Initialisation
                List<Piece> ListOfPiece = new List<Piece>(); //On initialise notre liste de pièce

                //Definition de la requete
                string requete = "SELECT numP,quantiteP FROM velomax.livre where quantiteP <= 2;";

                //Connexion et creation de la commande Mysql
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = requete;

                //Execution de la commande 
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Piece new_piece = new Piece(); //Création d'une nouvelle pièce par ligne

                    //Lecture des colonnes
                    string numP = (string)reader["numP"];
                    int quantite = Convert.ToInt32(reader["quantiteP"]);

                    //Attribution des valeurs des colonnes à la piece
                    new_piece.NumP = numP; ;
                    new_piece.Quantite = quantite;
                    ListOfPiece.Add(new_piece);
                }
                reader.Close();
                command.Dispose();

                //Association de la listview a la liste de piece
                datagrid.ItemsSource = ListOfPiece;

                //Creation des colonnes de la listview
                GridViewColumn NumP = new GridViewColumn();
                NumP.Width = 120;
                NumP.DisplayMemberBinding = new Binding("NumP");
                NumP.Header = "Numero produit";

                GridViewColumn Quantite = new GridViewColumn();
                Quantite.Width = 170;
                Quantite.DisplayMemberBinding = new Binding("Quantite");
                Quantite.Header = "Quantité en stock";

                GridView maingrid = new GridView();
                maingrid.Columns.Add(NumP);
                maingrid.Columns.Add(Quantite);
                datagrid.View = maingrid;
            }
            else if (i == 4) //Page 4 (affichage du nombres de pieces fournis par fournisseur)
            {
                Clear_datagrid();
                text_demo.Text = "Nombres de pièces et/ou";
                text_demo2.Text = "vélos fournis par fournisseur";

                //Initialisation
                List<Reader> ListOfPiece = new List<Reader>(); //On initialise notre liste de piece

                ////Definition de la requete, Connexion et creation de la commande Mysql
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nomf,GROUP_CONCAT(DISTINCT numP) FROM livre,fournisseur WHERE fournisseur.siret=livre.siret GROUP BY nomf;";


                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    Reader newS = new Reader(); //Creation d'un reader pour lire les valeurs

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

                //Association de la listview a la liste de piece
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
            else if (i==5) //Page 5 creation des fichiers JSON et XML
            {
                Clear_datagrid();
                text_demo.Text = "Fichier XML et JSON crées";
                text_demo2.Text = "Regardez dans le debug";
                create_XML(sender, e);
                create_JSON(sender, e);           
            }
            else if (i==6) //Fermeture de la fenetre après la page 5
            {
                this.Close();
            }
            i++;
        }
        #endregion

        //-------------------------------------------------------Fermer la fenetre

        #region Bouton quitter
        public void btn_quitter(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // FICHIER JSON ET XML
        //=================================================================================================================================================================================================================================================

        //------------------------------------------------------Creation du fichier XML

        #region Fichier XML
        public void create_XML (object sender, RoutedEventArgs e)
        {
            MySqlConnection maConnexion = Utilities.Connexion();
            var table = new DataSet();
            using (var da = new MySqlDataAdapter("SELECT piece.numP,GROUP_CONCAT(nomf),group_concat(prixf),GROUP_CONCAT(delai)" +
                "FROM piece,livre,fournisseur WHERE quantitéEnStockPiece=1 AND piece.numP=livre.numP AND  fournisseur.siret = livre.siret" +
                " GROUP BY piece.numP; ", maConnexion))
            {
                da.Fill(table);
            }
            string queryString = "SELECT piece.numP,GROUP_CONCAT(nomf),group_concat(prixf),GROUP_CONCAT(delai)" +
                " FROM piece,livre,fournisseur WHERE quantitéEnStockPiece=1 AND piece.numP=livre.numP AND " +
                "fournisseur.siret = livre.siret GROUP BY piece.numP; ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, maConnexion);
            DataSet piece = new DataSet();
            adapter.Fill(piece, "Pieces");
            string filePath = "tableXml.xml";
            table.WriteXml(filePath);
            Console.WriteLine("Done");
        }
        #endregion

        //--------------------------------------------------------Creation du fichier JSON

        #region Fichier JSON
        public void create_JSON (object sender, RoutedEventArgs e)
        {
            //On récupère la liste ayant une adhésion se terminant dans les 2 prochains mois
            List<Tuple<string, string>> fin = finAdhesion();
            List<Client> membre = new List<Client>();
            for (int i = 0; i <= fin.Count - 1; i++)
            {
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT * FROM particulier WHERE nomPa=@nomPa AND prenom=@prenom";
                command.Parameters.Add("@nomPa", MySqlDbType.String).Value = fin[i].Item1;
                command.Parameters.Add("@prenom", MySqlDbType.String).Value = fin[i].Item2;
                command.ExecuteNonQuery();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Client a = new Client();
                    a.CodePa = reader.GetInt32(0);
                    a.AdresseP = reader.GetInt32(1);
                    a.NomPa = reader.GetString(2);
                    a.Prenom = reader.GetString(3);
                    a.Telephone = reader.GetString(4);
                    a.Courriel = reader.GetString(5);
                    a.DateA = reader.GetDateTime(6);
                    a.NumProg = reader.GetInt32(7);
                    membre.Add(a);

                }
                reader.Close();
                command.Dispose();
            }
            string file = "Client.json";
            StreamWriter fileWriter = new StreamWriter(file);
            JsonTextWriter jsonwriter = new JsonTextWriter(fileWriter);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonwriter, membre);
            jsonwriter.Close();
            fileWriter.Close();
            Console.WriteLine("\nlecture des informations de Client.json\n");
            Console.WriteLine("Ensemble des clients dont l'adhésion à un programme se termine dans les deux mois");
        }
        #endregion

        //----------------------------------------------Récupèration des dates d'adhésion des clients
        //(aide à la création du fichier JSON)

        #region Fin des des d'adhésion
        static List<Tuple<string, string>> finAdhesion()
        {
            List<Tuple<string, string>> membre = new List<Tuple<string, string>>();
            DateTime ajd = DateTime.Now;
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT nomPa,prenom,programme.description,dateA,duree FROM particulier,programme " +
                "WHERE programme.numProg = particulier.numProg ORDER BY programme.numProg ";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DateTime adhesion = reader.GetDateTime(3);
                int duree = reader.GetInt32(4);
                DateTime fin = adhesion.AddYears(duree);
                if (ajd.AddMonths(2) >= fin)
                {
                    Tuple<string, string> a = Tuple.Create((string)reader["nomPa"], (string)reader["prenom"]);
                    membre.Add(a);
                }
            }
            reader.Close();
            command.Dispose();
            return membre;
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // AUTRES FONCTIONS
        //=================================================================================================================================================================================================================================================

        //--------------------------Réinitilise l'affichage de la listview

        #region Reinitialiser
        private void Clear_datagrid()
        {
            GridView maingrid = new GridView();
            datagrid.View = maingrid;
            datagrid.ClearValue(ItemsControl.ItemsSourceProperty);
        }
        #endregion
    }
}
