using System.Text.Json.Serialization; 

namespace KpiSessionSimulator.Models
{
    public class Player
    {
        public string NickName { get; set; }
        public string Password { get; set; }
        public PlayerStats Stats { get; set; }
        public string Faculty { get; set; }

        [JsonIgnore]
        public int WrongAnswersStreak { get; set; }

        public Player()
        {
            Faculty = "FPSPM";
            WrongAnswersStreak = 0;

            Stats = new PlayerStats
            {
                PassedExams = 0,
                Tokens = 30,
                Deaths = 0,
                IsONSecondary = false,
                IsExpelled = false,
                EagleEyeCount = 0,
                ImmunityCount = 0,
                LoyaltyCount = 0,
                TrickyHandsCount = 0
            };
        }
    }
}
