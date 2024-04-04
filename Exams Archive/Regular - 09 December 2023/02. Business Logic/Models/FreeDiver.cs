using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class FreeDiver : Diver
    {
        private const int oxygenLevel = 120;

        public FreeDiver(string name) : base(name, oxygenLevel)
        {
        }

        public override void Miss(int TimeToCatch)
        {
            double oxygenReduction = TimeToCatch * 0.6;
            OxygenLevel -= (int)Math.Round(oxygenReduction, MidpointRounding.AwayFromZero);
        }

        public override void RenewOxy()
        {
            OxygenLevel = oxygenLevel;
        }
    }
}
