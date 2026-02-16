using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMS.AnimalsGen
{
    /// <summary>
    /// Manager class for handling animal collections using generics.
    /// </summary>
    public class AnimalManager
    {
        private List<IAnimal> _animals = new List<IAnimal>();

        /// <summary>
        /// Adds a new animal to the collection.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(IAnimal animal)
        {
            if (animal == null) throw new ArgumentNullException(nameof(animal));
            _animals.Add(animal);
        }

        /// <summary>
        /// Modifies an existing animal in the collection.
        /// </summary>
        /// <param name="id">The ID of the animal to modify.</param>
        /// <param name="updatedAnimal">The updated animal data.</param>
        public void ModifyAnimal(string id, IAnimal updatedAnimal)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            if (updatedAnimal == null) throw new ArgumentNullException(nameof(updatedAnimal));

            int index = _animals.FindIndex(a => a.Id == id);
            if (index == -1) throw new KeyNotFoundException("Animal not found.");
            _animals[index] = updatedAnimal;
        }

        /// <summary>
        /// Deletes an animal from the collection by ID.
        /// </summary>
        /// <param name="id">The ID of the animal to delete.</param>
        public void DeleteAnimal(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            int index = _animals.FindIndex(a => a.Id == id);
            if (index == -1) throw new KeyNotFoundException("Animal not found.");
            _animals.RemoveAt(index);
        }

        /// <summary>
        /// Gets all animals in the collection.
        /// </summary>
        /// <returns>A read only list of animals.</returns>
        public IReadOnlyList<IAnimal> GetAllAnimals()
        {
            return _animals.AsReadOnly();
        }

        /// <summary>
        /// Finds an animal by ID.
        /// </summary>
        /// <param name="id">The ID to search for.</param>
        /// <returns>The animal if found, otherwise null.</returns>
        public IAnimal GetAnimalById(string id)
        {
            return _animals.Find(a => a.Id == id);
        }
    }
}
