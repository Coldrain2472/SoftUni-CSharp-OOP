using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class CenterBack : Player
    {
        private const double rating = 4;

        public CenterBack(string name) : base(name, rating)
        {
        }

        public override void DecreaseRating()
        {
            if (Rating - 1 >= 1)
            {
                Rating -= 1;
            }
            else
            {
                Rating = 1;
            }
        }

        public override void IncreaseRating()
        {
            if (Rating + 1 <= 10)
            {
                Rating += 1;
            }
            else
            {
                Rating = 10;
            }
        }
    }
}
