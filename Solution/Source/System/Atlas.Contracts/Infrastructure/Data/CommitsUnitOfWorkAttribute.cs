using System;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data
{
    /// <summary>
    /// This attribute marks methods in order to specify whether they commit or no the current unit of work instance
    /// (<see cref="IUnitOfWork"/>).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class CommitAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the attribute <see cref="CommitAttribute"/> specifying that the unit of work will be committed.
        /// </summary>
        public CommitAttribute()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the attribute <see cref="CommitAttribute"/> specifying that the unit of work will be committed.
        /// </summary>
        /// <param name="commit">True to specify that the unit of work will be committed; false otherwise.</param>
        public CommitAttribute(bool commit)
        {
            Commit = commit;
        }


        /// <summary>
        /// Gets the value indicating whether the unit of work must be committed at the end of the method or not.
        /// </summary>
        public bool Commit { get; private set; }
    }
}
