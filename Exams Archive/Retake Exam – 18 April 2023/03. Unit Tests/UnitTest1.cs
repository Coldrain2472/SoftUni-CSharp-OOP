using NUnit.Framework;
using System.Collections.Generic;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GarageConstructorShouldInitializeCorrectly()
        {
            Garage garage = new Garage(10);

            Assert.AreEqual(10, garage.Capacity);
        }

        [Test]
        public void VehicleConstructorShouldInitializeCorrectly()
        {
            Vehicle vehicle = new Vehicle("VW", "Golf", "H3993BM");
            
            string expectedBrand = "VW";
            string expectedModel = "Golf";
            string expectedLicensePlateNumber = "H3993BM";
            
            Assert.AreEqual(expectedBrand, vehicle.Brand);
            Assert.AreEqual(expectedModel, vehicle.Model);
            Assert.AreEqual(expectedLicensePlateNumber, vehicle.LicensePlateNumber);
        }

        [Test]
        public void AddVehicleShouldReturnTrueIfSuccesfullyAddsAVehicle()
        {
            Vehicle vehicle = new Vehicle("Test", "test", "123");
            Garage garage = new Garage(2);
            Assert.IsTrue(garage.AddVehicle(vehicle));
        }

        [Test]
        public void AddVehicleShouldReturnFalseIfThereIsNoCapacity()
        {
            Vehicle vehicle = new Vehicle("Test", "test", "123");
            Vehicle vehicle2 = new Vehicle("Second test", "test test", "529");
            Garage garage = new Garage(1);
            garage.AddVehicle(vehicle);
            Assert.IsFalse(garage.AddVehicle(vehicle2));
        }

        [Test]
        public void AddVehicleShouldReturnFalseIfThereIsAVehicleWithTheSameLicensePlateNumber()
        {
            Vehicle vehicle = new Vehicle("Test", "test", "123");
            Vehicle vehicle2 = new Vehicle("Second test", "test test", "123");
            Garage garage = new Garage(2);
            garage.AddVehicle(vehicle);
            Assert.IsFalse(garage.AddVehicle(vehicle2));
        }

        [Test]
        public void GarageChargeVehiclesShouldReturnTheNumberChargedVehicles()
        {
            // Arrange
            Garage garage = new Garage(3);
            Vehicle vehicleOne = new Vehicle("Volkswagen", "Polo", "CB2404XA");
            Vehicle vehicleTwo = new Vehicle("Mercedes", "Benz", "PA2354TB");
            Vehicle vehicleThree = new Vehicle("Renault", "Clio", "CA2583KP");

            // Act
            vehicleOne.BatteryLevel = 50;
            vehicleTwo.BatteryLevel = 49;
            vehicleThree.BatteryLevel = 77;

            garage.AddVehicle(vehicleOne);
            garage.AddVehicle(vehicleTwo);
            garage.AddVehicle(vehicleThree);

            // Assert
            Assert.AreEqual(2, garage.ChargeVehicles(50));
            Assert.AreEqual(100, vehicleOne.BatteryLevel);
            Assert.AreEqual(100, vehicleTwo.BatteryLevel);
            Assert.AreEqual(77, vehicleThree.BatteryLevel);
        }

        [Test]
        public void GarageDriveVehicleShouldDecreaseTheCarsBattery()
        {
            // Arrange
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("Volkswagen", "Polo", "CB2404XA");
            garage.AddVehicle(vehicle);

            // Act
            garage.DriveVehicle("CB2404XA", 20, false);

            // Assert
            Assert.AreEqual(80, vehicle.BatteryLevel);
        }

        [Test]
        public void GarageDriveVehicleShouldDamageTheCarIfAccidentHappened()
        {
            // Arrange
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("Volkswagen", "Polo", "CB2404XA");
            Vehicle vehicleTwo = new Vehicle("Mercedes", "Benz", "PA2354TB");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicleTwo);

            // Act
            garage.DriveVehicle("CB2404XA", 20, true);
            garage.DriveVehicle("PA2354TB", 20, false);

            // Assert
            Assert.AreEqual(80, vehicle.BatteryLevel);
            Assert.IsTrue(vehicle.IsDamaged);
            Assert.IsFalse(vehicleTwo.IsDamaged);
        }

        [Test]
        public void GarageDriveVehicleMethodShouldntDoAnythingWhenIsDamaged()
        {
            // Arrange
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("Volkswagen", "Polo", "CB2404XA");

            // Act
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("CB2404XA", 20, true);
            garage.DriveVehicle("CB2404XA", 20, false);

            // Assert
            Assert.AreEqual(80, vehicle.BatteryLevel);
        }

        [Test]
        public void GarageDriveVehicleMethodShouldntDoAnythingWhenBatterydrainageIsOver100()
        {
            // Arrange
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("Mercedes", "Benz", "PA2354TB");

            // Act
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("PA2354TB", 101, false);

            // Assert
            Assert.AreEqual(100, vehicle.BatteryLevel);
        }

        [Test]
        public void GarageDriveVehicleMethodShouldntDoAnythingWhenBatteryLevelIsLessThanBatteryDrainage()
        {
            // Arrange
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("Renault", "Clio", "CA2583KP");
            vehicle.BatteryLevel = 10;

            // Act
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("CA2583KP", 11, false);

            // Assert
            Assert.AreEqual(10, vehicle.BatteryLevel);
        }

        [Test]
        public void GarageRepairVehiclesShouldReturnTheNumberRepairedVehicles()
        {
            // Arrange
            Garage garage = new Garage(3);
            Vehicle vehicleOne = new Vehicle("Volkswagen", "Polo", "CB2404XA");
            Vehicle vehicleTwo = new Vehicle("Mercedes", "Benz", "PA2354TB");
            Vehicle vehicleThree = new Vehicle("Renault", "Clio", "CA2583KP");

            vehicleOne.IsDamaged = true;
            vehicleTwo.IsDamaged = false;
            vehicleThree.IsDamaged = true;

            garage.AddVehicle(vehicleOne);
            garage.AddVehicle(vehicleTwo);
            garage.AddVehicle(vehicleThree);

            // Assert
            Assert.AreEqual("Vehicles repaired: 2", garage.RepairVehicles());
            Assert.IsFalse(vehicleOne.IsDamaged);
            Assert.IsFalse(vehicleTwo.IsDamaged);
            Assert.IsFalse(vehicleThree.IsDamaged);
        }
    }
}