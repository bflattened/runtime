// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Security.Cryptography.Tests
{
    /// <summary>
    /// Sha384Managed has a copy of the same implementation as SHA384
    /// </summary>
    public class Sha384ManagedTests : Sha384Tests
    {
        protected override HashAlgorithm Create()
        {
            return new SHA384Managed();
        }
    }
}
