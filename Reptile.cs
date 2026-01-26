using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Derived class for reptiles, inheriting from Animal.
    /// </summary>
    public class Reptile : Animal
    {
        private double _bodyLength;
        private bool _livesInWater;
        private int _aggressivenessLevel;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Reptile() : base()
        {
            _bodyLength = 0.0;
            _livesInWater = false;
            _aggressivenessLevel = 0;
        }
    }
}
