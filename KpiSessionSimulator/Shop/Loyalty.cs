using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Shop
{
    public class Loyalty : IItemCommand
    {
        public string Name => "Лояльність";

        public int Price => 200;

        public string Description => "Ви нарешті прийняли душ, ваша харизма та зовнішній вигляд значно покращилися, викладач не стримається і дасть право на зміну білету";

        public void Execute(Player player)
        {
            if(player.Stats.Tokens < Price)
            {
                Console.WriteLine($"\nНедостатньо токенов для покупки {Name}");
                return;
            }

            player.Stats.Tokens -= Price;
            player.Stats.LoyaltyCount++;

            Console.WriteLine($"\nВи придбали {Name}");
        }
    }
}
