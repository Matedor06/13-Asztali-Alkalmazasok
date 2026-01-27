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

// 4 - 5 feladatot már megcsináltad csak nem pusholtad fel

var groupByCity = weather.GroupBy(x => x.CityId).ToDictionary(g => g.Key, g => g.ToList());

var stringBuilder = new StringBuilder();

foreach (var group in groupByCity)
{
    stringBuilder.AppendLine($"{group.Key}");

    foreach(var measurement in group.Value)
    {
        stringBuilder.Append($"   {measurement.Time} ");
        var strength = int.Parse(measurement.Speed);
        for(int i = 0; i < strength; i += 1)
        {
            stringBuilder.Append("#");
        }
        stringBuilder.AppendLine();

    }
}

await File.WriteAllTextAsync("x.txt", stringBuilder.ToString(), Encoding.UTF8);