namespace CustomStack
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            StackOfStrings stack = new StackOfStrings();
            Console.WriteLine(stack.IsEmpty());

            stack.AddRange(new string[] { "1", "2", "3" });

            Console.WriteLine(string.Join(", ", stack));
        }
    }
}