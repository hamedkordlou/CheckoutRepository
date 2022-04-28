using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutRepository.Calculator
{
    public class Calculator : ICalculator
    {
        public double CalculateCheckout (List<IGroceryItem> items, List<string> shoppingList)
        {
            // check if any arg is null
            if (items == null || shoppingList == null)
                throw new ArgumentNullException();

            // check if any cost is negative
            if (items.Any(item => ((GroceryItem)item).Cost < 0))
                throw new ArgumentException();

            // check if there is an item in shopping list which does not exist in grocery items
            shoppingList.ForEach(shoppingItem =>
            {
                if (!items.Any(item => ((GroceryItem)item).Name == shoppingItem))
                    throw new ArgumentException();
            });




            return 0;
        }
    }
}
