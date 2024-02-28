using FoodShortage.Models;
using FoodShortage.Models.Interfaces;

public class StartUp
{
    public static void Main(string[] args)
    {
        int numberOfPeople = int.Parse(Console.ReadLine());
        List<IBuyer> buyers = new List<IBuyer>();

        for (int i = 0; i < numberOfPeople; i++)
        {
            string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (input.Length == 4)
            {
                // citizen
                Citizen citizen = new Citizen(input[0], int.Parse(input[1]), input[2], input[3]);
                buyers.Add(citizen);
            }
            else if (input.Length == 3)
            {
                // rebel
                Rebel rebel = new Rebel(input[0], int.Parse(input[1]), input[2]);
                buyers.Add(rebel);
            }
        }

        string command = string.Empty;
        List<string> names = new List<string>();

        while ((command = Console.ReadLine()) != "End")
        {
            names.Add(command);
        }

        foreach (var name in names)
        {
            if (buyers.FirstOrDefault(b=>b.Name == name) != default)
            {
                IBuyer currentBuyer = buyers.FirstOrDefault(b => b.Name == name);
                currentBuyer.BuyFood();
            }
        }

        Console.WriteLine(buyers.Sum(b => b.Food));
    }
}