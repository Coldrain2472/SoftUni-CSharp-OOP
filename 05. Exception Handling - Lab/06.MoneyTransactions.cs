string[] input = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);
Dictionary<int, double> bankAccountsWithBalance = new Dictionary<int, double>();

for (int i = 0; i < input.Length; i++)
{
    string[] bankAccount = input[i].Split("-");
    int accountNumber = int.Parse(bankAccount[0]);
    double accountBalance = double.Parse(bankAccount[1]);

    bankAccountsWithBalance.Add(accountNumber, accountBalance);
}

string commandInfo = string.Empty;
while ((commandInfo = Console.ReadLine()) != "End")
{
    string[] command = commandInfo.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    int accountInfo = int.Parse(command[1]);
    double sum = double.Parse(command[2]);
    try
    {

        if (command[0] != "Deposit" && command[0] != "Withdraw")
        {
            throw new InvalidCommandException(InvalidCommandException.InvalidCommandExceptionMessage);
        }
        if (!bankAccountsWithBalance.ContainsKey(accountInfo))
        {
            throw new InvalidAccountException(InvalidAccountException.InvalidAccountExceptionMessage);
        }

        if (command[0] == "Deposit")
        {
            bankAccountsWithBalance[accountInfo] += sum;
        }
        else if (command[0] == "Withdraw")
        {
            if (bankAccountsWithBalance[accountInfo] - sum < 0)
            {
                throw new InsufficientBalanceException(InsufficientBalanceException.InsufficientBalanceExceptionMessage);
            }
            bankAccountsWithBalance[accountInfo] -= sum;
        }
        Console.WriteLine($"Account {accountInfo} has new balance: {bankAccountsWithBalance[accountInfo]:f2}");
    }
    catch (InvalidCommandException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (InvalidAccountException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (InsufficientBalanceException ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        Console.WriteLine("Enter another command");
    }
}

internal class InvalidCommandException : Exception
{
    public const string InvalidCommandExceptionMessage = "Invalid command!";

    public InvalidCommandException(string invalidCommandExceptionMessage) : base(invalidCommandExceptionMessage) 
    { 
    
    }
}

internal class InvalidAccountException : Exception
{
    public const string InvalidAccountExceptionMessage = "Invalid account!";

    public InvalidAccountException(string invalidAccountExceptionMessage) : base(invalidAccountExceptionMessage) 
    { 
    
    }
}

internal class InsufficientBalanceException : Exception
{
    public const string InsufficientBalanceExceptionMessage = "Insufficient balance!";

    public InsufficientBalanceException(string insufficientBalanceExceptionMessage) : base(insufficientBalanceExceptionMessage) 
    { 
    
    }
}