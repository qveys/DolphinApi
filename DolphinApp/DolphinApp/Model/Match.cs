using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinApp.Model
{
    public class Match
    {

        public int ID_MATCH { get; set; }
        public DateTime DATE_MATCH { get; set; }
        public bool SECOND_MATCH { get; set; }
        public decimal DISTANCE { get; set; }
        public decimal COUT { get; set; }
        public int ID_PISCINE { get; set; }
        public int ID_DIVISION { get; set; }
        public int ID_UTILISATEUR { get; set; }
        public Utilisateur UTILISATEUR { get; set; }
        public Piscine PISCINE { get; set; }
        public Division DIVISION { get; set; }

        public string GetDateMatch {
            get { return DATE_MATCH.ToString("dd/MM/yyyy"); }
        }

    }
}
