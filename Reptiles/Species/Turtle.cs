using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Concrete species class a Turtle, inheriting from Reptile.
    /// </summary>
    public class Turtle : Reptile
    {
        private int _shellHardness;
        private double _shellWidth;
        /// <summary>
        /// Default constructor. Initializes a turtle with default values.
        /// </summary>
        public Turtle() : base() { }
        /// <summary>
        /// Parameterized constructor. Initializes a turtle with specified values.
        /// </summary>
        /// <param name="bodyLength">Body length of the turtle.</param>
        /// <param name="livesInWater">Indicates if the turtle lives in water.</param>
        /// <param name="aggressivenessLevel">Aggressiveness level of the turtle.</param>
        public Turtle(double bodyLength, bool livesInWater, int aggressivenessLevel)
            : base(bodyLength, livesInWater, aggressivenessLevel) { }
        /// <summary>
        /// Gets or sets the hardness of the turtle's shell on a scale from 0 to 10.
        /// </summary>
        public int ShellHardness
        {
            get => _shellHardness;
            set => _shellHardness = value >= 0 && value <= 10 ? value : 0;
        }
        /// <summary>
        /// Gets or sets the width of the shell. Values less than zero are set to 0.0.
        /// </summary>
        public double ShellWidth
        {
            get => _shellWidth;
            set => _shellWidth = value >= 0 ? value : 0.0;
        }
        
        /// <summary>
        /// Gets the average lifespan of the turtle in years.
        /// </summary>
        public override int GetAverageLifeSpan() => 50;   // Many turtles live 30-100+ years
        
        /// <summary>
        /// Gets the daily food requirements of the turtle.
        /// </summary>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "Pellets + leafy greens",
                ["Evening"] = "Vegetables + occasional protein"
            };
        
        /// <summary>
        /// Returns a string representation of the turtle, including shell properties.
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $"\nShell Hardness: {ShellHardness}\nShell Width: {ShellWidth} cm";
        }
    }
}
