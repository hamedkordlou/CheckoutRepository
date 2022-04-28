using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutRepository.Calculator
{
    public interface ICalculator
    {
        double CalculateCheckout(List<IWeeklyOffer> items, List<string> shoppingList);
    }
}
