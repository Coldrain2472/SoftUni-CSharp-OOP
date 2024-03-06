int[] array = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
int exceptionsCount = 0;

while (exceptionsCount < 3)
{
    string[] command = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
	try
	{
		if (command[0] == "Replace")
		{
			int index = int.Parse(command[1]);
			int element = int.Parse(command[2]);
			array[index] = element;
		}
		else if (command[0] == "Print")
		{
			int startIndex = int.Parse(command[1]);
			int endIndex = int.Parse(command[2]);
            if (startIndex < 0 || startIndex >= array.Length || endIndex < 0 || endIndex >= array.Length)
            {
                throw new IndexOutOfRangeException();
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (i < endIndex)
                {
                    Console.Write($"{array[i]}, ");
                }
                else
                {
                    Console.WriteLine($"{array[i]}");
                }
            }
        }
		else if (command[0] == "Show")
		{
			int index = int.Parse(command[1]);
			Console.WriteLine(array[index]);
        }
	}
	catch (IndexOutOfRangeException)
	{
        exceptionsCount++;
        Console.WriteLine("The index does not exist!");
    }
    catch (FormatException)
    {
        exceptionsCount++;
        Console.WriteLine("The variable is not in the correct format!");
    }
}

Console.WriteLine(string.Join(", ", array));