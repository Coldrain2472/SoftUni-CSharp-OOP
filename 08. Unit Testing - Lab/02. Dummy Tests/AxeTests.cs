using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void WeaponShouldLooseDurabilityAfterEachAttack()
        {
            // Arrange
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(100, 100);

            // Act
            axe.Attack(dummy);

            // Assert
            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe Durability doesn't change after attack");
        }

        [Test]
        public void ShouldThrowAnExceptionWhenAttackingWithABrokenAxe()
        {
            // Arrange and Act
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(100, 100);

           // Assert
            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
        }
    }   
}