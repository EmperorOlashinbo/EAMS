using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS
{
    /// <summary>
    /// Concrete class for dogs, inheriting from Mammal.
    /// </summary>
    public class Dog : Mammal
    {
        private string _breed;
        private bool _isTrained;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Dog() : base()
        {
            _breed = string.Empty;
            _isTrained = false;
        }
    }
}
