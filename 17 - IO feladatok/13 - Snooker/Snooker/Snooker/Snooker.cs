using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snooker;

public class Snooker
{
    public int Helyezes { get; set; }
    public string Nev { get; set; }
    public string Orszag { get; set; }
    public int Nyeremeny { get; set; }

    public Snooker(string sor)
    {
        string[] adatok = sor.Split(';');
        Helyezes = int.Parse(adatok[0]);
        Nev = adatok[1];
        Orszag = adatok[2];
        Nyeremeny = int.Parse(adatok[3]);
    }
}
