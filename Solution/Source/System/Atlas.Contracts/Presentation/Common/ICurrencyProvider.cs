﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface ICurrencyProvider
    {
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<ICurrencyPresenter> Currencies { get; }

        void AddFromScratch(string muname, string s);
    }
}
