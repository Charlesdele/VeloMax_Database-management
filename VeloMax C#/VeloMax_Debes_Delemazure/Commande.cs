using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace VeloMax_Debes_Delemazure
{
    class Commande
    {
        //==================================================================================================================================================================================================================================================
        // CONSTRUCTEUR
        //==================================================================================================================================================================================================================================================

        #region Constructeur

        //On construit la classe en fonctions des paramètres de la table Commande de MYSQL             

        public int NumC { get; set; }
        public DateTime DateC { get; set; }
        public int AdresseL { get; set; }
        public DateTime DateL { get; set; }
        public int CodePa { get; set; }
        public string NomB { get; set; }

        public Commande () { }

        #endregion

        //==================================================================================================================================================================================================================================================
        // FUNCTIONS
        //==================================================================================================================================================================================================================================================

        //----------------------------------------Creation/modification/suppression d'une commande

        #region Create/Edit/Delete
        public void CreateEditDelete(string methode)
        {
            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter numC = new MySqlParameter("@numC", MySqlDbType.Int32);
            MySqlParameter dateC = new MySqlParameter("@dateC", MySqlDbType.Date);
            MySqlParameter adresseL = new MySqlParameter("@adresseL", MySqlDbType.Int32);
            MySqlParameter dateL = new MySqlParameter("@dateL", MySqlDbType.Date);
            MySqlParameter codePa = new MySqlParameter("@codePa", MySqlDbType.Int32);
            MySqlParameter nomB = new MySqlParameter("@nomB", MySqlDbType.VarChar);

            //Attribution des valeurs aux paramètres
            numC.Value = NumC;
            dateC.Value = DateC;
            adresseL.Value = AdresseL;
            dateL.Value = DateL;
            if (CodePa ==0)
            {
                codePa = new MySqlParameter("@codePa", DBNull.Value);
            }
            else
            {
                codePa.Value = CodePa;
            }
            if(NomB == "Null")
            {
                nomB = new MySqlParameter("@nomb", DBNull.Value);
            }
            else
            {
                nomB.Value = NomB;
            }

            //Definition de la requete
            string requete = "";
            if (methode == "create")
            {
                requete = "insert into commande Values (@numC, @dateC, @adresseL, @dateL,@codePa,@nomB);";
            }
            else if (methode == "edit")
            {
                requete = "update `commande` set `dateC` = @dateC,`adresseL`= @adresseL,`dateL`= @dateL,`codePa`= @codePa,`nomB`= @nomB where `numC`= @numC;";
            }
            else if (methode=="delete")
            {
                requete = "delete from commande where `numC`= @numC;";
            }

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(numC);
            command.Parameters.Add(dateC);
            command.Parameters.Add(adresseL);
            command.Parameters.Add(dateL);
            command.Parameters.Add(codePa);
            command.Parameters.Add(nomB);

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

