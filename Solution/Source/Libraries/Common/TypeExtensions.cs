namespace System
{
    /// <summary>
    ///     Provides extensions for the <see cref="System.Type" /> type.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Determines whether the given type implements the given interface.
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Type" /> of the interface to check whether the given type implements it.</typeparam>
        /// <param name="type">The <see cref="System.Type" /> to check whether it implements the interface.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type" /> is null.</exception>
        /// <returns>True when the type implements the interface; false otherwise.</returns>
        public static bool Implements<T>(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            Type @interface = typeof(T);

            return type.GetInterface(@interface.FullName) != null;
        }

        /// <summary>
        ///     Determines whether the given type implements the given interface.
        /// </summary>
        /// <param name="type">The <see cref="System.Type" /> to check whether it implements the interface.</param>
        /// <param name="iface">The <see cref="System.Type" /> of the interface to check whether the given type implements it.</param>
        /// <exception cref="ArgumentNullException">Either <paramref name="type" /> or <paramref name="iface" /> is null.</exception>
        /// <returns>True when the type implements the interface; false otherwise.</returns>
        public static bool Implements(this Type type, Type iface)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (iface == null) throw new ArgumentNullException("iface");

            return type.GetInterface(iface.FullName) != null;
        }

        /// <summary>
        ///     Determines whether the given type implements <see cref="System.ICloneable" />.
        /// </summary>
        /// <param name="type">The <see cref="System.Type" /> to determine if it implements <see cref="System.ICloneable" />.</param>
        /// <exception cref="ArgumentNullException">Either <paramref name="type" /> or <paramref name="iface" /> is null.</exception>
        /// <returns>True in case the type implements <see cref="System.ICloneable" />; false otherwise.</returns>
        public static bool IsCloneable(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return Implements<ICloneable>(type);
        }
    }
}