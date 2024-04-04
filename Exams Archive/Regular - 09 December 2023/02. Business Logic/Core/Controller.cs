using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private readonly DiverRepository divers;
        private readonly FishRepository fishes;

        public Controller()
        {
            divers = new DiverRepository();
            fishes = new FishRepository();
        }

        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            IDiver diver = divers.GetModel(diverName);
            if (diver == null)
            {
                return string.Format(OutputMessages.DiverNotFound, divers.GetType().Name, diverName);
            }

            IFish fish = fishes.GetModel(fishName);
            if (fish == null)
            {
                return string.Format(OutputMessages.FishNotAllowed, fishName);
            }

            if (diver.HasHealthIssues)
            {
                return string.Format(OutputMessages.DiverHealthCheck, diverName);
            }

            if (diver.OxygenLevel < fish.TimeToCatch)
            {
                diver.Miss(fish.TimeToCatch);
                if (diver.OxygenLevel <= 0)
                {
                    diver.UpdateHealthStatus();
                }
                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }
            else if (diver.OxygenLevel == fish.TimeToCatch)
            {
                if (isLucky)
                {
                    diver.Hit(fish);
                    diver.UpdateHealthStatus();
                    return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);
                }
                else
                {
                    diver.Miss(fish.TimeToCatch);
                    if (diver.OxygenLevel <= 0)
                    {
                        diver.UpdateHealthStatus();
                    }
                    return string.Format(OutputMessages.DiverMisses, diverName, fishName);
                }
            }
            else
            {
                diver.Hit(fish);
                return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);
            }
        }

        public string CompetitionStatistics()
        {
            var arrangedDivers = divers.Models
            .Where(d => !d.HasHealthIssues)
            .OrderByDescending(d => d.CompetitionPoints)
            .ThenByDescending(d => d.Catch.Count)
            .ThenBy(d => d.Name);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("**Nautical-Catch-Challenge**");
            foreach (var diver in arrangedDivers)
            {
                sb.AppendLine(diver.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {
            IDiver diver = divers.GetModel(diverName);
            if (diver != null)
            {
                return string.Format(OutputMessages.DiverNameDuplication, diverName, divers.GetType().Name);
            }
            else
            {
                if (diverType == nameof(FreeDiver))
                {
                    diver = new FreeDiver(diverName);
                }
                else if (diverType == nameof(ScubaDiver))
                {
                    diver = new ScubaDiver(diverName);
                }
                else
                {
                    return string.Format(OutputMessages.DiverTypeNotPresented, diverType);
                }

                divers.AddModel(diver);
                return string.Format(OutputMessages.DiverRegistered, diverName, divers.GetType().Name);
            }
        }

        public string DiverCatchReport(string diverName)
        {
            IDiver diver = divers.GetModel(diverName);
            if (diver == null)
            {
                return string.Format(OutputMessages.DiverNotFound, divers.GetType().Name, diverName);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(diver.ToString());
            sb.AppendLine("Catch Report:");
            foreach (var fish in diver.Catch)
            {
                IFish currentFish = fishes.GetModel(fish);
                sb.AppendLine(currentFish.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string HealthRecovery()
        {
            int count = 0;
            foreach (var diver in divers.Models)
            {
                if (diver.HasHealthIssues)
                {
                    diver.UpdateHealthStatus();
                    diver.RenewOxy();
                    count++;
                }
            }

            return string.Format(OutputMessages.DiversRecovered, count);
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            IFish fish = fishes.GetModel(fishName);
            if (fish != null)
            {
                return string.Format(OutputMessages.FishNameDuplication, fishName, fishes.GetType().Name);
            }
            else
            {
                if (fishType == nameof(ReefFish))
                {
                    fish = new ReefFish(fishName, points);
                }
                else if (fishType == nameof(PredatoryFish))
                {
                    fish = new PredatoryFish(fishName, points);
                }
                else if (fishType == nameof(DeepSeaFish))
                {
                    fish = new DeepSeaFish(fishName, points);
                }
                else
                {
                    return string.Format(OutputMessages.FishTypeNotPresented, fishType);
                }

                fishes.AddModel(fish);
                return string.Format(OutputMessages.FishCreated, fishName);
            }
        }
    }
}
