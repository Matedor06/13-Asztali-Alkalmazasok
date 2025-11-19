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
Console.WriteLine($"2018-ban {csatlakozasok.Where(x => x.JoinDate.Year <= 2018).Count()} tagállam volt az eu ban");

//4. feladat
var csatlakozas2007 = csatlakozasok.Where(x => x.JoinDate.Year == 2007).Select(x => x.Name);
Console.WriteLine("2007-ben csatlakozott országok:");
foreach (var orszag in csatlakozas2007)
{
    Console.WriteLine(orszag);
}
//5. feladat
var magyarorszag = csatlakozasok.FirstOrDefault(x => x.Name == "Magyarorszag");
Console.WriteLine(magyarorszag.JoinDate.ToString("yyyy.MM.dd."));

