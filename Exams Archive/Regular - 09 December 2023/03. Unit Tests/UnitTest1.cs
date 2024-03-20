namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;

    public class Tests
    {
        private RailwayStation station;

        [SetUp]
        public void Setup()
        {
            station = new RailwayStation("Test Station");
        }

        [Test]
        public void TrainHasArrivedShouldRemoveTrainFromArrivalTrainList()
        {
            station.NewArrivalOnBoard("TrainOne");
            station.TrainHasArrived("TrainOne");

            Assert.That(!station.ArrivalTrains.Contains("TrainOne"));
            Assert.AreEqual(0, station.ArrivalTrains.Count);
        }


        [Test]
        public void TrainHasArrivedWhenTrainIsNextReturnsExpectedMessage()
        {
            // Arrange
            station.NewArrivalOnBoard("Train A");

            // Act
            var result = station.TrainHasArrived("Train A");

            // Assert
            Assert.AreEqual("Train A is on the platform and will leave in 5 minutes.", result);
        }

        [Test]
        public void TrainHasArrivedWhenOtherTrainsAreBeforeReturnsExpectedMessage()
        {
            // Arrange
            station.NewArrivalOnBoard("Train A");
            station.NewArrivalOnBoard("Train B");

            // Act
            var result = station.TrainHasArrived("Train B");

            // Assert
            Assert.AreEqual("There are other trains to arrive before Train B.", result);
        }

        [Test]
        public void TrainHasLeftShouldRemoveTrainFromDepartureTrainList()
        {
            station.NewArrivalOnBoard("FirstTrain");
            station.NewArrivalOnBoard("SecondTrain");
            station.TrainHasArrived("FirstTrain");
            station.TrainHasArrived("SecondTrain");
            station.TrainHasLeft("FirstTrain");

            Assert.IsFalse(station.DepartureTrains.Contains("FirstTrain"));
        }

        [Test]
        public void TrainHasLeftWhenTrainHasLeftReturnsTrue()
        {
            // Arrange
            station.NewArrivalOnBoard("Train A");
            station.TrainHasArrived("Train A");

            // Act
            var result = station.TrainHasLeft("Train A");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TrainHasLeftWhenTrainHasNotLeftReturnsFalse()
        {
            // Arrange
            station.NewArrivalOnBoard("Train A");
            station.TrainHasArrived("Train A");

            // Act
            var result = station.TrainHasLeft("Train B");

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void RailwayStationConstructorNameThrowsArgumentException(string name)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new RailwayStation(null));
            Assert.Throws<ArgumentException>(() => new RailwayStation(""));
            Assert.Throws<ArgumentException>(() => new RailwayStation("   "));
        }
    }
}