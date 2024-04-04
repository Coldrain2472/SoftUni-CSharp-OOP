namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using static System.Collections.Specialized.BitVector32;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CounstructorShouldInitializeCorrectly()
        {
            RailwayStation railway = new RailwayStation("Hogwarts");
            Assert.AreEqual("Hogwarts", railway.Name);
            Assert.AreEqual(0, railway.ArrivalTrains.Count);
            Assert.AreEqual(0, railway.DepartureTrains.Count);
        }

        [TestCase("  ")]
        [TestCase("")]
        [TestCase(null)]
        public void RailWayNameShouldThrowAnException(string name)
        {
            Assert.Throws<ArgumentException>(() => new RailwayStation("  "));
            Assert.Throws<ArgumentException>(() => new RailwayStation(""));
            Assert.Throws<ArgumentException>(() => new RailwayStation(null));
        }

        [Test]
        public void NewArrivalOnBoardShouldWorkProperly()
        {
            RailwayStation railway = new RailwayStation("Hogwarts");
            railway.NewArrivalOnBoard("Train A");
            Assert.AreEqual("Train A", railway.ArrivalTrains.Peek());
        }

        [Test]
        public void TrainHasArrivedShouldReturnTheCorrectMessage()
        {
            RailwayStation railway = new RailwayStation("Hogwarts");
            railway.NewArrivalOnBoard("Train A");
            railway.NewArrivalOnBoard("Train B");
            var result = railway.TrainHasArrived("Train B");
            Assert.AreEqual("There are other trains to arrive before Train B.", result);
        }

        [Test]
        public void TrainHasArrivedShouldReturnTheCorrectString()
        {
            RailwayStation railway = new RailwayStation("Hogwarts");
            railway.NewArrivalOnBoard("Train A");
            var result = railway.TrainHasArrived("Train A");
            Assert.AreEqual("Train A is on the platform and will leave in 5 minutes.", result);
        }

        [Test]
        public void TrainHasLeftShouldRemoveTrainFromDepartureTrainList()
        {
             RailwayStation railway = new RailwayStation("Hogwarts");
            railway.NewArrivalOnBoard("Train A");
            railway.NewArrivalOnBoard("Train B");
            railway.TrainHasArrived("Train A");
            railway.TrainHasArrived("Train B");
            railway.TrainHasLeft("Train A");

            Assert.IsFalse(railway.DepartureTrains.Contains("Train A"));
        }

        [Test]
        public void TrainHasLeftWhenTrainHasLeftReturnsTrue()
        {
            // Arrange
            RailwayStation railway = new RailwayStation("Hogwarts");
            railway.NewArrivalOnBoard("Train A");
            railway.TrainHasArrived("Train A");

            // Act
            var result = railway.TrainHasLeft("Train A");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TrainHasLeftWhenTrainHasNotLeftReturnsFalse()
        {
            // Arrange
            RailwayStation railway = new RailwayStation("Hogwarts");
            railway.NewArrivalOnBoard("Train A");
            railway.TrainHasArrived("Train A");

            // Act
            var result = railway.TrainHasLeft("Train B");

            // Assert
            Assert.IsFalse(result);
        }

    }
}