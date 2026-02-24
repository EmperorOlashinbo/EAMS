using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Reptiles.Species
{
    /// <summary>
    /// Concrete species class for crocodiles, inheriting from Reptile.
    /// </summary>
    public class Crocodile : Reptile
    {
        private int _jawStrength;
        private bool _isSaltwater;
        /// <summary>
        /// Default constructor. Initializes a crocodile with default values.
        /// </summary>
        public Crocodile() : base() { }
        /// <summary>
        /// Parameterized constructor. Initializes a crocodile with specified values.
        /// </summary>
        /// <param name="bodyLength">Body length of the crocodile.</param>
        /// <param name="livesInWater">Indicates if the crocodile lives in water.</param>
        /// <param name="aggressivenessLevel">Aggressiveness level of the crocodile.</param>
        public Crocodile(double bodyLength, bool livesInWater, int aggressivenessLevel)
            : base(bodyLength, livesInWater, aggressivenessLevel) { }
        /// <summary>
        /// Gets or sets the jaw strength of the crocodile on a scale from 0 to 100.
        /// </summary>
        public int JawStrength
        {
            get => _jawStrength;
            set => _jawStrength = value < 0 ? 0 : (value > 100 ? 100 : value);
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the crocodile is a saltwater species.
        /// </summary>
        public bool IsSaltwater { get => _isSaltwater; set => _isSaltwater = value; }
        
        /// <summary>
        /// Gets the average lifespan of the crocodile in years.
        /// </summary>
        /// <returns>The average lifespan of the crocodile in years.</returns>
        public override int GetAverageLifeSpan() => 70;
        
        /// <summary>
        /// Gets the daily food requirements of the crocodile.
        /// </summary>
        /// <returns>A dictionary containing the daily food requirements of the crocodile.</returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "Fish + meat",
                ["Evening"] = "Small mammals or birds"
            };
        
        /// <summary>
        /// Returns a string representation of the crocodile, including jaw strength and saltwater status.
        /// </summary>
        /// <returns>A string representation of the crocodile.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nJaw Strength: {JawStrength}\nIs Saltwater: {IsSaltwater}";
        }
    }
}