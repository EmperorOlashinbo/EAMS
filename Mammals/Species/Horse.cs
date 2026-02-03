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
        /// Default constructor.
        /// </summary>
        public Horse() : base()
        {
            _breed = string.Empty;
            _isRacing = false;
        }

        /// <summary>
        /// Parameterized constructor passing to base.
        /// </summary>
        /// <param name="numberOfTeeth">Number of teeth.</param>
        /// <param name="tailLength">Tail length.</param>
        public Horse(int numberOfTeeth, double tailLength) : base(numberOfTeeth, tailLength)
        {
            _breed = string.Empty;
            _isRacing = false;
        }

        public string Breed { get => _breed; set => _breed = value; }
        public bool IsRacing { get => _isRacing; set => _isRacing = value; }

        /// <summary>
        /// Returns a string representation including base and horse data.
        /// </summary>
        /// <returns>String with horse data.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nBreed: {Breed}\nIs Racing: {IsRacing}";
        }
    }
}
