namespace RobotService.Models;

internal class SpecializedArm : Supplement
{
    private const int interfaceStandard = 10045;
    private const int batteryUsage = 10000;

    public SpecializedArm() : base(interfaceStandard, batteryUsage){}
}