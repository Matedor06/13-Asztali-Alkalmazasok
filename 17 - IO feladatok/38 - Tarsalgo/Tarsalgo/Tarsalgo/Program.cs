using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tarsalgo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Beolvasás
            List<Entry> entries = new List<Entry>();
            foreach (string line in File.ReadAllLines("ajto.txt"))
            {
                entries.Add(new Entry(line));
            }

            // 2. Első belépő és utolsó kilépő
            Console.WriteLine("2. feladat");
            int firstEntry = entries.First(e => e.Direction == "be").PersonId;
            int lastExit = entries.Last(e => e.Direction == "ki").PersonId;
            Console.WriteLine($"Az elso belepo: {firstEntry}");
            Console.WriteLine($"Az utolso kilepo: {lastExit}");

            // 3. Áthaladások száma
            Dictionary<int, int> passageCount = new Dictionary<int, int>();
            foreach (var entry in entries)
            {
                if (!passageCount.ContainsKey(entry.PersonId))
                    passageCount[entry.PersonId] = 0;
                passageCount[entry.PersonId]++;
            }

            using (StreamWriter writer = new StreamWriter("athaladas.txt"))
            {
                foreach (var kvp in passageCount.OrderBy(x => x.Key))
                {
                    writer.WriteLine($"{kvp.Key} {kvp.Value}");
                }
            }

            // Kik vannak bent a végén
            HashSet<int> insideNow = new HashSet<int>();
            foreach (var entry in entries)
            {
                if (entry.Direction == "be")
                    insideNow.Add(entry.PersonId);
                else
                    insideNow.Remove(entry.PersonId);
            }

            Console.WriteLine("4. feladat");
            Console.WriteLine("A vegen a tarsalgoban voltak: " + string.Join(" ", insideNow.OrderBy(x => x)));

            // 5. Legtöbben bent
            int currentInside = 0;
            int maxInside = 0;
            string maxTime = "";

            foreach (var entry in entries)
            {
                if (entry.Direction == "be")
                    currentInside++;
                else
                    currentInside--;

                if (currentInside > maxInside)
                {
                    maxInside = currentInside;
                    maxTime = entry.TimeString;
                }
            }

            Console.WriteLine("5. feladat");
            Console.WriteLine($"Peldaul {maxTime}-kor voltak a legtobben a tarsalgoban.");

            // 6. Személy bekérése
            Console.WriteLine("6. feladat");
            Console.Write("Adja meg a szemely azonositojat! ");
            int personId = int.Parse(Console.ReadLine());

            // 7. Időszakok
            Console.WriteLine("7. feladat");
            List<Entry> personEntries = entries.Where(e => e.PersonId == personId).ToList();
            
            List<string> periods = new List<string>();
            for (int i = 0; i < personEntries.Count; i += 2)
            {
                string start = personEntries[i].TimeString;
                string end = (i + 1 < personEntries.Count) ? personEntries[i + 1].TimeString : "";
                if (end == "")
                    periods.Add($"{start}-");
                else
                    periods.Add($"{start}-{end}");
            }
            Console.WriteLine(string.Join(" ", periods));

            // 8. Összesen mennyi idő
            Console.WriteLine("8. feladat");
            int totalMinutes = 0;
            bool stillInside = false;

            for (int i = 0; i < personEntries.Count; i += 2)
            {
                if (i + 1 < personEntries.Count)
                {
                    int entryTime = personEntries[i].TotalMinutes;
                    int exitTime = personEntries[i + 1].TotalMinutes;
                    totalMinutes += exitTime - entryTime;
                }
                else
                {
                    // Még bent van a megfigyelés végén (15:00 = 900 perc)
                    int entryTime = personEntries[i].TotalMinutes;
                    totalMinutes += 900 - entryTime;
                    stillInside = true;
                }
            }

            string insideText = stillInside ? "a tarsalgoban volt" : "nem volt a tarsalgoban";
            Console.WriteLine($"A(z) {personId}. szemely osszesen {totalMinutes} percet volt bent, a megfigyeles vegen {insideText}.");
        }
    }
}
