using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class ScubaDiver : Diver
    {
        private const int oxygenLevel = 540;

        public ScubaDiver(string name) : base(name, oxygenLevel)
        {
        }

        public override void Miss(int TimeToCatch)
        {
            OxygenLevel -= (int)Math.Round(0.3 * TimeToCatch, MidpointRounding.AwayFromZero);
            if (OxygenLevel < 0)
            {
                OxygenLevel = 0;
            }

        }

        public override void RenewOxy()
        {
            this.OxygenLevel = 540;
        }
    }
}
