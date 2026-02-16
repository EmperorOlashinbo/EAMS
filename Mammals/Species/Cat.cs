using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Mammals.Species
{
    /// <summary>
    /// Concrete class for cats, inheriting from Mammal.
    /// </summary>
    public class Cat : Mammal
    {
        private string _furColor;
        private bool _isIndoor;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Cat() : base()
        {
            _furColor = string.Empty;
            _isIndoor = false;
        }
        /// <summary>
        /// Parameterized constructor passing to base.
        /// </summary>
        /// <param name="numberOfTeeth">Number of teeth</param>
        /// <param name="tailLength">Tail length in cm</param>
        public Cat(int numberOfTeeth, double tailLength) : base(numberOfTeeth, tailLength)
        {
            _furColor = string.Empty;
            _isIndoor = false;
        }
        /// <summary>
        /// Gets or sets the fur color of the cat. 
        /// The fur color is represented as a string, 
        /// which can describe the color of the cat's fur.
        /// </summary>
        public string FurColor
        {
            get => _furColor;
            set => _furColor = value;
        }
        /// <summary>
        /// Gets or sets whether the cat is an indoor cat.
        /// </summary>
        public bool IsIndoor
        {
            get => _isIndoor;
            set => _isIndoor = value;
        }
        ///<summary>
        /// Returns a string representation of the cat.
        /// </summary>
        /// <returns>String with cat data</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nFur Color: {FurColor}\nIs Indoor: {IsIndoor}";
        }

    }
}
