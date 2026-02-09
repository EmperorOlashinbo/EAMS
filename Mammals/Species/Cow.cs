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
        /// Default constructor.
        /// </summary>
        public Cow() : base()
        {
            _milkProduction = 0.0;
        }

        /// <summary>
        /// Parameterized constructor passing to base.
        /// </summary>
        /// <param name="numberOfTeeth">Number of teeth.</param>
        /// <param name="tailLength">Tail length.</param>
        public Cow(int numberOfTeeth, double tailLength) : base(numberOfTeeth, tailLength)
        {
            _milkProduction = 0.0;
        }
        /// <summary>
        /// Gets or sets the milk production of the cow in liters per day.
        /// </summary>
        public double MilkProduction { get => _milkProduction; set => _milkProduction = value >= 0 ? value : 0.0; }

        /// <summary>
        /// Returns a string representation including base and cow data.
        /// </summary>
        /// <returns>String with cow data.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nMilk Production: {MilkProduction}";
        }
    }
}
