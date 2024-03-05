using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Core.Interfaces;
using WildFarm.Factories.Interfaces;
using WildFarm.IO.Interfaces;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IFoodFactory foodFactory;
        private readonly IAnimalFactory animalFactory;

        private ICollection<IAnimal> animals;

        public Engine(IReader reader, IWriter writer, IFoodFactory foodFactory, IAnimalFactory animalFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.foodFactory = foodFactory;
            this.animalFactory = animalFactory;
            animals = new List<IAnimal>();
        }

        public void Run()
        {
            string command = string.Empty;

            while ((command = reader.ReadLine()) != "End")
            {
                IAnimal animal = CreateAnimal(command);
                IFood food = CreateFood();

                writer.WriteLine(animal.ProduceSound());

                try
                {
                    animal.Eat(food);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
                animals.Add(animal);
            }

            foreach (IAnimal animal in animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }

        private IAnimal CreateAnimal(string command)
        {
            string[] animalTokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IAnimal animal = animalFactory.CreateAnimal(animalTokens);
            return animal;
        }
        private IFood CreateFood()
        {
            string[] foodTokens = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string foodType = foodTokens[0];
            int foodQuantity = int.Parse(foodTokens[1]);

            IFood food = foodFactory.CreateFood(foodType, foodQuantity);
            return food;
        }
    }
}
