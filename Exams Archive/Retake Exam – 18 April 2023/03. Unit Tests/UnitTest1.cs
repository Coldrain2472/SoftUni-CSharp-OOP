using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VehicleGarage.Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VehicleConstructorShouldInitializeCorrectly()
        {
            Vehicle vehicle = new Vehicle("VW", "Passat", "H3993BM");
            Assert.AreEqual("VW", vehicle.Brand);
            Assert.AreEqual("Passat", vehicle.Model);
            Assert.AreEqual("H3993BM", vehicle.LicensePlateNumber);
            Assert.AreEqual(100, vehicle.BatteryLevel);
            Assert.IsFalse(vehicle.IsDamaged);
        }

        [Test]
        public void GarageConstructorShouldInitializeCorrectly()
        {
            Garage garage = new Garage(10);
            Assert.AreEqual(10, garage.Capacity);
            Assert.AreEqual(0, garage.Vehicles.Count);
        }

        [Test]
        public void AddVehicleShouldWorkCorrectlyIfThereIsNoFreeCapacity()
        {
            Garage garage = new Garage(1);
            Vehicle vehicleOne = new Vehicle("VW", "Passat", "H3993BM");
            Vehicle vehicleTwo = new Vehicle("VW", "Golf", "H7557BT");
            garage.AddVehicle(vehicleOne);
            var result = garage.AddVehicle(vehicleTwo);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddVehicleShouldWorkCorrectlyIfThereIsAVehicleWithTheSameLicensePlateNumber()
        {
            Garage garage = new Garage(3);
            Vehicle vehicleOne = new Vehicle("VW", "Passat", "H3993BM");
            Vehicle vehicleTwo = new Vehicle("VW", "Golf", "H3993BM");
            garage.AddVehicle(vehicleOne);
            var result = garage.AddVehicle(vehicleTwo);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddVehicleShouldReturnTrueWhenTheAboveCasesAreMet()
        {
            Garage garage = new Garage(3);
            Vehicle vehicleOne = new Vehicle("VW", "Passat", "H3993BM");
            Vehicle vehicleTwo = new Vehicle("VW", "Golf", "H7557BT");
            garage.AddVehicle(vehicleOne);
            var result = garage.AddVehicle(vehicleTwo);
            Assert.IsTrue(result);
            Assert.AreEqual(2, garage.Vehicles.Count);
        }

        [Test]
        public void ChargeVehicleShouldWorkProperly()
        {
            Garage garage = new Garage(3);
            Vehicle vehicleOne = new Vehicle("VW", "Passat", "H3993BM");
            Vehicle vehicleTwo = new Vehicle("VW", "Golf", "H7557BT");
            garage.AddVehicle(vehicleOne);
            garage.AddVehicle(vehicleTwo);
            var result = garage.ChargeVehicles(100);
            Assert.AreEqual(100, vehicleOne.BatteryLevel);
            Assert.AreEqual(100, vehicleTwo.BatteryLevel);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void ChargeVehiclesShouldReturnZeroIfAllVehicleBatteryLevelIsGreaterThanParameterBatteryLevel()
        {
            Garage garage = new Garage(3);
            Vehicle vehicleOne = new Vehicle("VW", "Passat", "H3993BM");
            Vehicle vehicleTwo = new Vehicle("VW", "Golf", "H7557BT");
            garage.AddVehicle(vehicleOne);
            garage.AddVehicle(vehicleTwo);
            Assert.AreEqual(0, garage.ChargeVehicles(50));
        }


        [Test]
        public void DriveVehicleShouldNotWorkIfVehicleIsDamaged()
        {
            Garage garage = new Garage(3);
            Vehicle vehicleOne = new Vehicle("VW", "Passat", "H3993BM");
            Vehicle vehicleTwo = new Vehicle("VW", "Golf", "H7557BT");
            vehicleOne.IsDamaged = true;
            garage.AddVehicle(vehicleOne);
            garage.DriveVehicle(vehicleOne.LicensePlateNumber, 20, false);
            Assert.AreEqual(100, garage.Vehicles.First(v => v.LicensePlateNumber == "H3993BM").BatteryLevel);
            Assert.IsTrue(vehicleOne.IsDamaged);
        }

        [Test]
        public void DriveVehicleShouldNotWorkIfBatteryDrainageIsGreaterThan100()
        {
            Garage garage = new Garage(3);
            Vehicle vehicleOne = new Vehicle("VW", "Passat", "H3993BM");
            garage.AddVehicle(vehicleOne);
            Assert.AreEqual(100, vehicleOne.BatteryLevel);
            garage.DriveVehicle(vehicleOne.LicensePlateNumber, 101, false);
            Assert.AreEqual(100, vehicleOne.BatteryLevel);
        }

        [Test]
        public void DriveVehicleShouldNotWorkIfBatteryLevelIsLessThanBatteryDrainage()
        {
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("VW", "Passat", "H3993BM");
            garage.AddVehicle(vehicle);
            vehicle.BatteryLevel = 80;
            garage.DriveVehicle(vehicle.LicensePlateNumber, 90, false);
            Assert.AreEqual(80, garage.Vehicles.First(v => v.LicensePlateNumber == "H3993BM").BatteryLevel);
        }

        [Test]
        public void DriveCarWhenSuccessfulShouldDecreaseBatteryLevelCorrectly()
        {
             Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("VW", "Passat", "H3993BM");
            garage.AddVehicle(vehicle);
            Assert.AreEqual(100, garage.Vehicles.First(v => v.LicensePlateNumber == "H3993BM").BatteryLevel);
            garage.DriveVehicle(vehicle.LicensePlateNumber, 50, false);
            Assert.AreEqual(100 - 50, garage.Vehicles.First(v => v.LicensePlateNumber == "H3993BM").BatteryLevel);
        }

        [Test]
        public void DriveCarWhenSuccessfulShouldChangeIsDamagedPropertyOfVehicleWhenAccidentOccured()
        {
            Garage garage = new Garage(3);
            Vehicle vehicleOne = new Vehicle("VW", "Passat", "H3993BM");
            Vehicle vehicleTwo = new Vehicle("VW", "Golf", "H7557BT");
            garage.AddVehicle(vehicleOne);
            garage.AddVehicle(vehicleTwo);
            garage.DriveVehicle(vehicleOne.LicensePlateNumber, 50, true);
            garage.DriveVehicle(vehicleTwo.LicensePlateNumber, 50, false);
            Assert.IsTrue(vehicleOne.IsDamaged);
            Assert.IsFalse(vehicleTwo.IsDamaged);
        }

        [Test]
        public void RepairVehiclesShouldChangeIsDamagedPropertyOfAVehicle()
        {
             Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("VW", "Passat", "H3993BM");
            vehicle.IsDamaged = true;
            garage.AddVehicle(vehicle);
            Assert.IsTrue(garage.Vehicles.First(v => v.LicensePlateNumber == "H3993BM").IsDamaged);
            garage.RepairVehicles();
            Assert.IsFalse(garage.Vehicles.First(v => v.LicensePlateNumber == "H3993BM").IsDamaged);
        }

        [Test]
        public void RepairVehiclesShouldCorrectlyReturnStringWithCountOfRepairedVehicles()
        {
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("VW", "Passat", "H3993BM");
            int repairedVehicles = 0;
            string expectedResult = $"Vehicles repaired: {repairedVehicles}";
            garage.AddVehicle(vehicle);
            Assert.AreEqual(expectedResult, garage.RepairVehicles());
            garage.DriveVehicle(vehicle.LicensePlateNumber, 10, true);
            expectedResult = $"Vehicles repaired: {++repairedVehicles}";
            Assert.AreEqual(expectedResult, garage.RepairVehicles());
        }
    }
}