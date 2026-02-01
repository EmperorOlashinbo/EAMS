using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    public class Animal
    {
        private string _id;
        private string _name;
        private GenderType _gender;
        private int _age;
        private double _weight;
        private string _imagePath;

        /// <summary>
        /// Default constructor. Generates a unique ID for the animal.
        /// </summary>
        public Animal()
        {
            _id = Guid.NewGuid().ToString();
            _name = string.Empty;
            _gender = GenderType.Unknown;
            _age = 0;
            _weight = 0.0;
            _imagePath = string.Empty;
        }
        public string Id => _id;
        public string Name { get => _name; set => _name = value; }
        public GenderType Gender { get => _gender; set => _gender = value; }
        public int Age { get => _age; set { if (value >= 0) _age = value; } }
        public double Weight { get => _weight; set { if (value >= 0) _weight = value; } }
        public string ImagePath { get => _imagePath; set => _imagePath = value; }

        /// <summary>
        /// Returns a string representation of the animal.
        /// </summary>
        /// <returns>String with animal data</returns>
        public override string ToString()
        {
            return $"ID: {Id}\nName: {Name}\nGender: {Gender}\nAge: {Age}\nWeight: {Weight}\nImage Path: {ImagePath}";
        }
    }
}