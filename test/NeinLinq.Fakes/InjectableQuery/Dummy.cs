﻿using System;
using System.Linq.Expressions;

#pragma warning disable CA1801
#pragma warning disable CA1822

namespace NeinLinq.Fakes.InjectableQuery
{
    public class Dummy : IDummy
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public double Distance { get; set; }

        public double Time { get; set; }

        public double Velocity
            => Distance / Time;

        [InjectLambda(nameof(InjectVelocityInternal))]
        public double VelocityInternal { get; }

        public double VelocityInternalWithGetter
        {
            [InjectLambda(nameof(InjectVelocityInternal))]
            get => throw new NotSupportedException();
        }

        public static Expression<Func<Dummy, double>> InjectVelocityInternal()
            => v => v.Distance / v.Time;

        [InjectLambda(typeof(DummyExtensions))]
        public double VelocityExternal { get; }

        public double VelocityExternalWithGetter
        {
            [InjectLambda(typeof(DummyExtensions), nameof(DummyExtensions.VelocityExternal))]
            get => throw new NotSupportedException();
        }

        [InjectLambda(nameof(InjectVelocityInternalProperty))]
        public double VelocityInternalProperty { get; }

        public static Expression<Func<Dummy, double>> InjectVelocityInternalProperty
            => v => v.Distance / v.Time;

        [InjectLambda(typeof(DummyExtensions))]
        public double VelocityExternalProperty { get; }

        public double VelocityWithConvention { get; }

        public static Expression<Func<Dummy, double>> VelocityWithConventionExpr
            => v => v.Distance / v.Time;

        [InjectLambda]
        public double VelocityWithMetadata { get; }

        public static Expression<Func<Dummy, double>> VelocityWithMetadataExpr
            => v => v.Distance / v.Time;

        [InjectLambda]
        public double VelocityWithNull { get; }

        public static Expression<Func<Dummy, double>> VelocityWithNullExpr
            => null!;

        public double VelocityWithoutSibling { get; }

        public double VelocityWithStupidSiblingResult { get; }

        public static Lazy<Func<Dummy, double>> VelocityWithStupidSiblingResultExpr
            => new Lazy<Func<Dummy, double>>(() => v => v.Distance / v.Time);

        public double VelocityWithInvalidSiblingResult { get; }

        public static Func<Dummy, double> VelocityWithInvalidSiblingResultExpr
            => v => v.Distance / v.Time;

        public double VelocityWithStupidSiblingSignature { get; }

        public static Expression<Func<Dummy, float>> VelocityWithStupidSiblingSignatureExpr
            => v => (float)(v.Distance / v.Time);

        public double VelocityWithInvalidSiblingSignature { get; }

        public static Expression<Func<double, double, double>> VelocityWithInvalidSiblingSignatureExpr
            => (d, t) => d / t;

        public double VelocityWithNonPublicSibling { get; }

        private static Expression<Func<Dummy, double>> VelocityWithNonPublicSiblingExpr
            => v => v.Distance / v.Time;
    }
}
