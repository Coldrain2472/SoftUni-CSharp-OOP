using System;

namespace Restaurant
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Food food = new Food("Fish", 4.5m, 420);
            Tea tea = new Tea("SweetTea", 2.1m, 330);
            Soup soup = new Soup("ChickenSoup", 3.1m, 450);
            Cake cake = new Cake("ChocolateCake");
            Dessert dessert = new Dessert("ChocolateCake", 2.8m, 300, 800);
            ColdBeverage coldBeverage = new ColdBeverage("GreyGoose", 10.80m, 50);

            Console.WriteLine(food.Name, food.Price, food.Grams);
            Console.WriteLine(tea.Name, tea.Price, tea.Milliliters);
            Console.WriteLine(soup.Name, soup.Price, soup.Grams);
            Console.WriteLine(cake.Name, cake.Price, cake.Grams);
            Console.WriteLine(dessert.Name, dessert.Price, dessert.Grams);
            Console.WriteLine(coldBeverage.Name, coldBeverage.Price, coldBeverage.Milliliters);
        }
    }
}