// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//

using System;

struct S
{
    public String str;
}

class Test_struct3
{
    public static void c(S s1, S s2, S s3)
    {
        Console.WriteLine(s1.str + s2.str + s3.str);
    }

    public static int Main()
    {
        S sM;

        sM.str = "test";
        c(sM, sM, sM);
        return 100;
    }
}
