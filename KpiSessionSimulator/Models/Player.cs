namespace KpiSessionSimulator.Models
{
    public class Player
    {
        public string NickName { get; set; }
        public string Password { get; set; }

        public PlayerStats Stats;     
        public string Faculty { get; set; }     
        public int WrongAnswersStreak { get; set; }

        public Player()
        {
            Faculty = "ФПСПМ";
            WrongAnswersStreak = 0;
            Stats = new PlayerStats();
            Stats.PassedExams = 0;
            Stats.Tokens = 30;
            Stats.Deaths = 0;
            Stats.Achievements = new List<string>();
            Stats.IsONSecondary = false;
            Stats.IsExpelled = false;
            Stats.Retakes = 0;
        }
    }
}
