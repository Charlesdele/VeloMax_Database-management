using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;


namespace VeloMax_Debes_Delemazure
{
    class Client
    {
        //==================================================================================================================================================================================================================================================
        // CONSTRUCTEUR
        //==================================================================================================================================================================================================================================================

        #region Constructeur

        //On construit la classe en fonctions des paramètres de la table particulier de MYSQL

        public int CodePa { get; set; }
        public int AdresseP { get; set; }
        public string NomPa { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        public DateTime DateA { get; set; }
        public int NumProg { get; set; }

        public Client () { }

        #endregion

        //==================================================================================================================================================================================================================================================
        // FUNCTIONS
        //==================================================================================================================================================================================================================================================

        //---------------------------------------Creation/modification/Suppression d'un client

        #region Create/Edit/Delete
        public void CreateEditDelete(string methode)
        {
            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter codePa = new MySqlParameter("@codePa", MySqlDbType.Int32);
            MySqlParameter adresseP = new MySqlParameter("@adresseP", MySqlDbType.Int32);
            MySqlParameter nomPa = new MySqlParameter("@nomPa", MySqlDbType.VarChar);
            MySqlParameter prenom = new MySqlParameter("@prenom", MySqlDbType.VarChar);
            MySqlParameter telephone = new MySqlParameter("@telephone", MySqlDbType.VarChar);
            MySqlParameter courriel = new MySqlParameter("@courriel", MySqlDbType.VarChar);
            MySqlParameter dateA = new MySqlParameter("@dateA", MySqlDbType.Date);
            MySqlParameter numProg = new MySqlParameter("@numProg", MySqlDbType.Int32);

            //Attribution des valeurs aux paramètres
            codePa.Value = this.CodePa;
            adresseP.Value = this.AdresseP;
            nomPa.Value = this.NomPa;
            prenom.Value = this.Prenom;
            telephone.Value = this.Telephone;
            courriel.Value = this.Courriel;
            dateA.Value = this.DateA;
            numProg.Value = this.NumProg;

            //Definition de la requete
            string requete = "";
            if (methode == "create")
            {
                requete = "insert into particulier Values (@codePa, @adresseP, @nomPa, @prenom, @telephone,@courriel, @dateA, @numProg);";
            }
            else if (methode == "edit")
            {
                requete = "update `particulier` set `adresseP` = @adresseP,`nomPa`= @nomPa,`prenom`= @prenom,`telephone`= @telephone,`courriel`= @courriel,`dateA`= @dateA,`numProg`= @numProg where `codePa`= @codePa;";
            }
            else if (methode =="delete")
            {
                requete = "delete from particulier where `codePa`= @codePa;";
            }

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(codePa);
            command.Parameters.Add(adresseP);
            command.Parameters.Add(nomPa);
            command.Parameters.Add(prenom);
            command.Parameters.Add(telephone);
            command.Parameters.Add(courriel);
            command.Parameters.Add(dateA);
            command.Parameters.Add(numProg);

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
