﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class SportCar : Car
    {
        public SportCar(int horsePower, double fuel) : base(horsePower, fuel)
        {

        }

        private const double DefaultFuelConsumption = 10;
        public override double FuelConsumption => DefaultFuelConsumption;

        public override void Drive(double kilometers)
        {
            Fuel -= kilometers * DefaultFuelConsumption;
        }
    }
}
