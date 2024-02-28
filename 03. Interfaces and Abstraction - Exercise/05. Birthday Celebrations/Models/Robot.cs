using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthdayCelebrations.Models.Interfaces;

namespace BirthdayCelebrations.Models
{
    public class Robot : IIdentifiable
    {
        public string Model { get; private set; }
        public string Id { get; private set; }

        public Robot(string model, string id)
        {
            Model = model;
            Id = id;
        }
    }
}
