using System;

namespace CommonLibrary.Models
{
    public class WeeklyOffer : IWeeklyOffer
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public Tuple<int, double> Offer { get; set; }
    }
}
