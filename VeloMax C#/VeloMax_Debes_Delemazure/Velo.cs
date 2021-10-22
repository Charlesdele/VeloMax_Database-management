using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace VeloMax_Debes_Delemazure
{
    class Velo
    {
        //==================================================================================================================================================================================================================================================
        // CONSTRUCTEUR
        //==================================================================================================================================================================================================================================================

        #region Constructeur

        //On construit la classe en fonctions des paramètres de la table Velo de MYSQL      

        public int NumV { get; set; }
        public string Nom { get; set; }
        public string Grandeur { get; set; }
        public int Prix { get; set; }
        public string Ligne { get; set; }
        public DateTime DateD { get; set; }
        public DateTime DateF { get; set; }
        public int NumA { get; set; }
        public int QuantitéEnStockVelo { get; set; }

        public Velo() { }

        #endregion

        //==================================================================================================================================================================================================================================================
        // FONCTIONS
        //==================================================================================================================================================================================================================================================

        //-----------------------------------------Creation/modiciation/suppression d'un velo

        #region Create/Edit/Delete
        public void CreateEditDelete(string methode)
        {
            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter numV = new MySqlParameter("@numV", MySqlDbType.Int32);
            MySqlParameter nom = new MySqlParameter("@nom", MySqlDbType.VarChar);
            MySqlParameter grandeur = new MySqlParameter("@grandeur", MySqlDbType.VarChar);
            MySqlParameter prix = new MySqlParameter("@prix", MySqlDbType.Int32);
            MySqlParameter ligne = new MySqlParameter("@ligne", MySqlDbType.VarChar);
            MySqlParameter dateD = new MySqlParameter("@dateD", MySqlDbType.Date);
            MySqlParameter dateF = new MySqlParameter("@dateF", MySqlDbType.Date);
            MySqlParameter numA = new MySqlParameter("@numA", MySqlDbType.Int32);
            MySqlParameter quantitéEnStockVelo = new MySqlParameter("@quantitéEnStockVelo", MySqlDbType.Int32);

            //Attribution des valeurs aux paramètres
            numV.Value = NumV;
            nom.Value = Nom;
            grandeur.Value = Grandeur;
            prix.Value = Prix;
            ligne.Value = Ligne;
            dateD.Value = DateD;
            dateF.Value = DateF;
            numA.Value = NumA;
            quantitéEnStockVelo.Value = QuantitéEnStockVelo;
           
            //Definition de la requete
            string requete = "";
            if (methode == "create") //Requete pour insérer une nouveau vélo dans la base de données
            {
                requete = "insert into velo Values (@numV, @nom, @grandeur, @prix, @ligne,@dateD,@dateF,@numA,@quantitéEnStockVelo);";
            }
            else if (methode == "edit") // Requete pour modifier un velo existant dans la base de données
            {
                requete = "update `velo` set `nom` = @nom,`grandeur`= @grandeur,`prix`= @prix,`ligne`= @ligne,`dateD`= @dateD,`dateF`= @dateF,`numA`= @numA,`quantitéEnStockVelo`= @quantitéEnStockVelo where `numV`= @numV;";
            }
            else if(methode =="delete") // Requete pour supprimer un velo dans la base de données
            {
                requete = "delete from velo where `numV`= @numV;";
            }

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(numA);
            command.Parameters.Add(nom);
            command.Parameters.Add(grandeur);
            command.Parameters.Add(prix);
            command.Parameters.Add(ligne);
            command.Parameters.Add(dateD);
            command.Parameters.Add(dateF);
            command.Parameters.Add(numV);
            command.Parameters.Add(quantitéEnStockVelo);

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
        #endregion
    }
}
