using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for peacocks, inheriting from Bird.
    /// </summary>
    public class Peacock : Bird
    {
        private string _plumeColor;
        /// <summary>
        /// Default constructor. Initializes a peacock with default values.
        /// </summary>
        public Peacock() : base() { _plumeColor = string.Empty; }

        /// <summary>
        /// Parameterized constructor for peacock specific data.
        /// </summary>
        /// <param name="wingspan">The wingspan of the peacock.</param>
        /// <param name="tailLength">The tail length of the peacock.</param>
        public Peacock(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _plumeColor = string.Empty;
        }
        
        /// <summary>
        /// Gets or sets the plume color of the peacock.
        /// </summary>
        public string PlumeColor
        {
            get => _plumeColor;
            set => _plumeColor = value ?? string.Empty;
        }
        
        /// <summary>
        /// Gets the average lifespan of the peacock in years.
        /// </summary>
        /// <returns>The average lifespan of the peacock in years.</returns>
        public override int GetAverageLifeSpan() => 20;
        
        /// <summary>
        /// Gets the daily food requirements of the peacock.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the peacock.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "Seeds and grains",
                ["Evening"] = "Insects and small fruits"
            };
        
        /// <summary>
        /// Returns a string representation of the peacock, including plume color.
        /// </summary>
        /// <returns>A string representation of the peacock.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nPlume Color: {PlumeColor}";
        }
        /// <summary>
        /// Return queue of an upcoming events.
        /// </summary>
        /// <returns></returns>
        public override Queue<string> GetUpcomingEvents()
        {
            var q = new Queue<string>();
            q.Enqueue($"Molting inspection - {DateTime.Now.AddDays(30):yyyy-MM-dd}");
            q.Enqueue($"Behavioral check - {DateTime.Now.AddDays(75):yyyy-MM-dd}");
            return q;
        }
    }
}