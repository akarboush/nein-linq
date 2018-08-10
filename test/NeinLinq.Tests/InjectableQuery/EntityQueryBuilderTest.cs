﻿using NeinLinq.EntityFrameworkCore;
using NeinLinq.Fakes.InjectableQuery;
using System;
using System.Linq;
using Xunit;

namespace NeinLinq.Tests.InjectableQuery
{
    public class EntityQueryBuilderTest
    {
        readonly object query = Enumerable.Empty<Dummy>().AsQueryable().OrderBy(d => d.Id);

        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            Assert.Throws<ArgumentNullException>(() => ((IQueryable)query).ToInjectable(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((IQueryable)query).ToInjectable(null, null));
            Assert.Throws<ArgumentNullException>(() => ((IQueryable<Dummy>)query).ToInjectable(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((IQueryable<Dummy>)query).ToInjectable(null, null));
            Assert.Throws<ArgumentNullException>(() => ((IOrderedQueryable)query).ToInjectable(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((IOrderedQueryable)query).ToInjectable(null, null));
            Assert.Throws<ArgumentNullException>(() => ((IOrderedQueryable<Dummy>)query).ToInjectable(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((IOrderedQueryable<Dummy>)query).ToInjectable(null, null));
        }

        [Fact]
        public void ShouldRewriteUntypedQueryable()
        {
            var actual = ((IQueryable)query).ToInjectable();

            AssertQuery(actual);
        }

        [Fact]
        public void ShouldRewriteTypedQueryable()
        {
            var actual = ((IQueryable<Dummy>)query).ToInjectable();

            AssertQuery(actual);
        }

        [Fact]
        public void ShouldRewriteUntypedOrderedQueryable()
        {
            var actual = ((IOrderedQueryable)query).ToInjectable();

            AssertQuery(actual);
        }

        [Fact]
        public void ShouldRewriteTypedOrderedQueryable()
        {
            var actual = ((IOrderedQueryable<Dummy>)query).ToInjectable();

            AssertQuery(actual);
        }

        static void AssertQuery(IQueryable actual)
        {
            Assert.IsType<RewriteEntityQueryable<Dummy>>(actual);
            Assert.IsType<RewriteEntityQueryProvider>(actual.Provider);

            var actualProvider = (RewriteEntityQueryProvider)actual.Provider;

            Assert.IsType<InjectableQueryRewriter>(actualProvider.Rewriter);
            Assert.IsType<EnumerableQuery<Dummy>>(actualProvider.Provider);
        }
    }
}
