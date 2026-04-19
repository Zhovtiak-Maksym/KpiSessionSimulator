using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Teachers
{
    public abstract class BasicTeacher
    {
        public string Name { get; set; }
        protected IPunishment PunishmentStrategy { get; set; }
        public string Subject { get; set; }

        protected BasicTeacher(string name, IPunishment punishment, string subject)
        {
            Name = name;
            PunishmentStrategy = punishment;
            Subject = subject;
        }

        public abstract void Interact(Player player);

        public void Punish(Player player)
        {
            if (PunishmentStrategy != null)
            {
                PunishmentStrategy.DoPunishment(player);
            }
        }   
    }
}
