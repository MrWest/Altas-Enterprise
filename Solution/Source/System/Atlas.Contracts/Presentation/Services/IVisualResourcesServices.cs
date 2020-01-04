namespace CompanyName.Atlas.Contracts.Presentation.Services
{
    /// <summary>
    /// Defines the interface of the services provider aiding to manage the visual resurces of the application. It allows to merge at application level Windows Presentation Foundation's
    /// resource dictionaries.
    /// </summary>
    public interface IVisualResourcesServices
    {
        /// <summary>
        /// Merges the resource dictionary at a given uri with the current <see cref="App"/> one.
        /// </summary>
        /// <param name="dictionaryUri">The <see cref="string"/> representing the URI leading to the resource dictionary to merge.</param>
        void Merge(string dictionaryUri);
    }
}
