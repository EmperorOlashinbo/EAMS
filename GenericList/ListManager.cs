using EAMS.AnimalsGen;
using EAMS.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.GenericList
{
    /// <summary>
    /// Generic implementation of IListManager using List<T>.
    /// Can manage any type, including IAnimal or Animal.
    /// </summary>
    /// <typeparam name="T">The type of objects being managed</typeparam>
    public class ListManager<T> : IListManager<T>
    {
        private readonly List<T> _items = new List<T>();
        /// <summary>
        /// Adds an item to the list. Throws ArgumentNullException if item is null.
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _items.Add(item);
        }
        /// <summary>
        /// Edits an existing item in the list. For IAnimal, it matches by Id. For other types, 
        /// it does not support editing (returns false).
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Edit(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            // If T is IAnimal, we can use Id to find match
            if (item is IAnimal newAnimal)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i] is IAnimal existing && existing.Id == newAnimal.Id)
                    {
                        _items[i] = item;
                        return true;
                    }
                }
            }
            // If not IAnimal, we can't reliably identify not supported
            return false;
        }
        /// <summary>
        /// Deletes an item from the list. For IAnimal, it matches by Id. For other types, 
        /// it uses default equality.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Delete(T item)
        {
            if (item == null)
                return false;

            return _items.Remove(item);
        }
        /// <summary>
        /// Gets all items in the list as a read only collection. 
        /// This prevents external modification of the internal list.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<T> GetAll()
        {
            return _items.AsReadOnly();
        }
        /// <summary>
        /// Gets the number of items currently in the list. 
        /// This is a read only property that reflects the current state of the collection.
        /// </summary>
        public int Count => _items.Count;
        /// <summary>
        /// Clears all items from the list. After calling this method, 
        /// the list will be empty and Count will be 0.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }
        // Persistence implementations:
        // Supported only when T is Animal (concrete persistence helper works with Animal)

        /// <summary>
        /// Serializes the list of Animal objects to a JSON file.
        /// </summary>
        /// <param name="fileName">The path to the file where the JSON data will be saved.</param>
        /// <returns>true if serialization succeeds.</returns>
        /// <exception cref="NotSupportedException">Thrown if T is not Animal.</exception>
        public bool JsonSerialize(string fileName)
        {
            if (typeof(T) != typeof(Animal)) throw new NotSupportedException("JsonSerialize supported only for ListManager<Animal>.");
            var animals = new List<Animal>();
            foreach (var it in _items) animals.Add(it as Animal);
            PersistenceHelper.SaveJson(animals, fileName);
            return true;
        }
        /// <summary>
        /// Deserializes a JSON file and populates the collection with Animal objects.
        /// </summary>
        /// <param name="fileName">The path to the JSON file to deserialize.</param>
        /// <returns>true if deserialization is successful.</returns>
        /// <exception cref="NotSupportedException">Thrown if the generic type parameter is not Animal.</exception>
        public bool JsonDeserialize(string fileName)
        {
            if (typeof(T) != typeof(Animal)) throw new NotSupportedException("JsonDeserialize supported only for ListManager<Animal>.");
            var loaded = PersistenceHelper.LoadJson(fileName);
            _items.Clear();
            foreach (var a in loaded) _items.Add((T)(object)a);
            return true;
        }
        /// <summary>
        /// Serializes the list of Animal objects to an XML file.
        /// </summary>
        /// <param name="fileName">The path and name of the file to which the XML data will be saved.</param>
        /// <returns>true if the serialization is successful.</returns>
        /// <exception cref="NotSupportedException">Thrown if the generic type parameter is not Animal.</exception>
        public bool XmlSerialize(string fileName)
        {
            if (typeof(T) != typeof(Animal)) throw new NotSupportedException("XmlSerialize supported only for ListManager<Animal>.");
            var animals = new List<Animal>();
            foreach (var it in _items) animals.Add(it as Animal);
            PersistenceHelper.SaveXml(animals, fileName);
            return true;
        }
        /// <summary>
        /// Deserializes a list of Animal objects from an XML file and populates the collection.
        /// </summary>
        /// <param name="fileName">The path to the XML file to deserialize.</param>
        /// <returns>true if deserialization is successful.</returns>
        /// <exception cref="NotSupportedException">Thrown if the generic type parameter T is not Animal.</exception>
        public bool XmlDeserialize(string fileName)
        {
            if (typeof(T) != typeof(Animal)) throw new NotSupportedException("XmlDeserialize supported only for ListManager<Animal>.");
            var loaded = PersistenceHelper.LoadXml(fileName);
            _items.Clear();
            foreach (var a in loaded) _items.Add((T)(object)a);
            return true;
        }
        /// <summary>
        /// Serializes the list of Animal objects to a text file in JSON format.
        /// </summary>
        /// <param name="fileName">The path of the file to which the data will be saved.</param>
        /// <returns>true if the serialization succeeds.</returns>
        /// <exception cref="NotSupportedException">Thrown if the generic type parameter is not Animal.</exception>
        public bool TextSerialize(string fileName)
        {
            if (typeof(T) != typeof(Animal)) throw new NotSupportedException("TextSerialize supported only for ListManager<Animal>.");
            var animals = new List<Animal>();
            foreach (var it in _items) animals.Add(it as Animal);
            PersistenceHelper.SaveTextAsJson(animals, fileName);
            return true;
        }         
    }
}
