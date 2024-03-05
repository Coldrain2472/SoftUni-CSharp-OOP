using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Models
{
    public abstract class BaseHero : IHero
    {
        private string name;
        private int power;

        protected BaseHero(string name)
        {
            Name = name;
        }

        public string Name { get => name; private set => name = value; }
        public abstract int Power { get; }

        public virtual string CastAbility()
        {
            return $"{this.GetType().Name} - {Name}";
        }
    }
}
