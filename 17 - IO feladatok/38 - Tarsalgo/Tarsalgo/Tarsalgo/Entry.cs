namespace Tarsalgo
{
    internal class Entry
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int PersonId { get; set; }
        public string Direction { get; set; }

        public Entry(string line)
        {
            string[] parts = line.Split(' ');
            Hour = int.Parse(parts[0]);
            Minute = int.Parse(parts[1]);
            PersonId = int.Parse(parts[2]);
            Direction = parts[3];
        }

        public int TotalMinutes => Hour * 60 + Minute;

        public string TimeString => $"{Hour}:{Minute:D2}";
    }
}
