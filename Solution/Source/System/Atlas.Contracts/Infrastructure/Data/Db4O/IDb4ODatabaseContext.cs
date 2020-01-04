using Db4objects.Db4o;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O
{
    /// <summary>
    /// This is the contract to be implemented by the database contexts using database based in Db4O Object Oriented
    /// databases.
    /// </summary>
    public interface IDb4ODatabaseContext : IDatabaseContext, IObjectContainer
    {
        
    }
}
