using Konyvek;
using System.Text;

var fileData = await  File.ReadAllLinesAsync("adatok.txt",Encoding.UTF7);
var books = new List<Book>();
foreach (var line in fileData)
    {
    //Console.WriteLine(line);
    var data = line.Split('\t');
    books.Add(new Book
    {
        FirstName = data[0],
        LastName = data[1],
        Birthday = DateTime.Parse(data[2]),
        Title = data[3],
        ISBN = data[4],
        publisher = data[5],
        PublishYear = int.Parse(data[6]),
        Price = int.Parse(data[7]),
        Theme = data[8],
        PagNumber = int.Parse(data[9]),
        Honoratium = int.Parse(data[10])
    });
}

//Írjuk ki a képernyőre az össz adatot
foreach (var book in books)
    {
    Console.WriteLine(book);
}


//Keressük ki az informatika témajú könyveket és mentsük el őket az informatika.txt állömányba
var informationBooks = fileData.Where(x => x.Contains("informatika")).ToList();
File.WriteAllLinesAsync("informatika.txt", informationBooks, Encoding.UTF8);

//Az 1900.txt állományba mentsük el azokat a könyveket amelyek az 1900-as években íródtak
var booksPublishedIn1900s = books.Where(x => x.PublishYear >= 1900 && x.PublishYear < 2000).Select(x => x.ToFullString()).ToList();
File.WriteAllLinesAsync("1900.txt", booksPublishedIn1900s, Encoding.UTF8);

//Rendezzük az adatokat a könyvek oldalainak száma szerint csökkenő sorrendbe és a sorbarakott.txt állományba mentsük el.
var booksSortedByPageNumberDesc = books.OrderByDescending(x => x.PagNumber).Select(x => x.ToString()).ToList();
File.WriteAllLinesAsync("sorbarakott.txt", booksSortedByPageNumberDesc, Encoding.UTF8);

//„kategoriak.txt” állományba mentse el a könyveket téma szerint.






Console.ReadKey();