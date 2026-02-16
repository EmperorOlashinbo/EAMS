using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Concrete class for cats, inheriting from Mammal.
    /// </summary>
    public class Cat : Mammal
    {
        private string _furColor;
        private bool _isIndoor;
        /// <summary>
        /// Default constructor for Cat, initializing properties to default values.
        /// </summary>
        public Cat() : base() { }
        /// <summary>
        /// Parameterized constructor for Cat, allowing initialization of teeth and tail length.
        /// </summary>
        /// <param name="teeth"></param>
        /// <param name="tail"></param>
        public Cat(int teeth, double tail) : base(teeth, tail) { }
        /// <summary>
        /// Gets or sets the fur color of the cat. The fur color is represented as a string,
        /// </summary>
        public string FurColor { get => _furColor; set => _furColor = value ?? ""; }
        /// <summary>
        /// Gets or sets whether the cat is an indoor cat. The indoor status is represented as a boolean,
        /// </summary>
        public bool IsIndoor { get => _isIndoor; set => _isIndoor = value; }
        /// <summary>
        /// Returns the average lifespan of a cat in years. 
        /// This method overrides the abstract method from the base class.
        /// </summary>
        /// <returns></returns>
        public override int GetAverageLifeSpan() => 15;
        /// <summary>
        /// Returns the daily food requirement for a cat as a dictionary with meal times as keys and food descriptions as values.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> DailyFoodRequirement() =>
            new Dictionary<string, string>
            {
                ["Morning"] = "100g wet food",
                ["Evening"] = "80g dry food"
            };
        /// <summary>
        /// Returns a string representation of the cat, including base animal info and cat specific properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"\nFur Color: {FurColor}\nIs Indoor: {IsIndoor}";
        }
    }
}
