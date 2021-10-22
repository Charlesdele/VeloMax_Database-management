using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax_Debes_Delemazure
{
    public class Reader
    {
        //Classe dédié à la lecture des données SQL 
        // Les multiples int, string, datetime, float 
        //permet d'attribuer autant de paramètres que l'on choisit

        #region Constructeur

        public int int1 { get; set; }
        public int int2 { get; set; }
        public int int3 { get; set; }
        public int int4 { get; set; }
        public int int5 { get; set; }
        public int int6 { get; set; }
        public string string1 { get; set; }
        public string string2 { get; set; }
        public string string3 { get; set; }
        public string string4 { get; set; }
        public string string5 { get; set; }
        public string string6 { get; set; }
        public DateTime date1 { get; set; }
        public DateTime date2 { get; set; }
        public DateTime date3 { get; set; }
        public DateTime date4 { get; set; }
        public float float1 { get; set; }

        public Reader() { }

        #endregion
    }
}
