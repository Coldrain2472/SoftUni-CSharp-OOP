﻿using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;
using System.Text;

namespace HighwayToPeak.Models;
public abstract class Climber : IClimber
{
    private string name;
    private int stamina;
    private List<string> conqueredPeaks;

    protected Climber(string name, int stamina)
    {
        Name = name;
        Stamina = stamina;
        conqueredPeaks = new List<string>();
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
                stamina = 0;
            }
            else if (value > 10)
            {
                stamina = 10;
            }
            else
            {
                stamina = value;
            }
        }
    }

    public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks.AsReadOnly();

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

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{GetType().Name} - Name: {Name}, Stamina: {Stamina}");
        string peaksConquered = conqueredPeaks.Count == 0 ? "no peaks conquered" : $"{conqueredPeaks.Count}";
        sb.AppendLine($"Peaks conquered: {peaksConquered}");

        return sb.ToString().TrimEnd();
    }
}