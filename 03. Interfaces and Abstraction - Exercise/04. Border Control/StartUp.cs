using BorderControl;
using System.Diagnostics.Metrics;

public class StartUp
{
    public static void Main(string[] args)
    {
        string command = string.Empty;
        List<IIdentifiable> ids = new List<IIdentifiable>();
        while ((command = Console.ReadLine()) != "End")
        {
            string[] commandInfo = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (commandInfo.Length == 3)
            {
                // citizen - name, age, id
                Citizen citizen = new Citizen(commandInfo[0], int.Parse(commandInfo[1]), commandInfo[2]);
                ids.Add(citizen);
            }
            else
            {
                // robot - model, id
                Robot robot = new Robot(commandInfo[0], commandInfo[1]);
                ids.Add(robot);
            }
        }

        string lastDigits = Console.ReadLine();
        List<IIdentifiable> detainedEnters = new List<IIdentifiable>();

        foreach (var item in ids)
        {
            if (item.Id.EndsWith(lastDigits))
            {
                detainedEnters.Add(item);
            }
        }

        foreach (var item in detainedEnters)
        {
            Console.WriteLine(item.Id);
        }
    }
}