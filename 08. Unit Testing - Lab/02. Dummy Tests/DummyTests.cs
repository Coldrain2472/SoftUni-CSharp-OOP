using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void DummyShouldLoseHealthAfterAnAttack()
        {
            // Arrange
            Dummy dummy = new Dummy(100, 100);
            Axe axe = new Axe(10, 10);
            // Act
            dummy.TakeAttack(10);
            // Assert
            Assert.AreEqual(90, dummy.Health);
        }

        [Test]
        public void DeadDummyShouldThrowAnExceptionIfAttacked()
        {
            // Arrange
            Dummy dummy = new Dummy(100, 100);

            // Act
            dummy.TakeAttack(100);

           // Assert
            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(100), "Dummy is dead.");
        }

        [Test]
        public void DeadDummyCanGiveXP()
        {
            // Arrange
            Dummy dummy = new Dummy(100, 100);
            
            // Act
            dummy.TakeAttack(100);
            
            // Assert
            Assert.AreEqual(100, dummy.GiveExperience());
        }

        [Test]
        public void AliveDummyCantGiveXP()
        {
            // Arrange
            Dummy dummy = new Dummy(110, 100);
           
            // Act
            dummy.TakeAttack(100);

            // Assert
            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(), "Target is not dead");
        }
    }
}