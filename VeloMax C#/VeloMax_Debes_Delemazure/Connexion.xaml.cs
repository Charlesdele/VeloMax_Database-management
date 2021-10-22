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

namespace VeloMax_Debes_Delemazure
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        public Connexion()
        {
            InitializeComponent();
        }

        //=======================================================================================================================
        // CONNEXION
        //=======================================================================================================================

        #region Login
        public void Bon_login(object sender, RoutedEventArgs e)
        {
            if((get_id.Text == "bozo" & get_password.Password == "bozo")|(get_id.Text == "root" & get_password.Password == "root"))
            {
                if(get_id.Text == "bozo" & get_password.Password == "bozo")
                {
                    MessageBox.Show("Connecté en mode Lecture");
                }
                else if (get_id.Text == "root" & get_password.Password == "root")
                {
                    MessageBox.Show("Connecté en mode Administrateur");
                    
                }
                Utilities.password = get_password.Password;
                Utilities.uid = get_id.Text;
                this.Close();            
            }
            else
            {
                get_id.Text = "";
                get_password.Password = "";
                MessageBox.Show("Identifiant ou Mot de passe Incorrect");
            }                    
        }
        #endregion
    }
}
