﻿using NeinLinq.Interactive;
using NeinLinq.Fakes.SubstitutionQuery;
using System;
using System.Linq;
using Xunit;

namespace NeinLinq.Tests.SubstitutionQuery
{
    public class AsyncQueryBuilderTest
    {
        readonly object query = Enumerable.Empty<Dummy>().ToAsyncEnumerable().AsAsyncQueryable().OrderBy(d => d.Id);

        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            Assert.Throws<ArgumentNullException>(() => ((IAsyncQueryable)query).ToSubstitution(null, typeof(OtherFunctions)));
            Assert.Throws<ArgumentNullException>(() => ((IAsyncQueryable)query).ToSubstitution(typeof(Functions), null));
            Assert.Throws<ArgumentNullException>(() => ((IAsyncQueryable<Dummy>)query).ToSubstitution(null, typeof(OtherFunctions)));
            Assert.Throws<ArgumentNullException>(() => ((IAsyncQueryable<Dummy>)query).ToSubstitution(typeof(Functions), null));
            Assert.Throws<ArgumentNullException>(() => ((IOrderedAsyncQueryable)query).ToSubstitution(null, typeof(OtherFunctions)));
            Assert.Throws<ArgumentNullException>(() => ((IOrderedAsyncQueryable)query).ToSubstitution(typeof(Functions), null));
            Assert.Throws<ArgumentNullException>(() => ((IOrderedAsyncQueryable<Dummy>)query).ToSubstitution(null, typeof(OtherFunctions)));
            Assert.Throws<ArgumentNullException>(() => ((IOrderedAsyncQueryable<Dummy>)query).ToSubstitution(typeof(Functions), null));
        }

        [Fact]
        public void ShouldRewriteUntypedQueryable()
        {
            var actual = ((IAsyncQueryable)query).ToSubstitution(typeof(Functions), typeof(OtherFunctions));

            AssertQuery(actual);
        }

        [Fact]
        public void ShouldRewriteTypedQueryable()
        {
            var actual = ((IAsyncQueryable<Dummy>)query).ToSubstitution(typeof(Functions), typeof(OtherFunctions));

            AssertQuery(actual);
        }

        [Fact]
        public void ShouldRewriteUntypedOrderedQueryable()
        {
            var actual = ((IOrderedAsyncQueryable)query).ToSubstitution(typeof(Functions), typeof(OtherFunctions));

            AssertQuery(actual);
        }

        [Fact]
        public void ShouldRewriteTypedOrderedQueryable()
        {
            var actual = ((IOrderedAsyncQueryable<Dummy>)query).ToSubstitution(typeof(Functions), typeof(OtherFunctions));

            AssertQuery(actual);
        }

        static void AssertQuery(IAsyncQueryable actual)
        {
            Assert.IsType<RewriteAsyncQueryable<Dummy>>(actual);
            Assert.IsType<RewriteAsyncQueryProvider>(actual.Provider);

            var actualProvider = (RewriteAsyncQueryProvider)actual.Provider;

            Assert.IsType<SubstitutionQueryRewriter>(actualProvider.Rewriter);
            Assert.IsAssignableFrom<IAsyncQueryProvider>(actualProvider.Provider);
        }
    }
}
