using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Core
{
    public class ExamState
    {
        public int CorrectAnswers { get; set; }
        public Difficulty CurrentDifficulty { get; set; }
        public int BlackjackLosses { get; set; }

        public ExamState()
        {
            CorrectAnswers = 0;
            CurrentDifficulty = Difficulty.Easy;
            BlackjackLosses = 0;
        }
    }
}
