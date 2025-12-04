using System.Text;
using VB2018;
var fileData = await File.ReadAllLinesAsync("vb2018.txt",Encoding.UTF8);
var vb2018 = new List<Stadium>();

foreach(var line in fileData)
{
    var data = line.Split(';');
    vb2018.Add(new Stadium
    {
        City = data[0],
        Name1 = data[1],
        Name2 = data[2],
        Capacity = int.Parse(data[3])
    });
}
Console.WriteLine($"{vb2018.Count}");
var minStadium = vb2018.MinBy(s => s.Capacity).ToString();
Console.WriteLine(minStadium);

var averageCapacity = vb2018.Average(s => s.Capacity);
Console.WriteLine($"A stadionok átlagos kapacitása: {averageCapacity:F1}");

var twoNameStadiums = vb2018.Where(s => s.Name2 != "n.a.").ToList();
Console.WriteLine($"Kétnévű stadionok száma: {twoNameStadiums.Count}");


while (true)
{
    Console.Write("Kérem a város nevét: ");
    var city = Console.ReadLine();
    var stadium = vb2018.FirstOrDefault(s => s.City.Equals(city, StringComparison.OrdinalIgnoreCase));
    if(city.Length < 3)
    {
        continue;
    }
    if (stadium != null)
    {
        Console.WriteLine($"{stadium.Name1}, {stadium.Name2}, Kapacitás: {stadium.Capacity}");
        break;
    }
    else
    {
        Console.WriteLine("Nincs ilyen város a listában.");
    }
}

var allCities = vb2018.Select(s => s.City).Distinct().ToList();
Console.WriteLine($"Az összes város amiben vb rendeztek {allCities.Count}");

