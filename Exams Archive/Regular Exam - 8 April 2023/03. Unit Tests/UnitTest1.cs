using NUnit.Framework;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RobotConstructorShouldInitializeCorrectly()
        {
            // Arrange & Act
            Robot robot = new Robot("Test", 10.0, 5);
            
            // Assert
            Assert.IsNotNull(robot);
            Assert.IsNotNull(robot.Supplements);
            Assert.AreEqual("Test", robot.Model);
            Assert.AreEqual(10.0, robot.Price);
            Assert.AreEqual(5, robot.InterfaceStandard);
        }

        public void SupplementConstructorShouldInitializeCorrectly()
        {
            // Arrange & Act
            Supplement supplement = new Supplement("Test", 10);
            
            // Assert
            Assert.IsNotNull(supplement);
            Assert.AreEqual(supplement.Name, "Pistol");
            Assert.AreEqual(supplement.InterfaceStandard, 10);
        }

        [Test]
        public void ProduceRobotValidParams()
        {
            Factory factory = new Factory("Test", 10);
            string actualResult = factory.ProduceRobot("Robo-3", 2500, 22);
            string expectedResult = "Produced --> Robot model: Robo-3 IS: 22, Price: 2500.00";

            Assert.AreEqual(expectedResult, actualResult);
        }

        public void ProduceRobotWhenCapacityAvailableProducesRobot()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);

            // Act
            string result = factory.ProduceRobot("Test Model", 100, 3);

            // Assert
            Assert.AreEqual("Produced --> Robot model: Test Model IS: 3, Price: 100.00", result);
            Assert.AreEqual(1, factory.Robots.Count);
        }

        [Test]
        public void ProduceRobotWhenCapacityExceededReturnsErrorMessage()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 1);
            factory.ProduceRobot("First Robot", 100, 3);

            // Act
            string result = factory.ProduceRobot("Second Robot", 200, 3);

            // Assert
            Assert.AreEqual("The factory is unable to produce more robots for this production day!", result);
            Assert.AreEqual(1, factory.Robots.Count);
        }

        [Test]
        public void ProduceSupplement_ValidParams()
        {
            Factory factory = new Factory("SpaceX", 2);

            string actualResult = factory.ProduceSupplement("SpecializedArm", 8);

            string expectedResult = "Supplement: SpecializedArm IS: 8";
            Assert.AreEqual(expectedResult, actualResult);
        }

            [Test]
        public void ProduceSupplementAddsSupplementToList()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);

            // Act
            string result = factory.ProduceSupplement("Test Supplement", 3);

            // Assert
            Assert.AreEqual("Supplement: Test Supplement IS: 3", result);
            Assert.AreEqual(1, factory.Supplements.Count);
        }

        [Test]
        public void UpgradeRobotWhenSupplementCompatibleAddsSupplementToRobot()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);
            Robot robot = new Robot("Test Model", 100, 3);
            factory.Robots.Add(robot);
            Supplement supplement = new Supplement("Test Supplement", 3);

            // Act
            bool result = factory.UpgradeRobot(robot, supplement);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, robot.Supplements.Count);
        }

        [Test]
        public void UpgradeRobotWhenSupplementIncompatibleReturnsFalse()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);
            Robot robot = new Robot("Test Model", 100, 3);
            factory.Robots.Add(robot);
            Supplement supplement = new Supplement("Test Supplement", 4);

            // Act
            bool result = factory.UpgradeRobot(robot, supplement);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, robot.Supplements.Count);
        }

        [Test]
        public void SellRobotReturnsHighestPricedRobotWithinBudget()
        {
            // Arrange
            Factory factory = new Factory("Test Factory", 10);
            factory.Robots.Add(new Robot("Model A", 100, 3));
            factory.Robots.Add(new Robot("Model B", 200, 3));
            factory.Robots.Add(new Robot("Model C", 150, 3));

            // Act
            Robot soldRobot = factory.SellRobot(175);

            // Assert
            Assert.IsNotNull(soldRobot);
            Assert.AreEqual("Model C", soldRobot.Model);
        }

        [Test]
        public void RobotToStringReturnsCorrectFormat()
        {
            // Arrange
            Robot robot = new Robot("Test Model", 100, 3);

            // Act
            string result = robot.ToString();

            // Assert
            Assert.AreEqual("Robot model: Test Model IS: 3, Price: 100.00", result);
        }

        [Test]
        public void SupplementToStringReturnsCorrectFormat()
        {
            // Arrange
            Supplement supplement = new Supplement("Test Supplement", 3);

            // Act
            string result = supplement.ToString();

            // Assert
            Assert.AreEqual("Supplement: Test Supplement IS: 3", result);
        }
    }
}