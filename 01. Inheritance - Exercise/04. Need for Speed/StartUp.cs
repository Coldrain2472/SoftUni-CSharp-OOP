using System;

namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SportCar sportCar = new SportCar(500, 150);
            RaceMotorcycle raceMotorCycle = new RaceMotorcycle(300, 100);
            Car car = new Car(60, 60);

            sportCar.Drive(10);
            raceMotorCycle.Drive(10);
            car.Drive(10);

            Console.WriteLine($"Sport Car remaining fuel: {sportCar.Fuel}");
            Console.WriteLine($"Race Motorcycle remaining fuel: {raceMotorCycle.Fuel}");
            Console.WriteLine($"Car remaining fuel: {car.Fuel}");
        }
    }
}
