using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Models.Interfaces;

namespace Telephony
{
    public class StationaryPhone : ICallable
    {
        private string number;
        public string Number => number;

        public void Call(string number)
        {
            if (number.Any(char.IsLetter))
            {
                throw new ArgumentException("Invalid number!");
            }

            Console.WriteLine($"Dialing... {number}");
        }
    }
}