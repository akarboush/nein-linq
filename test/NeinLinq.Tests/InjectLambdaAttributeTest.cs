using Xunit;

namespace NeinLinq.Tests;

public class InjectLambdaAttributeTest
{
    [Fact]
    public void Ctor_NullArgument_Throws()
    {
        var methodError = Assert.Throws<ArgumentNullException>(()
            => new InjectLambdaAttribute((string)null!));
        var targetError = Assert.Throws<ArgumentNullException>(()
            => new InjectLambdaAttribute((Type)null!));
        var targetMethodError = Assert.Throws<ArgumentNullException>(()
            => new InjectLambdaAttribute(null!, "Narf"));
        var methodTargetError = Assert.Throws<ArgumentNullException>(()
            => new InjectLambdaAttribute(typeof(object), null!));

        Assert.Equal("method", methodError.ParamName);
        Assert.Equal("target", targetError.ParamName);
        Assert.Equal("target", targetMethodError.ParamName);
        Assert.Equal("method", methodTargetError.ParamName);
    }
}
