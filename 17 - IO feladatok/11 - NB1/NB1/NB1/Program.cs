using NB1;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

// Adatok beolvasása
List<Player> players = new List<Player>();
try
{
    string[] lines = File.ReadAllLines("adatok.txt");
    foreach (string line in lines)
    {
   string[] parts = line.Split('\t');
        if (parts.Length >= 9)
 {
 players.Add(new Player
         {
                KlubNeve = parts[0],
      MezSzam = int.Parse(parts[1]),
      Utonev = parts[2],
                Vezeteknev = parts[3],
                SzuletesiDatum = DateTime.Parse(parts[4]),
MagyarAllampolgar = parts[5] == "-1",
        KulfoldiAllampolgar = parts[6] == "-1",
       Ertek = int.Parse(parts[7]),
    PosztNeve = parts[8]
         });
        }
    }
    
    Console.WriteLine($"Összesen {players.Count} játékos adatait töltöttük be.\n");
}
catch (Exception ex)
{
    Console.WriteLine($"Hiba az adatok beolvasása során: {ex.Message}");
    return;
}
// a) A kapusokon kívül mindenkit mez?nyjátékosnak tekintünk. 
//    Keresse ki a legid?sebb mez?nyjátékos vezeték- és utónevét, 
//    valamint születési dátumát! 
//    (Feltételezheti, hogy csak egy ilyen játékos van.)
Console.WriteLine("=== A) LEGID?SEBB MEZ?NYJÁTÉKOS ===\n");

var legidosebb = players
 .Where(p => p.Mezonyjatekos)
    .OrderBy(p => p.SzuletesiDatum)
    .FirstOrDefault();

    Console.WriteLine($"Név: {legidosebb.TeljesNev}");
    Console.WriteLine($"Születési dátum: {legidosebb.SzuletesiDatum:yyyy.MM.dd.}");
    Console.WriteLine($"Kor: {legidosebb.Kor} év");



// b) Határozza meg hány magyar, külföldi és kett?s állampolgárságú játékos van!

int magyarok = players.Count(p => p.MagyarAllampolgar && !p.KulfoldiAllampolgar);
int kulfoldi = players.Count(p => !p.MagyarAllampolgar && p.KulfoldiAllampolgar);
int kettos = players.Count(p => p.MagyarAllampolgar && p.KulfoldiAllampolgar);

Console.WriteLine($"Magyar állampolgárok: {magyarok} f?");
Console.WriteLine($"Külföldi állampolgárok: {kulfoldi} f?");
Console.WriteLine($"Kett?s állampolgárok: {kettos} f?");



// c) Határozza meg játékosok összértékét csapatonként és írja ki a képerny?re! 
//    A csapatok neve és a játékosainak összértéke jelenjen meg!


var osszertekek = players
    .GroupBy(p => p.KlubNeve)
.Select(g => new { Csapat = g.Key, Osszertek = g.Sum(p => p.Ertek) })
 .OrderByDescending(x => x.Osszertek);

foreach (var csapat in osszertekek)
{
    Console.WriteLine($"{csapat.Csapat,-30} {csapat.Osszertek,10:N0} ezer euró");
}



// d) Keresse ki, hogy mely csapatoknál mely posztokon van csupán egy 
//    szerz?dtetett játékos! Írja ki a csapat nevet és a posztot amire 
// csak egy játékost szerz?dtettek!


var egyediPosztok = players
 .GroupBy(p => new { p.KlubNeve, p.PosztNeve })
    .Where(g => g.Count() == 1)
    .Select(g => new { g.Key.KlubNeve, g.Key.PosztNeve })
    .OrderBy(x => x.KlubNeve)
    .ThenBy(x => x.PosztNeve);

if (egyediPosztok.Any())
{
    foreach (var item in egyediPosztok)
    {
        Console.WriteLine($"{item.KlubNeve,-30} - {item.PosztNeve}");
    }
}
else
{
    Console.WriteLine("Nincs olyan csapat, ahol csak egy játékos lenne egy poszton.");
}



