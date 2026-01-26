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
        /// Default constructor
        /// </summary>
        public Cat() : base()
        {
            _furColor = string.Empty;
            _isIndoor = false;
        }
    }
}
