// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Net
{
    internal static class RangeValidationHelpers
    {
        public static bool ValidateRange(int actual, int fromAllowed, int toAllowed)
        {
            // On false, API should throw new ArgumentOutOfRangeException("argument").
            return actual >= fromAllowed && actual <= toAllowed;
        }

        // There are threading tricks a malicious app can use to create an ArraySegment with mismatched
        // array/offset/count.  Copy locally and make sure they're valid before using them.
        public static void ValidateSegment(ArraySegment<byte> segment)
        {
            // ArraySegment<byte> is not nullable.
            ArgumentNullException.ThrowIfNull(segment.Array, nameof(segment));

            // Length zero is explicitly allowed
            if (segment.Offset < 0 || segment.Count < 0 || segment.Count > (segment.Array.Length - segment.Offset))
            {
                throw new ArgumentOutOfRangeException(nameof(segment));
            }
        }
    }
}
