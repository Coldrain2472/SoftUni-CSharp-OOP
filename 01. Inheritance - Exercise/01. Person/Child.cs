using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    public class Child : Person
    {
        public Child(string name, int age) : base(name, age) 
        {

        }
        public override bool IsAgeCorrect()
        {
            if (Age > 15)
            {
                return false;
            }

            return true;
        }
    }
}
