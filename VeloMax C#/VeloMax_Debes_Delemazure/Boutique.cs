using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace VeloMax_Debes_Delemazure
{
    class Boutique
    {
        //==================================================================================================================================================================================================================================================
        // CONSTRUCTEUR
        //==================================================================================================================================================================================================================================================

        #region Constructeur

        //On construit la classe en fonctions des paramètres de la table boutique de MYSQL

        public string NomB { get; set; }
        public int AdresseB { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        public string Contact { get; set; }
        public int Remise { get; set; }

        public Boutique() { }

        #endregion

        //==================================================================================================================================================================================================================================================
        // FUNCTIONS
        //==================================================================================================================================================================================================================================================

        //-----------------------------------Creation/modification/Suppression d'une boutique

        #region Create/Edit/Delete
        public void CreateEditDelete(string methode)
        {
            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter nomB = new MySqlParameter("@nomB", MySqlDbType.VarChar);
            MySqlParameter adresseB = new MySqlParameter("@adresseB", MySqlDbType.Int32);
            MySqlParameter telephone = new MySqlParameter("@telephone", MySqlDbType.VarChar);
            MySqlParameter courriel = new MySqlParameter("@courriel", MySqlDbType.VarChar);
            MySqlParameter contact = new MySqlParameter("@contact", MySqlDbType.VarChar);
            MySqlParameter remise = new MySqlParameter("@remise", MySqlDbType.Int32);

            //Attribution des valeurs aux paramètres
            nomB.Value = NomB;
            adresseB.Value = AdresseB;
            telephone.Value = Telephone;
            courriel.Value = Courriel;
            contact.Value = Contact;
            remise.Value = Remise;

            //Definition de la requete
            string requete = ""; 
            if (methode == "create")
            {
                requete = "insert into boutique Values (@nomB, @adresseB, @telephone, @courriel, @contact,@remise);";
            }
            else if (methode == "edit")
            {
                requete = "update `boutique` set `adresseB` = @adresseB,`telephone`= @telephone,`courriel`= @courriel,`contact`= @contact,`remise`=@remise where `nomB`= @nomB;";
            }
            else if(methode == "delete")
            {
                requete = "delete from boutique where `nomB`= @nomB;";
            }

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(telephone);
            command.Parameters.Add(courriel);
            command.Parameters.Add(nomB);
            command.Parameters.Add(contact);
            command.Parameters.Add(remise);
            command.Parameters.Add(adresseB);

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
