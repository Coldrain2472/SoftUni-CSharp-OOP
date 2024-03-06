using System;

List<int> numbers = new List<int>();

while (numbers.Count != 10)
{
    try
    {
        int num = int.Parse(Console.ReadLine());

        if (num <= 1 || num >= 100)
        {
            int lastNumber = numbers.LastOrDefault() == 0 ? 1 : numbers.LastOrDefault();
            throw new ArgumentOutOfRangeException("", $"Your number is not in range {lastNumber} - 100!");
        }
        else if (numbers.Count == 0)
        {
            numbers.Add(num);
        }
        else if (numbers.LastOrDefault() < num)
        {
            numbers.Add(num);
        }
        else
        {
            throw new ArgumentOutOfRangeException("", $"Your number is not in range {numbers.LastOrDefault()} - 100!");
        }
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid Number!");
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
Console.WriteLine(string.Join(", ", numbers));