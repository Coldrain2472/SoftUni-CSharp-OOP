using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthdayCelebrations.Models.Interfaces;

namespace BirthdayCelebrations.Models
{
    public class Pet : IAlive
    {
        public string Name { get; private set; }
        public string Birthdate { get; private set; }

        public Pet(string name, string birthdate)        {
            Name = name;
            Birthdate = birthdate;
        }

    }
}
