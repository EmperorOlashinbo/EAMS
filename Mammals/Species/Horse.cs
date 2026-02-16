using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Mammals.Species
{
    /// <summary>
    /// Concrete species class for horses, inheriting from Mammal.
    /// </summary>
    public class Horse : Mammal
    {
        private string _breed;
        private bool _isRacing;
        /// <summary>
        /// Default constructor for Horse, initializing properties to default values.
        /// </summary>
        public Horse() : base() { }
        /// <summary>
        /// Initializes a new instance of the Horse class with the specified number of teeth and tail length.
        /// </summary>
        /// <param name="teeth"></param>
        /// <param name="tail"></param>
        public Horse(int teeth, double tail) : base(teeth, tail) { }
        /// <summary>
        /// Gets or sets the breed of the horse. The breed is represented as a string,
        /// </summary>
        public string Breed { get => _breed; set => _breed = value ?? ""; }
        /// <summary>
        /// Gets or sets whether the horse is a racing horse. The racing status is represented as a boolean,
        /// </summary>
        public bool IsRacing { get => _isRacing; set => _isRacing = value; }
        /// <summary>
        /// Returns the average lifespan of a horse in years.
        /// </summary>
        /// <returns></returns>
        public override int GetAverageLifeSpan() => 25;
        /// <summary>
        /// Returns the daily food requirement for a horse as a dictionary with meal times as keys and food descriptions as values.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "6kg hay",
                ["Evening"] = "4kg grain + 3kg hay"
            };
        /// <summary>
        /// Returns a string representation of the horse, 
        /// including base animal info and horse specific properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"\nBreed: {Breed}\nIs Racing: {IsRacing}";
        }
    }
}
