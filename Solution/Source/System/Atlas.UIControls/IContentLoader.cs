﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// The contract for loading content.
    /// </summary>
    public interface IContentLoader
    {
        /// <summary>
        /// Asynchronously loads content from specified uri.
        /// </summary>
        /// <param name="uri">The content uri.</param>
        /// <param name="cancellationToken">The token used to cancel the load content task.</param>
        /// <returns>The loaded content.</returns>
        Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken);
    }
}
