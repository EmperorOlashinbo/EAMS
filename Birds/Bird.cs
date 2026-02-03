using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds
{
    /// <summary>
    /// Derived class for birds, adding category properties.
    /// </summary>
    public class Bird : Animal
    {
        private double _wingspan;       // in cm
        private double _tailLength;     // in cm 

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Bird() : base()
        {
            _wingspan = 0.0;
            _tailLength = 0.0;
        }

        /// <summary>
        /// Parameterized constructor for bird specific data.
        /// </summary>
        public Bird(double wingspan, double tailLength) : base()
        {
            Wingspan = wingspan;
            TailLength = tailLength;
        }

        public double Wingspan
        {
            get => _wingspan;
            set => _wingspan = value >= 0 ? value : 0.0;
        }

        public double TailLength
        {
            get => _tailLength;
            set => _tailLength = value >= 0 ? value : 0.0;
        }

        /// <summary>
        /// Returns a string representation including base and bird data.
        /// </summary>
        public override string ToString()
        {
            return base.ToString() +
                   $"\nWingspan: {Wingspan} cm" +
                   $"\nTail Length: {TailLength} cm";
        }
    }
}
