using Telephony;

public class StartUp
{
    public static void Main(string[] args)
    {
        List<string> inputNumbers = new List<string>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries));
        List<string> websites = new List<string>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries));

        foreach (var number in inputNumbers)
        {
            try
            {
                if (number.Length == 10)
                {
                    Smartphone smartphone = new Smartphone();
                    smartphone.Call(number);
                }
                else if (number.Length == 7)
                {
                    StationaryPhone stationaryPhone = new StationaryPhone();
                    stationaryPhone.Call(number);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        foreach (var website in websites)
        {
            try
            {
                Smartphone smartphone = new Smartphone();
                smartphone.Browse(website);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}