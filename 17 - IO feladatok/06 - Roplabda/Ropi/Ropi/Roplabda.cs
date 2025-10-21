using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konyvek;

public class Roplabda
{
    public string Nev { get; set; }
    public int Magassag { get; set; }
    public string Poszt { get; set; }
    public string Nemzetiseg { get; set; }
    public string Csapat { get; set; }
    public string Orszag { get; set; }

    public override string ToString()
    {
        return $"{Nev}\t{Magassag}\t{Poszt}\t{Nemzetiseg}\t{Csapat}\t{Orszag}";
    }
}
