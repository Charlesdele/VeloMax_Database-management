using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace VeloMax_Debes_Delemazure
{
    class Piece
    {
        //==================================================================================================================================================================================================================================================
        // CONSTRUCTEUR
        //==================================================================================================================================================================================================================================================

        #region Constructeur

        //On construit la classe en fonctions des paramètres de la table Piece de MYSQL      

        public string NumP { get; set; }
        public string Description { get; set; }
        public int PrixU { get; set; }
        public DateTime DateI { get; set; }
        public DateTime DateD { get; set; }
        public int Quantite { get; set; }

        public Piece() { }

        #endregion

        //==================================================================================================================================================================================================================================================
        // FUNCTIONS
        //==================================================================================================================================================================================================================================================

        //------------------------------------Création/modification/suppression d'une pièce

        #region Create/Edit/Delete
        public void CreateEditDelete(string methode)
        {
            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter numP = new MySqlParameter("@numP", MySqlDbType.VarChar);
            MySqlParameter description = new MySqlParameter("@description", MySqlDbType.VarChar);
            MySqlParameter prixU = new MySqlParameter("@prixU", MySqlDbType.Int32);
            MySqlParameter dateI = new MySqlParameter("@dateI", MySqlDbType.Date);
            MySqlParameter dateD = new MySqlParameter("@dateD", MySqlDbType.Date);
            MySqlParameter quantite = new MySqlParameter("@quantitéEnStockPiece",MySqlDbType.Int64);

            //Attribution des valeurs aux paramètres
            numP.Value = this.NumP;
            description.Value = this.Description;
            prixU.Value = this.PrixU;
            dateI.Value = this.DateI;
            dateD.Value = this.DateD;
            quantite.Value = this.Quantite;

            //Definition de la requete
            string requete = "";
            if (methode == "create")
            {
                requete = "insert into piece Values (@numP, @description, @prixU,STR_TO_DATE(@dateI,'%m/%d/%Y),STR_TO_DATE(@dateD,'%m/%d/%Y),@quantitéEnStockPiece);";
            }
            else if (methode == "edit")
            {
                requete = "update `piece` set `description` = @description,`prixU`= @prixU,`dateI`= @dateI,`dateD`= @dateD,`quantitéEnStockPiece`= @quantitéEnStockPiece where `numP`= @numP;";
            }
            else if (methode=="delete")
            {
                requete = "delete from piece where `numP`= @numP;";
            }

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(numP);
            command.Parameters.Add(description);
            command.Parameters.Add(prixU);
            command.Parameters.Add(dateI);
            command.Parameters.Add(dateD);
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
        #endregion
    }
}
