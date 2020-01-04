using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.Edition
{
    /// <summary>
    ///     Describes the strategy of edition where modifications are actually done over the copy of the original
    ///     editable object. After the edition starts, the object exposed through the EditingObject property is the
    ///     copy, not the original object given to the BeginEdition method.
    /// </summary>
    /// <typeparam name="T">The type of object to manage its edition process.</typeparam>
    public class EditionOverCopyStrategy<T> : EditionStrategyBase<T> where T : class, new()
    {
        /// <summary>
        ///     Gets or sets the original object.
        /// </summary>
        public T OriginalObject { get; private set; }

        /// <summary>
        ///     Gets the properties there should be copied for edition.
        /// </summary>
        protected virtual IEnumerable<PropertyInfo> Properties
        {
            get
            {
                return from property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    // Ignore read-only properties
                    where property.SetMethod != null
                    select property;
            }
        }

        /// <summary>
        ///     Gets a value saying whether there were changes made to this editable object.
        /// </summary>
        public override bool HasChanges
        {
            get { return InEdition && DetermineWasModified(OriginalObject, EditingObject, Properties.Select(p => p.Name).ToArray()); }
        }


        /// <summary>
        ///     Determines whether there made modifications to an object, given its two versions: the original and the current one.
        /// </summary>
        /// <param name="original">
        ///     The original version object, which properties values are exactly as they were before edition
        ///     started.
        /// </param>
        /// <param name="current">
        ///     The current version of the object, which properties may be already altered because of edition
        ///     operations.
        /// </param>
        /// <param name="propertiesNames">
        ///     The properties which changes are going to be used as reference to determine whether the
        ///     object has been modified or not.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="current" />, <paramref name="propertiesNames" /> or <paramref name="original" /> is
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertiesNames" /> contains nulls or properties not owned by the object's type.
        /// </exception>
        /// <returns>true if there were changes made to the object; false otherwise</returns>
        protected virtual bool DetermineWasModified<TEntity>(TEntity original, TEntity current, params string[] propertiesNames)
        {
            // Select all the names of the non read only properties of the objects being compared
            IEnumerable<string> allProperties =
                from property in typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                where property.SetMethod != null
                select property.Name;

            // Don't let invalid arguments in
            if (Equals(original, null))
                throw new ArgumentNullException("original");
            if (Equals(current, null))
                throw new ArgumentNullException("current");
            if (propertiesNames != null && propertiesNames.Any(propertyName => propertyName == null || allProperties.All(property => property != propertyName)))
                throw new ArgumentException(System.Properties.Resources.ContainsInvalidNameOfProperties);

            // Get the properties to use in the process, the property in the original type and the one in the current type.
            // This takes care to eliminate the impedance mismatch there could be in the two given objects, because may not
            // be of the same type by inherit TEntity
            Type originalType = original.GetType(), currentType = current.GetType();
            propertiesNames = (propertiesNames == null || !propertiesNames.Any() 
                ? allProperties
                : allProperties.Where(propertiesNames.Contains)).ToArray();
            var properties =
                from propertyName in propertiesNames
                let originalProperty = originalType.GetProperty(propertyName)
                let currentProperty = currentType.GetProperty(propertyName)
                select new { OriginalProperty = originalProperty, CurrentProperty = currentProperty };

            // Now return false if all the values of the properties in the two given objects are equal
            return (from props in properties
                let originalValue = props.OriginalProperty.GetValue(original)
                let currentValue = props.CurrentProperty.GetValue(current)
                where !Equals(currentValue, originalValue)
                select currentValue).Any();
        }

        /// <summary>
        ///     Starts the edition in the specified editable object.
        /// </summary>
        protected override void InternalBeginEdition()
        {
            base.InternalBeginEdition();

            OriginalObject = EditingObject;
            EditingObject = CreateCopy();
        }

        /// <summary>
        ///     Performs the edition ending logic by copying the new values to the original object.
        /// </summary>
        protected override void InternalEndEdition()
        {
            EditingObject.UpdateProperties(OriginalObject, Properties.Select(x => x.Name).ToArray());

            PropertyInfo[] properties = Properties.ToArray();
            foreach (var propertyData in Values)
            {
                PropertyInfo property = properties.Single(p => p.Name == propertyData.Key);
                property.SetValue(OriginalObject, propertyData.Value);
            }

            EditingObject = OriginalObject;
            base.InternalEndEdition();
        }

        /// <summary>
        ///     Performs the edition cancellation logic.
        /// </summary>
        protected override void InternalCancelEdition()
        {
            OriginalObject.UpdateProperties(EditingObject, Properties.Select(x => x.Name).ToArray());

            EditingObject = OriginalObject;
            base.InternalEndEdition();
        }

        /// <summary>
        ///     Creates a copy of the editable object and passes to the copy the value of all the public properties of
        ///     the editing object.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="T" />.</returns>
        protected virtual T CreateCopy()
        {
            var copy = (T)Activator.CreateInstance(OriginalObject.GetType());

            OriginalObject.UpdateProperties(copy, Properties.Select(x => x.Name).ToArray());

            return copy;
        }
    }
}