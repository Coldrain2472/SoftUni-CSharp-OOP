﻿using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private double competitionPoints;
        private readonly List<string> catches;

        public Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;
            catches = new List<string>();
        }

        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DiversNameNull);
                }
                name = value;
            }
        }

        public int OxygenLevel {get;protected set; }

        public IReadOnlyCollection<string> Catch => catches;

        public double CompetitionPoints
        {
            get => Math.Round(competitionPoints, 1);
            private set => competitionPoints = value;
        }

        public bool HasHealthIssues {get;private set;} = false;

        public void Hit(IFish fish)
        {
            OxygenLevel -= fish.TimeToCatch;
            catches.Add(fish.Name);
            CompetitionPoints+=fish.Points;
        }

        public abstract void Miss(int TimeToCatch);

        public abstract void RenewOxy();

        public void UpdateHealthStatus()
        {
           HasHealthIssues = !HasHealthIssues;
        }

        public override string ToString()
        {
            return $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {catches.Count}, Points earned: {CompetitionPoints} ]";
        }
    }
}
