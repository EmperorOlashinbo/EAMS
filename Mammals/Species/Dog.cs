using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Concrete species class for dogs, inheriting from Mammal.
    /// </summary>
    public class Dog : Mammal
    {
        private string _breed;
        private bool _isTrained;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Dog() : base()
        {
            _breed = string.Empty;
            _isTrained = false;
        }
        /// <summary>
        /// Parameterized constructor passing to base.
        /// </summary>
        /// <param name="numberOfTeeth">Number of teeth</param>
        /// <param name="tailLength">Tail length in cm</param>
        public Dog(int numberOfTeeth, double tailLength) : base(numberOfTeeth, tailLength)
        {
            _breed = string.Empty;
            _isTrained = false;
        }
        /// <summary>
        /// Gets or sets the breed of the dog.
        /// </summary>
        public string Breed
        {
            get => _breed;
            set => _breed = value;
        }
        /// <summary>
        /// Gets or sets whether the dog is trained.
        /// </summary>
        public bool IsTrained
        {
            get => _isTrained;
            set => _isTrained = value;
        }
        /// <summary>
        /// Returns a string representation of the dog.
        /// <summary>
        /// <returns>String with dog data</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nBreed: {Breed}\nIs Trained: {IsTrained}";
        }
    }
}
