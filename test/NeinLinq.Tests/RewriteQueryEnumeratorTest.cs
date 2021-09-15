using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NeinLinq.Tests
{
    public class RewriteQueryEnumeratorTest
    {
        [Fact]
        public void Ctor_NullArgument_Throws()
        {
            var enumeratorError = Assert.Throws<ArgumentNullException>(()
                => new RewriteQueryEnumerator<Model>(null!));

            Assert.Equal("enumerator", enumeratorError.ParamName);
        }

        [Fact]
        public void TypedCurrent_ReturnsCurrent()
        {
            using var enumerator = new TestEnumerator();
            using var subject = new RewriteQueryEnumerator<Model>(enumerator);

            Assert.Equal(enumerator.Current, subject.Current);
        }

        [Fact]
        public void UntypedCurrent_ReturnsCurrent()
        {
            using var enumerator = new TestEnumerator();
            using var subject = new RewriteQueryEnumerator<Model>(enumerator);

            Assert.Equal(enumerator.Current, ((IEnumerator)subject).Current);
        }

        [Fact]
        public void MoveNext_MovesNext()
        {
            using var enumerator = new TestEnumerator();
            using var subject = new RewriteQueryEnumerator<Model>(enumerator);

            _ = subject.MoveNext();

            Assert.True(enumerator.MoveNextCalled);
        }

        [Fact]
        public void Reset_Resets()
        {
            using var enumerator = new TestEnumerator();
            using var subject = new RewriteQueryEnumerator<Model>(enumerator);

            subject.Reset();

            Assert.True(enumerator.ResetCalled);
        }

        [Fact]
        public async Task MoveNextAsync_MovesNextAsync()
        {
            using var enumerator = new TestEnumerator();
            using var subject = new RewriteQueryEnumerator<Model>(enumerator);

            _ = await subject.MoveNextAsync();

            Assert.True(enumerator.MoveNextCalled);
        }

#pragma warning disable S3966

        [Fact]
        public void Dispose_Disposes()
        {
            using var enumerator = new TestEnumerator();
            using var subject = new RewriteQueryEnumerator<Model>(enumerator);

            subject.Dispose();

            Assert.True(enumerator.DisposeCalled);
        }

#pragma warning restore S3966

        [Fact]
        public async Task DisposeAsync_DisposesAsync()
        {
            using var enumerator = new TestEnumerator();
            using var subject = new RewriteQueryEnumerator<Model>(enumerator);

            await subject.DisposeAsync();

            Assert.True(enumerator.DisposeCalled);
        }

#pragma warning disable CA1812
#pragma warning disable S1121
#pragma warning disable S3881

        private class Model
        {
        }

        private class TestEnumerator : IEnumerator<Model>
        {
            public Model Current { get; set; }
                = new Model();

            object IEnumerator.Current => Current;

            public bool DisposeCalled { get; set; }

            public void Dispose()
                => DisposeCalled = true;

            public bool MoveNextCalled { get; set; }

            public bool MoveNext()
                => !(MoveNextCalled = true);

            public bool ResetCalled { get; set; }

            public void Reset()
                => ResetCalled = true;
        }
    }
}
