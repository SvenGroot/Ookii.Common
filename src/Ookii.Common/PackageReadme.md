# Ookii.Common

Ookii.Common is a library containing utilities types and methods shared by several of my other
projects.

- Types that add functionality that would be equivalent to `Nullable<ReadOnlySpan<T>>` or a tuple
  of `(ReadOnlySpan<T>, ReadOnlySpan<T>)`, which aren't natively available because ref structs
  cannot be used as generic type arguments.
- Some string functions available for both `ReadOnlySpan<char>` and `ReadOnlyMemory<char>`:
  `SplitOnce`, `SplitOnceLast`, `StripPrefix`, and `StripSuffix`.

For more information, see the [class library documentation](https://www.ookii.org/Link/OokiiCommonDoc).
