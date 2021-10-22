using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace VeloMax_Debes_Delemazure
{
    class Fournisseur
    {
        //==================================================================================================================================================================================================================================================
        // CONSTRUCTEUR
        //==================================================================================================================================================================================================================================================

        #region Constructeur

        //On construit la classe en fonctions des paramètres de la table fournisseur de MYSQL

        public string Siret { get; set; }
        public string Nomf { get; set; }
        public string Contact { get; set; }
        public int AdresseF { get; set; }
        public int Libelle { get; set; }

        public Fournisseur () { }

        #endregion

        //==================================================================================================================================================================================================================================================
        // FUNCTIONS
        //==================================================================================================================================================================================================================================================

        //-----------------------------------Creation/modification/suppression d'un fournisseur

        #region Create/Edit/Delete
        public void CreateEditDelete(string methode)
        {
            //Creation des paramètres MySql pour que la requete identifie les variables
            MySqlParameter siret = new MySqlParameter("@siret", MySqlDbType.VarChar);
            MySqlParameter nomf = new MySqlParameter("@nomf", MySqlDbType.VarChar);
            MySqlParameter contact = new MySqlParameter("@contact", MySqlDbType.VarChar);
            MySqlParameter adresseF = new MySqlParameter("@adresseF", MySqlDbType.Int32);
            MySqlParameter libelle = new MySqlParameter("@libelle", MySqlDbType.Int32);

            //Attribution des valeurs aux paramètres
            siret.Value = Siret;
            nomf.Value = Nomf;
            contact.Value = Contact;
            adresseF.Value = AdresseF;
            libelle.Value = Libelle;

            //Definition de la requete
            string requete = "";
            if (methode == "create")
            {
                requete = "insert into fournisseur Values (@siret, @nomf, @contact, @adresseF, @libelle);";
            }
            else if (methode == "edit")
            {
                requete = "update `fournisseur` set `nomf` = @nomf,`contact`= @contact,`adresseF`= @adresseF,`libelle`= @libelle where `siret`= @siret;";
            }
            else if (methode =="delete")
            {
                requete = "delete from fournisseur where `siret`= @siret;";
            }

            //Connexion et creation de la commande Mysql
            MySqlConnection maConnexion = Utilities.Connexion();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete;
            command.Parameters.Add(siret);
            command.Parameters.Add(nomf);
            command.Parameters.Add(contact);
            command.Parameters.Add(adresseF);
            command.Parameters.Add(libelle);

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
