using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NB1
{
    internal class Player
    {
        public int MezSzam { get; set; }
        public string Utonev { get; set; }
        public string Vezeteknev { get; set; }
        public DateTime SzuletesiDatum { get; set; }
        public bool MagyarAllampolgar { get; set; }
        public bool KulfoldiAllampolgar { get; set; }
        public int Ertek { get; set; } // euró ezrekben
        public string KlubNeve { get; set; }
        public string PosztNeve { get; set; }

        public string TeljesNev => $"{Vezeteknev} {Utonev}".Trim();
        
        public int Kor
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - SzuletesiDatum.Year;
                if (SzuletesiDatum.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        public bool Mezonyjatekos => PosztNeve.ToLower() != "kapus";
    }
}
