using BirthdayCelebrations.Models;
using BirthdayCelebrations.Models.Interfaces;

public class StartUp
{
    public static void Main(string[] args)
    {
        string command = string.Empty;
        List<IAlive> aliveObjects = new List<IAlive>();

        while ((command = Console.ReadLine()) != "End")
        {
            string[] commandInfo = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (command.StartsWith("Citizen"))
            {
                Citizen citizen = new Citizen(commandInfo[1], int.Parse(commandInfo[2]), commandInfo[3], commandInfo[4]);
                aliveObjects.Add(citizen);
            }
            else if (command.StartsWith("Pet"))
            {
                Pet pet = new Pet(commandInfo[1], commandInfo[2]);
                aliveObjects.Add(pet);
            }
            else if (command.StartsWith("Robot"))
            {
                Robot robot = new Robot(commandInfo[1], commandInfo[2]);
            }
        }

        string year = Console.ReadLine();
        List<IAlive> sameBirthdate = new List<IAlive>();

        foreach (var item in aliveObjects)
        {
            if (item.Birthdate.EndsWith(year))
            {
                sameBirthdate.Add(item);
            }
        }

        foreach (var item in sameBirthdate)
        {
            Console.WriteLine(item.Birthdate);
        }
    }
}