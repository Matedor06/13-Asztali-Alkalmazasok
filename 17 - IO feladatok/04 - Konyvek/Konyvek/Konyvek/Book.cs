using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konyvek;

public class Book
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }

    public string Title { get; set; }

    public string ISBN { get; set; }

    public string publisher { get; set; }

    public int PublishYear { get; set; }

    public int Price { get; set; }

    public string Theme { get; set; }

    public int PagNumber { get; set; }

    public int Honoratium { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} {Birthday:yyyy-MM-dd} {Title} {ISBN} {publisher} {PublishYear} {Price} {Theme} {PagNumber} {Honoratium}";
    }

    public string ToFullString()
    {
        return $"{FirstName}\t{LastName}\t{Birthday:yyyy-MM-dd}\t{Title}\t{ISBN}\t{publisher}\t{PublishYear}\t{Price}\t{Theme}\t{PagNumber}\t{Honoratium}";
    }
}
