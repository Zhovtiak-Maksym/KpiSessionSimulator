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
        public int EagleEyeCount { get; set; }
        public int ImmunityCount { get; set; }
        public int LoyaltyCount { get; set; }
        public int TrickyHandsCount { get; set; }
    }
}
