using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Models.Divers;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System.Reflection;
using System.Text;

namespace NauticalCatchChallenge.Core;
public class Controller : IController
{
    private IRepository<IDiver> _divers;
    private IRepository<IFish> _fish;

    public Controller()
    {
        _divers = new DiverRepository();
        _fish = new FishRepository();
    }
    public string DiveIntoCompetition(string diverType, string diverName)
    {
        var children = Assembly.GetAssembly(typeof(Diver))
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(Diver).IsAssignableFrom(t))
            .Select(t => t.Name);

        if (!children.Contains(diverType))
        {
            return string.Format(OutputMessages.DiverTypeNotPresented, diverType);
        }

        if (_divers.Models.Any(d => d.Name == diverName))
        {
            return string.Format(OutputMessages.DiverNameDuplication, diverName, _divers.GetType().Name);
        }

        IDiver diver = null;

        switch (diverType)
        {
            case "FreeDiver":
                diver = new FreeDiver(diverName);
                break;
            case "ScubaDiver":
                diver = new ScubaDiver(diverName);
                break;
        }

        _divers.AddModel(diver);

        return string.Format(OutputMessages.DiverRegistered, diverName, _divers.GetType().Name);
    }

    public string SwimIntoCompetition(string fishType, string fishName, double points)
    {
        var children = Assembly.GetAssembly(typeof(Fish))
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(Fish).IsAssignableFrom(t))
            .Select(t => t.Name);

        if (!children.Contains(fishType))
        {
            return string.Format(OutputMessages.FishTypeNotPresented, fishType);
        }

        if (_fish.Models.Any(f => f.Name == fishName))
        {
            return string.Format(OutputMessages.FishNameDuplication, fishName, _fish.GetType().Name);
        }

        IFish fish = null;

        switch (fishType)
        {
            case "ReefFish":
                fish = new ReefFish(fishName, points);
                break;
            case "DeepSeaFish":
                fish = new DeepSeaFish(fishName, points);
                break;
            case "PredatoryFish":
                fish = new PredatoryFish(fishName, points);
                break;
        }

        _fish.AddModel(fish);

        return string.Format(OutputMessages.FishCreated, fishName);
    }

    public string ChaseFish(string diverName, string fishName, bool isLucky)
    {
        if (_divers.Models.All(d => d.Name != diverName))
        {
            return string.Format(OutputMessages.DiverNotFound, _divers.GetType().Name, diverName);
        }

        if (_fish.Models.All(f => f.Name != fishName))
        {
            return string.Format(OutputMessages.FishNotAllowed, fishName);
        }

        IDiver diver = _divers.Models.First(d => d.Name == diverName);

        if (diver.HasHealthIssues)
        {
            return string.Format(OutputMessages.DiverHealthCheck, diverName);
        }

        IFish fish = _fish.Models.First(f => f.Name == fishName);

        if (diver.OxygenLevel < fish.TimeToCatch)
        {
            diver.Miss(fish.TimeToCatch);

            if (diver.OxygenLevel <= 0)
            {
                diver.UpdateHealthStatus();
            }
            return string.Format(OutputMessages.DiverMisses, diverName, fishName);
        }

        if (diver.OxygenLevel == fish.TimeToCatch)
        {
            if (isLucky)
            {
                diver.Hit(fish);
                diver.UpdateHealthStatus();

                return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);
            }

            diver.Miss(fish.TimeToCatch);
            if (diver.OxygenLevel <= 0)
            {
                diver.UpdateHealthStatus();
            }
            return string.Format(OutputMessages.DiverMisses, diverName, fishName);
        }

        diver.Hit(fish);
        if (diver.OxygenLevel <= 0)
        {
            diver.UpdateHealthStatus();
        }
        return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);
    }

    public string HealthRecovery()
    {
        List<IDiver> unwellDivers = _divers.Models.Where(d => d.HasHealthIssues).ToList();

        foreach (var diver in unwellDivers)
        {
            diver.UpdateHealthStatus();
            diver.RenewOxy();
        }

        return string.Format(OutputMessages.DiversRecovered, unwellDivers.Count);
    }

    public string DiverCatchReport(string diverName)
    {
        StringBuilder reportBuilder = new StringBuilder();
        IDiver diver = _divers.Models.First(d => d.Name == diverName);

        reportBuilder
            .AppendLine($"Diver [ Name: {diver.Name}, Oxygen left: {diver.OxygenLevel}, Fish caught: {diver.Catch.Count}, Points earned: {diver.CompetitionPoints} ]");
        reportBuilder.AppendLine("Catch Report:");

        foreach (var fish in diver.Catch)
        {
            IFish currentFish = _fish.Models.First(f => f.Name == fish);

            reportBuilder.AppendLine(currentFish.ToString());
        }

        return reportBuilder.ToString().TrimEnd();
    }

    public string CompetitionStatistics()
    {
        List<IDiver> statisticsDivers = _divers.Models
            .Where(d => !d.HasHealthIssues)
            .OrderByDescending(d => d.CompetitionPoints)
            .ThenByDescending(d => d.Catch.Count)
            .ThenBy(d => d.Name).ToList();

        StringBuilder statisticsBuilder = new StringBuilder();
        statisticsBuilder.AppendLine("**Nautical-Catch-Challenge**");

        foreach (var diver in statisticsDivers)
        {
            statisticsBuilder.AppendLine(diver.ToString());
        }

        return statisticsBuilder.ToString().TrimEnd();
    }
}