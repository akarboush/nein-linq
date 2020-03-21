﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NeinLinq.Fakes.InjectableQuery
{
    public static class Functions
    {
        public static double VelocityWithoutSibling(this Dummy value)
        {
            throw new NotSupportedException();
        }

        public static double VelocityWithConvention(this Dummy value)
        {
            throw new NotSupportedException();
        }

        public static Expression<Func<Dummy, double>> VelocityWithConvention()
        {
            return v => v.Distance / v.Time;
        }

        [InjectLambda]
        public static double VelocityWithMetadata(this Dummy value)
        {
            throw new NotSupportedException();
        }

        public static Expression<Func<Dummy, double>> VelocityWithMetadata()
        {
            return v => v.Distance / v.Time;
        }

        [InjectLambda(typeof(OtherFunctions), nameof(OtherFunctions.Narf))]
        public static double VelocityWithTypeAndMethodMetadata(this Dummy value)
        {
            throw new NotSupportedException();
        }

        [InjectLambda(typeof(OtherFunctions))]
        public static double VelocityWithTypeMetadata(this Dummy value)
        {
            throw new NotSupportedException();
        }

        [InjectLambda(nameof(Narf))]
        public static double VelocityWithMethodMetadata(this Dummy value)
        {
            throw new NotSupportedException();
        }

        public static Expression<Func<Dummy, double>> Narf()
        {
            return v => v.Distance / v.Time;
        }

        public static double VelocityWithStupidSiblingResult(this Dummy value)
        {
            throw new NotSupportedException();
        }

        public static IEnumerable<Func<Dummy, double>> VelocityWithStupidSiblingResult()
        {
            yield return v => v.Distance / v.Time;
        }

        public static double VelocityWithInvalidSiblingResult(this Dummy value)
        {
            throw new NotSupportedException();
        }

        public static Func<Dummy, double> VelocityWithInvalidSiblingResult()
        {
            return v => v.Distance / v.Time;
        }

        public static double VelocityWithStupidSiblingSignature(this Dummy value)
        {
            throw new NotSupportedException();
        }

        public static Expression<Func<Dummy, float>> VelocityWithStupidSiblingSignature()
        {
            return v => (float)(v.Distance / v.Time);
        }

        public static double VelocityWithInvalidSiblingSignature(this Dummy value)
        {
            throw new NotSupportedException();
        }

        public static Expression<Func<double, double, double>> VelocityWithInvalidSiblingSignature()
        {
            return (d, t) => d / t;
        }

        public static double VelocityWithGenericArguments<TDummy>(this TDummy value)
            where TDummy : IDummy
        {
            throw new NotSupportedException();
        }

        public static Expression<Func<TDummy, double>> VelocityWithGenericArguments<TDummy>()
            where TDummy : IDummy
        {
            return v => v.Distance / v.Time;
        }

        public static double VelocityWithInvalidGenericArguments<TDummy>(this TDummy value)
            where TDummy : IDummy
        {
            throw new NotSupportedException();
        }

        public static Expression<Func<Dummy, double>> VelocityWithInvalidGenericArguments()
        {
            return v => v.Distance / v.Time;
        }

        public static double VelocityWithNonPublicSibling(this Dummy value)
        {
            throw new NotSupportedException();
        }

        private static Expression<Func<Dummy, double>> VelocityWithNonPublicSibling()
        {
            return v => v.Distance / v.Time;
        }

        private static CachedExpression<Func<Dummy, double>> VelocityWithCachedExpressionExpr { get; }
            = CachedExpression.From<Func<Dummy, double>>(v => v.Distance / v.Time);

        [InjectLambda]
        public static double VelocityWithCachedExpression(this Dummy value)
            => VelocityWithCachedExpressionExpr.Compiled(value);
    }
}
