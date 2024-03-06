string[] line = Console.ReadLine().Split(" ");
int sum = 0;

foreach (string item in line)
{
    try
    {
        int currentNum = int.Parse(item);
        sum += currentNum;
    }
    catch (FormatException)
    {
        Console.WriteLine($"The element '{item}' is in wrong format!");
    }
    catch (OverflowException)
    {
        Console.WriteLine($"The element '{item}' is out of range!");
    }
    finally
    {
        Console.WriteLine($"Element '{item}' processed - current sum: {sum}");
    }
}

Console.WriteLine($"The total sum of all integers is: {sum}");