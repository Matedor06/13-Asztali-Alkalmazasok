using System.Text;
using VB2018;

var fileData = await File.ReadAllLinesAsync("vb2018.txt", Encoding.UTF8);
var stadionok = new List<Stadium>();

foreach (var line in fileData)
{
    var data = line.Split(';');
    stadionok.Add(new Stadium
    {
        City = data[0],
        Name1 = data[1],
        Name2 = data[2],
        Capacity = int.Parse(data[3])
    });
}

foreach (var stadion in stadionok)
{
    Console.WriteLine(stadion);
}
Console.WriteLine();


Console.WriteLine($"2. feladat: Beolvasva {stadionok.Count} stadion adatait");
Console.WriteLine();

// 3. Jelenítse meg a képernyőn, hogy hány stadionban zajszották a VB mérkőzéseit!
Console.WriteLine($"3. feladat:");
Console.WriteLine($"A VB mérkőzéseit {stadionok.Count} stadionban játszották.");
Console.WriteLine();

Console.WriteLine($"4. feladat:");
var legkisebbStadion = stadionok.MinBy(s => s.Capacity);
Console.WriteLine($"Város: {legkisebbStadion.City}");
Console.WriteLine($"Stadion neve: {legkisebbStadion.Name1}");
Console.WriteLine($"Férőhely: {legkisebbStadion.Capacity}");
Console.WriteLine();

Console.WriteLine($"5. feladat:");
var atlagKapacitas = stadionok.Average(s => s.Capacity);
Console.WriteLine($"A stadionok átlagos férőhelyszáma: {atlagKapacitas:F1}");
Console.WriteLine();

Console.WriteLine($"6. feladat:");
var ketnevuStadionok = stadionok.Count(s => s.Name2 != "n.a.");
Console.WriteLine($"Két néven is ismert stadionok száma: {ketnevuStadionok}");
Console.WriteLine();

Console.WriteLine($"7. feladat:");
string varosNev;
do
{
    Console.Write("Adjon meg egy város nevet (legalább 3 karakter): ");
    varosNev = Console.ReadLine();
} while (varosNev.Length < 3);
Console.WriteLine();

var talalat = stadionok.FirstOrDefault(s => s.City.Equals(varosNev, StringComparison.OrdinalIgnoreCase));
if (talalat != null)
{
    Console.WriteLine($"A(z) {varosNev} városban rendeztek VB mérkőzéseket!");
    Console.WriteLine($"Stadion neve: {talalat.Name1}");
    if (talalat.Name2 != "n.a.")
    {
        Console.WriteLine($"Másik neve: {talalat.Name2}");
    }
    Console.WriteLine($"Kapacitás: {talalat.Capacity}");
}
else
{
    Console.WriteLine($"A(z) {varosNev} nem szerepel a VB helyszínek között! (Szocsi)");
}
Console.WriteLine();

Console.WriteLine($"9. feladat:");
var kulonbozoVarosok = stadionok.Select(s => s.City).Distinct().Count();
Console.WriteLine($"{kulonbozoVarosok} különböző városban zajlottak a VB mérkőzései.");
Console.WriteLine();

Console.WriteLine("Nyomjon meg egy billentyűt a kilépéshez...");
Console.ReadKey();

