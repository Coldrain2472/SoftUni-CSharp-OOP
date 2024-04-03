using NUnit.Framework;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FactoryConstructorShouldInitializeCorrectly()
        {
            Factory factory = new Factory("Ficosota", 200);
            Assert.AreEqual("Ficosota", factory.Name);
            Assert.AreEqual(200, factory.Capacity);
        }

        [Test]
        public void RobotConstructorShouldInitializeCorrectly()
        {
            Robot robot = new Robot("R2D2", 500, 5);
            Assert.AreEqual("R2D2", robot.Model);
            Assert.AreEqual(500, robot.Price);
            Assert.AreEqual(5, robot.InterfaceStandard);
        }

        [Test]
        public void SUpplementConstructorShouldInitializeCorreclty()
        {
            Supplement supplement = new Supplement("Arm", 6);
            Assert.AreEqual("Arm", supplement.Name);
            Assert.AreEqual(6, supplement.InterfaceStandard);
        }

        [Test]
        public void SupplementToStringMethodShouldWorkProperly()
        {
            Supplement supplement = new Supplement("Arm", 6);
            var result = supplement.ToString();
            Assert.AreEqual($"Supplement: {supplement.Name} IS: {supplement.InterfaceStandard}", result);
        }

        [Test]
        public void RobotToStringMethodShouldWorkProperly()
        {
            Robot robot = new Robot("R2D2", 500, 5);
            var result = robot.ToString();
            Assert.AreEqual($"Robot model: {robot.Model} IS: {robot.InterfaceStandard}, Price: {robot.Price:f2}", result);
        }

        [Test]
        public void ProduceRobotShouldReturnProducedMessageWhenFactoryHasCapacity()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);

            // Act
            string result = factory.ProduceRobot("Model1", 1000.0, 12345);

            // Assert
            Assert.AreEqual("Produced --> Robot model: Model1 IS: 12345, Price: 1000.00", result);
        }

        [Test]
        public void ProduceRobotShouldReturnErrorMessageWhenFactoryIsAtCapacity()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 1);
            factory.ProduceRobot("Model1", 1000.0, 12345);

            // Act
            string result = factory.ProduceRobot("Model2", 1500.0, 54321);

            // Assert
            Assert.AreEqual("The factory is unable to produce more robots for this production day!", result);
        }

        [Test]
        public void ProduceSupplementShouldAddSupplementToList()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);

            // Act
            string result = factory.ProduceSupplement("Supplement1", 12345);

            // Assert
            Assert.AreEqual("Supplement: Supplement1 IS: 12345", result);
            Assert.AreEqual(1, factory.Supplements.Count);
        }

        [Test]
        public void UpgradeRobotShouldReturnTrueWhenSupplementIsAddedSuccessfully()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);
            Robot robot = new Robot("Model1", 1000.0, 12345);
            Supplement supplement = new Supplement("Supplement1", 12345);

            // Act
            bool result = factory.UpgradeRobot(robot, supplement);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, robot.Supplements.Count);
        }

        [Test]
        public void UpgradeRobotShouldReturnFalseWhenSupplementIsAlreadyAddedOrIncompatible()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);
            Robot robot = new Robot("Model1", 1000.0, 12345);
            Supplement supplement = new Supplement("Supplement1", 12345);
            factory.UpgradeRobot(robot, supplement);

            // Act
            bool result = factory.UpgradeRobot(robot, supplement);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, robot.Supplements.Count); 
        }

        [Test]
        public void SellRobotShouldReturnHighestPricedRobotWhenPriceIsValid()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);
            factory.ProduceRobot("Model1", 1000.0, 12345);
            factory.ProduceRobot("Model2", 1500.0, 54321);

            // Act
            Robot result = factory.SellRobot(1200.0);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Model1", result.Model); 
        }

        [Test]
        public void SellRobotShouldReturnNullWhenPriceIsBelowMinimum()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);
            factory.ProduceRobot("Model1", 1000.0, 12345);

            // Act
            Robot result = factory.SellRobot(800.0);

            // Assert
            Assert.IsNull(result);
        }

    }
}