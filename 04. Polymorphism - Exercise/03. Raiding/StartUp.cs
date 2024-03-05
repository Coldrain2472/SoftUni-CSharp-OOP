using Raiding.Core;
using Raiding.Core.Interfaecs;
using Raiding.Factories;
using Raiding.IO;

namespace Raiding
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter(), new HeroFactory());
            engine.Run();
        }
    }
}
