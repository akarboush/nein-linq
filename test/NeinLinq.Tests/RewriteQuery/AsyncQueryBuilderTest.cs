﻿using NeinLinq.Fakes.RewriteQuery;
using NeinLinq.Interactive;
using System;
using System.Linq;
using Xunit;

namespace NeinLinq.Tests.RewriteQuery
{
    public class AsyncQueryBuilderTest
    {
        readonly object query = Enumerable.Empty<Dummy>().ToAsyncEnumerable().AsAsyncQueryable().OrderBy(d => d.Id);

        [Fact]
        public void RewriteShouldHandleInvalidArguments()
        {
            Assert.Throws<ArgumentNullException>(() => default(IAsyncQueryable).Rewrite(new Rewriter()));
            Assert.Throws<ArgumentNullException>(() => ((IAsyncQueryable)query).Rewrite(null));
            Assert.Throws<ArgumentNullException>(() => default(IAsyncQueryable<Dummy>).Rewrite(new Rewriter()));
            Assert.Throws<ArgumentNullException>(() => ((IAsyncQueryable<Dummy>)query).Rewrite(null));
            Assert.Throws<ArgumentNullException>(() => default(IOrderedAsyncQueryable).Rewrite(new Rewriter()));
            Assert.Throws<ArgumentNullException>(() => ((IOrderedAsyncQueryable)query).Rewrite(null));
            Assert.Throws<ArgumentNullException>(() => default(IOrderedAsyncQueryable<Dummy>).Rewrite(new Rewriter()));
            Assert.Throws<ArgumentNullException>(() => ((IOrderedAsyncQueryable<Dummy>)query).Rewrite(null));
        }

        [Fact]
        public void ShouldRewriteUntypedQueryable()
        {
            var actual = ((IAsyncQueryable)query).Rewrite(new Rewriter());

            AssertQuery(actual);
        }

        [Fact]
        public void ShouldRewriteTypedQueryable()
        {
            var actual = ((IAsyncQueryable<Dummy>)query).Rewrite(new Rewriter());

            AssertQuery(actual);
        }

        [Fact]
        public void ShouldRewriteUntypedOrderedQueryable()
        {
            var actual = ((IOrderedAsyncQueryable)query).Rewrite(new Rewriter());

            AssertQuery(actual);
        }

        [Fact]
        public void ShouldRewriteTypedOrderedQueryable()
        {
            var actual = ((IOrderedAsyncQueryable<Dummy>)query).Rewrite(new Rewriter());

            AssertQuery(actual);
        }

        static void AssertQuery(IAsyncQueryable actual)
        {
            Assert.IsType<RewriteAsyncQueryable<Dummy>>(actual);
            Assert.IsType<RewriteAsyncQueryProvider>(actual.Provider);

            var actualProvider = (RewriteAsyncQueryProvider)actual.Provider;

            Assert.IsType<Rewriter>(actualProvider.Rewriter);
            Assert.IsAssignableFrom<IAsyncQueryProvider>(actualProvider.Provider);
        }
    }
}
