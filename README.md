# Ookii.Common [![NuGet](https://img.shields.io/nuget/v/Ookii.Common)](https://www.nuget.org/packages/Ookii.Common/)

Ookii.Common is a library containing utilities types and methods shared by several of my other
projects.

- Types that add functionality that would be equivalent to `Nullable<ReadOnlySpan<T>>` or a tuple
  of `(ReadOnlySpan<T>, ReadOnlySpan<T>)`, which aren't natively available because ref structs
  cannot be used as generic type arguments.
- Some string functions available for both `ReadOnlySpan<char>` and `ReadOnlyMemory<char>`:
  `SplitOnce`, `SplitOnceLast`, `StripPrefix`, and `StripSuffix`.

For more information, see the [class library documentation](https://www.ookii.org/Link/OokiiCommonDoc).

## Requirements

Ookii.Common can be used in any project supporting one of the following:

- .Net Standard 2.0
- .Net Standard 2.1
- .Net 8.0 and later

## Building and testing

To build Ookii.Common, make sure you have the following installed:

- [Microsoft .Net 8.0 SDK](https://dotnet.microsoft.com/download) or later

To build the library, tests and samples, simply use the `dotnet build` command in the `src`
directory. You can run the unit tests using `dotnet test`.

The tests are built and run for .Net 8.0 and .Net Framework 4.8. Running the .Net Framework tests on
a non-Windows platform may require the use of [Mono](https://www.mono-project.com/).

The class library documentation is generated using [Sandcastle Help File Builder](https://github.com/EWSoftware/SHFB).
