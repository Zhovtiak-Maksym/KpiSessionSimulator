using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;
using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Minigames;

namespace KpiSessionSimulator.Core
{
    public class ProgramProcess
    {
        public const int QuestionsToAnswer = 8;
        public const int QuestionsToPass = 6;

        public const int RouletteProbability = 35;        
        public const int BlackjackLossesForPenalty = 3;  
        
        private const int ShortPauseMs = 1500;            
        private const int LongPauseMs = 3000;             

        private Player Player;
        private BasicTeacher Teacher;
        private List<Question> Questions;
        private Random Rnd = new Random();
        private Roulette ExamRoulette;
        private ExamState State;

        public ProgramProcess(Player player, BasicTeacher teacher)
        {
            Player = player;
            Teacher = teacher;
            Questions = GenerateTestQuestions();
            ExamRoulette = new Roulette();
            State = new ExamState();
        }

        public void Exam()
        {
            Console.WriteLine($"\nВи прийшли на екзамен до {Teacher.Name} з предмету '{Teacher.Subject}'");
            Thread.Sleep(ShortPauseMs);
            Console.WriteLine("Сподіваємося, що ви потерли шар на поляні...");
            Thread.Sleep(ShortPauseMs);
            Console.WriteLine($"На столі лежать білети. Вам потрібно відповісти на {QuestionsToPass}/{QuestionsToAnswer} питань");
            Thread.Sleep(LongPauseMs);

            for (int i = 1; i <= QuestionsToAnswer; i++)
            {
                if (Player.IsExpelled)
                {
                    Console.WriteLine("\nШановний, ви були відраховані! Йдіть у бурсу або на Донецький напрямок!");
                    break;
                }

                Console.WriteLine($"\nПитання {i} з {QuestionsToAnswer} (Поточна складність: {State.CurrentDifficulty})");

                int ticket1 = Rnd.Next(1, 10);
                int ticket2 = Rnd.Next(10, 20);
                int ticket3 = Rnd.Next(20, 31);

                Question q1 = GetQuestion();
                Question q2 = GetQuestion();
                Question q3 = GetQuestion();

                Console.WriteLine($"\nОберіть білети: 1) N.{ticket1} 2) N.{ticket2} 3) N.{ticket3}");
                string choice = Console.ReadLine();

                Question curQuestion = q1;

                if (choice == "2")
                {
                    curQuestion = q2;
                }
                else if (choice == "3")
                {
                    curQuestion = q3;
                }
                else if (choice != "1")
                {
                    Console.WriteLine("Такої опції не існує. Викладач впихує вам перший білет!");
                }

                AskQuestion(curQuestion, i);
            }

            if (!Player.IsExpelled)
            {
                Console.WriteLine($"\nЕкзамен Завершено...");
                Console.WriteLine($"Ваш результат: {State.CorrectAnswers} з {QuestionsToAnswer} правильних відповідей.");

                if (State.CorrectAnswers >= QuestionsToPass)
                {
                    Console.WriteLine("Вітаємо! Ви склали екзамен!");
                }
                else
                {
                    Console.WriteLine("Ви не набрали прохідний бал...");
                    Teacher.Punish(Player);
                }
            }
        }

        private void AskQuestion(Question question, int questionNum)
        {
            Console.WriteLine($"\nПитання: {question.Text}");

            for (int i = 0; i < question.Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {question.Options[i]}");
            }

            Console.Write("\nВаша відповідь (1-4): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int answerIdx) && (answerIdx - 1) == question.IndexOfCorrectAnswer)
            {
                Console.WriteLine("\nВідповідь зараховано!");
                State.CorrectAnswers++;
                Player.WrongAnswersStreak = 0;
            }
            else
            {
                HandleWrongAnswer(questionNum);
            }
        }

        private void HandleWrongAnswer(int questionNum)
        {
            Console.WriteLine($"\nНеправильно! {Teacher.Name} випалює очами у вас дірку...");
            Player.WrongAnswersStreak++;

            Console.Write("Відіграєтесь у мінігрі? (так/ні): ");

            if (Console.ReadLine()?.ToLower() == "так")
            {
                bool isRoulette = Rnd.Next(1, 101) <= RouletteProbability;
                IMiniGame miniGame = isRoulette ? ExamRoulette : new Blackjack();

                Console.WriteLine("\nПідготовка до міні-гри...");
                Thread.Sleep(ShortPauseMs);

                Console.WriteLine("\n-------------------------------------");
                Console.WriteLine(isRoulette ? "      МІНІГРА: РУЛЕТКА      " : "          МІНІГРА: БЛЕКДЖЕК           ");
                Console.WriteLine("-------------------------------------");
                Thread.Sleep(ShortPauseMs);

                bool wonMinigame = miniGame.Play(Player, Teacher, questionNum);

                Thread.Sleep(ShortPauseMs);
                Console.WriteLine("\n-------------------------------------");
                Console.WriteLine("        Повернення до екзамену...         ");
                Console.WriteLine("--------------------------------------");
                Thread.Sleep(ShortPauseMs);

                if (wonMinigame)
                {
                    Console.WriteLine("Ви виграли в мінігру! Маєте другий шанс");
                }
                else
                {
                    Console.WriteLine("Ви програли в мінігру! Викладач починає карати...");

                    if (!isRoulette)
                    {
                        State.BlackjackLosses++;

                        if (State.BlackjackLosses % BlackjackLossesForPenalty == 0)
                        {
                            Console.WriteLine($"Ви програли в Блекджек вже {State.BlackjackLosses} рази");
                            IncreaseDifficulty();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\nВи прийняли мінус без бою");
                IncreaseDifficulty();
            }
        }

        private void IncreaseDifficulty()
        {
            if (State.CurrentDifficulty == Difficulty.Easy)
            {
                State.CurrentDifficulty = Difficulty.Medium;
                Console.WriteLine("\nНаступні питання будуть складнішими...");
            }
            else if (State.CurrentDifficulty == Difficulty.Medium)
            {
                State.CurrentDifficulty = Difficulty.Difficult;
                Console.WriteLine("\nНаступні питання будуть найскладнішими...");
            }
        }

        private Question GetQuestion()
        {
            var availableQuestions = Questions.Where(q => q.Difficulty == State.CurrentDifficulty).ToList();

            if (availableQuestions.Count == 0)
            {
                return Questions[Rnd.Next(Questions.Count)];
            }

            int idx = Rnd.Next(availableQuestions.Count);

            return availableQuestions[idx];
        }

        private List<Question> GenerateTestQuestions()
        {
            return new List<Question>
            {
                new Question
                {
                    Text = "Що таке інкапсуляція?",
                    Options = new List<string> { "Приховування даних", "Спадкування", "Поліморфізм", "Тип даних" },
                    IndexOfCorrectAnswer = 0,
                    Difficulty = Difficulty.Easy
                },
                new Question
                {
                    Text = "Скільки байт займає тип int у C#?",
                    Options = new List<string> { "1", "2", "4", "8" },
                    IndexOfCorrectAnswer = 2,
                    Difficulty = Difficulty.Easy
                }
            };
        }
    }
}