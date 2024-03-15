using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        private CoffeeMat coffeeMat;
        private int waterCapacity = 100;
        private int buttonCount = 2;
        private string coffee = "coffee";
        private double coffeePrice = 1;
        private string tea = "tea";
        private double teaPrice = 0.5;
        private string macciato = "macciato";
        private double macciatoPrice = 1.2;

        [SetUp]
        public void Setup()
        {
            coffeeMat = new CoffeeMat(waterCapacity, buttonCount);
        }

        [Test]
        public void IsConstructorInitializingCoffeeMatt()
        {
            Assert.IsNotNull(coffeeMat);
        }

        [Test]
        public void ConstructorShouldSetWaterCapacityCorrectly()
        {
            Assert.AreEqual(waterCapacity, coffeeMat.WaterCapacity);
        }

        [Test]
        public void ConstructorShouldSetButtonCountCorrectly()
        {
            Assert.AreEqual(buttonCount, coffeeMat.ButtonsCount);
        }

        [Test]
        public void ConstructorShouldSetIncomeToZero()
        {
            Assert.AreEqual(0, coffeeMat.Income);
        }

        [Test]
        public void FillWaterTankMethodShouldReturnCorrectMessageWhenFilledSuccessfully()
        {
            string expectedResult = $"Water tank is filled with {waterCapacity}ml";
            Assert.AreEqual(expectedResult, coffeeMat.FillWaterTank());
        }

        [Test]
        public void FillWaterTankShouldReturnCorrectMessageWhenAlreadyFull()
        {
            string expectedResult = $"Water tank is already full!";
            coffeeMat.FillWaterTank();
            Assert.AreEqual(expectedResult, coffeeMat.FillWaterTank());
        }

        [Test]
        public void AddDrinkShouldReturnTrueWhenSuccessfullyAddingADrink()
        {
            Assert.IsTrue(coffeeMat.AddDrink(coffee, coffeePrice));
            Assert.IsTrue(coffeeMat.AddDrink(tea, teaPrice));
        }

        [Test]
        public void AddDrinkShouldReturnFalseWhenAllButtonsAreTaken()
        {
            coffeeMat.AddDrink(coffee, coffeePrice);
            coffeeMat.AddDrink(tea, teaPrice);
            Assert.IsFalse(coffeeMat.AddDrink(macciato, macciatoPrice));
        }

        [Test]
        public void AddDrinkShouldReturnFalseWhenDrinkIsAlreadyAdded()
        {
            coffeeMat.AddDrink(coffee, coffeePrice);
            Assert.IsFalse(coffeeMat.AddDrink(coffee, coffeePrice));
        }

        [Test]
        public void BuyDrinkShouldReturnCorrectStringWhenWaterInTankIsBelow80()
        {
            string expectedResult = "CoffeeMat is out of water!";
            Assert.AreEqual(expectedResult, coffeeMat.BuyDrink(coffee));
        }

        [Test]
        public void BuyDrinkShouldReturnCorrectStringIfDrinkIsNotAvailable()
        {
            coffeeMat.FillWaterTank();
            string expectedResult = $"{coffee} is not available!";
            Assert.AreEqual(expectedResult, coffeeMat.BuyDrink(coffee));
        }

        [Test]
        public void BuyDrinkSuccessfullyShouldReduceWaterInTank()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink(coffee, coffeePrice);
            coffeeMat.BuyDrink(coffee);
            string expectedResult = "CoffeeMat is out of water!";
            Assert.AreEqual(expectedResult, coffeeMat.BuyDrink(coffee));
        }

        [Test]
        public void BuyDrinkSuccessfullyShouldIncreaseIncome()
        {
            Assert.AreEqual(0, coffeeMat.Income);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink(coffee, coffeePrice);
            coffeeMat.BuyDrink(coffee);
            Assert.AreEqual(coffeePrice, coffeeMat.Income);
        }

        [Test]
        public void BuyDrinkSuccessfullyShouldReturnCorrectString()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink(coffee, coffeePrice);

            string expectedResult = $"Your bill is {coffeePrice:f2}$";
            Assert.AreEqual(expectedResult, coffeeMat.BuyDrink(coffee));
        }

        [Test]
        public void CollectIncomeShouldSetIncomeToZero()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink(coffee, coffeePrice);
            coffeeMat.BuyDrink(coffee);
            coffeeMat.CollectIncome();

            Assert.AreEqual(0, coffeeMat.Income);
        }

        [Test]
        public void CollectIncomeShouldCorrectlyReturnCollectedIncome()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink(coffee, coffeePrice);
            coffeeMat.BuyDrink(coffee);

            Assert.AreEqual(coffeePrice, coffeeMat.CollectIncome());
        }
    }
}