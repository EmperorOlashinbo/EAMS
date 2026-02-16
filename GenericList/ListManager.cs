using EAMS.AnimalsGen;
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
    }
}
