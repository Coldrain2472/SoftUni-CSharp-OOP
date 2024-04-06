using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace SocialMediaManager.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Influencer_ConstructorShouldInitializeCorrectly()
        {
            Influencer influencer = new Influencer("coldrain", 100);
            Assert.AreEqual("coldrain", influencer.Username);
            Assert.AreEqual(100, influencer.Followers);
        }

        [Test]
        public void InfluencerRepository_ShouldInitializeCorrectly()
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();
            Assert.AreEqual(0, influencerRepository.Influencers.Count);
        }

        [Test]
        public void RegisterInfluencer_ShouldThrowAnExceptionIfInfluenceIsNull()
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RegisterInfluencer(null));
        }

        [Test]
        public void RegisterInfluencer_ShouldThrowAnExceptionIfTheInfluenceExists()
        {
            Influencer influencerOne = new Influencer("coldrain", 100);
            Influencer influencerTwo = new Influencer("coldrain", 20);
            InfluencerRepository influencerRepository = new InfluencerRepository();
            influencerRepository.RegisterInfluencer(influencerOne);
            Assert.Throws<InvalidOperationException>(() => influencerRepository.RegisterInfluencer(influencerTwo));
        }

        [Test]
        public void RegisterInfluencer_ShouldAddNewInfluencerWhenTheAboveCriteriasAreMet()
        {
            Influencer influencerOne = new Influencer("coldrain", 100);
            InfluencerRepository influencerRepository = new InfluencerRepository();
            var result = influencerRepository.RegisterInfluencer(influencerOne);
            Assert.AreEqual(1, influencerRepository.Influencers.Count);
            Assert.AreEqual($"Successfully added influencer {influencerOne.Username} with {influencerOne.Followers}", result);
        }

        [Test]
        public void RemoveInfluencer_ShouldThrowAnExceptionIfTheUsernameIsNull()
        {
            Influencer influencerOne = new Influencer("coldrain", 100);
            InfluencerRepository influencerRepository = new InfluencerRepository();
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RemoveInfluencer(null));
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RemoveInfluencer(""));
        }

        [Test]
        public void RemoveInfluencer_ShouldWorkProperly()
        {
            Influencer influencerOne = new Influencer("coldrain", 100);
            InfluencerRepository influencerRepository = new InfluencerRepository();
            Influencer influencerTwo = new Influencer("saffrona", 20);
            influencerRepository.RegisterInfluencer(influencerOne);
            influencerRepository.RegisterInfluencer(influencerTwo);
            var result = influencerRepository.RemoveInfluencer("coldrain");
            Assert.AreEqual(1, influencerRepository.Influencers.Count);
            Assert.IsFalse(influencerRepository.Influencers.Contains(influencerOne));
            Assert.IsTrue(result);
        }

        [Test]
        public void GetInfluencerShouldGetTheInfluenceWithTheMostFollowers()
        {
            Influencer influencerOne = new Influencer("coldrain", 100);
            Influencer influencerTwo = new Influencer("saffrona", 20);
            InfluencerRepository influencerRepository = new InfluencerRepository();
            influencerRepository.RegisterInfluencer(influencerOne);
            influencerRepository.RegisterInfluencer(influencerTwo);
            var result = influencerRepository.GetInfluencerWithMostFollowers();
            Assert.AreEqual(influencerOne, result);
        }

        [Test]
        public void GetInfluencer_ShouldWorkProperly()
        {
            Influencer influencerOne = new Influencer("coldrain", 100);
            Influencer influencerTwo = new Influencer("saffrona", 20);
            InfluencerRepository influencerRepository = new InfluencerRepository();
            influencerRepository.RegisterInfluencer(influencerOne);
            influencerRepository.RegisterInfluencer(influencerTwo);
            var result = influencerRepository.GetInfluencer("coldrain");
            Assert.AreEqual(influencerOne, result);
        }

        [Test]
        public void GetInfluencer_InvalidUsername_ReturnsNull()
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();

            var result = influencerRepository.GetInfluencer("invalidUser");

            Assert.IsNull(result);
        }
    }
}