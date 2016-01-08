using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinApp.Model
{
    public class Utilisateur
    {

        public int ID_UTILISATEUR { get; set; }
        public string LOGIN { get; set; }
        public string NOM { get; set; }
        public string PRENOM { get; set; }
        public string MOTDEPASSE { get; set; }
        public decimal ADR_LATITUDE { get; set; }
        public decimal ADR_LONGITUDE { get; set; }

    }
}
