﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NeinLinq.Interactive
{
    /// <summary>
    /// Proxy for rewritten async queries.
    /// </summary>
    public class RewriteAsyncQuery<T> : IOrderedAsyncQueryable<T>
    {
        private readonly Type elementType;
        private readonly Expression expression;
        private readonly IAsyncQueryProvider provider;

        private readonly Lazy<IAsyncEnumerable<T>> enumerable;

        /// <summary>
        /// Create a new query to rewrite.
        /// </summary>
        /// <param name="query">The actual query.</param>
        /// <param name="rewriter">The rewriter to rewrite the query.</param>
        public RewriteAsyncQuery(IAsyncQueryable query, ExpressionVisitor rewriter)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));
            if (rewriter == null)
                throw new ArgumentNullException(nameof(rewriter));

            elementType = query.ElementType;
            expression = query.Expression;

            // replace query provider for further chaining
            provider = new RewriteAsyncQueryProvider(query.Provider, rewriter);

            // rewrite on enumeration
            enumerable = new Lazy<IAsyncEnumerable<T>>(() =>
                query.Provider.CreateQuery<T>(rewriter.Visit(query.Expression)));
        }

        /// <inheritdoc />
        public IAsyncEnumerator<T> GetEnumerator() => enumerable.Value.GetEnumerator();

        /// <inheritdoc />
        public Type ElementType => elementType;

        /// <inheritdoc />
        public Expression Expression => expression;

        /// <inheritdoc />
        public IAsyncQueryProvider Provider => provider;
    }
}
