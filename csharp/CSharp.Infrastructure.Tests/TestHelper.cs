using System.Diagnostics.CodeAnalysis;

namespace CSharp.Infrastructure.Tests;

public static class TestHelper
{
    public static void ThrowIfNull([NotNull] object? @object)
    {
        if (@object is null)
            throw new Exception("Object was null");
    }
}
