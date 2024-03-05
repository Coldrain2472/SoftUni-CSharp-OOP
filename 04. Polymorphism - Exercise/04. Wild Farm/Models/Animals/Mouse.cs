using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Abstract_Classes;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Mouse : Mammal
    {
        private const double mouseWeightMultiplier = 0.1;

        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion) 
        { 
        
        }

        protected override double WeightMultiplier => mouseWeightMultiplier;

        protected override IReadOnlyCollection<Type> PreferredFoodTypes
            => new HashSet<Type> { typeof(Fruit), typeof(Vegetable) };

        public override string ProduceSound() => "Squeak";

        public override string ToString()
        {
            return base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
