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
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="bodyLength">Body length in cm</param>
        /// <param name="livesInWater">Indicates if the reptile lives in water</param>
        /// <param name="aggressivenessLevel">Aggressiveness level from 0 to 10</param>
        public Reptile(double bodyLength, bool livesInWater, int aggressivenessLevel) : base()
        {
            _bodyLength = bodyLength >= 0 ? bodyLength : 0.0;
            _livesInWater = livesInWater;
            _aggressivenessLevel = (aggressivenessLevel >= 0 && aggressivenessLevel <= 10) ? aggressivenessLevel : 0;
        }
        /// <summary>
        /// Gets or sets the body length of the reptile in centimeters.
        /// </summary>
        public double BodyLength
        {
            get => _bodyLength;
            set
            {
                if (value >= 0)
                    _bodyLength = value;
            }
        }
        /// <summary>
        /// Gets or sets whether the reptile lives in water. 
        /// This is a boolean property that indicates if the reptile is aquatic or not.
        /// </summary>
        public bool LivesInWater
        {
            get => _livesInWater;
            set => _livesInWater = value;
        }
        /// <summary>
        /// Gets or sets the aggressiveness level of the reptile. 
        /// The aggressiveness level is represented as an integer, 
        /// where 0 means not aggressive at all and 10 means extremely aggressive.
        /// </summary>
        public int AggressivenessLevel
        {
            get => _aggressivenessLevel;
            set
            {
                if (value >= 0 && value <= 10)
                    _aggressivenessLevel = value;
            }
        }
        /// <summary>
        /// Return a string representation including base and reptile.
        /// </summary>
        /// <returns>String with reptile data</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nBody Length: {BodyLength}\nLives in Water: {LivesInWater}\nAggressiveness Level: {AggressivenessLevel}";
        }
    }
}
