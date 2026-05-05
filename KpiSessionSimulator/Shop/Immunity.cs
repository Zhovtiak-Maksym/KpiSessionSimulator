using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Shop
{
    public class Immunity : IItemCommand
    {
        public string Name => "Імунітет";

        public int Price => 500;

        public string Description => "Через маленьку стипендію, ваша травна система еволюціонвала, тепер ваш організм здатен розкласти кулю у голові на поживні речовини";

        public void Execute(Player player)
        {
            if(player.Stats.Tokens < Price)
            {
                Console.WriteLine($"\nНедостатньо токенов для покупки {Name}");
                return;
            }

            player.Stats.Tokens -= Price;
            player.Stats.ImmunityCount++;

            Console.WriteLine($"\nВи придбали {Name}");
        }
    }
}
