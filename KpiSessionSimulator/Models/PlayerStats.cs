namespace KpiSessionSimulator.Models
{
    public struct PlayerStats
    {
        public int PassedExams { get; set; }
        public int Tokens { get; set; }
        public int Deaths { get; set; }
        public List<string> Achievements { get; set; }
        public bool IsONSecondary { get; set; }
        public bool IsExpelled { get; set; }
    }
}
