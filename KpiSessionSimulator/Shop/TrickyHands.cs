using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Shop
{
    public class TrickyHands : IItemCommand
    {
        public string Name => "Спритні руки";

        public int Price => 150;

        public string Description => "Здача тасків за 5 хвилин до дедлайну навчила ваші руки небувалої спритності, яка може непомітно змінити карту на столі при грі в Блек-Джек";

        public void Execute(Player player)
        {
            if(player.Stats.Tokens < Price)
            {
                Console.WriteLine($"\nНедостатньо токенів для покупки {Name}");
                return;
            }

            player.Stats.Tokens -= Price;
            player.Stats.TrickyHandsCount++;

            Console.WriteLine($"\nВи придбали {Name}");
        }
    }
}
