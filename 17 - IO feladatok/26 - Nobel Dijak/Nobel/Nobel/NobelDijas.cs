namespace Nobel;

public class NobelDijas
{
    public int Ev { get; set; }
    public string Tipus { get; set; } = "";
    public string Keresztnev { get; set; } = "";
    public string Vezeteknev { get; set; } = "";

    public override string ToString()
    {
        return $"{Ev}\t{Tipus}\t{Keresztnev}\t{Vezeteknev}";
    }
}
