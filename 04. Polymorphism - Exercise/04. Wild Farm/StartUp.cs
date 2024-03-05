using WildFarm.Core;
using WildFarm.Core.Interfaces;
using WildFarm.Factories;
using WildFarm.IO;

namespace WildFarm
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter(), new FoodFactory(), new AnimalFactory());

            engine.Run();
        }
    }
}
