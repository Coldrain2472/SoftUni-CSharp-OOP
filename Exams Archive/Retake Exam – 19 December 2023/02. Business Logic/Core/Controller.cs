using HighwayToPeak.Core.Contracts;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using HighwayToPeak.Repositories.Contracts;
using HighwayToPeak.Utilities.Messages;
using System.Text;

namespace HighwayToPeak.Core;
public class Controller : IController
{
    private IRepository<IPeak> peaks;
    private IRepository<IClimber> climbers;
    private IBaseCamp baseCamp;
    private readonly string[] validDifficultyLevels = new[] { "extreme", "hard", "moderate" };
    
    public Controller()
    {
        peaks = new PeakRepository();
        climbers = new ClimberRepository();
        baseCamp = new BaseCamp();
    }

    public string AddPeak(string name, int elevation, string difficultyLevel)
    {
        if (peaks.All.Any(p => p.Name == name))
        {
            return string.Format(OutputMessages.PeakAlreadyAdded, name);
        }

        if (!validDifficultyLevels.Contains(difficultyLevel.ToLower()))
        {
            return string.Format(OutputMessages.PeakDiffucultyLevelInvalid, difficultyLevel);
        }

        peaks.Add(new Peak(name, elevation, difficultyLevel));
        return string.Format(OutputMessages.PeakIsAllowed, name, peaks.GetType().Name);
    }

    public string NewClimberAtCamp(string name, bool isOxygenUsed)
    {
        if (climbers.All.Any(c => c.Name == name))
        {
            return string.Format(OutputMessages.ClimberCannotBeDuplicated, name, climbers.GetType().Name);
        }

        IClimber climber;

        if (isOxygenUsed)
        {
            climber = new OxygenClimber(name);
        }
        else
        {
            climber = new NaturalClimber(name);
        }

        climbers.Add(climber);
        baseCamp.ArriveAtCamp(name);

        return string.Format(OutputMessages.ClimberArrivedAtBaseCamp, name);
    }

    public string AttackPeak(string climberName, string peakName)
    {
        if (climbers.All.All(c => c.Name != climberName))
        {
            return string.Format(OutputMessages.ClimberNotArrivedYet, climberName);
        }

        if (peaks.All.All(p => p.Name != peakName))
        {
            return string.Format(OutputMessages.PeakIsNotAllowed, peakName);
        }

        if (baseCamp.Residents.All(r => r != climberName))
        {
            return string.Format(OutputMessages.ClimberNotFoundForInstructions, climberName, peakName);
        }

        IClimber currentClimber = climbers.All.FirstOrDefault(c => c.Name == climberName);
        IPeak currentPeak = peaks.All.FirstOrDefault(p => p.Name == peakName);

        if (currentPeak.DifficultyLevel == "Extreme" && currentClimber is NaturalClimber)
        {
            return string.Format(OutputMessages.NotCorrespondingDifficultyLevel, climberName, peakName);
        }

        currentClimber.Climb(currentPeak);
        baseCamp.LeaveCamp(currentClimber.Name);

        if (currentClimber.Stamina == 0)
        {
            return string.Format(OutputMessages.NotSuccessfullAttack, currentClimber.Name);
        }

        baseCamp.ArriveAtCamp(currentClimber.Name);
        return string.Format(OutputMessages.SuccessfulAttack, currentClimber.Name, currentPeak.Name);

    }

    public string CampRecovery(string climberName, int daysToRecover)
    {
        if (baseCamp.Residents.All(r => r != climberName))
        {
            return string.Format(OutputMessages.ClimberIsNotAtBaseCamp, climberName);
        }

        IClimber climber = climbers.All.FirstOrDefault(c => c.Name == climberName);

        if (climber.Stamina == 10)
        {
            return string.Format(OutputMessages.NoNeedOfRecovery, climberName);
        }

        climber.Rest(daysToRecover);

        return string.Format(OutputMessages.ClimberRecovered, climberName, daysToRecover);
    }

    public string BaseCampReport()
    {
        if (baseCamp.Residents.Count == 0)
        {
            return "BaseCamp is currently empty.";
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("BaseCamp residents:");

        foreach (string resident in baseCamp.Residents)
        {
            IClimber currentClimber = climbers.All.FirstOrDefault(c => c.Name == resident);

            if (currentClimber != null)
            {
                sb.AppendLine($"Name: {currentClimber.Name}, Stamina: {currentClimber.Stamina}, Count of Conquered Peaks: {currentClimber.ConqueredPeaks.Count}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    public string OverallStatistics()
    {
        StringBuilder stats = new StringBuilder();
        stats.AppendLine("***Highway-To-Peak***");

        foreach (var climber in climbers.All.OrderByDescending(c => c.ConqueredPeaks.Count)
                     .ThenBy(c => c.Name))
        {
            stats.AppendLine(climber.ToString());
            List<IPeak> climberPeaks = new List<IPeak>();

            foreach (var peakName in climber.ConqueredPeaks)
            {
                IPeak currentPeak = peaks.All.First(p => p.Name == peakName);
                climberPeaks.Add(currentPeak);
            }

            foreach (var peak in climberPeaks.OrderByDescending(p => p.Elevation))
            {
                stats.AppendLine(peak.ToString());
            }
        }
        return stats.ToString().TrimEnd();
    }
}