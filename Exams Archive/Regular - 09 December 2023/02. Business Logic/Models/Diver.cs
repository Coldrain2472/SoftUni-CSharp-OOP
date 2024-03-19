using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private int oxygenLevel;
        private List<string> catchh;
        private double competitionPoints;
        private bool hasHealthIssues;
        
        protected Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;
            catchh = new List<string>();
            CompetitionPoints = 0;
            HasHealthIssues = false;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DiversNameNull);
                }
                name = value;
            }
        }

        public int OxygenLevel
        {
            get => oxygenLevel;
            protected set
            {
                if (value < 0)
                {
                    oxygenLevel = 0;
                }
                else
                {
                    oxygenLevel = value;
                }
            }
        }
        public IReadOnlyCollection<string> Catch => catchh.AsReadOnly();

        public double CompetitionPoints
        {
            get => Math.Round(competitionPoints, 1);
            private set => competitionPoints = value;
        }
        public bool HasHealthIssues { get; private set; }
       
        public void Hit(IFish fish)
        {
            OxygenLevel -= fish.TimeToCatch;
            catchh.Add(fish.Name);
            CompetitionPoints += fish.Points;
        }

        public abstract void Miss(int timeToCatch);

        public void UpdateHealthStatus()
        {
            HasHealthIssues = !HasHealthIssues;
        }

        public abstract void RenewOxy();

        public override string ToString()
        {
            return $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {Catch.Count}, Points earned: {CompetitionPoints} ]";
        }
    }
}
