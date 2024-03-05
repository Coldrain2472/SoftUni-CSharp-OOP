using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Abstract_Classes;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        private const double catWeightMultiplier = 0.3;

        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed) 
        {
        
        }

        protected override double WeightMultiplier => catWeightMultiplier;

        protected override IReadOnlyCollection<Type> PreferredFoodTypes
            => new HashSet<Type> { typeof(Meat), typeof(Vegetable) };

        public override string ProduceSound() => "Meow";
    }
}
