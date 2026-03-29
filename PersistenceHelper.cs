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
        /// <summary>
        /// Saves a collection of Animal objects to a file in JSON format.
        /// </summary>
        /// <param name="animals">The collection of Animal objects to serialize.</param>
        /// <param name="fileName">The path of the file to save the JSON data to.</param>
        public static void SaveTextAsJson(IEnumerable<Animal> animals, string fileName) => SaveJson(animals, fileName);
        /// <summary>
        /// Loads a list of Animal objects from a JSON file.
        /// </summary>
        /// <param name="fileName">The path to the JSON file containing animal data.</param>
        /// <returns>A list of Animal objects deserialized from the specified file.</returns>
        public static List<Animal> LoadTextAsJson(string fileName) => LoadJson(fileName);

        /// <summary>
        /// Saves a collection of Animal objects to an XML file. The XML structure is derived from the JSON representation,
        /// ensuring a consistent format between JSON and XML outputs.
        /// </summary>
        /// <param name="animals">The collection of Animal objects to serialize.</param>
        /// <param name="fileName">The path of the file to save the XML data to.</param>
        /// <exception cref="ArgumentNullException">Thrown if animals or fileName is null.</exception>
        public static void SaveXml(IEnumerable<Animal> animals, string fileName)
        {
            if (animals == null) throw new ArgumentNullException(nameof(animals));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            ValidateNoDuplicates(animals);

            // Wrap array into an object to create a root element
            var json = JsonConvert.SerializeObject(new { AnimalList = animals }, JsonSettings);
            // Convert JSON to XmlDocument
            var doc = JsonConvert.DeserializeXmlNode(json, "Root");
            using (var writer = XmlWriter.Create(fileName, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
            {
                doc.WriteTo(writer);
            }
        }
        /// <summary>
        /// Loads animal data from an XML file and returns a list of Animal objects.
        /// </summary>
        /// <param name="fileName">The path to the XML file containing animal data.</param>
        /// <returns>A list of Animal objects parsed from the XML file, or an empty list if no animals are found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when fileName is null, empty, or consists only of white-space characters.</exception>
        public static List<Animal> LoadXml(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            var doc = new XmlDocument();
            doc.Load(fileName);

            // Convert XML to JSON
            string json = JsonConvert.SerializeXmlNode(doc.DocumentElement, Formatting.None, true);

            var rootObj = JsonConvert.DeserializeObject<JObject>(json);
            JToken animalsToken = null;

            // Try multiple probable paths
            if (rootObj.TryGetValue("AnimalList", out animalsToken) == false)
            {
                // look under Root
                var rootNode = rootObj.Properties().FirstOrDefault()?.Value as JObject;
                if (rootNode != null)
                {
                    rootNode.TryGetValue("AnimalList", out animalsToken);
                }
            }

            if (animalsToken == null)
            {
                // attempt to find first array token
                animalsToken = rootObj.Descendants().OfType<JArray>().FirstOrDefault();
            }

            if (animalsToken == null) return new List<Animal>();

            var animalsJson = animalsToken.ToString();
            var list = JsonConvert.DeserializeObject<List<Animal>>(animalsJson, JsonSettings);
            return list ?? new List<Animal>();
        }
    }
}