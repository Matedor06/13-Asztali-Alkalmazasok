using Nobel;
using System.Text;

// 2. Beolvasás a nobel.csv állományból
var fileData = await File.ReadAllLinesAsync("nobel.csv", Encoding.UTF8);
var dijasok = new List<NobelDijas>();

for (int i = 1; i < fileData.Length; i++)
{
    var line = fileData[i];
    var data = line.Split(';');
    dijasok.Add(new NobelDijas
    {
        Ev = int.Parse(data[0]),
        Tipus = data[1],
        Keresztnev = data[2],
        Vezeteknev = data.Length > 3 ? data[3] : ""
    });
}

// 3. Arthur B. McDonald típusú díjának kiírása
var arthur = dijasok.FirstOrDefault(d => d.Keresztnev == "Arthur B." && d.Vezeteknev == "McDonald");
if (arthur != null)
{
    Console.WriteLine($"3. feladat: Arthur B. McDonald {arthur.Tipus} Nobel-díjat kapott.");
}
Console.WriteLine();

// 4. 2017-ben irodalmi Nobel-díjat kapott
var irodalmi2017 = dijasok.FirstOrDefault(d => d.Ev == 2017 && d.Tipus == "irodalmi");
if (irodalmi2017 != null)
{
    Console.WriteLine($"4. feladat: {irodalmi2017.Keresztnev} {irodalmi2017.Vezeteknev}");
}
Console.WriteLine();

// 5. Béke Nobel-díjat kaptak 1990-től napjainkig
var bekeDijasok = dijasok.Where(d => d.Tipus == "béke" && d.Ev >= 1990).ToList();
Console.WriteLine($"5. feladat: {bekeDijasok.Count} szervezet/személy kapott béke Nobel-díjat 1990-től napjainkig.");
Console.WriteLine();

// 6. Curie család tagjai
var curieTagok = dijasok.Where(d => d.Vezeteknev.Contains("Curie")).ToList();
Console.WriteLine($"6. feladat: A Curie család {curieTagok.Count} tagja kapott Nobel-díjat.");
foreach (var tag in curieTagok)
{
    Console.WriteLine($"  {tag.Ev}: {tag.Keresztnev} {tag.Vezeteknev} - {tag.Tipus}");
}
Console.WriteLine();

// 7. Típusú díjból hány darabot osztottak ki
var tipusokSzama = dijasok.GroupBy(d => d.Tipus)
    .Select(g => new { Tipus = g.Key, Darab = g.Count() })
    .OrderByDescending(x => x.Darab)
    .ToList();

Console.WriteLine("7. feladat: Díjtípusok darabszáma:");
foreach (var tipus in tipusokSzama)
{
    Console.WriteLine($"  {tipus.Tipus}: {tipus.Darab} darab");
}
Console.WriteLine();

// 8. orvosi.txt - kiosztott orvosi Nobel-díj adatai
var orvosiDijasok = dijasok.Where(d => d.Tipus == "orvosi")
    .OrderBy(d => d.Ev)
    .ThenBy(d => d.Vezeteknev)
    .Select(d => $"{d.Ev};{d.Keresztnev} {d.Vezeteknev}")
    .ToList();

await File.WriteAllLinesAsync("orvosi.txt", orvosiDijasok, Encoding.UTF8);
Console.WriteLine("8. feladat: Az orvosi.txt fájl elkészült.");

Console.ReadKey();
