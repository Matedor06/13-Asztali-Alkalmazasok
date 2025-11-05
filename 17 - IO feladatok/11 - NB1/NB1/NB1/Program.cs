using NB1;
using System.Text;

// Adatok beolvasása
var fileData = await File.ReadAllLinesAsync("adatok.txt", Encoding.UTF8);
var players = new List<Player>();
foreach (var line in fileData)
{
    var data = line.Split('\t');

    bool hasSeparateNames = data.Length == 9;

    players.Add(new Player
    {
        ClubName = data[0],
        JerseyNumber = int.Parse(data[1]),
        FirstName = hasSeparateNames ? data[2] : "",
        LastName = hasSeparateNames ? data[3] : data[2],
        BirthDate = DateTime.Parse(data[hasSeparateNames ? 4 : 3]),
        IsHungarianCitizen = int.Parse(data[hasSeparateNames ? 5 : 4]) == -1,
      IsForeignCitizen = int.Parse(data[hasSeparateNames ? 6 : 5]) == -1,
        Value = int.Parse(data[hasSeparateNames ? 7 : 6]),
        PositionName = data[hasSeparateNames ? 8 : 7]
    });
}

// Menü
bool exit = false;
while (!exit)
{
    Console.Clear();
 Console.WriteLine("=== NB1 Labdarúgó Adatbázis ===\n");
    Console.WriteLine("a) Legidősebb mezőnyjátékos");
    Console.WriteLine("b) Állampolgárság szerintistatisztika");
    Console.WriteLine("c) Csapatok összértéke");
    Console.WriteLine("d) Egyedi posztonkénti szerződések");
    Console.WriteLine("e) Átlag alatti értékű játékosok");
    Console.WriteLine("f) 18-21 év közötti magyar játékosok");
    Console.WriteLine("g) Hazai és légiósjátékosok fájlba írása");
    Console.WriteLine("0) Kilépés\n");
    Console.Write("Válasszon: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "a":
            TaskA(players);
      break;
        case "b":
   TaskB(players);
            break;
        case "c":
     TaskC(players);
  break;
 case "d":
            TaskD(players);
            break;
        case "e":
    TaskE(players);
            break;
  case "f":
         TaskF(players);
       break;
  case "g":
            await TaskG(players);
    break;
     case "0":
        exit = true;
     break;
        default:
            Console.WriteLine("Érvénytelen választás!");
            break;
    }

    if (!exit)
    {
     Console.WriteLine("\nNyomjon meg egy billentyűt a folytatáshoz...");
   Console.ReadKey();
    }
}

// a) Legidősebb mezőnyjátékos
void TaskA(List<Player> players)
{
    Console.WriteLine("\nLegidősebb mezőnyjátékos\n");

    var oldestFieldPlayer = players
      .Where(p => p.IsFieldPlayer)
      .OrderBy(p => p.BirthDate)
        .First();

    Console.WriteLine($"Név: {oldestFieldPlayer.FullName}");
    Console.WriteLine($"Születési dátum: {oldestFieldPlayer.BirthDate:yyyy.MM.dd.}");
}

// b) Állampolgárság statisztika
void TaskB(List<Player> players)
{
    Console.WriteLine("\nÁllampolgárság szerint\n");

    var hungarianCount = players.Count(p => p.IsHungarianCitizen && !p.IsForeignCitizen);
    var foreignCount = players.Count(p => !p.IsHungarianCitizen && p.IsForeignCitizen);
    var dualCount = players.Count(p => p.IsHungarianCitizen && p.IsForeignCitizen);

    Console.WriteLine($"Magyar állampolgárok: {hungarianCount} fő");
    Console.WriteLine($"Külföldi állampolgárok: {foreignCount} fő");
    Console.WriteLine($"Kettős állampolgárok: {dualCount} fő");
}

