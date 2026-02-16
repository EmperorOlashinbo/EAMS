using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS.GenericList
{
    /// <summary>
    /// Generic interface for managing a collection of items.
    /// Used to store, modify and delete animals.
    /// </summary>
    /// <typeparam name="T">The type of objects being managed (e.g. IAnimal)</typeparam>
    public interface IListManager<T>
    {
        /// <summary>
        /// Adds a new item to the collection.
        /// </summary>
        /// <param name="item">The item to add. Cannot be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if item is null.</exception>
        void Add(T item);

        /// <summary>
        /// Replaces an existing item with a new one.
        /// </summary>
        /// <param name="item">The updated item.</param>
        /// <returns>true if the item was found and replaced, false otherwise.</returns>
        bool Edit(T item);

        /// <summary>
        /// Removes an item from the collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>true if the item was found and removed, false otherwise.</returns>
        bool Delete(T item);

        /// <summary>
        /// Returns a read only view of all items in the collection.
        /// </summary>
        IReadOnlyList<T> GetAll();

        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        void Clear();
    }
}
