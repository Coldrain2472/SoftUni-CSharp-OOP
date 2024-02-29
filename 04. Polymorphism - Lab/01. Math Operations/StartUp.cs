namespace Operations
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            MathOperations x = new MathOperations();
            Console.WriteLine(x.Add(2, 3));
            Console.WriteLine(x.Add(2.2, 3.3, 5.5));
            Console.WriteLine(x.Add(2.2m, 3.3m, 4.4m));
        }
    }
}