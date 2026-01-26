using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Derived class for mammals, adding category-specific properties. Inherits from Animal.
    /// </summary>
    public class Mammal : Animal
    {
        private int _numberOfTeeth;
        private double _tailLength;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Mammal() : base()
        {
            _numberOfTeeth = 0;
            _tailLength = 0.0;
        }
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="numberOfTeeth">Number of teeth</param>
        /// <param name="tailLength">Tail length in cm</param>
        public Mammal(int numberOfTeeth, double tailLength) : base()
        {
            _numberOfTeeth = numberOfTeeth >= 0 ? numberOfTeeth: 0;
            _tailLength = tailLength >= 0 ? tailLength : 0.0;
        }
        public int NumberOfTeeth
        {
            get => _numberOfTeeth;
            set
            {
                if (value >= 0)
                    _numberOfTeeth = value;
            }
        }
        public double TailLength
        {
            get => _tailLength;
            set
            {
                if (value >= 0)
                    _tailLength = value;
            }
        }
        /// <summary>
        /// Returns a string representation of the mammal.
        /// </summary>
        /// <returns>String with mammal data.</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nNumber of Teeth: {NumberOfTeeth}\nTail Length: {TailLength} cm";
        }

    }
}
