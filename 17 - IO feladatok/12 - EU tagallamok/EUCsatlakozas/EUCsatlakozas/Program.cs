using System.Text;
using EUCsatlakozas;


var fileData = await File.ReadAllLinesAsync("EUcsatlakozas.txt", Encoding.UTF8);
var csatlakozasok = new List<EUMembers>();

foreach (var line in fileData)
{
    var data = line.Split(';');
    csatlakozasok.Add(new EUMembers
    {
        Name = data[0],
        JoinDate = DateTime.Parse(data[1])
    });
}

//3. feladat
Console.WriteLine($"3. feladat: 2018-ban {csatlakozasok.Where(x => x.JoinDate.Year <= 2018).Count()} tagállam volt az EU-ban");

//4. feladat
var csatlakozas2007 = csatlakozasok.Where(x => x.JoinDate.Year == 2007).ToList();
Console.WriteLine($"\n4. feladat: 2007-ben csatlakozott országok: {csatlakozas2007.Count}");

//5. feladat
var magyarorszag = csatlakozasok.FirstOrDefault(x => x.Name == "Magyarorszag");
if (magyarorszag != null)
{
    Console.WriteLine($"5. feladat: Magyarország csatlakozási dátuma: {magyarorszag.JoinDate.ToString("yyyy.MM.dd.")}");
}

//6. feladat HAtározza meg, hogy májusban történt-e csatlakozás az eu hoz!
var majusiCsatlakozas = csatlakozasok.Any(x => x.JoinDate.Month == 5);
Console.WriteLine($"6. feladat: Májusban történt-e csatlakozás az EU-hoz? {(majusiCsatlakozas ? "Igen, májusban volt csatlakozás." : "Nem, májusban nem volt csatlakozás.")}");

//7. feladat
var utolsoCsatlakozas = csatlakozasok.OrderByDescending(x => x.JoinDate).First();
Console.WriteLine($"7. feladat: Legutoljára csatlakozott ország: {utolsoCsatlakozas.Name}");

//8. feladat 
var statisztika = csatlakozasok.GroupBy(x => x.JoinDate.Year)
    .Where(g => g.Count() >= 1)
    .OrderBy(g => g.Key)
    .Select(g => new { Ev = g.Key, Szam = g.Count() });
Console.WriteLine("\n8. feladat: Statisztika");
foreach (var stat in statisztika)
{
    Console.WriteLine($"\t{stat.Ev}: {stat.Szam} ország");
}