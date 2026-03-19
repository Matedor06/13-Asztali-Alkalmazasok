using System;
using System.Collections.Generic;
using System.Text;

namespace StarTrek;

public class Ship
{
 public string Name { get; set; }
 public string Class { get; set; }
 public string RaceFaction { get; set; }
 public int Length { get; set; }
 public int Crew { get; set; }
 public double MaxWarp { get; set; }
 public string Armament { get; set; } = string.Empty;
 public string ShieldType { get; set; } = string.Empty;
 public string HullMaterial { get; set; } = string.Empty;
 public string Role { get; set; } = string.Empty;

}
