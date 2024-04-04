using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Goalkeeper : Player
    {
        private const double rating = 2.5;

        public Goalkeeper(string name) : base(name, rating)
        {
        }

        public override void DecreaseRating()
        {
            if (Rating - 1.25 >= 1)
            {
                Rating -= 1.25;
            }
            else
            {
                Rating = 1;
            }
        }

        public override void IncreaseRating()
        {
            if (Rating + 0.75 <= 10)
            {
                Rating += 0.75;
            }
            else
            {
                Rating = 10;
            }
        }
    }
}
