namespace NauticalCatchChallenge.Models.Divers;

public class FreeDiver : Diver
{
    private const int oxygenLevel = 120;

    public FreeDiver(string name) : base(name, oxygenLevel)
    {
    }

    public override void Miss(int timeToCatch)
    {
        double oxygenReduction = timeToCatch * 0.6;
        OxygenLevel -= (int)Math.Round(oxygenReduction, MidpointRounding.AwayFromZero);
    }

    public override void RenewOxy()
    {
        OxygenLevel = oxygenLevel;
    }
}