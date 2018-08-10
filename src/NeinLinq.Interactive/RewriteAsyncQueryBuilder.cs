﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace NeinLinq.Interactive
{
    /// <summary>
    /// Create rewritten async queries.
    /// </summary>
    public static class RewriteAsyncQueryBuilder
    {
        /// <summary>
        /// Rewrite a given query.
        /// </summary>
        /// <param name="value">The query to rewrite.</param>
        /// <param name="rewriter">The rewriter to rewrite the query.</param>
        /// <returns>The rewritten query.</returns>
        public static IAsyncQueryable Rewrite(this IAsyncQueryable value, ExpressionVisitor rewriter)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (rewriter == null)
                throw new ArgumentNullException(nameof(rewriter));

            var provider = new RewriteAsyncQueryProvider(value.Provider, rewriter);

            return (IAsyncQueryable)Activator.CreateInstance(
                typeof(RewriteAsyncQueryable<>).MakeGenericType(value.ElementType),
                provider, value);
        }

        /// <summary>
        /// Rewrite a given query.
        /// </summary>
        /// <param name="value">The query to rewrite.</param>
        /// <param name="rewriter">The rewriter to rewrite the query.</param>
        /// <returns>The rewritten query.</returns>
        public static IOrderedAsyncQueryable Rewrite(this IOrderedAsyncQueryable value, ExpressionVisitor rewriter)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (rewriter == null)
                throw new ArgumentNullException(nameof(rewriter));

            var provider = new RewriteAsyncQueryProvider(value.Provider, rewriter);

            return (IOrderedAsyncQueryable)Activator.CreateInstance(
                typeof(RewriteAsyncQueryable<>).MakeGenericType(value.ElementType),
                provider, value);
        }

        /// <summary>
        /// Rewrite a given query.
        /// </summary>
        /// <typeparam name="T">The type of the query data.</typeparam>
        /// <param name="value">The query to rewrite.</param>
        /// <param name="rewriter">The rewriter to rewrite the query.</param>
        /// <returns>The rewritten query.</returns>
        public static IAsyncQueryable<T> Rewrite<T>(this IAsyncQueryable<T> value, ExpressionVisitor rewriter)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (rewriter == null)
                throw new ArgumentNullException(nameof(rewriter));

            var provider = new RewriteAsyncQueryProvider(value.Provider, rewriter);

            return new RewriteAsyncQueryable<T>(provider, value);
        }

        /// <summary>
        /// Rewrite a given query.
        /// </summary>
        /// <typeparam name="T">The type of the query data.</typeparam>
        /// <param name="value">The query to rewrite.</param>
        /// <param name="rewriter">The rewriter to rewrite the query.</param>
        /// <returns>The rewritten query.</returns>
        public static IOrderedAsyncQueryable<T> Rewrite<T>(this IOrderedAsyncQueryable<T> value, ExpressionVisitor rewriter)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (rewriter == null)
                throw new ArgumentNullException(nameof(rewriter));

            var provider = new RewriteAsyncQueryProvider(value.Provider, rewriter);

            return new RewriteAsyncQueryable<T>(provider, value);
        }
    }
}
