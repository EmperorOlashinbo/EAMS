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
        /// Default constructor
        /// </summary>
        public Dog() : base() { }
        /// <summary>
        /// Parameterized constructor passing to base.
        /// </summary>
        /// <param name="teeth"></param>
        /// <param name="tail"></param>
        public Dog(int teeth, double tail) : base(teeth, tail) { }
        /// <summary>
        /// Gets or sets the breed of the dog. The breed is represented as a string,
        /// </summary>
        public string Breed { get => _breed; set => _breed = value ?? ""; }
        /// <summary>
        /// Gets or sets whether the dog is trained. The training status is represented as a boolean,
        /// </summary>
        public bool IsTrained { get => _isTrained; set => _isTrained = value; }
        /// <summary>
        /// Returns the average lifespan of a dog in years. 
        /// This method overrides the abstract method from the base class.
        /// </summary>
        /// <returns></returns>
        public override int GetAverageLifeSpan() => 12;
        /// <summary>
        /// Returns the daily food requirement for a dog as a dictionary with meal times as keys and food descriptions as values.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "200g dry kibble",
                ["Evening"] = "150g wet food + 100g meat"
            };
        /// <summary>
        /// Returns a string representation of the dog, including base animal info and dog specific properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"\nBreed: {Breed}\nIs Trained: {IsTrained}";
        }
    }
}
