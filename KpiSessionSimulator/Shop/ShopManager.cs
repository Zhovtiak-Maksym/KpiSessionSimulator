using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Shop
{
    public class ShopManager
    {
        private List<IItemCommand> perks;
        public const int ShortPause = 1000;

        public ShopManager()
        {
            perks = new List<IItemCommand>
            {
                new EagleEye(),
                new Immunity(),
                new Loyalty(),
                new TrickyHands()
            };
        }

        public void ShopLife(Player player)
        {
            while(true)
            {
                Console.WriteLine("\nМагазин перків:");

                int counter = 1;
                foreach (var item in perks)
                {
                    Console.WriteLine($"{counter++}. {item.Name} - {item.Price} токенів ({item.Description})");
                }

                Console.Write("\nВведіть номер перку для покупки (0 - вихід): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice == 0)
                    {
                        Console.WriteLine("\nВихід з Магазину...");
                        break;
                    }
                    else if (choice > 0 && choice <= perks.Count)
                    {
                        var selectedPerk = perks[choice - 1];
                        selectedPerk.Execute(player);
                    }
                    else
                    {
                        Console.WriteLine("\nПерку під таким номером не існує...");
                    }
                }
                else
                {
                    Console.WriteLine("\nБудь ласка, введіть число!");
                }
            }
        }
    }
}
