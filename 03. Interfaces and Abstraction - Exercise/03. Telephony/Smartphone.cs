using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Models.Interfaces;

namespace Telephony
{
    public class Smartphone : IBrowsable
    {
        private string number;
        private string website;

        public string Number => number;
        public string Website => website;

        public void Browse(string website)
        {
            if (website.Any(char.IsDigit))
            {
                throw new ArgumentException("Invalid URL!");
            }

            Console.WriteLine($"Browsing: {website}!");
        }

        public void Call(string number)
        {
            if (number.Any(char.IsLetter))
            {
                throw new ArgumentException("Invalid number!");
            }

            Console.WriteLine($"Calling... {number}");
        }
    }
}