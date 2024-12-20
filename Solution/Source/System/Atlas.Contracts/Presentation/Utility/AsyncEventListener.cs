﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Presentation.Utility
{
    public class AsyncEventListener
    {
        private readonly Func<bool> _predicate;

        public AsyncEventListener()
            : this(() => true)
        {

        }

        public AsyncEventListener(Func<bool> predicate)
        {
            _predicate = predicate;
            Successfully = new Task(() => { });
        }

        public void Listen(object sender, EventArgs eventArgs)
        {
            if (!Successfully.IsCompleted && _predicate.Invoke())
            {
                Successfully.RunSynchronously();
            }
        }

        public Task Successfully { get; set; }
    }
}
