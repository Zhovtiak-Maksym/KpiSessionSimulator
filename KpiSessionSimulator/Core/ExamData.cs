using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Core
{
    public class ExamData
    {
        public BasicTeacher Teacher { get; set; }
        public List<Question> Questions { get; set; }
    }
}
