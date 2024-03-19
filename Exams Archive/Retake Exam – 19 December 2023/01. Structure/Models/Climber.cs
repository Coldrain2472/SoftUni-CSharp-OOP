using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber
    {
        private string name;
        private int stamina;
        private readonly List<string> conqueredPeaks;

        public Climber(string name, int stamina)
        {
            Name = name;
            Stamina = stamina;
            this.conqueredPeaks = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClimberNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Stamina
        {
            get => stamina;
            protected set
            {
                if (value < 0)
                {
                    value = 1;
                }
                else if (value > 10)
                {
                    value = 10;
                }
                else
                {
                    stamina = value;
                }
            }
        }

        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks;

        private int CalculateStaminaReduction(string difficulty)
        {
            switch (difficulty)
            {
                case "Extreme":
                    return 6;
                case "Hard":
                    return 4;
                case "Moderate":
                    return 2;
            }

            return default;
        }

        private void ReduceStamina(string difficulty)
        {
            int reduction = CalculateStaminaReduction(difficulty);

            Stamina -= reduction;
        }

        public void Climb(IPeak peak)
        {
            string peakFound = conqueredPeaks.FirstOrDefault(p => p == peak.Name);

            if (peakFound == null)
            {
                conqueredPeaks.Add(peak.Name);
            }

            ReduceStamina(peak.DifficultyLevel);
        }

        public abstract void Rest(int daysCount);

        public override string ToString()
        {
            if (conqueredPeaks.Count > 0)
            {
                return $"{GetType().Name} - Name: {Name}, Stamina: {Stamina}{Environment.NewLine}Peaks conquered: {conqueredPeaks.Count}";
            }
            else
            {
                return $"{GetType().Name} - Name: {Name}, Stamina: {Stamina}{Environment.NewLine}Peaks conquered: no peaks conquered";
            }
        }
    }
}
