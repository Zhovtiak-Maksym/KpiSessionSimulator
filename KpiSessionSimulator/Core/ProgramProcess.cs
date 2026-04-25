using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Core
{
    public class ProgramProcess
    {
        private Player Player;
        private BasicTeacher Teacher;
        private List<Question> Questions;
        private Random Rnd = new Random();
        public const int QuestionsToAnswer = 8;
        public const int QuestionsToPass = 6;

        public ProgramProcess(Player player, BasicTeacher teacher)
        {
            Player = player;
            Teacher = teacher;
            Questions = GenerateTestQuestions();
        }

        public void Exam()
        {
            Console.WriteLine($"\nВи прийшли на екзамен до {Teacher.Name} з предмету '{Teacher.Subject}'");
            Thread.Sleep(1500);
            Console.WriteLine("Сподіваємося, що ви потерли шар на поляні...");
            Thread.Sleep(1500);
            Console.WriteLine("На столі лежать білети. Вам потрібно відповісти на 6/8 питань");
            Thread.Sleep(3000);

            for(int i = 1; i <= QuestionsToAnswer; i++)
            {
                if(Player.IsExpelled)
                {
                    Console.WriteLine("\nШановний, ви були відраховані! Йдіть у бурсу або на Донецький напрямок!");

                    break;
                }

                Console.WriteLine($"\nПитання {i} з {QuestionsToAnswer}");

                int ticket1 = Rnd.Next(1, 30);
                int ticket2 = Rnd.Next(1, 30);
                int ticket3 = Rnd.Next(1, 30);

                Console.WriteLine($"\nОберіть білети: 1) N.{ticket1} 2) N.{ticket2} 3) N.{ticket3}");
                string choice = Console.ReadLine();

                Question curQuestion = GetQuestion();
                AskQuestion(curQuestion, i);
            }
        }

        private void AskQuestion(Question question, int questionNum)
        {
            Console.WriteLine($"Питання: {question.Text}");

            for(int i = 0; i < question.Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {question.Options[i]}");
            }

            //Додати перевірку правильної відповіді, при помилці викид мінігри
        }

        private Question GetQuestion()
        {
            int idx = Rnd.Next(Questions.Count);

            return Questions[idx];
        }

        private List<Question> GenerateTestQuestions()
        {
            return new List<Question>
            {
                new Question
                {
                    Text = "Що таке інкапсуляція?",
                    Options = new List<string> { "A) Приховування даних", "B) Спадкування", "C) Поліморфізм", "D) Тип даних" },
                    IndexOfCorrectAnswer = 0,
                    Difficulty = Difficulty.Easy
                },
                new Question
                {
                    Text = "Скільки байт займає тип int у C#?",
                    Options = new List<string> { "A) 1", "B) 2", "C) 4", "D) 8" },
                    IndexOfCorrectAnswer = 2,
                    Difficulty = Difficulty.Easy
                }
            };
        }
    }
}
