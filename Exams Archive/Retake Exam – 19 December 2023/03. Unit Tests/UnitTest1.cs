namespace Television.Tests
{
    using System;
    using NUnit.Framework;
    public class Tests
    {
        private TelevisionDevice television;

        [SetUp]
        public void Setup()
        {
            television = new TelevisionDevice("Sony", 499.99, 1920, 1080);
        }

        [Test]
        public void ConstructorShouldInitializeCorrectly()
        {
            TelevisionDevice tv = new TelevisionDevice("Test", 1000, 1920, 1080);
            Assert.AreEqual("Test", tv.Brand);
            Assert.AreEqual(1000, tv.Price);
            Assert.AreEqual(1920, tv.ScreenWidth);
            Assert.AreEqual(1080, tv.ScreenHeigth);
        }

        [Test]
        public void SwitchOnWhenCalledReturnsCorrectString()
        {
            string expected = "Cahnnel 0 - Volume 13 - Sound On";
            string result = television.SwitchOn();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ChangeChannelWithValidChannelReturnsCorrectChannel()
        {
            int expected = 5;
            int result = television.ChangeChannel(5);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ChangeChannelWithNegativeChannelThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => television.ChangeChannel(-1));
        }

        [Test]
        public void VolumeChangeIncreasingVolumeReturnsCorrectVolume()
        {
            string expected = "Volume: 20";
            string result = television.VolumeChange("UP", 7);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void VolumeChangeDecreasingVolumeReturnsCorrectVolume()
        {
            string expected = "Volume: 5";
            string result = television.VolumeChange("DOWN", 8);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MuteDeviceWhenCalledTogglesMuteStatus()
        {
            bool initialMuteStatus = television.IsMuted;
            television.MuteDevice();
            bool mutedAfterFirstCall = television.IsMuted;
            television.MuteDevice();
            bool mutedAfterSecondCall = television.IsMuted;

            Assert.IsFalse(initialMuteStatus);
            Assert.IsTrue(mutedAfterFirstCall);
            Assert.IsFalse(mutedAfterSecondCall);
        }

        [Test]
        public void ToString_eturnsCorrectString()
        {
            string expected = "TV Device: Sony, Screen Resolution: 1920x1080, Price 499.99$";
            string result = television.ToString();
            Assert.AreEqual(expected, result);
        }
    }
}