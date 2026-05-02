using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;
using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Minigames;
using KpiSessionSimulator.Services;

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

        public ProgramProcess(Player player, BasicTeacher teacher, List<Question> questions)
        {
            Player = player;
            Teacher = teacher;
            Questions = questions;
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

                bool repeatQuestion = AskQuestion(curQuestion, i);

                if (State.IsHospitalized)
                {
                    Console.WriteLine($"\n{Teacher.Name}: Шановний, у вас дірка вголові, схоже, що ви не зможете далі складати іспит...");

                    break;
                }

                if (repeatQuestion)
                {
                    i--;
                    Console.WriteLine("\nВи отримуєте нові білети, бо отримали другий шанс...");
                    Thread.Sleep(ShortPauseMs);
                }
            }

            if (!Player.IsExpelled && !State.IsHospitalized)
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

        private bool AskQuestion(Question question, int questionNum)
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

                return false; 
            }
            else
            {
                return HandleWrongAnswer(questionNum);
            }
        }

        private bool HandleWrongAnswer(int questionNum)
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

                    return true; 
                }
                else
                {
                    Console.WriteLine("Ви програли в мінігру! Викладач починає карати...");

                    if (!isRoulette)
                    {
                        State.BlackjackLosses++;

                        if (State.BlackjackLosses % BlackjackLossesForPenalty == 0)
                        {
                            Console.WriteLine($"Ви програли в Блекджек вже {State.BlackjackLosses} рази.");
                            IncreaseDifficulty();
                        }
                    }
                    else
                    {
                        State.IsHospitalized = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nВи прийняли мінус без бою");
                IncreaseDifficulty();
            }

            return false; 
        }

        private void IncreaseDifficulty()
        {
            switch (State.CurrentDifficulty)
            {
                case Difficulty.Easy:
                    State.CurrentDifficulty = Difficulty.Normal;
                    Console.WriteLine("\nСкладність підвищено до 'Нормальної'");
                    break;

                case Difficulty.Normal:
                    State.CurrentDifficulty = Difficulty.Medium;
                    Console.WriteLine("\nВикладач починає ставити додаткові питання. Складність 'Середня'");
                    break;

                case Difficulty.Medium:
                    State.CurrentDifficulty = Difficulty.Difficult;
                    Console.WriteLine("\nВикладач хмуриться. Наступні питання будуть дуже складними!");
                    break;

                case Difficulty.Difficult:
                    State.CurrentDifficulty = Difficulty.DeathMode;
                    Console.WriteLine("\nDeath Mode! Моліться Богу");
                    break;

                case Difficulty.DeathMode:
                    Console.WriteLine("\nСкладність вже максимальна. Атмосфера наганяє думки про відрахування...");
                    break;
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
    }
}