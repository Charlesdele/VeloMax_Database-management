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
    /// Logique d'interaction pour Passer_commande.xaml
    /// </summary>
    public partial class Passer_commande : Window
    {
        //==================================================================================================================================================================================================================================================
        // INITIALISATION
        //==================================================================================================================================================================================================================================================

        #region Initialisation
        public Passer_commande()
        {
            InitializeComponent();
            Affichage_piece();
            Affichage_velo();
            text_prix.Text = "";
            text_remise.Text = "";
        }

        public int quantite = 0; // quantite demande lors de l'ajout de pièce/vélo

        public List<Reader> MonPanier = new List<Reader>(); // liste des pièces et vélos que l'on a ajouté

        public int prixTOT = 0; // montant total a payer
        #endregion

        //==================================================================================================================================================================================================================================================
        // BOUTONS
        //==================================================================================================================================================================================================================================================

        //--------------------------------------------------------Ajoute les pièces/vélos dans le panier (listview du bas) avec la quantité désirée

        #region Valider ajout
        public void valider_ajout(object sender, RoutedEventArgs e)
        {
            if (datagrid_piece.SelectedItems.Count > 0 & quantite != 0) //Vérifie si des pièces sont sélectionnées
            {
                List<Piece> Listpiece = (List<Piece>)datagrid_piece.ItemsSource; //Initilisation d'une liste des pièces sélectionnées
                for (int i = 0; i < datagrid_piece.SelectedItems.Count; i++) //On récupère les informations de cette pièce pour l'ajouter dans le panier du client
                {
                    Reader ajout = new Reader();
                    Piece piece_ajout = datagrid_piece.SelectedItems[i] as Piece;
                    ajout.int1 = Convert.ToInt32(Quantite.Content);
                    ajout.string1 = piece_ajout.NumP;
                    ajout.int2 = piece_ajout.PrixU;
                    MonPanier.Add(ajout);
                }
            }
            if (datagrid_velo.SelectedItems.Count > 0 & quantite != 0) //Vérifie si des vélos sont sélectionnés
            {
                List<Velo> Listvelo = (List<Velo>)datagrid_velo.ItemsSource; //Initilisation d'une liste des vélos sélectionnés
                for (int i = 0; i < datagrid_velo.SelectedItems.Count; i++) //On récupère les informations de ce velo pour l'ajouter dans le panier du client
                {
                    Reader ajout = new Reader();
                    Velo velo_ajout = datagrid_velo.SelectedItems[i] as Velo;
                    ajout.int1 = Convert.ToInt32(Quantite.Content);
                    ajout.string1 = velo_ajout.Nom;
                    ajout.int2 = velo_ajout.Prix;
                    ajout.string2 = velo_ajout.Grandeur;
                    ajout.int3 = velo_ajout.NumV;
                    MonPanier.Add(ajout);
                }
            }
            clear_ajout(sender, e); //Reinitialiser les séléctions et la quantité
            update_total(); //Mise à jour du montant total à payer
            actualiser_panier(); //Mise à jour de l'affichage du panier du client
        }
        #endregion

        //--------------------------------------------------------------Applique la remise au prix final en fonction du client/boutique seletionné(e)

        #region Appliquer remise
        public void appliquer_remise(object sender, RoutedEventArgs e)
        {
            int montant = prixTOT; // On recupère le montant total actuelle

            double prixFinal = 0; // On initialise le prix après remise

            if (menu_boutique.SelectedValue != null) //Si le client est une boutique
            {
                //On initialise la remise
                double remise = 0;

                //Creation des paramètres MySql pour que la requete identifie les variables
                MySqlParameter nomB = new MySqlParameter("@nomB", MySqlDbType.VarBinary);

                //Attribution des valeurs aux paramètres
                nomB.Value = (string)menu_boutique.SelectedValue;

                //Connexion et creation de la commande Mysql
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();             
                command.CommandText = "SELECT remise FROM boutique WHERE nomB=@nomB;";
                command.Parameters.Add(nomB);


                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    remise = reader.GetDouble(0);
                }
                reader.Close();
                command.Dispose();

                //Calcul du montant avec la remise
                prixFinal = montant - ((remise / 100) * (double)montant);

                //Affichage de la remise appliquée
                text_remise.Text = "Remise de " + remise + "%";
            }
            if (menu_client.SelectedValue != null) //Si le client est un particulier
            {
                //Initialisation 
                int programme = 0; 
                double remise = 0;

                //Creation des paramètres MySql pour que la requete identifie les variables
                MySqlParameter codePa = new MySqlParameter("@codePa", MySqlDbType.VarBinary);

                //Attribution des valeurs aux paramètres
                codePa.Value = Convert.ToInt32(menu_client.SelectedValue);

                //Connexion et creation de la commande Mysql
                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlCommand command = maConnexion.CreateCommand();              
                command.CommandText = "SELECT numProg FROM particulier WHERE codePa=@codePa;";
                command.Parameters.Add(codePa);

                //Execution de la commande
                MySqlDataReader reader = command.ExecuteReader();

                //Lecture des résultats
                while (reader.Read())
                {
                    programme = reader.GetInt32(0);
                }
                reader.Close();
                command.Dispose();

                //Attribution de la remise en fonction du programme du client
                if (programme == 1)
                {
                    text_remise.Text = "Remise de 5%";  //Affichage de la remise appliquée
                    remise = 0.05;
                }
                else if (programme == 2)
                {
                    remise = 0.08;
                    text_remise.Text = "Remise de 8%"; //Affichage de la remise appliquée

                }
                else if (programme == 3)
                {
                    remise = 0.1;
                    text_remise.Text = "Remise de 10%"; //Affichage de la remise appliquée
                }
                else if (programme == 4)
                {
                    remise = 0.12;
                    text_remise.Text = "Remise de 12%"; //Affichage de la remise appliquée

                }
                else
                {
                    remise = 0;
                    text_remise.Text = "Pas de remise"; //Affichage de la remise appliquée
                }

                //Calcul du montant final
                prixFinal = montant - (remise * montant);
            }

            //Mise à jour du montant final après remise
            text_prix.Text = prixFinal + "€";
        }
        #endregion

        //--------------------------------------------------------Augmenter la quantité de pièce/vélo désirée

        #region Augmenter quantité
        public void Quantite_plus(object sender, RoutedEventArgs e)
        {
            quantite++;
            Quantite.Content = quantite;
        }
        #endregion

        //---------------------------------------------------------Réduire la quantité de pièce/vélo désirée

        #region Diminuer quantité
        public void Quantite_moins(object sender, RoutedEventArgs e)
        {
            if (quantite >= 1) //pour ne pas selectonne une quantité inférieur à 0
            {
                quantite--;
            }
            Quantite.Content = quantite;
        }
        #endregion

        //-------------------------------------------------Fermer la fenêtre

        #region Fermer la fenêtre
        public void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // PASSER UNE COMMANDE
        //==================================================================================================================================================================================================================================================

        //-----------------------------------------------------------Vérification des quantités disponibles et création de la commande

        #region Valider la commande
        public void valider_commande(object sender, RoutedEventArgs e)
        {
            //Vérirication si il y a des pièces manquantes en fonction de notre stock
            Dictionary<string, int> PieceManquantes = CommandeDispoHub(MonPanier);

            if (PieceManquantes.Count == 0) //Si il n'y a pas de pièces manquantes
            {
                //Creation de notre commande
                Commande commande = new Commande();

                //La nouvelle commande prend un numéro +1 au dessus du numéro de commande le plus grand existant
                commande.NumC = Utilities.load_numC();

                if (menu_boutique.SelectedValue != null) //On vérifie si le client est une boutique
                {
                    commande.NomB = Convert.ToString(menu_boutique.SelectedValue); //Le nom de la boutique est celui selectionné dans le menu déroulant
                    commande.AdresseL = search_adresse_boutique(commande.NomB); //L'adresse de livraison est celle de la boutique selectionné
                }
                else
                {
                    commande.NomB = "Null"; //Si c'est un client le nom de boutique est null
                }
                if (menu_client.SelectedValue != null) //On vérifie si le client est un particulier
                {
                    commande.CodePa = Convert.ToInt32(menu_client.SelectedValue); //Le code client est celui selectionné dans le menu déroulant
                    commande.AdresseL = search_adresse_client(commande.CodePa); // L'adresse de livraison est celle du client selectionné
                }
                else
                {
                    commande.CodePa = 0; //Si c'est une boutique le numero client est égale à 0
                }

                //Date de la commande
                DateTime dateC = DateTime.Now;
                commande.DateC = dateC;

                //Date de livraison
                DateTime dateL = DateTime.Now;
                dateL = dateL.AddDays(7);
                commande.DateL = dateL;

                //Creation de la commande
                commande.CreateEditDelete("create");

                //Mise à jour des quantités vendus
                vendre(commande.NumC);

                //Mise à jour des stocks
                deleteVelo(MonPanier);
                deletePiece(MonPanier);

                //Affichage de la date de livraison
                MessageBox.Show("La commande arrivera le " + dateL);
               
                this.Close();
            }
            else //Si il y a des pièces manquantes
            {
                //Commander les pièces manquantes et choisir les fournisseurs/délais pour chaque pièce manquante
                if_piecemanquante remplacer = new if_piecemanquante();
                remplacer.Piece_FD = fournisseur_delai(PieceManquantes);

                //Initialisation dans la page de commande de la liste et du menu deroulant des pièces manquantes
                List<string> PiecesManquantes = new List<string>();
                foreach (string piece in remplacer.Piece_FD.Keys)
                {
                    PiecesManquantes.Add(piece);
                }
                remplacer.menu_piece.ItemsSource = PiecesManquantes;
                remplacer.PiecesManquantes = PiecesManquantes;
                remplacer.ShowDialog();
                while (remplacer.IsVisible == true) { }

                //////////////////////////////////

                //Creation de notre commande
                Commande commande = new Commande();

                //La nouvelle commande prend un numéro +1 au dessus du numéro de commande le plus grand existant
                commande.NumC = Utilities.load_numC();

                if (menu_boutique.SelectedValue != null) // On vérifie si le client est une boutique
                {
                    commande.NomB = Convert.ToString(menu_boutique.SelectedValue); //Le nom de la boutique est celui selectionné dans le menu déroulant
                    commande.AdresseL = search_adresse_boutique(commande.NomB); //L'adresse de livraison est celle de la boutique selectionné
                }
                else
                {
                    commande.NomB = "Null"; //Si c'est un client le nom de boutique est null
                }
                if (menu_client.SelectedValue != null) // On vérifie si le client est un particulier
                {
                    commande.CodePa = Convert.ToInt32(menu_client.SelectedValue); //Le code client est celui selectionné dans le menu déroulant
                    commande.AdresseL = search_adresse_client(commande.CodePa); // L'adresse de livraison est celle du client selectionné
                }
                else
                {
                    commande.CodePa = 0; //Si c'est une boutique le numero client est égale à 0
                }

                //Date de la commande
                DateTime dateC = DateTime.Now;
                commande.DateC = dateC;

                //Date de livraison
                DateTime dateL = DateTime.Now;
                dateL = dateL.AddDays(7);
                dateL = dateL.AddDays(remplacer.delai); //On ajoute le delai de livraison des pièces
                commande.DateL = dateL;

                //Creation de la commande
                commande.CreateEditDelete("create");

                //Mise à jour des quantités vendues
                vendre(commande.NumC);

                //Affichage de la date de livraison
                MessageBox.Show("La commande arrivera le " + dateL);

                this.Close();
            }
        }
        #endregion

        //-------------------------------------------Suppression des quantités de pièces qui sont vendues

        #region Supprimer les pièces commandées du stock
        static void deletePiece(List<Reader> commande) 
        {
            for (int i = 0; i <= commande.Count - 1; i++) //Parcours la commande
            {
                if (commande[i].string1.Length <= 5) //On identifie les pièces par leur courte description
                {
                    //Connexion, creation de la commande et définition de la requête
                    MySqlConnection maConnexion = Utilities.Connexion();
                    MySqlCommand command = maConnexion.CreateCommand();
                    command.CommandText = "UPDATE piece SET quantitéEnStockPiece=quantitéEnStockPiece-@quantite WHERE numP=@nom;"; //Mise à jour du stock

                    //Creation des paramètres MySql pour que la requete identifie les variables
                    command.Parameters.Add("@nom", MySqlDbType.String).Value = commande[i].string1;
                    command.Parameters.Add("@quantite", MySqlDbType.Int32).Value = commande[i].int1;

                    //Execution de la commande
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        //------------------------------------------Suppression des quantités de vélos qui sont venudus

        #region Supprimer les vélos commmandés du stock
        static void deleteVelo(List<Reader> commande)
        {
            int quantiteReste = 0;
            //On récupère la liste des vélos
            List<string> veloAll = new List<string>();
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT nom FROM velo ";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                veloAll.Add(reader.GetString(0));
            }
            reader.Close();
            command.Dispose();
            //On test si on a un vélo dans notre commande
            for (int i = 0; i <= commande.Count - 1; i++)
            {
                if (veloAll.Contains(commande[i].string1) == true)
                {
                    //On a un velo
                    //On regarde si on a une quantité en stock suffisant
                    //On récupère la quantité
                    MySqlConnection maConnexion2 = Utilities.Connexion();
                    MySqlCommand command2 = maConnexion2.CreateCommand();
                    command2.CommandText = "SELECT quantitéEnStockVelo FROM velo WHERE nom=@nom AND grandeur=@grandeur";
                    command2.Parameters.Add("@nom", MySqlDbType.String).Value = commande[i].string1;
                    command2.Parameters.Add("@grandeur", MySqlDbType.String).Value = commande[i].string2;
                    MySqlDataReader reader2 = command2.ExecuteReader();
                    int quantiteStockVelo = 0;
                    while (reader2.Read())
                    {
                        quantiteStockVelo = reader2.GetInt32(0);
                    }
                    reader2.Close();
                    command2.Dispose();
                    //Si on a assez de stock on retranche simplement avec une commande SQL
                    if (quantiteStockVelo >= commande[i].int1)
                    {
                        MySqlConnection maConnexion3 = Utilities.Connexion();
                        MySqlCommand command3 = maConnexion3.CreateCommand();
                        command3.CommandText = "UPDATE velo SET quantitéEnStockVelo=quantitéEnStockVelo-@quantite WHERE nom=@nom AND grandeur=@grandeur;";
                        command3.Parameters.Add("@nom", MySqlDbType.String).Value = commande[i].string1;
                        command3.Parameters.Add("@grandeur", MySqlDbType.String).Value = commande[i].string2;
                        command3.Parameters.Add("@quantite", MySqlDbType.Int32).Value = commande[i].int1;
                        command3.ExecuteNonQuery();
                    }
                    //Sinon il faut assembler un nouveau vélo avec les pièces détachées 
                    //On met le stock vélo  à 0
                    else
                    {
                        MySqlConnection maConnexion4 = Utilities.Connexion();
                        MySqlCommand command4 = maConnexion4.CreateCommand();
                        command4.CommandText = "UPDATE velo SET quantitéEnStockVelo=0 WHERE nom=@nom AND grandeur=@grandeur;";
                        command4.Parameters.Add("@nom", MySqlDbType.String).Value = commande[i].string1;
                        command4.Parameters.Add("@grandeur", MySqlDbType.String).Value = commande[i].string2;
                        command4.ExecuteNonQuery();
                        //Nombre de vélos que l'on doit assembler
                        quantiteReste = commande[i].int1 - quantiteStockVelo;
                        //On regarde les pièces nécessaires à l'assemblage
                        MySqlConnection maConnexion5 = Utilities.Connexion();
                        MySqlCommand command5 = maConnexion5.CreateCommand();
                        command5.CommandText = "SELECT assemblage.cadre, assemblage.guidon, assemblage.freins, assemblage.selle, assemblage.derailleurAv," +
                            "assemblage.derailleurAr, assemblage.roueAv,assemblage.roueAr,assemblage.reflecteurs,assemblage.pedalier,assemblage.ordinateur, assemblage.panier" +
                            " FROM velo,assemblage WHERE velo.numA=assemblage.numA AND " +
                            "nom=@nom AND velo.grandeur=@grandeur";
                        command5.Parameters.Add("@nom", MySqlDbType.String).Value = commande[i].string1;
                        command5.Parameters.Add("@grandeur", MySqlDbType.String).Value = commande[i].string2;
                        MySqlDataReader reader5 = command5.ExecuteReader();
                        while (reader5.Read())
                        {
                            for (int j = 0; j <= 11; j++)
                            {
                                //Tous les vélos ne possèdent pas toutes les options
                                if (reader5.GetString(j) != "")
                                {
                                    MySqlConnection maConnexion6 = Utilities.Connexion();
                                    MySqlCommand command6 = maConnexion6.CreateCommand();
                                    command6.CommandText = "UPDATE piece SET quantitéEnStockPiece=quantitéEnStockPiece-@quantite WHERE numP=@nom;";
                                    command6.Parameters.Add("@nom", MySqlDbType.String).Value = reader5.GetString(j);
                                    command6.Parameters.Add("@quantite", MySqlDbType.Int32).Value = quantiteReste;
                                    command6.ExecuteNonQuery();
                                }
                            }
                        }
                        reader5.Close();
                        command5.Dispose();

                    }
                }
            }
        }
        #endregion

        //-----------------------------------Mise à jour des quantités vendues

        #region Mettre à jour les quantités vendues
        public void vendre(int numeroCommande)
        {
            for (int i = 0; i < MonPanier.Count; i++) //On parcours notre commande
            {
                if (MonPanier[i].string2 == "" | MonPanier[i].string2 == null) //On identifie les pièces par le fait qu'elles n'aient pas de grandeur comme les vélos
                {
                    //Creation des paramètres MySql pour que la requete identifie les variables
                    MySqlParameter numC = new MySqlParameter("@numC", MySqlDbType.Int32);
                    MySqlParameter numP = new MySqlParameter("@numP", MySqlDbType.VarChar);
                    MySqlParameter quantite = new MySqlParameter("@quantite", MySqlDbType.Int32);

                    //Attribution des valeurs de la commande de pièce(s) aux paramètres
                    numC.Value = numeroCommande;
                    numP.Value = MonPanier[i].string1;
                    quantite.Value = MonPanier[i].int1;

                    //Definition de la requete (lorsque c'est une pièce on modifie les données de la table contientp)
                    string requete = "insert into `contientp`(`numC`,`numP`,`quantite`) VALUES (@numC,@numP,@quantite);"; //

                    //Connexion et creation de la commande Mysql
                    MySqlConnection maConnexion = Utilities.Connexion();
                    MySqlCommand command = maConnexion.CreateCommand();
                    command.CommandText = requete;
                    command.Parameters.Add(numP);
                    command.Parameters.Add(numC);
                    command.Parameters.Add(quantite);

                    //Execution de la commande
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Erreur de connexion : " + e.ToString());
                    }
                    command.Dispose();
                }
                else //Sinon c'est un velo 
                {
                    //Creation des paramètres MySql pour que la requete identifie les variables
                    MySqlParameter numC = new MySqlParameter("@numC", MySqlDbType.Int32);
                    MySqlParameter numV = new MySqlParameter("@numV", MySqlDbType.Int32);
                    MySqlParameter quantite = new MySqlParameter("@quantite", MySqlDbType.Int32);

                    //Attribution des valeurs de la commande de velo(s) aux paramètres
                    numC.Value = numeroCommande;
                    numV.Value = MonPanier[i].int3;
                    quantite.Value = MonPanier[i].int1;

                    //Définition de la requête (lorsque c'est un velo on modifie les données de la table contientv)
                    string requete = "insert into `contientv`(`quantite`,`numC`,`numV`) VALUES (@quantite,@numC,@numV);";

                    //Connexion et creation de la commande Mysql
                    MySqlConnection maConnexion = Utilities.Connexion();
                    MySqlCommand command = maConnexion.CreateCommand();
                    command.CommandText = requete;
                    command.Parameters.Add(numC);
                    command.Parameters.Add(numV);
                    command.Parameters.Add(quantite);

                    //Execution de la commande
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Erreur de connexion : " + e.ToString());
                    }
                    command.Dispose();
                }
            }
        }
        #endregion

        //--------------------------------------------Récupère l'adresse de la boutique selectionne
        //(nécessaire à la création de la commande)

        #region Adresses boutiques
        public int search_adresse_boutique(string nom)
        {
            //Initialisation de l'adresse de la boutique
            int adresse = 0;

            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter Boutique = new MySqlParameter("nomB", MySqlDbType.VarChar);

            //Attribution des valeurs aux paramètres
            Boutique.Value = nom;

            //Definition de la requete
            string requete = "select adresseB from boutique where nomB = @nomB";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(Boutique);

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                adresse = Convert.ToInt32(reader["adresseB"]);
            }
            reader.Close();
            command.Dispose();
            return adresse;
        }
        #endregion

        //------------------------------------------Récupère l'adresse du client selectionne
        //(nécessaire à la création de la commande)

        #region Adresses clients
        public int search_adresse_client(int client)
        {
            //Initialisation de l'adresse du client
            int adresse = 0;

            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter codePa = new MySqlParameter("codePa", MySqlDbType.Int32);

            //Attribution des valeurs aux paramètres
            codePa.Value = client;

            //Definition de la requete
            string requete = "select adresseP from particulier where codePa = @codePa";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(codePa);

            //Execution de la commande
            MySqlDataReader reader = command.ExecuteReader();

            //Lecture des résultats
            while (reader.Read())
            {
                adresse = Convert.ToInt32(reader["adresseP"]);
            }
            reader.Close();
            command.Dispose();
            return adresse;
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // VERIFICATION D'UNE COMMANDE
        //==================================================================================================================================================================================================================================================

        //-------------------------------------------------------------Vérification des stocks de vélo (sinon vérification des stocks des pièces permettant l'assemblage d'un velo)

        #region Stock des vélos
        static Dictionary<string, int> VeloDispo(List<Reader> commande)
        {
            Dictionary<string, int> pieces = new Dictionary<string, int>();
            //On récupère la liste des vélos
            List<string> veloAll = new List<string>();
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT nom FROM velo ";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                veloAll.Add(reader.GetString(0));
            }
            reader.Close();
            command.Dispose();
            //On test si on a un vélo dans notre commande
            for (int i = 0; i <= commande.Count - 1; i++)
            {
                if (veloAll.Contains(commande[i].string1) == true)
                {
                    //On a un velo
                    //On regarde si on a une quantité en stock suffisant
                    //On récupère la quantité
                    //On doit différencier les différentes grandeurs de chaque modèle de vélo
                    MySqlConnection maConnexion2 = Utilities.Connexion();
                    MySqlCommand command2 = maConnexion2.CreateCommand();
                    command2.CommandText = "SELECT quantitéEnStockVelo FROM velo WHERE nom=@nom AND grandeur=@grandeur";
                    command2.Parameters.Add("@nom", MySqlDbType.String).Value = commande[i].string1;
                    command2.Parameters.Add("@grandeur", MySqlDbType.String).Value = commande[i].string2;
                    MySqlDataReader reader2 = command2.ExecuteReader();
                    int quantiteStockVelo = 0;
                    while (reader2.Read())
                    {
                        quantiteStockVelo = reader2.GetInt32(0);
                    }
                    reader2.Close();
                    command2.Dispose();
                    //on teste si on a assez de stock de velo
                    if (commande[i].int1 > quantiteStockVelo)
                    {
                        //On a pas assez de stock on regarde les pièces nécessaires à l'assemblage
                        MySqlConnection maConnexion3 = Utilities.Connexion();
                        MySqlCommand command3 = maConnexion3.CreateCommand();
                        command3.CommandText = "SELECT assemblage.cadre, assemblage.guidon, assemblage.freins, assemblage.selle, assemblage.derailleurAv," +
                            "assemblage.derailleurAr, assemblage.roueAv,assemblage.roueAr,assemblage.reflecteurs,assemblage.pedalier,assemblage.ordinateur, assemblage.panier" +
                            " FROM velo,assemblage WHERE velo.numA=assemblage.numA AND " +
                            "nom=@nom AND velo.grandeur=@grandeur";
                        command3.Parameters.Add("@nom", MySqlDbType.String).Value = commande[i].string1;
                        command3.Parameters.Add("@grandeur", MySqlDbType.String).Value = commande[i].string2;
                        MySqlDataReader reader3 = command3.ExecuteReader();
                        while (reader3.Read())
                        {
                            for (int j = 0; j <= 11; j++)
                            {
                                //Tous les vélos ne possèdent pas toutes les options
                                if (reader3.GetString(j) != "")
                                {
                                    //Si on a deja une entrée de la piece on augment juste la quantité
                                    if (pieces.Keys.Contains(reader3.GetString(j)) == true)
                                    {
                                        pieces[reader3.GetString(j)] += commande[i].int1;
                                    }
                                    //Sinon on crée une nouvelle entrée
                                    else
                                    {
                                        pieces[reader3.GetString(j)] = commande[i].int1 - quantiteStockVelo;
                                    }

                                }
                            }
                        }
                        reader3.Close();
                        command3.Dispose();
                    }

                }
            }
            return pieces;

        }
        #endregion

        //-----------------------------------------Vérification des stocks de pièce

        #region Stock des pièces
        static Dictionary<string, int> stockPiece()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT numP,quantitéEnStockPiece FROM piece ;";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result[reader.GetString(0)] = reader.GetInt32(1);
            }
            reader.Close();
            command.Dispose();
            return result;
        }
        #endregion

        //-------------------------------------------------------------------Liste des pièces manquantes après vérification des quantités

        #region Articles disponibles ?
        static Dictionary<string, int> CommandeDispoHub(List<Reader> commande)
        {
            Dictionary<string, int> piecesManquantes = new Dictionary<string, int>();
            Dictionary<string, int> piecesNecessaires = VeloDispo(commande);
            //Pas le velo en stock on regarde si on peut l'assembler et qu'on a toutes les autres pièces
            //On récupère le dictionnaire des pièces avec leur quantité
            Dictionary<string, int> stock = stockPiece();
            //On ajoute les pièces détachées commandées aux pièces (si elles existent) pour assembler les vélos
            for (int i = 0; i <= commande.Count - 1; i++)
            {
                //On a deja une entrée dans notre dictionnaire 
                if (piecesNecessaires.Keys.Contains(commande[i].string1) == true)
                {
                    piecesNecessaires[commande[i].string1] += commande[i].int1;
                }
                //Sinon on crée une nouvelle entrée
                else
                {
                    //On vérifie qu'il s'agit d'une pièce
                    if (commande[i].string1.Length <= 5)
                    {
                        piecesNecessaires[commande[i].string1] = commande[i].int1;
                    }

                }
            }
            //On test si on a toutes les pièces dans notre stock
            foreach (string i in piecesNecessaires.Keys)
            {
                if (stock[i] < piecesNecessaires[i])
                {
                    piecesManquantes[i] = piecesNecessaires[i] - stock[i];
                }

            }
            return piecesManquantes;
        }
        #endregion

        //-------------------------------------------------------------------------------------------Association de chacun des pièces manquantes à une liste des fournisseurs disponibles avec leur délai de livraison

        #region Fournisseur delai
        public Dictionary<string, List<Reader>> fournisseur_delai(Dictionary<string, int> ListOfPiece)
        {
            Dictionary<string, List<Reader>> ListFournisseur = new Dictionary<string, List<Reader>>();
            foreach (string piece in ListOfPiece.Keys)
            {
                List<Reader> fournisseur_delai = new List<Reader>();
                ListFournisseur[piece] = fournisseur_delai;

                MySqlConnection maConnexion = Utilities.Connexion();
                MySqlParameter numP = new MySqlParameter("numP", MySqlDbType.VarChar);
                numP.Value = piece;
                string requete = "select nomf,delai from livre natural join fournisseur where numP= @numP";
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = requete;
                command.Parameters.Add(numP);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Reader fournisseur = new Reader();
                    fournisseur.string1 = (string)reader["nomf"];
                    fournisseur.int1 = Convert.ToInt32(reader["delai"]);
                    fournisseur_delai.Add(fournisseur);
                }
                reader.Close();
                command.Dispose();
            }
            return ListFournisseur;
        }
        #endregion

        //==================================================================================================================================================================================================================================================
        // FONCTIONS
        //==================================================================================================================================================================================================================================================

        //-----------------------------Mise à jour du panier dans la listview

        #region Actualiser le panier
        public void actualiser_panier()
        {
            //Update de la listview avec la public list du panier
            datagrid_panier.ItemsSource = MonPanier;

            //Creation des colonnes de la listview
            GridViewColumn Description = new GridViewColumn();
            Description.Width = 100;
            Description.DisplayMemberBinding = new Binding("string1");
            Description.Header = "Description";

            GridViewColumn Quantite = new GridViewColumn();
            Quantite.Width = 70;
            Quantite.DisplayMemberBinding = new Binding("int1");
            Quantite.Header = "Quantité";

            GridViewColumn Prix = new GridViewColumn();
            Prix.Width = 70;
            Prix.DisplayMemberBinding = new Binding("int2");
            Prix.Header = "Prix";

            GridViewColumn Grandeur = new GridViewColumn();
            Grandeur.Width = 100;
            Grandeur.DisplayMemberBinding = new Binding("string2");
            Grandeur.Header = "Grandeur";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(Quantite);
            maingrid.Columns.Add(Description);
            maingrid.Columns.Add(Grandeur);
            maingrid.Columns.Add(Prix);
            datagrid_panier.View = maingrid;
        }
        #endregion

        //-------------------------------------------------------Déselectionne les articles après qu'ils soient ajoutés

        #region Reinitialiser les sélections
        public void clear_ajout(object sender, RoutedEventArgs e)
        {
            //On remet à jour les séléctions et la quantité demandé après l'ajout d'un ou plusieurs articles
            Quantite.Content = 0;
            quantite = 0;
            datagrid_velo.UnselectAll();
            datagrid_piece.UnselectAll();
        }
        #endregion

        //------------------------Mise à jour de l'affichage du prix total à payer

        #region Actualiser le prix total
        public void update_total()
        {
            int montant_total = 0;
            for (int i = 0; i < MonPanier.Count; i++) //On parcours notre panier
            {
                int quantite = MonPanier[i].int1; //On récupère la quantité de l'article
                int prix = MonPanier[i].int2; //On récupère le prix de l'article
                montant_total += prix * quantite; //On additionne le prix à payer pour une quantité x de cet article au montant total
            }
            prixTOT = montant_total; //Mise à jour du montant total dans la classe 
            text_prix.Text = prixTOT + "€"; //Mise à jour de l'affichage
        }
        #endregion


        //==================================================================================================================================================================================================================================================
        // AFFICHAGE
        //==================================================================================================================================================================================================================================================

        //--------------------------Affichage de la liste des pièces du catalogue lorsque l'on veut passer une commande

        #region Affichage pièces
        public void Affichage_piece()
        {
            //Initialisation de notre liste de pièce
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
                Piece new_piece = new Piece(); //Initialisation d'une pièce à chaque ligne

                //Lecture des colonnes
                string numP = (string)reader["numP"];
                string description = (string)reader["description"];
                int prixU = Convert.ToInt32(reader["prixU"]);

                //Attribution des valeurs des colonnes au reader
                new_piece.NumP = numP; ;
                new_piece.Description = description;
                new_piece.PrixU = prixU;
                ListOfPiece.Add(new_piece);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste de piece
            datagrid_piece.ItemsSource = ListOfPiece;

            //Creation des colonnes de la listview
            GridViewColumn NumP = new GridViewColumn();
            NumP.Width = 50;
            NumP.DisplayMemberBinding = new Binding("NumP");
            NumP.Header = "Pièce";

            GridViewColumn Description = new GridViewColumn();
            Description.Width = 150;
            Description.DisplayMemberBinding = new Binding("Description");
            Description.Header = "Description";

            GridViewColumn PrixU = new GridViewColumn();
            PrixU.Width = 100;
            PrixU.DisplayMemberBinding = new Binding("PrixU");
            PrixU.Header = "Prix unitaire";

            GridView maingrid = new GridView();
            maingrid.Columns.Add(NumP);
            maingrid.Columns.Add(Description);
            maingrid.Columns.Add(PrixU);

            datagrid_piece.View = maingrid;
        }
        #endregion

        //--------------------------Affichage de la liste des vélos du catalogue lorsque l'on veut passer une commande

        #region Affichage vélos
        public void Affichage_velo()
        {
            //Initialisation de notre liste de velo
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
                Velo new_velo = new Velo(); //Initialisation d'un velo a chaque ligne

                //Lecture des colonnes
                string nom = (string)reader["nom"];
                string grandeur = (string)reader["grandeur"];
                int prix = Convert.ToInt32(reader["prix"]);
                string ligne = (string)reader["ligne"];
                int numV = Convert.ToInt32(reader["numV"]);

                //Attribution des valeurs des colonnes au reader
                new_velo.Nom = nom;
                new_velo.Grandeur = grandeur;
                new_velo.Prix = prix;
                new_velo.Ligne = ligne;
                new_velo.NumV = numV;
                ListOfVelo.Add(new_velo);
            }
            reader.Close();
            command.Dispose();

            //Association de la listview a la liste de velo
            datagrid_velo.ItemsSource = ListOfVelo;

            //Creation des colonnes de la listview
            GridViewColumn Nom = new GridViewColumn();
            Nom.Width = 135;
            Nom.DisplayMemberBinding = new Binding("Nom");
            Nom.Header = "Velo";

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

            GridView maingrid = new GridView();
            maingrid.Columns.Add(Nom);
            maingrid.Columns.Add(Ligne);
            maingrid.Columns.Add(Grandeur);
            maingrid.Columns.Add(Prix);

            datagrid_velo.View = maingrid;
        }
        #endregion


        //==================================================================================================================================================================================================================================================
        // CLIENT PARTICULIER/BOUTIQUE
        //==================================================================================================================================================================================================================================================

        //------------------------------------------------------Creation d'un nouveau client si non existant

        #region Nouveau client ?
        public void nouv_client(object sender, RoutedEventArgs e)
        {
            AddEdit_client add = new AddEdit_client(); //Page de creation d'un nouveau client
            add.ShowDialog(); //Affichage de la page
            while (add.IsVisible == true) { }
            menu_client.ItemsSource = Utilities.Loadcodepa(); //Mise à jour des clients dans le menu déroulant
            //pour choisir le nouveau client dans le menu déroulant
        }
        #endregion

        //--------------------------------------------------------Creation d'une nouvelle boutique si non existante

        #region Nouvelle boutique ?
        public void nouv_boutique(object sender, RoutedEventArgs e)
        {
            AddEdit_boutique add = new AddEdit_boutique(); //Page de creation d'une nouvelle boutique
            add.ShowDialog(); //Affichage de la page
            while (add.IsVisible == true) { }
            menu_boutique.ItemsSource = Utilities.Loadnomb(); //Mise à jour des boutiques dans le menu déroulant
            //pour choisir la nouvelle boutique dans le menu deroulant
        }
        #endregion

    }
}
