namespace KpiSessionSimulator.Models
{
    public class Player
    {
        public string NickName { get; set; }
        public string Password { get; set; }    
        public PlayerStats Stats { get; set; }
        public List<string> Achievements { get; set; }
        public string Faculty { get; set; }

        public Player()
        {
            Achievements = new List<string>();
            Faculty = "ФПСПМ";
        }
    }
}
