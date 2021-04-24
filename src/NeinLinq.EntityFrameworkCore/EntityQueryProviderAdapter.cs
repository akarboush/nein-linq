using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

#pragma warning disable EF1001

namespace NeinLinq
{
    [ExcludeFromCodeCoverage]
    internal class EntityQueryProviderAdapter : EntityQueryProvider
    {
        private readonly RewriteEntityQueryProvider provider;

        public ExpressionVisitor Rewriter => provider.Rewriter;

        public EntityQueryProviderAdapter(RewriteEntityQueryProvider provider)
            : base(new EmptyQueryCompiler())
        {
            this.provider = provider;
        }

        public override IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            => provider.CreateQuery<TElement>(expression);

        public override IQueryable CreateQuery(Expression expression)
            => provider.CreateQuery(expression);

        public override TResult Execute<TResult>(Expression expression)
            => provider.Execute<TResult>(expression);

        public override object? Execute(Expression expression)
            => provider.Execute(expression);

        public override TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
            => provider.ExecuteAsync<TResult>(expression, cancellationToken);

        private class EmptyQueryCompiler : IQueryCompiler
        {
            public Func<QueryContext, TResult> CreateCompiledAsyncQuery<TResult>(Expression query)
                => throw new NotSupportedException();

            public Func<QueryContext, TResult> CreateCompiledQuery<TResult>(Expression query)
                => throw new NotSupportedException();

            public TResult Execute<TResult>(Expression query)
                => throw new NotSupportedException();

            public TResult ExecuteAsync<TResult>(Expression query, CancellationToken cancellationToken)
                => throw new NotSupportedException();
        }
    }
}
