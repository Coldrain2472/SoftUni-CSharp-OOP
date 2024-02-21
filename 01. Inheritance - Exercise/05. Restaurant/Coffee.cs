using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Restaurant
{
    public class Coffee : HotBeverage
    {
        private const double CoffeeMililiters = 50;
        private const decimal CoffeePrice = 3.50m;
        public double Caffeine { get;  set; }

        public Coffee(string name, double caffeine) : base(name, CoffeePrice, CoffeeMililiters)
        {
            Caffeine = caffeine;
        }
    }
}
