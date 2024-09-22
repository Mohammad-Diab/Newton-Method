# Newton Method

A small Windows app that finds a root of an equation f(x) = 0 with Newton's method and shows every step it takes. Written in C# with WPF on .NET Framework 4.0.

## Features

- Type the function in x, or build it with the on-screen buttons: **x**, **^**, **√x**, **∛x**, **π** and **e**. The operators are `+`, `-`, `*`, `/` and `^`.
- Set the search interval with **Domain** (**From** / **To**).
- Stop either by precision (**ε**, up to 0.025) or after a fixed number of iterations (**Iter-Max**, 1 to 100): fill one of the two. With ε it gives up after 100 iterations.
- **Newton** checks that f(a) · f(b) < 0, picks the starting point x0 from the sign of f · f'' at the ends of the interval, then iterates x(n+1) = x(n) − f(x(n)) / f'(x(n)) and reports the root, the number of iterations and |f(x)|.
- Every step is written to the log box, with a warning when a step lands outside the domain; **Clear History** empties it.

## Writing the function

- Spaces are ignored and `X` is the same as `x`.
- `^` is applied first (right to left), then `*` and `/`, then `+` and `-`.
- No parentheses, no leading minus and no negative numbers: write `x^2-4`, not `-4+x^2`.
- The number 0 on its own is refused (`x^2+0*x-4`); numbers like `0.5` are fine.
- **√x** and **∛x** add `^0.5` and `^0.33333333333`, so they apply only to the number or x just before them: `2*x+1` followed by **√x** gives `2*x+1^0.5`, not √(2x+1).

## Requirements

Windows with .NET Framework 4.0 or later. Windows 8, 10 and 11 already include it.

## Usage

Download the latest `Newton-Method-v<version>.7z` from [Releases](https://github.com/Mohammad-Diab/Newton-Method/releases), extract it, and run `Newton Method.exe`. There is no installer.

The exe is not signed, so Windows SmartScreen may warn the first time you run it: click **More info**, then **Run anyway**.

## Building

```
git clone https://github.com/Mohammad-Diab/Newton-Method.git
```

Open `Newton Method.sln` in Visual Studio and build, or run:

```
MSBuild "Newton Method.sln" /p:Configuration=Release /p:Platform=x86
```

The exe is written to `Newton Method\bin\Release`.

The project targets .NET Framework 4.0. Recent Visual Studio versions don't ship its reference assemblies; if the build stops with `MSB3644`, extract the [Microsoft.NETFramework.ReferenceAssemblies.net40](https://www.nuget.org/packages/Microsoft.NETFramework.ReferenceAssemblies.net40) NuGet package and point MSBuild at its `build` folder:

```
MSBuild "Newton Method.sln" /p:Configuration=Release /p:Platform=x86 /p:TargetFrameworkRootPath=<package>\build
```

The [`mid-method`](https://github.com/Mohammad-Diab/Newton-Method/tree/mid-method) branch holds Mid Method, a separate copy of this program.

## Author

Mohammad Diab

## License

[Apache 2.0](LICENSE)
