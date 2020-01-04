using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System
{
    /// <summary>
    ///     Provides helpers to work with objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Compares two objects of the same type by their public properties values.
        /// </summary>
        /// <typeparam name="T">The type of the objects to compare.</typeparam>
        /// <param name="obj1">The first object to compare.</param>
        /// <param name="obj2">The second object to compare.</param>
        /// <param name="ignoreProperties">A list of properties to ignore when comparing.</param>
        /// <returns>
        ///     True if both objects are not null and all the public properties of the first are equal to the
        ///     corresponding public properties of the second; otherwise false.
        /// </returns>
        public static bool EqualsByMembers<T>(this T obj1, T obj2, params string[] ignoreProperties)
        {
            if (Equals(obj1, null) || Equals(obj2, null)) return false;
            if (obj1 is IEnumerable) throw new NotSupportedException(Properties.Resources.CannotBeEnumerable);

            ignoreProperties = ignoreProperties.Concat(new[] { "Item" }).ToArray();
            Type type = typeof(T);
            IEnumerable<PropertyInfo> properties = type.GetProperties().Where(p => !ignoreProperties.Contains(p.Name) && p.Name != "Item" && p.GetIndexParameters().Length == 0);

            return properties.All(p => Equals(p.GetValue(obj1), p.GetValue(obj2)));
        }

        /// <summary>
        ///     Passes the changes from an object's properties to the other's. If no properties names are provided, then
        ///     all the non read only properties of <paramref name="source" /> type are used. NOTE: both object must
        ///     share the same properties, or at least all those properties provided in <paramref name="propertiesNames" />.
        /// </summary>
        /// <param name="source">The object to copy properties values from.</param>
        /// <param name="destination">The object to put copied properties values to.</param>
        /// <param name="propertiesNames">
        ///     A list of properties names which will be the ones to pass their values from the first object to the
        ///     second one.
        /// </param>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="propertiesNames" /> contains null strings or names of properties not present in the
        ///     entities type.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="source" /> or <paramref name="destination" /> is null.
        /// </exception>
        public static void UpdateProperties(this object source, object destination, params string[] propertiesNames)
        {
            // Don't let null objects
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");

            // Don't let invalid properties
            Type sourceType = source.GetType(), destinationType = destination.GetType();
            IEnumerable<PropertyInfo> allProperties = sourceType.GetProperties().Where(p => p.SetMethod != null);
            if (propertiesNames != null && propertiesNames.Any(p => allProperties.All(property => property.Name != p)))
                throw new ArgumentException(Properties.Resources.ContainsInvalidNameOfProperties, "propertiesNames");

            // Adjust the propertiesNames value to the source type's poperties in case it's null of contains no property
            propertiesNames = (propertiesNames == null || !propertiesNames.Any()
                ? allProperties.Select(p => p.Name)
                : propertiesNames).ToArray();

            // Find all the properties
            var properties =
                from property in propertiesNames
                let sourceProperty = sourceType.GetProperty(property)
                let destinationProperty = destinationType.GetProperty(property)
                where destinationProperty.SetMethod != null
                select new { sourceProperty, destinationProperty };

            // Then restore the original values of all the properties which value actually changed
            foreach (var props in properties)
            {
                PropertyInfo sourceProperty = props.sourceProperty, destinationProperty = props.destinationProperty;
                object sourceValue = sourceProperty.GetValue(source);
                object destinationValue = destinationProperty.GetValue(destination);
                if (!Equals(destinationValue, sourceValue)) destinationProperty.SetValue(destination, sourceValue);
            }
        }

        /// <summary>
        ///     Searches the properties in the given type which names are contained in the given properties names list. If no
        ///     properties names are provided, then all the non read only properties.
        /// </summary>
        /// <param name="type">The type which selected properties will be returned.</param>
        /// <param name="propertiesNames">The names of the properties to select.</param>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="propertiesNames" /> contains names of properties not present in
        ///     <paramref name="type" />.
        /// </exception>
        public static IEnumerable<PropertyInfo> FindProperties(Type type, params string[] propertiesNames)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            IEnumerable<PropertyInfo> properties =
                (propertiesNames != null && propertiesNames.Any()
                    ? type.GetProperties(flags).Where(p => propertiesNames.Contains(p.Name) && p.SetMethod != null)
                    : type.GetProperties(flags).Where(p => p.SetMethod != null)).ToArray();

            if (propertiesNames != null && propertiesNames.Any() && propertiesNames.Count() != properties.Count())
                throw new ArgumentException(Properties.Resources.ContainsInvalidNameOfProperties, "propertiesNames");

            return properties;
        }
    }
}