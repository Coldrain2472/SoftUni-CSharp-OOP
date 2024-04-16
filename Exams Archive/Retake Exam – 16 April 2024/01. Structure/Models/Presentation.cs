using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheContentDepartment.Models
{
    public class Presentation : Resource
    {
        private const int priority = 3;

        public Presentation(string name, string creator) : base(name, creator, priority)
        {
        }
    }
}
