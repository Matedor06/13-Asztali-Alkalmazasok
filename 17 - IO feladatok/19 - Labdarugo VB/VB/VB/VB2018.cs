using System;
using System.Collections.Generic;
using System.Text;

namespace VB2018;

public class Stadium
{
    public string City { get; set; }
    public string Name1 { get; set; }
    public string Name2 { get; set; }
    public int Capacity { get; set; }

    public override string ToString()
    {
        return $"{City}\t{Name1}\t{Name2}\t{Capacity}";
    }
}
