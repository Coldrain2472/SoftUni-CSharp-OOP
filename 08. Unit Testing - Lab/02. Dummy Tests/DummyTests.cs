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
            Dummy dummy = new Dummy(100, 100);
            Axe axe = new Axe(10, 10);

            dummy.TakeAttack(10);

            Assert.AreEqual(90, dummy.Health);
        }

        [Test]
        public void DeadDummyShouldThrowAnExceptionIfAttacked()
        {
            Dummy dummy = new Dummy(100, 100);
            dummy.TakeAttack(100);

            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(100), "Dummy is dead.");
        }

        [Test]
        public void DeadDummyCanGiveXP()
        {
            Dummy dummy = new Dummy(100, 100);
            dummy.TakeAttack(100);

            Assert.AreEqual(100, dummy.GiveExperience());
        }

        [Test]
        public void AliveDummyCantGiveXP()
        {
            Dummy dummy = new Dummy(110, 100);
            dummy.TakeAttack(100);

            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(), "Target is not dead");
        }
    }
}