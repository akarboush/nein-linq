﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace NeinLinq
{
    /// <summary>
    /// Proxy for query enumerable.
    /// </summary>
    public class RewriteQueryEnumerable<T> : IEnumerable<T>
#if !(NET40 || NET45)
        , IAsyncEnumerable<T>
#endif
    {
        private readonly IEnumerable<T> enumerable;

        /// <summary>
        /// Create a new enumerable proxy.
        /// </summary>
        /// <param name="enumerable">The actual enumerable.</param>
        public RewriteQueryEnumerable(IEnumerable<T> enumerable)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            this.enumerable = enumerable;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RewriteQueryEnumerator<T>(enumerable.GetEnumerator());
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return new RewriteQueryEnumerator<T>(enumerable.GetEnumerator());
        }

#if !(NET40 || NET45)
        /// <inheritdoc />
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new RewriteQueryEnumerator<T>(enumerable.GetEnumerator());
        }
#endif
    }
}
