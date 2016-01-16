using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinApp.Model
{
    public class DeleteEventArgs : EventArgs
    {

        public string Division { get; set; }
        public string MatchDate { get; set; }
        public string Piscine { get; set; }

        public DeleteEventArgs(string matchDate, string division, string piscine)
        {
            this.MatchDate = matchDate;
            this.Division = division;
            this.Piscine = piscine;
        }
    }
}