// c) Csapatok összértéke
void TaskC(List<Player> players)
{
    Console.WriteLine("\nCsapatok összértéke\n");

    var teamTotalValues = players
        .GroupBy(p => p.ClubName)
        .Select(g => new { Team = g.Key, TotalValue = g.Sum(p => p.Value) })
        .OrderByDescending(x => x.TotalValue);

    foreach (var team in teamTotalValues)
    {
        Console.WriteLine($"{team.Team}: {team.TotalValue}");
    }
}

// d) Egyedi posztonkénti szerződések
void TaskD(List<Player> players)
{
    Console.WriteLine("\nEgyedi posztonkénti szerződések\n");

    var uniquePositionPlayers = players
        .GroupBy(p => new { p.ClubName, p.PositionName })
        .Where(g => g.Count() == 1)
        .Select(g => new { Team = g.Key.ClubName, Position = g.Key.PositionName })
        .GroupBy(x => x.Team)
        .OrderBy(g => g.Key);

    foreach (var teamGroup in uniquePositionPlayers)
    {
   Console.WriteLine(teamGroup.Key);
        foreach (var position in teamGroup.OrderBy(x => x.Position))
        {
 Console.WriteLine($"  {position.Position}");
        }
        Console.WriteLine();
    }
}

// e) Átlag alatti értékű játékosok
void TaskE(List<Player> players)
{
    Console.WriteLine("\nÁtlag alatti értékű játékosok\n");

    var averageValue = players.Average(p => p.Value);

    var belowAveragePlayers = players
        .Where(p => p.Value <= averageValue)
        .OrderBy(p => p.Value);

    foreach (var player in belowAveragePlayers)
    {
   Console.WriteLine($"{player.FullName,-30} ({player.ClubName,-25}) - {player.Value,4} ezer €");
    }
}

// f) 18-21 év közötti magyar játékosok
void TaskF(List<Player> players)
{
    Console.WriteLine("\n18-21 év közötti magyar játékosok\n");

 var youngHungarians = players
   .Where(p => p.IsHungarianCitizen && p.Age >= 18 && p.Age <= 21)
        .OrderBy(p => p.Age)
        .ThenBy(p => p.FullName);

 foreach (var player in youngHungarians)
    {
     Console.WriteLine($"{player.FullName} - {player.BirthDate:yyyy.MM.dd.}");
    }
}

// g) Hazai és légiósjátékosok fájlba írása
async Task TaskG(List<Player> players)
{
    Console.WriteLine("\nHazai és légiósjátékos fájlba írása\n");

    // Hazai játékosok (csak magyar állampolgárok)
    var domesticPlayers = players
        .Where(p => p.IsHungarianCitizen && !p.IsForeignCitizen)
        .GroupBy(p => p.ClubName)
        .OrderBy(g => g.Key);

var domesticLines = new List<string>();
    foreach (var team in domesticPlayers)
    {
        domesticLines.Add(team.Key);
        domesticLines.Add(new string('-', team.Key.Length));

     foreach (var player in team.OrderBy(p => p.FullName))
        {
            domesticLines.Add($"  {player.FullName,-30} {player.PositionName,-25} {player.Value,5} ezer €");
        }

        domesticLines.Add("");
    }

    await File.WriteAllLinesAsync("hazai.txt", domesticLines, Encoding.UTF8);

    // Légiósjátékosok (külföldi állampolgársággal rendelkezők)
    var foreignPlayers = players
        .Where(p => p.IsForeignCitizen)
        .GroupBy(p => p.ClubName)
     .OrderBy(g => g.Key);

    var foreignLines = new List<string>();
    foreach (var team in foreignPlayers)
    {
        foreignLines.Add(team.Key);
        foreignLines.Add(new string('-', team.Key.Length));

        foreach (var player in team.OrderBy(p => p.FullName))
        {
            foreignLines.Add($"  {player.FullName,-30} {player.PositionName,-25} {player.Value,5} ezer €");
     }

        foreignLines.Add("");
    }

    await File.WriteAllLinesAsync("legios.txt", foreignLines, Encoding.UTF8);

    Console.WriteLine("✓ hazai.txt fájl létrehozva");
    Console.WriteLine("✓ legios.txt fájl létrehozva");
}

