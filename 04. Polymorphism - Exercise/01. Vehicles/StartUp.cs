using Vehicles.Models;
namespace Vehicles
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Car car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]));
            string[] truckInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Truck truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]));

            int numberOfCommands = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                string[] command = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                // refuel/drive car/truck

                if (command[0] == "Refuel")
                {
                    double fuel = double.Parse(command[2]);

                    if (command[1] == "Car")
                    {
                        car.Refuel(fuel);
                    }
                    else if (command[1] == "Truck")
                    {
                        truck.Refuel(fuel);
                    }
                }
                else if (command[0] == "Drive")
                {
                    double distance = double.Parse(command[2]);

                    if (command[1] == "Car")
                    {
                        Console.WriteLine(car.Drive(distance));
                    }
                    else if (command[1] == "Truck")
                    {
                        Console.WriteLine(truck.Drive(distance));
                    }
                }
            }

            Console.WriteLine($"Car: {car.FuelQuantity:f2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:f2}");
        }
    }
}