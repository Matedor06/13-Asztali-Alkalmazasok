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