using CheckoutRepository.Calculator;
using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace CheckoutTests
{
    public class CheckoutTests
    {
        private readonly Calculator _calculator;

        public CheckoutTests()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void Checkout_without_offer()
        {
            // Arrange
            var groceryItems = new List<IGroceryItem>()
            {
                new GroceryItem() { Name = "Apple", Cost = 30, Offer = null},
                new GroceryItem() { Name = "Banana", Cost = 50, Offer = null},
                new GroceryItem() { Name = "Peach", Cost = 60, Offer = null}
            };
            double expected = 140;


            // Act
            var actual = _calculator.CalculateCheckout(groceryItems);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
