using System;
using System.Collections.Generic;

namespace EAMS
{
    /// <summary>
    /// Abstract base class for all reptiles.
    /// Implements category-specific properties and required abstract/virtual methods.
    /// </summary>
    public abstract class Reptile : Animal
    {
        private double _bodyLength;
        private bool _livesInWater;
        private int _aggressivenessLevel;

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected Reptile() : base()
        {
            _bodyLength = 0.0;
            _livesInWater = false;
            _aggressivenessLevel = 0;
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        protected Reptile(double bodyLength, bool livesInWater, int aggressivenessLevel) : base()
        {
            BodyLength = bodyLength;
            LivesInWater = livesInWater;
            AggressivenessLevel = aggressivenessLevel;
        }
        /// <summary>
        /// Gets or sets the body length of the reptile in centimeters.
        /// </summary>
        public double BodyLength
        {
            get => _bodyLength;
            set => _bodyLength = value >= 0 ? value : 0.0;
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the reptile lives in water.
        /// </summary>
        public bool LivesInWater
        {
            get => _livesInWater;
            set => _livesInWater = value;
        }
        
        /// <summary>
        /// Gets or sets the aggressiveness level of the reptile (0-10).
        /// </summary>
        public int AggressivenessLevel
        {
            get => _aggressivenessLevel;
            set => _aggressivenessLevel = (value >= 0 && value <= 10) ? value : 0;
        }

        /// <summary>
        /// Virtual method - Grade C+ requirement (can be overridden).
        /// </summary>
        public virtual void SetSleepTime()
        {
            // Default for reptiles: basking/resting periods
            Console.WriteLine($"{GetType().Name} rests/basks 10-14 hours per day.");
        }

        /// <summary>
        /// Abstract method - must be implemented by each species (Grade C+).
        /// </summary>
        public abstract int GetAverageLifeSpan();

        /// <summary>
        /// Abstract method - daily food requirement (Grade B+).
        /// </summary>
        public abstract Dictionary<string, string> DailyFoodRequirement();

        /// <summary>
        /// Summary for ListBox display (Assignment 2 requirement).
        /// </summary>
        public virtual string ToStringSummary()
        {
            return $"{Id,-8} {Name,-12} {Age,6} {Weight,6:F1} {Gender} | Length: {BodyLength}cm, Agg: {AggressivenessLevel}";
        }

        /// <summary>
        /// Full ToString with hierarchy.
        /// </summary>
        public override string ToString()
        {
            return base.ToString() +
                   $"\nBody Length: {BodyLength} cm" +
                   $"\nLives in Water: {LivesInWater}" +
                   $"\nAggressiveness Level: {AggressivenessLevel}/10";
        }
    }
}