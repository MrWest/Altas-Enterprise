namespace System.ComponentModel
{
    /// <summary>
    ///     Describes the contract of a initializable object which is linked to other types like type.
    /// </summary>
    public interface IInitializable : ISupportInitialize
    {
        /// <summary>
        ///     Occurs when this initializable object has loaded.
        /// </summary>
        event EventHandler Loaded;


        /// <summary>
        ///     Gets whether the current object is initializing.
        /// </summary>
        bool Initializing { get; }


        /// <summary>
        ///     Triggers the initialization of the current object.
        /// </summary>
        void Load();

        /// <summary>
        ///     Reloads the current initializable object.
        /// </summary>
        /// <param name="full">If true, all the initializable objects are reloaded as well.</param>
        void Reload(bool full);
    }
}