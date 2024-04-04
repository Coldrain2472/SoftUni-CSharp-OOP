namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorShouldInitializeCorrectly()
        {
            Device device = new Device(5);
            Assert.AreEqual(5, device.MemoryCapacity);
            Assert.AreEqual(0, device.Photos);
            Assert.AreEqual(0, device.Applications.Count);
            Assert.AreEqual(5, device.AvailableMemory);
        }

        [Test]
        public void TakePhotoShouldWorkProperly()
        {
            Device device = new Device(5);
            device.TakePhoto(2);
            Assert.AreEqual(3, device.AvailableMemory);
            Assert.AreEqual(1, device.Photos);
        }

        [Test]
        public void TakePhotoShouldWorkCorrectly()
        {
            Device device = new Device(5);
            var result = device.TakePhoto(6);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void InstallAppShouldThrowAnException()
        {
            Device device = new Device(5);
            Assert.Throws<InvalidOperationException>(()=> device.InstallApp("Tiktok", 6));
        }

        [Test]
        public void InstallAppShouldWorkProperly()
        {
            Device device = new Device(5);
            var result = device.InstallApp("Facebook", 3);
            Assert.AreEqual(1, device.Applications.Count);
            Assert.AreEqual(2, device.AvailableMemory);
            Assert.AreEqual($"Facebook is installed successfully. Run application?", result);
        }

        [Test]
        public void FormatDeviceShouldWorkCorrectly()
        {
            Device device = new Device(5);
            device.TakePhoto(1);
            device.FormatDevice();
            Assert.AreEqual(0, device.Photos);
            Assert.AreEqual(5, device.AvailableMemory);
            Assert.AreEqual(0, device.Applications.Count);
        }

        [Test]
        public void GetDeviceStatusShouldReturnTheCorrectString()
        {
            Device device = new Device(10000);
            device.TakePhoto(2);
            int expectedMemory = device.MemoryCapacity - 2 * 3;
            device.InstallApp("Tiktok", 2);
            device.InstallApp("Facebook", 2);
            StringBuilder expectedResult = new StringBuilder();
            expectedResult.AppendLine($"Memory Capacity: {device.MemoryCapacity} MB, Available Memory: {expectedMemory} MB");
            expectedResult.AppendLine($"Photos Count: 1");
            expectedResult.AppendLine($"Applications Installed: Tiktok, Facebook");

            Assert.AreEqual(expectedResult.ToString().TrimEnd(), device.GetDeviceStatus());
            var result = device.GetDeviceStatus();
            Assert.AreEqual($"Memory Capacity: {device.MemoryCapacity} MB, Available Memory: {device.AvailableMemory} MB{Environment.NewLine}Photos Count: {device.Photos}{Environment.NewLine}Applications Installed: {string.Join(", ", device.Applications)}", result);
        }
    }
}