using NB1;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

// Adatok beolvas�sa
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
    
    Console.WriteLine($"�sszesen {players.Count} j�t�kos adatait t�lt�tt�k be.\n");
}
catch (Exception ex)
{
    Console.WriteLine($"Hiba az adatok beolvas�sa sor�n: {ex.Message}");
    return;
}
// a) A kapusokon k�v�l mindenkit mez?nyj�t�kosnak tekint�nk. 
//    Keresse ki a legid?sebb mez?nyj�t�kos vezet�k- �s ut�nev�t, 
//    valamint sz�let�si d�tum�t! 
//    (Felt�telezheti, hogy csak egy ilyen j�t�kos van.)
Console.WriteLine("=== A) LEGID?SEBB MEZ?NYJ�T�KOS ===\n");

var legidosebb = players
 .Where(p => p.Mezonyjatekos)
    .OrderBy(p => p.SzuletesiDatum)
    .FirstOrDefault();

    Console.WriteLine($"N�v: {legidosebb.TeljesNev}");
    Console.WriteLine($"Sz�let�si d�tum: {legidosebb.SzuletesiDatum:yyyy.MM.dd.}");
    Console.WriteLine($"Kor: {legidosebb.Kor} �v");



// b) Hat�rozza meg h�ny magyar, k�lf�ldi �s kett?s �llampolg�rs�g� j�t�kos van!

int magyarok = players.Count(p => p.MagyarAllampolgar && !p.KulfoldiAllampolgar);
int kulfoldi = players.Count(p => !p.MagyarAllampolgar && p.KulfoldiAllampolgar);
int kettos = players.Count(p => p.MagyarAllampolgar && p.KulfoldiAllampolgar);

Console.WriteLine($"Magyar �llampolg�rok: {magyarok} f?");
Console.WriteLine($"K�lf�ldi �llampolg�rok: {kulfoldi} f?");
Console.WriteLine($"Kett?s �llampolg�rok: {kettos} f?");



// c) Hat�rozza meg j�t�kosok �ssz�rt�k�t csapatonk�nt �s �rja ki a k�perny?re! 
//    A csapatok neve �s a j�t�kosainak �ssz�rt�ke jelenjen meg!


var osszertekek = players
    .GroupBy(p => p.KlubNeve)
.Select(g => new { Csapat = g.Key, Osszertek = g.Sum(p => p.Ertek) })
 .OrderByDescending(x => x.Osszertek);

foreach (var csapat in osszertekek)
{
    Console.WriteLine($"{csapat.Csapat,-30} {csapat.Osszertek,10:N0} ezer eur�");
}



// d) Keresse ki, hogy mely csapatokn�l mely posztokon van csup�n egy 
//    szerz?dtetett j�t�kos! �rja ki a csapat nevet �s a posztot amire 
// csak egy j�t�kost szerz?dtettek!


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
    Console.WriteLine("Nincs olyan csapat, ahol csak egy j�t�kos lenne egy poszton.");
}



// e) Keress�k ki azon j�t�kosokat, akiknek az �rt�k�k nem haladja meg 
//    a j�t�kosok �rt�k�nek �tlag �rt�k�t.


double atlag = players.Average(p => p.Ertek);
Console.WriteLine($"J�t�kosok �rt�k�nek �tlaga: {atlag:N2} ezer eur�\n");

var atlagAlattiJatekosok = players
    .Where(p => p.Ertek <= atlag)
    .OrderBy(p => p.Ertek);

Console.WriteLine($"�tlag alatti vagy �tlagos �rt�k? j�t�kosok ({atlagAlattiJatekosok.Count()} f?):\n");

foreach (var jatekos in atlagAlattiJatekosok)
{
 Console.WriteLine($"{jatekos.TeljesNev,-30} {jatekos.Ertek,8:N0} ezer � - {jatekos.KlubNeve}");
}



// f) �rja ki azon j�t�kosok nev�t, sz�let�si d�tum�t �s csapataik nev�t, 
//    akik 18 �s 21 �v k�zt vannak �s magyar �llampolg�rok. 
//    Ha nincs ilyen, akkor megfelel? �zenettel helyettes�tse a kimenetet.


var fiatalMagyarok = players
    .Where(p => p.MagyarAllampolgar && p.Kor >= 18 && p.Kor <= 21)
    .OrderBy(p => p.Kor)
    .ThenBy(p => p.Vezeteknev);

foreach (var jatekos in fiatalMagyarok)
{
    Console.WriteLine($"{jatekos.TeljesNev,-30} {jatekos.SzuletesiDatum:yyyy.MM.dd.} ({jatekos.Kor} �v) - {jatekos.KlubNeve}");
}





// g) A �hazai.txt" illetve a �legios.txt" �llom�nyokba keresse ki a magyar, 
//    illetve a k�lf�ldi �llampolg�rs�g� j�t�kosokat csapatonk�nt. 
//    A sz�veges �llom�nyoknak tartalmazniuk kell a csapat nev�t majd alatta 
//    felsorolva a j�t�kosok teljes nev�t, poszt nevet �s �rt�k�ket.
try
{
    // hazai.txt - Magyar �llampolg�rok
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
      sw.WriteLine($"{jatekos.TeljesNev,-30} {jatekos.PosztNeve,-20} {jatekos.Ertek,8:N0} ezer �");
            }
            
   sw.WriteLine();
        }
    }
    Console.WriteLine("? hazai.txt sikeresen l�trehozva (magyar j�t�kosok)");

    // legios.txt - K�lf�ldi �llampolg�rok
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
     sw.WriteLine($"  {jatekos.TeljesNev,-30} {jatekos.PosztNeve,-20} {jatekos.Ertek,8:N0} ezer �");
      }
      
       sw.WriteLine();
 }
    }
    Console.WriteLine("? legios.txt sikeresen l�trehozva (k�lf�ldi j�t�kosok)");
}
catch (Exception ex)
{
  Console.WriteLine($"Hiba a f�jlok �r�sa sor�n: {ex.Message}");
}