// e) Keressük ki azon játékosokat, akiknek az értékük nem haladja meg 
//    a játékosok értékének átlag értékét.


double atlag = players.Average(p => p.Ertek);
Console.WriteLine($"Játékosok értékének átlaga: {atlag:N2} ezer euró\n");

var atlagAlattiJatekosok = players
    .Where(p => p.Ertek <= atlag)
    .OrderBy(p => p.Ertek);

Console.WriteLine($"Átlag alatti vagy átlagos érték? játékosok ({atlagAlattiJatekosok.Count()} f?):\n");

foreach (var jatekos in atlagAlattiJatekosok)
{
 Console.WriteLine($"{jatekos.TeljesNev,-30} {jatekos.Ertek,8:N0} ezer € - {jatekos.KlubNeve}");
}



// f) Írja ki azon játékosok nevét, születési dátumát és csapataik nevét, 
//    akik 18 és 21 év közt vannak és magyar állampolgárok. 
//    Ha nincs ilyen, akkor megfelel? üzenettel helyettesítse a kimenetet.


var fiatalMagyarok = players
    .Where(p => p.MagyarAllampolgar && p.Kor >= 18 && p.Kor <= 21)
    .OrderBy(p => p.Kor)
    .ThenBy(p => p.Vezeteknev);

foreach (var jatekos in fiatalMagyarok)
{
    Console.WriteLine($"{jatekos.TeljesNev,-30} {jatekos.SzuletesiDatum:yyyy.MM.dd.} ({jatekos.Kor} év) - {jatekos.KlubNeve}");
}





// g) A „hazai.txt" illetve a „legios.txt" állományokba keresse ki a magyar, 
//    illetve a külföldi állampolgárságú játékosokat csapatonként. 
//    A szöveges állományoknak tartalmazniuk kell a csapat nevét majd alatta 
//    felsorolva a játékosok teljes nevét, poszt nevet és értéküket.
try
{
    // hazai.txt - Magyar állampolgárok
    using (StreamWriter sw = new StreamWriter("hazai.txt", false, Encoding.UTF8))
    {
    var magyarCsapatok = players
            .Where(p => p.MagyarAllampolgar && !p.KulfoldiAllampolgar)
            .GroupBy(p => p.KlubNeve)
            .OrderBy(g => g.Key);

        foreach (var csapat in magyarCsapatok)
        {
            sw.WriteLine(csapat.Key);
            sw.WriteLine(new string('-', csapat.Key.Length));
    
   foreach (var jatekos in csapat.OrderBy(p => p.Vezeteknev))
            {
      sw.WriteLine($"{jatekos.TeljesNev,-30} {jatekos.PosztNeve,-20} {jatekos.Ertek,8:N0} ezer €");
            }
            
   sw.WriteLine();
        }
    }
    Console.WriteLine("? hazai.txt sikeresen létrehozva (magyar játékosok)");

    // legios.txt - Külföldi állampolgárok
    using (StreamWriter sw = new StreamWriter("legios.txt", false, Encoding.UTF8))
    {
        var kulfoldiCsapatok = players
        .Where(p => p.KulfoldiAllampolgar)
   .GroupBy(p => p.KlubNeve)
  .OrderBy(g => g.Key);

        foreach (var csapat in kulfoldiCsapatok)
     {
            sw.WriteLine(csapat.Key);
       sw.WriteLine(new string('-', csapat.Key.Length));
          
     foreach (var jatekos in csapat.OrderBy(p => p.Vezeteknev))
     {
     sw.WriteLine($"  {jatekos.TeljesNev,-30} {jatekos.PosztNeve,-20} {jatekos.Ertek,8:N0} ezer €");
      }
      
       sw.WriteLine();
 }
    }
    Console.WriteLine("? legios.txt sikeresen létrehozva (külföldi játékosok)");
}
catch (Exception ex)
{
  Console.WriteLine($"Hiba a fájlok írása során: {ex.Message}");
}
