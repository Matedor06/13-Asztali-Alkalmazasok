﻿using Konyvek;
using System.Text;

var fileData = await File.ReadAllLinesAsync("adatok.txt", Encoding.UTF8);
var jatekosok = new List<Roplabda>();

foreach (var line in fileData)
{
    var data = line.Split('\t');
    jatekosok.Add(new Roplabda
  {
        Nev = data[0],
        Magassag = int.Parse(data[1]),
        Poszt = data[2],
        Nemzetiseg = data[3],
 Csapat = data[4],
        Orszag = data[5]
    });
}

// 1. Írjuk ki a képernyőre az össz adatot
Console.WriteLine("=== OSSZES ADAT ===");
foreach (var jatekos in jatekosok)
{
    Console.WriteLine(jatekos);
}
Console.WriteLine();

// 2. Keressük ki az ütő játékosokat az utok.txt állömányba
var utok = jatekosok.Where(x => x.Poszt == "ütõ").Select(x => x.ToString()).ToList();
await File.WriteAllLinesAsync("utok.txt", utok, Encoding.UTF8);
Console.WriteLine($"Ütő játékosok mentve: utok.txt ({utok.Count} játékos)") ;

// 3. A csapattagok.txt állományba mentsük a csapatokat és a hozzájuk tartozó játékosokat
var csapatokCsoportositva = jatekosok.GroupBy(x => x.Csapat)
    .Select(g => $"{g.Key}: {string.Join(",", g.Select(j => j.Nev))},")
    .ToList();
await File.WriteAllLinesAsync("csapattagok.txt", csapatokCsoportositva, Encoding.UTF8);
Console.WriteLine($"Csapattagok mentve: csapattagok.txt ({csapatokCsoportositva.Count} csapat)");

 // 4. Rendezzük a játékosokat magasság szerint növekvő sorrendbe és a magaslatok.txt állományba mentsük el
var magassagSzerintRendezve = jatekosok.OrderBy(x => x.Magassag).Select(x => x.ToString()).ToList();
await File.WriteAllLinesAsync("magaslatok.txt", magassagSzerintRendezve, Encoding.UTF8);
Console.WriteLine($"Magasság szerint rendezve mentve: magaslatok.txt");

// 5. Mutassuk be a nemzetisegek.txt állományba, hogy mely nemzetiségek képviseltetik magukat és milyen számban
var nemzetisegek = jatekosok.GroupBy(x => x.Nemzetiseg)
    .Select(g => $"{g.Key}: {g.Count()} fő")
    .OrderByDescending(x => int.Parse(x.Split(':')[1].Trim().Split(' ')[0]))
    .ToList();
await File.WriteAllLinesAsync("nemzetisegek.txt", nemzetisegek, Encoding.UTF8);
Console.WriteLine($"Nemzetiségek mentve: nemzetisegek.txt ({nemzetisegek.Count} nemzetiség)");
// 6. atlagnalmagasabbak.txt állományba keressük azon játékosok nevét és magasságát akik magasabbak mint az átlagos magasság
var atlagMagassag = jatekosok.Average(x => x.Magassag);
var atlagnalMagasabbak = jatekosok.Where(x => x.Magassag > atlagMagassag)
    .Select(x => $"{x.Nev}\t{x.Magassag} cm")
    .ToList();
await File.WriteAllLinesAsync("atlagnalmagasabbak.txt", atlagnalMagasabbak, Encoding.UTF8);
Console.WriteLine($"Átlagnál magasabbak mentve: atlagnalmagasabbak.txt (Átlag: {atlagMagassag:F2} cm, {atlagnalMagasabbak.Count} játékos)");

// 7. Állítsa növekvő sorrendbe a posztok szerint a játékosok össz magasságát
var posztokMagassagOsszege = jatekosok.GroupBy(x => x.Poszt)
    .Select(g => new { Poszt = g.Key, OsszMagassag = g.Sum(j => j.Magassag) })
    .OrderBy(x => x.OsszMagassag)
.Select(x => $"{x.Poszt}: {x.OsszMagassag} cm")
    .ToList();
await File.WriteAllLinesAsync("posztok_magassag.txt", posztokMagassagOsszege, Encoding.UTF8);
Console.WriteLine($"Posztok magasság összege mentve: posztok_magassag.txt");

// 8. „alacsonyak.txt" keresse ki a játékosok átlagmagasságától alacsonyabb játékosokat
var alacsonyabbak = jatekosok.Where(x => x.Magassag < atlagMagassag)
    .Select(x => $"{x.Nev}\t{x.Magassag} cm\t{(atlagMagassag - x.Magassag):F2} cm alacsonyabb")
    .ToList();
await File.WriteAllLinesAsync("alacsonyak.txt", alacsonyabbak, Encoding.UTF8);
Console.WriteLine($"Átlagnál alacsonyabbak mentve: alacsonyak.txt ({alacsonyabbak.Count} játékos)");

Console.WriteLine("\n=== OSSZES FELADAT KESZ ===");
Console.ReadKey();