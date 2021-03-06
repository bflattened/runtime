// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

internal static partial class Interop
{
    internal static partial class Crypt32
    {
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct CMSG_CTRL_KEY_AGREE_DECRYPT_PARA
        {
            internal int cbSize;
            internal IntPtr hProv;
            internal CryptKeySpec dwKeySpec;
            internal CMSG_KEY_AGREE_RECIPIENT_INFO* pKeyAgree;
            internal int dwRecipientIndex;
            internal int dwRecipientEncryptedKeyIndex;
            internal CRYPT_BIT_BLOB OriginatorPublicKey;
        }
    }
}
