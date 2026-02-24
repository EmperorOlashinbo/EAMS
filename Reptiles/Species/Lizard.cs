using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Concrete species class a Lizard, inheriting from Reptile.
    /// </summary>
    public class Lizard : Reptile
    {
        private string _color;
        private bool _canClimb;
        
        /// <summary>
        /// Default constructor. Initializes a lizard with default values.
        /// </summary>
        public Lizard() : base() { }
        /// <summary>
        /// Parameterized constructor. Initializes a lizard with specified values.
        /// </summary>
        /// <param name="bodyLength">Body length of the lizard.</param>
        /// <param name="livesInWater">Indicates if the lizard lives in water.</param>
        /// <param name="aggressivenessLevel">Aggressiveness level of the lizard.</param>
        public Lizard(double bodyLength, bool livesInWater, int aggressivenessLevel)
            : base(bodyLength, livesInWater, aggressivenessLevel) { }
        /// <summary>
        /// Gets or sets the color of the lizard. If a null value is assigned, it defaults to "Unknown".
        /// </summary>
        public string Color { get => _color; set => _color = value ?? "Unknown"; }
        /// <summary>
        /// Gets or sets a value indicating whether the lizard can climb.
        /// </summary>
        public bool CanClimb { get => _canClimb; set => _canClimb = value; }
        /// <summary>
        /// Gets the average lifespan of the lizard in years. 
        /// Average lifespan for many lizard species is around 5-15 years, 
        /// so we return 10 as a general average.
        /// </summary>
        /// <returns></returns>
        public override int GetAverageLifeSpan() => 10;
        /// <summary>
        /// Gets the daily food requirements of the lizard.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the lizard.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "Insects + vegetables",
                ["Evening"] = "Fruits + protein supplement"
            };
        
        /// <summary>
        /// Returns a string representation of the lizard, including color and climbing ability.
        /// </summary>
        /// <returns>A string representation of the lizard.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nColor: {Color}\nCan Climb: {CanClimb}";
        }
    }
}
