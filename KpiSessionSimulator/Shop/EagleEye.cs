using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Shop
{
    public class EagleEye: IItemCommand
    {
        public string Name => "Синє Око";

        public int Price => 300;

        public string Description => "Підвищує навички зору, які ви набули з Поляни, тепер не тільки можете побачити білочку, а й неправильну відповідь";

        public void Execute(Player player)
        {
            if(player.Stats.Tokens < Price)
            {
                Console.WriteLine($"\nНедостатньо токенов для покупки {Name}");
                return;
            }

            player.Stats.Tokens -= Price;
            player.Stats.EagleEyeCount++;

            Console.WriteLine($"\nВи придбали {Name}");
        }
    }
}
