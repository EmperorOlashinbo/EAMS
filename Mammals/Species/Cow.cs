using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Mammals.Species
{
    /// <summary>
    /// Concrete species class for cows, inheriting from Mammal.
    /// </summary>
    public class Cow : Mammal
    {
        private double _milkProduction;
        /// <summary>
        /// Default constructor for Cow, initializing properties to default values.
        /// </summary>
        public Cow() : base() { }
        /// <summary>
        /// Initializes a new instance of the Cow class with the specified number of teeth and tail length.
        /// </summary>
        /// <param name="teeth">The number of teeth.</param>
        /// <param name="tail">The length of the tail.</param>
        public Cow(int teeth, double tail) : base(teeth, tail) { }
        /// <summary>
        /// Gets or sets the daily milk production of the cow in liters. The milk production is represented as a double,
        /// </summary>
        public double MilkProduction { get => _milkProduction; set => _milkProduction = value >= 0 ? value : 0.0; }
        /// <summary>
        /// Returns the average lifespan of a cow in years.
        /// </summary>
        /// <returns></returns>
        public override int GetAverageLifeSpan() => 20;
        /// <summary>
        /// Returns the daily food requirement for a cow as a dictionary with meal times as keys and food descriptions as values.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "10kg hay + 5kg concentrate",
                ["Evening"] = "8kg hay"
            };
        /// <summary>
        /// Returns a string representation of the cow, including base animal info and cow specific properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"\nMilk Production: {MilkProduction} L/day";
        }
    }
}
