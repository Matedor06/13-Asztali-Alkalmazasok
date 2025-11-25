using System.Text;
using Snooker;

// 2. feladat: Adatok beolvasása
List<Snooker.Snooker> versenyzok = new List<Snooker.Snooker>();
string[] sorok = File.ReadAllLines(@"c:\Users\Admin\Desktop\github\13-Asztali-Alkalmazasok\17 - IO feladatok\13 - Snooker\snooker.txt", Encoding.UTF8);

for (int i = 1; i < sorok.Length; i++)
{
    versenyzok.Add(new Snooker.Snooker(sorok[i]));
}

// 3. feladat: Versenyzők száma
Console.WriteLine($"3. feladat: A világranglistán {versenyzok.Count} versenyző szerepel.");

// 4. feladat: Átlagos bevétel
double atlag = versenyzok.Average(v => v.Nyeremeny);
Console.WriteLine($"4. feladat: A versenyzők átlagosan {atlag:F2} font bevételre tettek szert.");

// 5. feladat: Legjobban kereső kínai játékos
var kinaiJatekosok = versenyzok.Where(v => v.Orszag == "Kína");
var legjobbKinai = kinaiJatekosok.OrderByDescending(v => v.Nyeremeny).First();
Console.WriteLine("5. feladat: A legjobban kereső kínai játékos:");
Console.WriteLine($"\tNév: {legjobbKinai.Nev}");
Console.WriteLine($"\tHelyezés: {legjobbKinai.Helyezes}");
Console.WriteLine($"\tNyeremény: {legjobbKinai.Nyeremeny * 380} Ft");

// 6. feladat: Norvég játékos
bool vanNorveg = versenyzok.Any(v => v.Orszag == "Norvégia");
if (vanNorveg)
{
    Console.WriteLine("6. feladat: Van norvég játékos a világranglistán.");
}
else
{
    Console.WriteLine("6. feladat: Nincs norvég játékos a világranglistán.");
}

// 7. feladat: Statisztika országok szerint
Console.WriteLine("7. feladat: Statisztika");
var orszagStatisztika = versenyzok
    .GroupBy(v => v.Orszag)
    .Where(g => g.Count() > 4)
    .OrderByDescending(g => g.Count());

foreach (var orszag in orszagStatisztika)
{
    Console.WriteLine($"\t{orszag.Key} - {orszag.Count()} fő");
}