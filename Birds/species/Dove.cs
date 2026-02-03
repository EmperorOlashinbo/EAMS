using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.Birds.species
{
    /// <summary>
    /// Concrete species class for doves, inheriting from Bird.
    /// </summary>
    public class Dove : Bird
    {
        private string _featherColor;

        public Dove() : base() { _featherColor = string.Empty; }

        public Dove(double wingspan, double tailLength) : base(wingspan, tailLength)
        {
            _featherColor = string.Empty;
        }

        public string FeatherColor
        {
            get => _featherColor;
            set => _featherColor = value ?? string.Empty;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nFeather Color: {FeatherColor}";
        }
    }
}
