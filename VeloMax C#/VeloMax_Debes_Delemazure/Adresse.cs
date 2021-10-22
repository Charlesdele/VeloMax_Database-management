using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace VeloMax_Debes_Delemazure
{
    class Adresse
    {
        //==================================================================================================================================================================================================================================================
        // CONSTRUCTEUR
        //==================================================================================================================================================================================================================================================

        #region Constructeur

        //On construit la classe en fonctions des paramètres de la table adresse de MYSQL

        public int NumAd { get; set; }
        public int Nombre { get; set; }
        public string Rue { get; set; }
        public int CodePostal { get; set; }
        public string Ville { get; set; }
        public string Province { get; set; }

        public Adresse() { }

        #endregion

        //==================================================================================================================================================================================================================================================
        // FUNCTIONS
        //==================================================================================================================================================================================================================================================

        //---------------------------------------Creation ou modification d'une Adresse

        #region Create/Edit
        public void CreateEdit(string methode)
        {
            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter numAd = new MySqlParameter("@numAd", MySqlDbType.Int32);
            MySqlParameter nombre = new MySqlParameter("@nombre", MySqlDbType.Int32);
            MySqlParameter rue = new MySqlParameter("@rue", MySqlDbType.VarChar);
            MySqlParameter codePostal = new MySqlParameter("@codePostal", MySqlDbType.Int32);
            MySqlParameter ville = new MySqlParameter("@ville", MySqlDbType.VarChar);
            MySqlParameter province = new MySqlParameter("@province", MySqlDbType.VarChar);

            //Attribution des valeurs aux paramètres
            numAd.Value = NumAd;
            nombre.Value = Nombre;
            rue.Value = Rue;
            codePostal.Value = CodePostal;
            ville.Value = Ville;
            province.Value = Province;

            //Definition de la requete
            string requete = "";
            if (methode == "create")
            {
                requete = "insert into adresse Values (@numAd, @nombre, @rue, @codePostal, @ville,@province);";
            }
            else if (methode == "edit")
            {
                requete = "update `adresse` set `nombre`= @nombre,`rue`= @rue,`codePostal`= @codePostal,`ville`= @ville,`province`= @province where `numAd`= @numAd;";
            }

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(numAd);
            command.Parameters.Add(nombre);
            command.Parameters.Add(rue);
            command.Parameters.Add(codePostal);
            command.Parameters.Add(ville);
            command.Parameters.Add(province);

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

        //---------------------Suppression d'une Adresse

        #region Delete
        public void delete()
        {
            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter numAd = new MySqlParameter("@numAd", MySqlDbType.VarChar);

            //Attribution des valeurs aux paramètres
            numAd.Value = this.NumAd;

            //Definition de la requete
            string requete = "delete from piece where `numAd`= @numAd;";

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(numAd);

            //Execution de la commande
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : " + e.ToString());
            }
        }
        #endregion


    }
}
