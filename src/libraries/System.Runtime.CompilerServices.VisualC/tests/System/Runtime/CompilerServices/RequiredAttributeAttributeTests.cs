// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Xunit;

namespace System.Runtime.CompilerServices.Tests
{
    public class RequiredAttributeAttributeTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(typeof(int))]
        [ActiveIssue("https://github.com/dotnet/runtime/issues/51211", typeof(PlatformDetection), nameof(PlatformDetection.IsBuiltWithAggressiveTrimming), nameof(PlatformDetection.IsBrowser))]
        public void Ctor_RequiredContract(Type requiredContract)
        {
            var attribute = new RequiredAttributeAttribute(requiredContract);
            Assert.Equal(requiredContract, attribute.RequiredContract);
        }
    }
}
