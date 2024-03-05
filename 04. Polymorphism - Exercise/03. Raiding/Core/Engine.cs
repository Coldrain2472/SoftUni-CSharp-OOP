using Raiding.Core.Interfaecs;
using Raiding.Factories.Interfaces;
using Raiding.IO.Interfaces;
using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IHeroFactory heroFactory;

        private ICollection<IHero> heroes;

        public Engine(IReader reader, IWriter writer, IHeroFactory heroFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.heroFactory = heroFactory;
            heroes = new List<IHero>();
        }

        public void Run()
        {
            int heroesCount = int.Parse(reader.ReadLine());

            for (int i = 0; i < heroesCount; i++)
            {
                string name = reader.ReadLine();
                string type = reader.ReadLine();

                try
                {
                    IHero currentHero = heroFactory.CreateHero(type, name);
                    heroes.Add(currentHero);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;
                }
            }

            int bossPower = int.Parse(reader.ReadLine());

            int heroesPowerSum = 0;

            foreach (IHero hero in heroes)
            {
                heroesPowerSum += hero.Power;
                Console.WriteLine(hero.CastAbility());
            }

            if (heroesPowerSum >= bossPower)
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }
        }
    }
}
