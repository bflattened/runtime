// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.CompilerServices;
using System.Text;

namespace System.IO
{
    public static class PathGenerator
    {
        public static string GenerateTestFileName([CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0)
            => GenerateTestFileName(null, memberName, lineNumber);

        public static string GenerateTestFileName(int? index, string memberName, int lineNumber) =>
            string.Format(
                index.HasValue ? "{0}_{1}_{2}_{3}" : "{0}_{1}_{3}",
                memberName ?? "TestBase",
                lineNumber,
                index.GetValueOrDefault(),
                Guid.NewGuid().ToString("N").Substring(0, 8)); // randomness to avoid collisions between derived test classes using same base method concurrently
    }
}
