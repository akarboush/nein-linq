﻿using System;
using System.Globalization;
using System.Linq;
using NeinLinq.Fakes.DynamicQuery;
using Xunit;

namespace NeinLinq.Tests.DynamicQuery
{
    public class WhereUntypedTest
    {
        private readonly IQueryable data
            = DummyStore.Data.AsQueryable();

        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            _ = Assert.Throws<ArgumentNullException>(() => default(IQueryable)!.Where("Number", DynamicCompare.Equal, null));
            _ = Assert.Throws<ArgumentNullException>(() => data.Where(null!, DynamicCompare.Equal, null));
            _ = Assert.Throws<ArgumentOutOfRangeException>(() => data.Where("Number", (DynamicCompare)(object)-1, null));
            _ = Assert.Throws<ArgumentNullException>(() => default(IQueryable)!.Where("Name", "Contains", "b"));
            _ = Assert.Throws<ArgumentNullException>(() => data.Where(null!, "Contains", "b"));
            _ = Assert.Throws<ArgumentNullException>(() => data.Where("Name", null!, "b"));
        }

        [Theory]
        [InlineData(DynamicCompare.Equal, new[] { 5 })]
        [InlineData(DynamicCompare.NotEqual, new[] { 1, 2, 3, 4, 6, 7, 8, 9 })]
        [InlineData(DynamicCompare.GreaterThan, new[] { 6, 7, 8, 9 })]
        [InlineData(DynamicCompare.GreaterThanOrEqual, new[] { 5, 6, 7, 8, 9 })]
        [InlineData(DynamicCompare.LessThan, new[] { 1, 2, 3, 4 })]
        [InlineData(DynamicCompare.LessThanOrEqual, new[] { 1, 2, 3, 4, 5 })]
        public void ShouldFilterByComparison(DynamicCompare comparison, int[] result)
        {
            var culture = new CultureInfo("de-AT");

            var empty = data.Where("Number", comparison, null);
            var compare = data.Where("Number", comparison, "222,222", culture);

            var emptyResult = empty.Cast<Dummy>().Select(d => d.Id).ToArray();
            var compareResult = compare.Cast<Dummy>().Select(d => d.Id).ToArray();

            var count = comparison == DynamicCompare.NotEqual ? 9 : 0;

            Assert.Equal(count, emptyResult.Length);
            Assert.Equal(result, compareResult);
        }

        [Fact]
        public void ShouldFilterByCustomComparison()
        {
            var contains = data.Where("Name", "Contains", "b");

            var containsResult = contains.Cast<Dummy>().Select(d => d.Id).ToArray();

            Assert.Equal(new[] { 2, 5, 8 }, containsResult);
        }
    }
}
