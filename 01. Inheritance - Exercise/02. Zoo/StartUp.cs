using System;

namespace Zoo
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Snake snake = new Snake("Azax");
            Bear bear = new Bear("Karla");

            Console.WriteLine(snake.Name);
            Console.WriteLine(bear.Name);
        }
    }
}