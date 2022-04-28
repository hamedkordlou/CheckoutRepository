using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutRepository.Calculator
{
    public class Calculator : ICalculator
    {
        public double CalculateCheckout (List<IWeeklyOffer> items, List<string> shoppingList)
        {
            CheckForNullArguments(items, shoppingList);
            CheckForNegativeCost(items);
            CheckForWrongNames(items, shoppingList);

            var dictionary = MakeDictionaryBaseOnShoppingList(shoppingList);
            return CalculateResult(items, dictionary);
        }

        private double CalculateResult(List<IWeeklyOffer> items, Dictionary<string, int> dictionary)
        {
            double result = 0;
            foreach (KeyValuePair<string, int> item in dictionary)
            {
                var weeklyOffer = FindOfferByName(items, item.Key);

                if (weeklyOffer.Offer == null)
                {
                    result += item.Value * weeklyOffer.Cost;
                }
                else
                {
                    var offerCount = weeklyOffer.Offer.Item1;
                    var offerCost = weeklyOffer.Offer.Item2;

                    result += (item.Value / offerCount) * offerCost + (item.Value % offerCount) * weeklyOffer.Cost;
                }
            }

            return result;
        }

        private Dictionary<string, int> MakeDictionaryBaseOnShoppingList(List<string> shoppingList)
        {
            var dictionary = new Dictionary<string, int>();
            // add shopping items to the dictionary and increment the value if an item repeats
            shoppingList.ForEach(shoppingItem =>
            {
                if (dictionary.ContainsKey(shoppingItem))
                {
                    dictionary[shoppingItem]++;
                }
                else
                {
                    dictionary.Add(shoppingItem, 1);
                }
            });

            return dictionary;
        }

        private WeeklyOffer FindOfferByName(List<IWeeklyOffer> items, string key)
        {
            var offer = (WeeklyOffer)items.First(item => ((WeeklyOffer)item).Name == key);
            return offer;
        }

        private static void CheckForWrongNames(List<IWeeklyOffer> items, List<string> shoppingList)
        {
            // check if there is an item in shopping list which does not exist in grocery items
            shoppingList.ForEach(shoppingItem =>
            {
                if (!items.Any(item => ((WeeklyOffer)item).Name == shoppingItem))
                    throw new ArgumentException();
            });
        }

        private static void CheckForNegativeCost(List<IWeeklyOffer> items)
        {
            // check if any cost is negative
            if (items.Any(item => ((WeeklyOffer)item).Cost < 0))
                throw new ArgumentException();
        }

        private static void CheckForNullArguments(List<IWeeklyOffer> items, List<string> shoppingList)
        {
            // check if any arg is null
            if (items == null || shoppingList == null)
                throw new ArgumentNullException();
        }
    }
}
