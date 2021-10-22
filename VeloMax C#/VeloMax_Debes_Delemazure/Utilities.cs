using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace VeloMax_Debes_Delemazure
{
    class Utilities
    {
        public static string uid; // Nom d'utilisateur pour la connexion à MySql

        public static string password; //Mot de passe pour la connexion à MySql

        //=======================================================================================================================
        // CONNEXION A MYSQL
        //=======================================================================================================================

        //------------------------------------------Connexion à la base de données VeloMax

        #region Connexion MySql
        public static MySqlConnection Connexion()
        {
            MySqlConnection maConnexion = null;
            try
            {
                string connexionString = "server=localhost; port=3306; database=velomax; uid="+uid+"; password="+password+";Allow User Variables = True;";
                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
            }
            return maConnexion;
        }
        #endregion

        //=======================================================================================================================
        // CHARGEMENT DE DONNEES DES CLEFS ETRANGERES 
        //=======================================================================================================================
        //((a introduire dans les combobox pour ne pas avoir d'erreurs lors de creation ou modification

        //----------------------------------Chargement des numéros d'assemblage

        #region Numéros d'assemblage
        public static List<string> Loadnuma()
        {
            //pour la classe velo le numéro d'assemblage est une clé étrangère

            List<string> strArray = new List<string>();
            MySqlConnection maConnexion = Utilities.Connexion();
            string requete = "select numA from assemblage";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                strArray.Add(Convert.ToString(reader["numA"]));
            }
            return strArray;
        }
        #endregion

        //----------------------------------------Chargement des numéros d'adresse

        #region Numéros d'adresse
        public static List<string> Loadadresse()
        {
            //pour la classe boutique, fournisseur, particulier l'adresse est une clé étrangère
            List<string> strArray = new List<string>();
            MySqlConnection maConnexion = Utilities.Connexion();
            string requete = "select numAd from adresse";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                strArray.Add(Convert.ToString(reader["numAd"]));
            }
            return strArray;
        }
        #endregion

        //----------------------------------------Chargement des numéros de programme (client)

        #region Numéros de programme
        public static List<string> Loadnumprog()
        {
            //pour la classe particulier le numéro du programme est une clé étrangère
            List<string> strArray = new List<string>();
            MySqlConnection maConnexion = Utilities.Connexion();
            string requete = "select numProg from programme";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                strArray.Add(Convert.ToString(reader["numProg"]));
            }
            return strArray;
        }
        #endregion

        //----------------------------------------Chargement des codes clients

        #region Code client particulier
        public static List<string> Loadcodepa()
        {
            //pour la classe commande le code du particulier est une clé étrangère
            List<string> strArray = new List<string>();
            MySqlConnection maConnexion = Utilities.Connexion();
            string requete = "select codePa from particulier";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                strArray.Add(Convert.ToString(reader["codePa"]));
            }
            strArray.Add("0");
            return strArray;
        }
        #endregion

        //--------------------------------------Chargement des noms de boutiques

        #region Noms des boutiques
        public static List<string> Loadnomb()
        {
            //pour la classe commande le nom de la boutique est une clé étrangère

            List<string> strArray = new List<string>();
            MySqlConnection maConnexion = Connexion();
            string requete = "select nomB from boutique";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                strArray.Add(Convert.ToString(reader["nomB"]));
            }
            return strArray;
        }
        #endregion

        //------------------------------Retourne le numéro de commande à prendre lors d'une nouvelle commande 

        #region Numéros de commande
        public static int load_numC()
        {
            //Dans le cs "passer_commande", pour que une nouvelle commande soit ajouter il faut que son numero (en clé primaire soit differents
            //de ceux affichés, donc +1
            List<int> numC = new List<int>();
            int nouv_commande = 0;
            MySqlConnection maConnexion = Connexion();
            string requete = "select numC from commande";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int numero = Convert.ToInt32(reader["numC"]);
                if (numero > nouv_commande) { nouv_commande = numero; }
            }
            return nouv_commande + 1;
        }
        #endregion

        //=======================================================================================================================
        // VERIFICATION DE LA QUANTITE DES PIECES/VELOS D'UNE COMMANDE
        //=======================================================================================================================

        //---------------------------------------Liste de la commande

        #region Liste de la commande
        public static List<int> getListCommande()
        {
            List<int> result = new List<int>();
            MySqlConnection maConnexion = Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT numC FROM commande ORDER BY numC; ";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader.GetInt32(0));
            }
            reader.Close();
            command.Dispose();
            return result;
        }
        #endregion

        //----------------------------------------------------------------------Prix commande

        #region Prix commande
        public static int prixCommande(int numCommande, List<int> indexCommande)
        {
            int montantVelo = 0;
            int montantPiece = 0;
            if (indexCommande.Contains(numCommande) == true)
            {
                MySqlConnection maConnexion = Connexion();
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT codePa FROM commande  WHERE numC=" + numCommande;
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.IsDBNull(0) == true)
                {
                    //commande de boutique
                    //on regarde si commande de pieces 
                    MySqlConnection maConnexion2 = Connexion();
                    MySqlCommand command2 = maConnexion2.CreateCommand();
                    command2.CommandText = "SELECT  SUM(contientp.quantite*prixU)  FROM commande, boutique, contientp, piece WHERE boutique.nomB= commande.nomB AND " +
                        "commande.numC = contientp.numC AND contientp.numP = piece.numP AND commande.numC =  " + numCommande;
                    MySqlDataReader reader2 = command2.ExecuteReader();
                    montantPiece = 0;
                    while (reader2.Read())
                    {
                        try
                        {
                            montantPiece = reader2.GetInt32(0);
                        }
                        catch
                        {

                        }
                    }
                    reader2.Close();
                    command2.Dispose();
                    //on regarde si commande de vélo 
                    MySqlConnection maConnexion3 = Connexion();
                    MySqlCommand command3 = maConnexion3.CreateCommand();
                    command3.CommandText = "SELECT SUM(contientv.quantite*prix)  FROM commande, contientv, velo ,boutique WHERE boutique.nomB = commande.nomB  " +
                        "AND commande.numC = contientv.numC AND contientv.numV = velo.numV  AND commande.numC =" + numCommande;
                    MySqlDataReader reader3 = command3.ExecuteReader();
                    montantVelo = 0;
                    while (reader3.Read())
                    {
                        try
                        {
                            montantVelo = reader3.GetInt32(0);
                        }
                        catch
                        {

                        }
                    }
                    reader3.Close();
                    command3.Dispose();

                    Console.WriteLine("La commande " + numCommande + " a un total de " + (montantPiece + montantVelo) + " euros");

                }
                else
                {
                    //commande de particulier
                    //on regarde si commande de pieces 
                    MySqlConnection maConnexion2 = Connexion();
                    MySqlCommand command2 = maConnexion2.CreateCommand();
                    command2.CommandText = "SELECT  SUM(contientp.quantite*prixU) FROM commande, particulier, contientp, piece WHERE particulier.codePa = commande.codePa AND  " +
                        "commande.numC = contientp.numC AND contientp.numP = piece.numP AND commande.numC =" + numCommande;
                    MySqlDataReader reader2 = command2.ExecuteReader();
                    montantPiece = 0;
                    while (reader2.Read())
                    {
                        try
                        {
                            montantPiece = reader2.GetInt32(0);
                        }
                        catch
                        {

                        }
                    }
                    reader2.Close();
                    command2.Dispose();
                    //on regarde si commande de vélo 
                    MySqlConnection maConnexion3 = Connexion();
                    MySqlCommand command3 = maConnexion3.CreateCommand();
                    command3.CommandText = "SELECT SUM(contientv.quantite*prix) FROM commande, particulier, contientv, velo WHERE particulier.codePa = commande.codePa  " +
                        "AND commande.numC = contientv.numC AND contientv.numV = velo.numV AND commande.numC =" + numCommande;
                    MySqlDataReader reader3 = command3.ExecuteReader();
                    montantVelo = 0;
                    while (reader3.Read())
                    {
                        try
                        {
                            montantVelo = reader3.GetInt32(0);
                        }
                        catch
                        {

                        }
                    }
                    reader3.Close();
                    command3.Dispose();

                    Console.WriteLine("La commande " + numCommande + " a un total de " + (montantPiece + montantVelo) + " euros");
                }
                reader.Close();
                command.Dispose();
            }
            else
            {
                Console.WriteLine("Commande inconnue");
            }
            return montantPiece + montantVelo;
        }
        #endregion

        //----------------------------------------------------------TriFusion

        #region TriFusion
        public static int[] TriFusion(int[] tab, int debut, int fin)
        {
            if (debut < fin)
            {
                int milieu = (debut + fin) / 2;
                TriFusion(tab, debut, milieu);
                TriFusion(tab, milieu + 1, fin);
                Fusion(tab, debut, milieu, fin);
            }
            return tab;
        }

        public static void Fusion(int[] tabMain, int debut, int milieu, int fin)
        {
            int[] tabClone = new int[tabMain.Length];
            for (int j = 0; j <= tabMain.Length - 1; j++)
            {
                tabClone[j] = tabMain[j];
            }
            int indexTabMain = debut;
            int index1 = debut;
            int index2 = milieu + 1;
            while (index1 <= milieu && index2 <= fin)
            {
                if (tabClone[index1] <= tabClone[index2])
                {
                    tabMain[indexTabMain] = tabClone[index1];
                    index1++;
                }
                else
                {
                    tabMain[indexTabMain] = tabClone[index2];
                    index2++;
                }
                indexTabMain++;
            }
            if (indexTabMain <= fin)
            {
                while (index1 <= milieu)
                {
                    tabMain[indexTabMain] = tabClone[index1];
                    indexTabMain++;
                    index1++;
                }
                while (index2 <= fin)
                {
                    tabMain[indexTabMain] = tabClone[index2];
                    indexTabMain++;
                    index2++;
                }
            }
        }
        #endregion

        //=======================================================================================================================
        // VERIFICATION DES ENTREES 
        //=======================================================================================================================

        //----------------------------------------------------------Vérification d'un string d'une certaine taille

        #region String ?
        public static bool is_varchar (string varchar,int taille)
        {
            //On prend on compte la taille car les paramètres SQL ont des limites (ex: VARCHAR(20))
            bool reponse = false;
            if (varchar.Length <= taille & varchar.All(char.IsDigit) == false)
            {
                reponse = true;
            }
            return reponse;
        }
        #endregion

        //-------------------------------------------------------Vérification d'un int d'une certaine taille

        #region Int ?
        public static bool is_int (string varchar, int taille)
        {
            //On vérifie également que ce ne soit pas un trop grand nombre ce qui serait illogique
            bool reponse = false;
            if (varchar.All(char.IsDigit) == true & varchar.Length <= taille)
            {
                reponse = true;
            }
            return reponse;
        }
        #endregion

        //=======================================================================================================================
        // AFFICHAGE FOURNISSEUR
        //=======================================================================================================================

        //------------------------------------------Attribution libelle en fonction de la réactivité d'un fournisseur
        //(utilisé dans la fonction Affichage_fournisseur dans MainWindow.xaml.cs)

        #region Libelle d'un fournisseur
        public static int get_libelle (string siret)
        {
            //Initialisation
            int libelle = 4; // à l'origine on attribue le libelle à 4 (niv réactivité = 4)
            int count = 0; // On initialise le nombre de commande total à 0
            int delai = 0; // On initilise le delai total à 0

            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter siretsql = new MySqlParameter("@siret", MySqlDbType.VarChar);

            //Attribution des valeurs aux paramètres
            siretsql.Value = siret;          

            //Definition de la requete
            string requete = "select siret,count(siret),sum(delai) from livre where siret=@siret;";


            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(siretsql);

            //Lecture des résultats
            MySqlDataReader reader = command.ExecuteReader();           
            while (reader.Read())
            {
                count = Convert.ToInt32(reader["count(siret)"]);
                delai = Convert.ToInt32(reader["sum(delai)"]);
            }
            reader.Close();
            command.Dispose();

            //Attribution libelle
            int calcul = (delai/count)*10; //On calcule le rapport du delai en fonction du nombre de commandes
            if (calcul >= 40 & calcul<60)
            {
                libelle = 3; //niv reactivite = moyen
            }
            else if (calcul <80 & calcul >=60)
            {
                libelle = 2; //niv reactivite = bon
            }
            else if (calcul >= 80)
            {
                libelle = 1; //niv reactivite = tres bon
            }
            return libelle;
        }
        #endregion
    }
}
