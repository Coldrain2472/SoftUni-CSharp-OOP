namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    public class RailwayStationTests
    {
        private RailwayStation station;
        private string name = "Hogwarts";
        private string train1 = "London-Dublin";
        private string train2 = "London-Hogwarts";
        private string train3 = "London-Hogsmead";

        [SetUp]
        public void SetUp()
        {
            station = new RailwayStation(name);
        }

        [Test]
        public void ConstructorShouldInitializeStation()
        {
            Assert.IsNotNull(station);
        }

        [Test]
        public void ConstructorShouldSetNameCorrectly()
        {
            Assert.AreEqual(name, station.Name);
        }

        [Test]
        public void ConstructorShouldInitializeArrivalTrains()
        {
            Assert.IsNotNull(station.ArrivalTrains);
        }

        [Test]
        public void ConstructorShouldInitializeDepartureTrains()
        {
            Assert.IsNotNull(station.DepartureTrains);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]

        public void SettingTheNameToNullOrWhitespaceShouldThrowException(string invalidName)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(()
                => station = new RailwayStation(invalidName));

            Assert.AreEqual("Name cannot be null or empty!", exception.Message);
        }

        [Test]
        public void NewArrivalOnBoardShouldCorrectlyEnqueueTrainOnArrivalTrains()
        {
            Assert.AreEqual(0, station.ArrivalTrains.Count);
            station.NewArrivalOnBoard(train1);
            Assert.AreEqual(1, station.ArrivalTrains.Count);
            Assert.AreEqual(train1, station.ArrivalTrains.Peek());
        }


        [Test]
        public void TrainHasArrivedShouldRemoveTrainFromArrivalTrainList()
        {
            station.NewArrivalOnBoard(train1);
            station.TrainHasArrived(train1);

            Assert.That(!station.ArrivalTrains.Contains(train1));
            Assert.AreEqual(0, station.ArrivalTrains.Count);
        }

        [Test]
        public void TrainHasArrivedShouldAddTrainToDepartureTrainList()
        {
            station.NewArrivalOnBoard(train1);
            Assert.AreEqual(0, station.DepartureTrains.Count);
            station.TrainHasArrived(train1);

            Assert.AreEqual(train1, station.DepartureTrains.Peek());
            Assert.AreEqual(1, station.DepartureTrains.Count);
        }

        [Test]
        public void TrainHasArrivedShouldReturnCorrectMessageAboutDeparturingTrain()
        {
            station.NewArrivalOnBoard(train1);

            string expectedResult = $"{train1} is on the platform and will leave in 5 minutes.";
            Assert.AreEqual(expectedResult, station.TrainHasArrived(train1));
        }

        [Test]
        public void TrainHasArrivedShouldReturnCorrectMessageIfThereAreMoreArrivingTrains()
        {
            station.NewArrivalOnBoard(train1);
            station.NewArrivalOnBoard(train2);
            station.NewArrivalOnBoard(train3);

            string expectedResult = $"There are other trains to arrive before {train3}.";
            Assert.AreEqual(expectedResult, station.TrainHasArrived(train3));
        }

        [Test]
        public void TrainHasArrivedShouldRemoveFirstFromArrivalTrainsAndQueueToDepartureTrains()
        {
            station.NewArrivalOnBoard(train1);
            station.NewArrivalOnBoard(train2);
            station.TrainHasArrived(train1);

            Assert.AreEqual(false, station.ArrivalTrains.Contains(train1));
            Assert.AreEqual(train2, station.ArrivalTrains.Peek());
            Assert.AreEqual(true, station.DepartureTrains.Contains(train1));
            Assert.AreEqual(train1, station.DepartureTrains.Peek());
        }

        [Test]
        public void TrainHasLeftShouldReturnTrueWhenTrainIsTheOneToLeave()
        {
            station.NewArrivalOnBoard(train1);
            station.NewArrivalOnBoard(train2);
            station.TrainHasArrived(train1);
            station.TrainHasArrived(train2);

            Assert.AreEqual(true, station.TrainHasLeft(train1));
        }

        [Test]
        public void TrainHasLeftShouldRemoveTrainFromDepartureTrainList()
        {
            station.NewArrivalOnBoard(train1);
            station.NewArrivalOnBoard(train2);
            station.TrainHasArrived(train1);
            station.TrainHasArrived(train2);
            station.TrainHasLeft(train1);

            Assert.IsFalse(station.DepartureTrains.Contains(train1));
        }

        [Test]
        public void TrainHasLeftShouldReturnFalseWhenTrainIsNotTheOneToLeave()
        {
            station.NewArrivalOnBoard(train1);
            station.NewArrivalOnBoard(train2);
            station.TrainHasArrived(train1);
            station.TrainHasArrived(train2);

            Assert.AreEqual(false, station.TrainHasLeft(train2));
        }
    }
}