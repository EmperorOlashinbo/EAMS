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
    }
}