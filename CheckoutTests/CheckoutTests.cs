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
        public void Checkout_without_offer()
        {
            // Arrange
            var groceryItems = new List<IGroceryItem>()
            {
                new GroceryItem() { Name = "Apple", Cost = 30, Offer = null},
                new GroceryItem() { Name = "Banana", Cost = 50, Offer = null},
                new GroceryItem() { Name = "Peach", Cost = 60, Offer = null}
            };
            var shoppingList = new List<string>() { "Apple", "Banana", "Peach" };
            double expected = 140;


            // Act
            var actual = _calculator.CalculateCheckout(groceryItems, shoppingList);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Checkout_with_one_offer()
        {
            // Arrange
            var groceryItems = new List<IGroceryItem>()
            {
                new GroceryItem() { Name = "Apple", Cost = 30, Offer = new Tuple<decimal, double>(2, 45) },
                new GroceryItem() { Name = "Banana", Cost = 50, Offer = null},
                new GroceryItem() { Name = "Peach", Cost = 60, Offer = null}
            };
            var shoppingList = new List<string>() { "Apple", "Banana", "Peach" };
            double expected = 140;


            // Act
            var actual = _calculator.CalculateCheckout(groceryItems, shoppingList);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
