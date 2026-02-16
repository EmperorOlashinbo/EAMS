using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.AnimalsGen
{
    /// <summary>
    /// Defines the interface for an animal, which includes properties
    /// </summary>
    public interface IAnimal
    {
        string Id { get; }
        string Name { get; set; }
        GenderType Gender { get; set; }
        int Age { get; set; }
        double Weight { get; set; }
        string ImagePath { get; set; }

        string ToString();
    }
}
