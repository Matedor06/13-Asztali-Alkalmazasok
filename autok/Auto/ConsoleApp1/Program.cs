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

Console.Write("Nap: ");
int day = int.Parse(Console.ReadLine());
var specificDay = lend.Where(x => x.day == day);
foreach (var item in specificDay)
{
    Console.Write($"{item.dayTime} {item.registrationNumber} {item.employeeNumber} ");
    if (item.inOut == 0)
    {
        Console.Write("ki");
    }
    else
    {
        Console.WriteLine("be");
    }
    Console.WriteLine();
}

var countNotBroughtBackCars = lend
    .GroupBy(x => x.registrationNumber)
    .Where(g => g.OrderByDescending(x => x.day).ThenByDescending(x => x.dayTime).First().inOut == 0)
    .Count();
Console.WriteLine($"A hónap végén {countNotBroughtBackCars} autó nem volt bent a parkolóban.");


Console.WriteLine("6. feladat");
var carsWithMinKm = lend
    .GroupBy(x => x.registrationNumber)
    .Select(g => new
    {
        RegistrationNumber = g.Key,
        MinKm = g.Min(x => x.kmCounter),
    })
    .OrderBy(x => x.RegistrationNumber)
    .ToList();

var carsWithMaxKm = lend
    .GroupBy(x => x.registrationNumber)
    .Select(g => new
    {
        RegistrationNumber = g.Key,
        MinKm = g.Max(x => x.kmCounter),
    })
    .OrderBy(x=> x.RegistrationNumber)
    .ToList();

for(int i = 0; i < carsWithMinKm.Count; i++)
{
    Console.WriteLine($"{carsWithMaxKm[i].RegistrationNumber} , {carsWithMaxKm[i].MinKm - carsWithMinKm[i].MinKm}");
}

// Robusztusabb megoldás: minden autó-dolgozó kombinációhoz keressük az OUT-IN párokat
var trips = new List<Trip>();

// Rendezzük az adatokat időrendi sorrendben
var sortedLend = lend.OrderBy(x => x.day).ThenBy(x => x.dayTime).ToList();

for (int i = 0; i < sortedLend.Count; i++)
{
    if (sortedLend[i].inOut == 0) // Ha OUT rekord
    {
        // Keressük meg a következő IN rekordot ugyanazzal az autóval és dolgozóval
        for (int j = i + 1; j < sortedLend.Count; j++)
        {
            if (sortedLend[j].registrationNumber == sortedLend[i].registrationNumber &&
                sortedLend[j].employeeNumber == sortedLend[i].employeeNumber &&
                sortedLend[j].inOut == 1)
            {
                int distance = sortedLend[j].kmCounter - sortedLend[i].kmCounter;
                trips.Add(new Trip 
                { 
                    EmployeeNumber = sortedLend[i].employeeNumber, 
                    Distance = distance 
                });
                break; // Megtaláltuk a párt, továbblépünk
            }
        }
    }
}

if (trips.Count > 0)
{
    var maxTrip = trips.MaxBy(t => t.Distance);
    Console.WriteLine($"Leghosszabb távolság egy úton: {maxTrip.EmployeeNumber} személy, {maxTrip.Distance} km");
}
else
{
    Console.WriteLine("Nincs befejezett út.");
}

// Menetlevél készítése
Console.Write("\nAdjon meg egy rendszámot a menetlevélhez: ");
string registrationNumber = Console.ReadLine();

var carRecords = sortedLend.Where(x => x.registrationNumber == registrationNumber).ToList();

if (carRecords.Count == 0)
{
    Console.WriteLine("Nincs ilyen rendszámú autó az adatbázisban.");
}
else
{
    string fileName = $"{registrationNumber}_menetlevel.txt";
    
    using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
    {
        foreach (var outRecord in carRecords.Where(x => x.inOut == 0))
        {
            var inRecord = carRecords
                .FirstOrDefault(x => x.inOut == 1 && 
                                    x.employeeNumber == outRecord.employeeNumber &&
                                    (x.day > outRecord.day || 
                                     (x.day == outRecord.day && x.dayTime.CompareTo(outRecord.dayTime) > 0)));
            
            // Írjuk ki az adatokat
            writer.Write($"{outRecord.employeeNumber}\t{outRecord.day}. {outRecord.dayTime}\t{outRecord.kmCounter} km");
            
            if (inRecord != null)
            {
                writer.WriteLine($"\t{inRecord.day}. {inRecord.dayTime}\t{inRecord.kmCounter} km");
            }
            else
            {
                writer.WriteLine(); // Még kint van
            }
        }
    }
    
    Console.WriteLine($"Menetlevél elkészült: {fileName}");
}


