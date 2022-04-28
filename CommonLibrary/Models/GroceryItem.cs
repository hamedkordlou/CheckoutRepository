using System;

namespace CommonLibrary.Models
{
    public class GroceryItem : IGroceryItem
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public Tuple<decimal, double> Offer { get; set; }
    }
}
