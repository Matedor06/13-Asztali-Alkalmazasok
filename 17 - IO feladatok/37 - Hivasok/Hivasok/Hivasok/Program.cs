using Hivasok;
using System.Text;

var fileData = await File.ReadAllLinesAsync("hivasok.txt", Encoding.UTF8);
var calls = new List<Call>();

for (int i = 0; i < fileData.Length; i += 2)
{
    var timeData = fileData[i].Split(' ');
    var phoneNumber = fileData[i + 1];

    calls.Add(new Call
    {
        StartHour = int.Parse(timeData[0]),
        StartMinute = int.Parse(timeData[1]),
        StartSecond = int.Parse(timeData[2]),
        EndHour = int.Parse(timeData[3]),
        EndMinute = int.Parse(timeData[4]),
        EndSecond = int.Parse(timeData[5]),
        PhoneNumber = phoneNumber
    });
}

Console.WriteLine("1. Feladat: Adjon meg egy telefonszámot:");
string inputPhone = Console.ReadLine();
var testCall = new Call { PhoneNumber = inputPhone };
Console.WriteLine($"A {inputPhone} telefonszám {(testCall.IsMobile() ? "mobil" : "vezetékes")} szám.\n");

Console.WriteLine("2. Feladat: Adja meg a hívás kezdeti időpontját (óra perc másodperc):");
var startInput = Console.ReadLine().Split(' ');
Console.WriteLine("Adja meg a hívás vége időpontját (óra perc másodperc):");
var endInput = Console.ReadLine().Split(' ');

var testCall2 = new Call
{
    StartHour = int.Parse(startInput[0]),
    StartMinute = int.Parse(startInput[1]),
    StartSecond = int.Parse(startInput[2]),
    EndHour = int.Parse(endInput[0]),
    EndMinute = int.Parse(endInput[1]),
    EndSecond = int.Parse(endInput[2])
};

Console.WriteLine($"A számlázott percek száma: {testCall2.GetBilledMinutes()} perc\n");

var percekLines = new List<string>();
foreach (var call in calls)
{
    percekLines.Add($"{call.GetBilledMinutes()} {call.PhoneNumber}");
}
await File.WriteAllLinesAsync("percek.txt", percekLines, Encoding.UTF8);
Console.WriteLine("3. Feladat: A számlázott percek elmentve a percek.txt fájlba.\n");

int peakCalls = calls.Count(c => c.IsPeakTime());
int offPeakCalls = calls.Count(c => !c.IsPeakTime());
Console.WriteLine("4. Feladat:");
Console.WriteLine($"Csúcsidőben: {peakCalls} hívás");
Console.WriteLine($"Csúcsidőn kívül: {offPeakCalls} hívás\n");

int mobileMinutes = calls.Where(c => c.IsMobile()).Sum(c => c.GetBilledMinutes());
int landlineMinutes = calls.Where(c => !c.IsMobile()).Sum(c => c.GetBilledMinutes());
Console.WriteLine("5. Feladat:");
Console.WriteLine($"Mobil számmal beszélt percek: {mobileMinutes} perc");
Console.WriteLine($"Vezetékes számmal beszélt percek: {landlineMinutes} perc\n");

double peakCost = calls.Where(c => c.IsPeakTime()).Sum(c => c.GetCost());
Console.WriteLine("6. Feladat:");
Console.WriteLine($"A csúcsdíjas hívások összege: {peakCost:F2} Ft\n");

Console.WriteLine("Nyomjon meg egy billentyűt a kilépéshez...");
Console.ReadKey();
