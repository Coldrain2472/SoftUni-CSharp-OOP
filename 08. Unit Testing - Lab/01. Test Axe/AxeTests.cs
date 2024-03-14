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
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(100, 100);

            axe.Attack(dummy);

            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe Durability doesn't change after attack");
        }

        [Test]
        public void ShouldThrowAnExceptionWhenAttackingWithABrokenAxe()
        {
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(100, 100);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
        }
    }   
}