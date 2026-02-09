using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for eagles, inheriting from Bird.
    /// </summary>
    public class Eagle : Bird
    {
        private bool _isBald;
        /// <summary>
        /// Default constructor. Initializes the eagle as a non-bald eagle by default.
        /// </summary>
        public Eagle() : base() { _isBald = false; }
        /// <summary>
        /// Parameterized constructor for eagle specific data, including wingspan, tail length, and bald status.
        /// </summary>
        /// <param name="wingspan"></param>
        /// <param name="tailLength"></param>
        public Eagle(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _isBald = false;
        }
        /// <summary>
        /// Gets or sets the bald status of the eagle. By default, eagles are considered non-bald, but this can be changed if needed.
        /// </summary>
        public bool IsBald
        {
            get => _isBald;
            set => _isBald = value;
        }
        /// <summary>
        /// Returns a string representation including base bird data and bald status.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"\nIs Bald Eagle: {IsBald}";
        }
    }
}
