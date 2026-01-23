using System.Buffers.Text;
using System.Text;
using weatherforecast;

var fileData = await File.ReadAllLinesAsync("weather.txt", Encoding.UTF8);
var weather = new List<Weather>();
foreach (var line in fileData)
{
    var data = line.Split(' ');
    weather.Add(new Weather
    {
        CityId = data[0],
        Time = $"{data[1].Substring(0, 2)}:{data[1].Substring(2, 2)}",
        Direction = data[2].Substring(0, 3),
        Speed = data[2].Substring(3),
        Temperature = int.Parse(data[3])
    });
}

Console.WriteLine("2. feladat");
Console.Write("Adja meg egy település kódját! Település: ");
string cityId = Console.ReadLine();
var lastmeasurementofTheCity = weather.Where(x => x.CityId == cityId).OrderByDescending(x => x.Time).Select(x => x.Time).FirstOrDefault();
Console.WriteLine($"Az utolsó mérési adat a megadott településről {lastmeasurementofTheCity}-kor érkezett.");
Console.WriteLine("3. feladat");

var leastTemperatureCity = weather.MinBy(x => x.Temperature);
Console.WriteLine($"A legalacsonyabb hőmérséklet: {leastTemperatureCity.CityId} {leastTemperatureCity.Time} {leastTemperatureCity.Temperature} fok.");


