namespace KpiSessionSimulator.Models
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }   
        public int IndexOfCorrectAnswer { get; set; }
        public Difficulty Difficulty { get; set; }

        public Question()
        {
            Options = new List<string>();
            Difficulty = Difficulty.Easy;
        }
    }
}
