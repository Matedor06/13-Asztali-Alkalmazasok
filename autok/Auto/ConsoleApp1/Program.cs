
using ConsoleApp1;
using System.Text;

var fileData = await File.ReadAllLinesAsync("autok.txt", Encoding.UTF8);
var lend = new List<Lend>();
foreach(var line in fileData)
{
    var data = line.Split(' ');
    lend.Add(new Lend
    {
        day = int.Parse(data[0]),
        dayTime = data[1],
        registrationNumber = data[2],
        employeeNumber = int.Parse(data[3]),
        kmCounter = int.Parse(data[4]),
        inOut = int.Parse(data[5]),
    });
}
var lastLend = lend.MaxBy(x => x.day);
Console.WriteLine($"{lastLend.day}. nap rendszám : {lastLend.registrationNumber}");
Console.WriteLine("szia");

Console.WriteLine("Nap:");
int day = int.Parse(Console.ReadLine());
var specificDay = lend.Where(x => x.day == day);
Console.WriteLine(specificDay);