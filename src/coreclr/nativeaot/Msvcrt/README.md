Dumped from the 10.0.19041.0 Windows Kit.

Generated with:

Dump contents of the import lib:

llvm-nm path_to_Windows_kit/ucrt/x64/ucrt.lib
llvm-nm path_to_Windows_kit/ucrt/arm64/ucrt.lib

Run this on the dumped text to parse out lib and symbol mapping:

```csharp
string dllName = "First";
string[] lines = File.ReadAllLines(@"C:\Temp\syms_x64.txt");
for (int i = 0; i < lines.Length - 2; i++)
{
    if (lines[i].EndsWith(':'))
    {
        if (!lines[i].StartsWith(dllName))
        {
            dllName = lines[i].Substring(0, lines[i].Length - 1);
            Console.WriteLine(dllName);
        }

        string line;
        if (lines[i + 1].Contains("__imp_"))
            line = lines[i + 2];
        else if (lines[i + 2].Contains("__imp_"))
            line = lines[i + 1];
        else
            continue;

        string[] tok = line.Split(' ');
        Console.WriteLine("\t" + tok[2]);
    }
}
```
