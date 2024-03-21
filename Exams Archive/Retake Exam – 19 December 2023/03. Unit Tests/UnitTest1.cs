namespace Television.Tests
{
    using System;
    using NUnit.Framework;
    public class Tests
    {
        private TelevisionDevice tv;

        [SetUp]
        public void Setup()
        {
            tv = new TelevisionDevice("Sony", 750.99, 1920, 1080);
        }

        [Test]
        public void ConstructorShouldInitializeCorrectly()
        {
            Assert.AreEqual("Sony", tv.Brand);
            Assert.AreEqual(750.99, tv.Price);
            Assert.AreEqual(1920, tv.ScreenWidth);
            Assert.AreEqual(1080, tv.ScreenHeigth);
        }

        [Test]
        public void SwitchOnWhenCalledReturnsCorrectString()
        {
            string expected = "Cahnnel 0 - Volume 13 - Sound On";
            string result = tv.SwitchOn();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ChangeChannelWithValidChannelReturnsCorrectChannel()
        {
            int expected = 5;
            int result = tv.ChangeChannel(5);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ChangeChannelWithNegativeChannelThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => tv.ChangeChannel(-1));
        }

        [Test]
        public void VolumeChangeIncreasingVolumeReturnsCorrectVolume()
        {
            string expected = "Volume: 20";
            string result = tv.VolumeChange("UP", 7);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void VolumeChangeDecreasingVolumeReturnsCorrectVolume()
        {
            string expected = "Volume: 5";
            string result = tv.VolumeChange("DOWN", 8);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MuteDeviceWhenCalledTogglesMuteStatus()
        {
            bool initialMuteStatus = tv.IsMuted;
            tv.MuteDevice();
            bool mutedAfterFirstCall = tv.IsMuted;
            tv.MuteDevice();
            bool mutedAfterSecondCall = tv.IsMuted;

            Assert.IsFalse(initialMuteStatus);
            Assert.IsTrue(mutedAfterFirstCall);
            Assert.IsFalse(mutedAfterSecondCall);
        }

        [Test]
        public void ToStringReturnsCorrectString()
        {
            string expected = "TV Device: Sony, Screen Resolution: 1920x1080, Price 750.99$";
            string result = tv.ToString();
            Assert.AreEqual(expected, result);
        }
    }
}