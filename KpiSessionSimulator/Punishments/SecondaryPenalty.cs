using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Punishments
{
    public class SecondaryPenalty : IPunishment
    {
        public void DoPunishment(Player player)
        {
            if (player.IsONSecondary)
            {
                Console.WriteLine("Ви не склали допку. Вас відраховано, вам краще вступити на менеджмент...");
                player.IsExpelled = true;

                return;
            }

            Console.WriteLine("Ви не склали екзамен з 'Основ Програмування', вас відправлено на допку. Готуйтеся побачити Бога...");
            player.IsONSecondary = true;
        }
    }
}
