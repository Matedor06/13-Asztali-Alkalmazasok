
using StarTrek;
using System.Globalization;
using System.Text;

var fileData = await File.ReadAllLinesAsync("star_trek_ships.csv", Encoding.UTF8);
var ships = new List<Ship>();
foreach (var line in fileData)
{
        if (line == fileData[0]) continue;
    var data = line.Split(',');
    ships.Add(new Ship
    {
        Name = data[0],
        Class = data[1],
        RaceFaction = data[2],
        Length = int.Parse(data[3]),
        Crew = int.Parse(data[4]),
        MaxWarp = double.Parse(data[5],CultureInfo.GetCultureInfo("en-EN")),
        Armament = data[6],
        ShieldType = data[7],
        HullMaterial = data[8],
        Role = data[9]
    });
}

//2 feladat Hány hajó található az adatbázisban.
var countOfShips = ships.Count;
Console.WriteLine(countOfShips);

//3feladat
//Számold ki és írd ki:
//Az összes hajón szolgáló legénység összesített létszámát.

var countOfMen = ships.Sum(ship => ship.Crew);
Console.WriteLine(countOfMen);

//4.Legnagyobb hajó keresése(1 pont)
var longestShip = ships.OrderByDescending(ship => ship.Length).First();
Console.WriteLine($"A leghosszabb hajó: {longestShip.Name}, Hossza: {longestShip.Length}");

//5. Hajók száma frakciónként (1 pont)


var factionCounts = ships.GroupBy(ship => ship.RaceFaction)
                         .Select(group => new { Faction = group.Key, Count = group.Count() });

foreach (var faction in factionCounts)
    {
    Console.WriteLine($"{faction.Faction}: {faction.Count}");
}


//6.Warp 9 feletti hajók listázása (1 pont)


var fastShips = ships.Where(x => x.MaxWarp > 9).ToList();
foreach (var ship in fastShips)
{
    Console.WriteLine($"{ship.Name}: {ship.MaxWarp}");
}

//7. Szerepkör (role) szerinti csoportosítás (1 pont)


var roleCounts = ships.GroupBy(ship => ship.Role)
                      .Select(group => new { Role = group.Key, Count = group.Count() });
foreach (var role in roleCounts)
{
    Console.WriteLine($"{role.Role}: {role.Count}");
}


//8. Átlagos hajóhossz kiszámítása (1 pont)

var averageLength = ships.Average(ship => ship.Length);
Console.WriteLine($"Az átlagos hajóhossz: {averageLength}");

//9. Legnagyobb legénységű hajó frakciónként (1 pont)


var largestCrewByFaction = ships.GroupBy(ship => ship.RaceFaction).Select(group => new
    {
        Faction = group.Key,
        LargestCrewShip = group.OrderByDescending(ship => ship.Crew).First()
    });

foreach (var faction in largestCrewByFaction)
{
    Console.WriteLine($"{faction.Faction}: {faction.LargestCrewShip.Name} (Crew: {faction.LargestCrewShip.Crew})");
}