using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace EAMS.Serialization
{
    using EAMS;
    using EAMS.Exceptions;
    using System.Xml.Linq;

    /// <summary>
    /// Helper for persisting lists of animals in JSON, plain text (JSON payload), and XML.
    /// Uses Newtonsoft.Json with type metadata to support polymorphic (de)serialization.
    /// Validation (duplicate detection) is performed before saving.
    /// Exceptions are allowed to propagate to the GUI layer for user friendly display.
    /// </summary>
    public static class PersistenceHelper
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Include
        };

        /// <summary>
        /// Validate list for duplicates. Rule: same name (case insensitive) AND same concrete type.
        /// Throws DuplicateAnimalException if duplicate found.
        /// </summary>
        public static void ValidateNoDuplicates(IEnumerable<Animal> animals)
        {
            if (animals == null) return;

            var dup = animals
                .Where(a => !string.IsNullOrWhiteSpace(a.Name))
                .GroupBy(a => new { Name = a.Name.Trim().ToLowerInvariant(), Type = a.GetType().FullName })
                .Where(g => g.Count() > 1)
                .Select(g => new { g.Key.Name, g.Key.Type, Count = g.Count(), Items = g.ToList() })
                .FirstOrDefault();

            if (dup != null)
            {
                string msg = $"Duplicate animals detected: name='{dup.Name}', type='{dup.Type}', count={dup.Count}.";
                throw new DuplicateAnimalException(msg);
            }
        }
        /// <summary>
        /// Saves the list of animals to a JSON file. Validates for duplicates before saving.
        /// </summary>
        /// <param name="animals">The list of animals to save.</param>
        /// <param name="fileName">The file path to save the JSON data.</param>
        /// <exception cref="ArgumentNullException">Thrown if animals or fileName is null.</exception>
        public static void SaveJson(IEnumerable<Animal> animals, string fileName)
        {
            if (animals == null) throw new ArgumentNullException(nameof(animals));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            ValidateNoDuplicates(animals);

            var json = JsonConvert.SerializeObject(animals, JsonSettings);
            using (var sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                sw.Write(json);
            }
        }
        /// <summary>
        /// Deserializes a JSON file into a list of Animal objects.
        /// </summary>
        /// <param name="fileName">The path to the JSON file to load.</param>
        /// <returns>A list of Animal objects deserialized from the specified file, or an empty list if the file is empty or
        /// null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when fileName is null, empty, or consists only of white-space characters.</exception>
        public static List<Animal> LoadJson(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
            using (var sr = new StreamReader(fileName, Encoding.UTF8))
            {
                var json = sr.ReadToEnd();
                var list = JsonConvert.DeserializeObject<List<Animal>>(json, JsonSettings);
                return list ?? new List<Animal>();
            }
        }
    }
}