using CheckoutRepository.Calculator;
using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace CheckoutTests
{
    public class CheckoutTests
    {
        private readonly ICalculator _calculator;

        public CheckoutTests()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void WhenArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            var groceryItems = new List<IWeeklyOffer>()
            {
                new WeeklyOffer() { Name = "Apple", Cost = 30, Offer = null},
                new WeeklyOffer() { Name = "Banana", Cost = 50, Offer = null},
                new WeeklyOffer() { Name = "Peach", Cost = 60, Offer = null}
            };

            var shoppingListWithOneItems = new List<string>() { "Apple", "Banana", "Peach" };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _calculator.CalculateCheckout(null, null));
            Assert.Throws<ArgumentNullException>(() => _calculator.CalculateCheckout(groceryItems, null));
            Assert.Throws<ArgumentNullException>(() => _calculator.CalculateCheckout(null, shoppingListWithOneItems));
            
        }

        [Fact]
        public void WhenBadArgument_ThrowsArgumentException()
        {
            // Arrange
            var groceryItems = new List<IWeeklyOffer>()
            {
                new WeeklyOffer() { Name = "Apple", Cost = -30, Offer = null},
                new WeeklyOffer() { Name = "Banana", Cost = 50, Offer = null},
                new WeeklyOffer() { Name = "Peach", Cost = 60, Offer = null}
            };

            var shoppingListWithOneNegativeItem = new List<string>() { "Apple", "Banana", "Peach" };
            var shoppingListWithOneNoneExistItem = new List<string>() { "Apple", "Banana", "Peachchch" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.CalculateCheckout(groceryItems, shoppingListWithOneNegativeItem));
            Assert.Throws<ArgumentException>(() => _calculator.CalculateCheckout(groceryItems, shoppingListWithOneNoneExistItem));

        }

        [Fact]
        public void WhenNoOffer_CheckoutShouldBeSum()
        {
            // Arrange
            var groceryItems = new List<IWeeklyOffer>()
            {
                new WeeklyOffer() { Name = "Apple", Cost = 30, Offer = null},
                new WeeklyOffer() { Name = "Banana", Cost = 50, Offer = null},
                new WeeklyOffer() { Name = "Peach", Cost = 60, Offer = null}
            };

            var shoppingListWithOneItems = new List<string>() { "Apple", "Banana", "Peach" };
            var shoppingListWithTwoApples = new List<string>() { "Apple", "Banana", "Peach", "Apple" };
            var shoppingListWithTwoBananas = new List<string>() { "Apple", "Banana", "Peach", "Banana" };

            double expectedWithOneItems = 140;
            double expectedWithTwoApples = 170;
            double expectedWithTwoBananas = 190;


            // Act
            var actualWithOneItems = _calculator.CalculateCheckout(groceryItems, shoppingListWithOneItems);
            var actualWithTwoApples = _calculator.CalculateCheckout(groceryItems, shoppingListWithTwoApples);
            var actualWithTwoBananas = _calculator.CalculateCheckout(groceryItems, shoppingListWithTwoBananas);

            // Assert
            Assert.Equal(expectedWithOneItems, actualWithOneItems);
            Assert.Equal(expectedWithTwoApples, actualWithTwoApples);
            Assert.Equal(expectedWithTwoBananas, actualWithTwoBananas);
        }

        [Fact]
        public void WhenThereIsOffer_AndNoneMeetCriteria_CheckoutShouldBeSum()
        {
            // Arrange
            var groceryItems = new List<IWeeklyOffer>()
            {
                new WeeklyOffer() { Name = "Apple", Cost = 30, Offer = new Tuple<int, double>(2, 45) },
                new WeeklyOffer() { Name = "Banana", Cost = 50, Offer = null},
                new WeeklyOffer() { Name = "Peach", Cost = 60, Offer = null}
            };

            var shoppingListWithTwoApples = new List<string>() { "Apple", "Banana", "Peach", "Peach" };
            var shoppingListWithTwoBananas = new List<string>() { "Apple", "Banana", "Peach", "Banana" };

            double expectedWithTwoApples = 30 + 50 + 60 + 60;
            double expectedWithTwoBananas = 30 + 50 + 60 + 50;


            // Act
            var actualWithTwoApples = _calculator.CalculateCheckout(groceryItems, shoppingListWithTwoApples);
            var actualWithTwoBananas = _calculator.CalculateCheckout(groceryItems, shoppingListWithTwoBananas);

            // Assert
            Assert.Equal(expectedWithTwoApples, actualWithTwoApples);
            Assert.Equal(expectedWithTwoBananas, actualWithTwoBananas);
        }

        [Fact]
        public void WhenThereIsOffer_AndSomeMeetCriteria_CheckoutShouldConciderMeetingOffers()
        {
            // Arrange
            var groceryItems = new List<IWeeklyOffer>()
            {
                new WeeklyOffer() { Name = "Apple", Cost = 30, Offer = new Tuple<int, double>(2, 45) },
                new WeeklyOffer() { Name = "Banana", Cost = 50, Offer = null},
                new WeeklyOffer() { Name = "Peach", Cost = 60, Offer = null}
            };

            var shoppingListWithThreeApples = new List<string>() { "Apple", "Banana", "Apple", "Peach", "Apple" };
            var shoppingListWithTwoBananas = new List<string>() { "Apple", "Banana", "Peach", "Banana" };

            double expectedWithThreeApples = 45 + 50 + 60 + 30;
            double expectedWithTwoBananas = 30 + 50 + 60 + 50;


            // Act
            var actualWithThreeApples = _calculator.CalculateCheckout(groceryItems, shoppingListWithThreeApples);
            var actualWithTwoBananas = _calculator.CalculateCheckout(groceryItems, shoppingListWithTwoBananas);

            // Assert
            Assert.Equal(expectedWithThreeApples, actualWithThreeApples);
            Assert.Equal(expectedWithTwoBananas, actualWithTwoBananas);
        }

        [Fact]
        public void WhenThereAreOffers_AndSomeMeetCriteria_CheckoutShouldConciderMeetingOffers()
        {
            // Arrange
            var groceryItems = new List<IWeeklyOffer>()
            {
                new WeeklyOffer() { Name = "Apple", Cost = 30, Offer = new Tuple<int, double>(2, 45) },
                new WeeklyOffer() { Name = "Banana", Cost = 50, Offer = new Tuple<int, double>(3, 130)},
                new WeeklyOffer() { Name = "Peach", Cost = 60, Offer = null}
            };

            var shoppingListMeetingOneOffer = new List<string>() { "Apple", "Banana", "Peach", "Apple" };
            var shoppingListMeetingAllOffer = new List<string>() { "Apple", "Banana", "Peach", "Banana", "Peach", "Banana", "Banana", "Apple" };
            var shoppingListMeetingAnOfferMultipleTimes = new List<string>() { "Apple", "Banana", "Peach", "Banana", "Peach", "Banana", "Banana", "Apple", "Apple", "Apple" };

            double expectedMeetingOneOffer = 45 + 50 + 60;
            double expectedMeetingAllOffer = 45 + 130 + 50 + 60 + 60;
            double expectedMeetingAnOfferMultipleTimes = 45 + 45 + 130 + 50 + 60 + 60;


            // Act
            var actualMeetingOneOffer = _calculator.CalculateCheckout(groceryItems, shoppingListMeetingOneOffer);
            var actualMeetingAllOffer = _calculator.CalculateCheckout(groceryItems, shoppingListMeetingAllOffer);
            var actualMeetingAnOfferMultipleTimes = _calculator.CalculateCheckout(groceryItems, shoppingListMeetingAnOfferMultipleTimes);

            // Assert
            Assert.Equal(expectedMeetingOneOffer, actualMeetingOneOffer);
            Assert.Equal(expectedMeetingAllOffer, actualMeetingAllOffer);
            Assert.Equal(expectedMeetingAnOfferMultipleTimes, actualMeetingAnOfferMultipleTimes);
        }
    }
}
