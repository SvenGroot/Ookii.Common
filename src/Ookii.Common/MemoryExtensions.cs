using System;
using System.Globalization;

namespace Ookii.Common;

/// <summary>
/// Provides extensions for the <see cref="ReadOnlySpan{T}"/> structure.
/// </summary>
public static partial class MemoryExtensions
{
    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into two parts at the specified index, optionally
    /// skipping the specified number of elements.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The span to split.</param>
    /// <param name="index">The index to split at.</param>
    /// <param name="skip">The number of elements to skip at the split point.</param>
    /// <returns>
    /// A pair containing the parts before and after the split point.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> or <paramref name="index"/> + <paramref name="skip"/> is less than
    /// 0 or greater than the length of <paramref name="span"/>.
    /// </exception>
    public static ReadOnlySpanPair<T, T> SplitAt<T>(this ReadOnlySpan<T> span, int index, int skip = 0)
        => new(span.Slice(0, index), span.Slice(index + skip));

    /// <summary>
    /// Splits a <see cref="ReadOnlyMemory{T}"/> into two parts at the specified index, optionally
    /// skipping the specified number of elements.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    /// <param name="span">The span to split.</param>
    /// <param name="index">The index to split at.</param>
    /// <param name="skip">The number of elements to skip at the split point.</param>
    /// <returns>
    /// A tuple containing the parts before and after the split point.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index"/> or <paramref name="index"/> + <paramref name="skip"/> is less than
    /// 0 or greater than the length of <paramref name="span"/>.
    /// </exception>
    public static (ReadOnlyMemory<T>, ReadOnlyMemory<T>) SplitAt<T>(this ReadOnlyMemory<T> span, int index, int skip = 0)
        => new(span.Slice(0, index), span.Slice(index + skip));

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into two parts at the first occurrence of a separator.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to split.</param>
    /// <param name="separator">The separator to split the span at.</param>
    /// <returns>
    /// If the separator was found, a <see cref="NullableReadOnlySpanPair{T, T}"/> containing the
    /// parts before and after the separator; otherwise, an empty <see cref="NullableReadOnlySpanPair{T, T}"/>.
    /// </returns>
    public static NullableReadOnlySpanPair<T, T> SplitOnce<T>(this ReadOnlySpan<T> span, T separator)
        where T : IEquatable<T>
    {
        var index = span.IndexOf(separator);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, 1);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlyMemory{T}"/> into two parts at the first occurrence of a separator.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/> to split.</param>
    /// <param name="separator">The separator to split the span at.</param>
    /// <returns>
    /// If the separator was found, a tuple containing the parts before and after the separator;
    /// otherwise, <see langword="null"/>.
    /// </returns>
    public static (ReadOnlyMemory<T>, ReadOnlyMemory<T>)? SplitOnce<T>(this ReadOnlyMemory<T> memory, T separator)
        where T : IEquatable<T>
    {
        var index = memory.Span.IndexOf(separator);
        if (index < 0)
        {
            return default;
        }

        return memory.SplitAt(index, 1);
    }

    /// <inheritdoc cref="SplitOnce{T}(ReadOnlySpan{T}, T)"/>
    public static NullableReadOnlySpanPair<T, T> SplitOnce<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separator)
        where T : IEquatable<T>
    {
        var index = span.IndexOf(separator);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, separator.Length);
    }

    /// <inheritdoc cref="SplitOnce{T}(ReadOnlyMemory{T}, T)"/>
    public static (ReadOnlyMemory<T>, ReadOnlyMemory<T>)? SplitOnce<T>(this ReadOnlyMemory<T> memory, ReadOnlySpan<T> separator)
        where T : IEquatable<T>
    {
        var index = memory.Span.IndexOf(separator);
        if (index < 0)
        {
            return default;
        }

        return memory.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a span of characters into two parts at the first occurrence of a separator.
    /// </summary>
    /// <param name="span">The span of characters to split.</param>
    /// <param name="separator">A span of characters containing the separator to split on.</param>
    /// <param name="comparisonType">The type of string comparison to use to find the separator.</param>
    /// <returns>
    /// If the separator was found, a <see cref="NullableReadOnlySpanPair{T, T}"/> containing the
    /// parts before and after the separator; otherwise, an empty <see cref="NullableReadOnlySpanPair{T, T}"/>.
    /// </returns>
    public static NullableReadOnlySpanPair<char, char> SplitOnce(this ReadOnlySpan<char> span, ReadOnlySpan<char> separator,
        StringComparison comparisonType)
    {
        var index = span.IndexOf(separator, comparisonType);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a span of characters into two parts at the first occurrence of a separator.
    /// </summary>
    /// <param name="memory">The span of characters to split.</param>
    /// <param name="separator">A span of characters containing the separator to split on.</param>
    /// <param name="comparisonType">The type of string comparison to use to find the separator.</param>
    /// <returns>
    /// If the separator was found, a tuple containing the parts before and after the separator;
    /// otherwise, <see langword="null"/>.
    /// </returns>
    public static (ReadOnlyMemory<char>, ReadOnlyMemory<char>)? SplitOnce(this ReadOnlyMemory<char> memory,
        ReadOnlySpan<char> separator, StringComparison comparisonType)
    {
        var index = memory.Span.IndexOf(separator, comparisonType);
        if (index < 0)
        {
            return default;
        }

        return memory.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a span of characters into two parts at the first occurrence of a separator.
    /// </summary>
    /// <param name="span">The span of characters to split.</param>
    /// <param name="separator">A span of characters containing the separator to split on.</param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> to use to perform string comparison when finding the
    /// separator.
    /// </param>
    /// <param name="options">The options to use for the string comparison.</param>
    /// <returns>
    /// If the separator was found, a <see cref="NullableReadOnlySpanPair{T, T}"/> containing the
    /// parts before and after the separator; otherwise, an empty <see cref="NullableReadOnlySpanPair{T, T}"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   When using the .Net Standard 2.0 or 2.1 version of this library, this method may allocate
    ///   strings to perform the comparison. This is because the <see cref="CompareInfo"/> class
    ///   does not provide string comparison methods for use with <see cref="ReadOnlySpan{T}"/>.
    /// </para>
    /// </remarks>
    public static NullableReadOnlySpanPair<char, char> SplitOnce(this ReadOnlySpan<char> span, ReadOnlySpan<char> separator,
        CultureInfo culture, CompareOptions options = CompareOptions.None)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        var index = culture.CompareInfo.IndexOf(span, separator, options);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a span of characters into two parts at the first occurrence of a separator.
    /// </summary>
    /// <param name="memory">The span of characters to split.</param>
    /// <param name="separator">A span of characters containing the separator to split on.</param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> to use to perform string comparison when finding the
    /// separator.
    /// </param>
    /// <param name="options">The options to use for the string comparison.</param>
    /// <returns>
    /// If the separator was found, a tuple containing the parts before and after the separator;
    /// otherwise, <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   When using the .Net Standard 2.0 or 2.1 version of this library, this method may allocate
    ///   strings to perform the comparison. This is because the <see cref="CompareInfo"/> class
    ///   does not provide string comparison methods for use with <see cref="ReadOnlySpan{T}"/>.
    /// </para>
    /// </remarks>
    public static (ReadOnlyMemory<char>, ReadOnlyMemory<char>)? SplitOnce(this ReadOnlyMemory<char> memory,
        ReadOnlySpan<char> separator, CultureInfo culture, CompareOptions options = CompareOptions.None)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        var index = culture.CompareInfo.IndexOf(memory.Span, separator, options);
        if (index < 0)
        {
            return default;
        }

        return memory.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into two parts at the first occurrence of any of the
    /// specified separators.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to split.</param>
    /// <param name="separators">The set of separators to split the span at.</param>
    /// <returns>
    /// If a separator was found, a tuple containing the parts before and after the separator;
    /// otherwise, <see langword="null"/>.
    /// </returns>
    public static NullableReadOnlySpanPair<T, T> SplitOnceAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separators)
        where T : IEquatable<T>
    {
        var index = span.IndexOfAny(separators);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, 1);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into two parts at the first occurrence of any of the
    /// specified separators.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="memory">The <see cref="ReadOnlySpan{T}"/> to split.</param>
    /// <param name="separators">The set of separators to split the span at.</param>
    /// <returns>
    /// If a separator was found, a <see cref="NullableReadOnlySpanPair{T, T}"/> containing the
    /// parts before and after the separator; otherwise, an empty <see cref="NullableReadOnlySpanPair{T, T}"/>.
    /// </returns>
    public static (ReadOnlyMemory<T>, ReadOnlyMemory<T>)? SplitOnceAny<T>(this ReadOnlyMemory<T> memory, ReadOnlySpan<T> separators)
        where T : IEquatable<T>
    {
        var index = memory.Span.IndexOfAny(separators);
        if (index < 0)
        {
            return null;
        }

        return memory.SplitAt(index, 1);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into two parts at the last occurrence of a separator.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to split.</param>
    /// <param name="separator">The separator to split the span at.</param>
    /// <returns>
    /// If the separator was found, a <see cref="NullableReadOnlySpanPair{T, T}"/> containing the
    /// parts before and after the separator; otherwise, an empty <see cref="NullableReadOnlySpanPair{T, T}"/>.
    /// </returns>
    public static NullableReadOnlySpanPair<T, T> SplitOnceLast<T>(this ReadOnlySpan<T> span, T separator)
        where T : IEquatable<T>
    {
        var index = span.LastIndexOf(separator);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, 1);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlyMemory{T}"/> into two parts at the first occurrence of a separator.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/> to split.</param>
    /// <param name="separator">The separator to split the span at.</param>
    /// <returns>
    /// If the separator was found, a tuple containing the parts before and after the separator;
    /// otherwise, <see langword="null"/>.
    /// </returns>
    public static (ReadOnlyMemory<T>, ReadOnlyMemory<T>)? SplitOnceLast<T>(this ReadOnlyMemory<T> memory, T separator)
        where T : IEquatable<T>
    {
        var index = memory.Span.LastIndexOf(separator);
        if (index < 0)
        {
            return default;
        }

        return memory.SplitAt(index, 1);
    }

    /// <inheritdoc cref="SplitOnce{T}(ReadOnlySpan{T}, T)"/>
    public static NullableReadOnlySpanPair<T, T> SplitOnceLast<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separator)
        where T : IEquatable<T>
    {
        var index = span.LastIndexOf(separator);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, separator.Length);
    }

    /// <inheritdoc cref="SplitOnceLast{T}(ReadOnlyMemory{T}, T)"/>
    public static (ReadOnlyMemory<T>, ReadOnlyMemory<T>)? SplitOnceLast<T>(this ReadOnlyMemory<T> memory, ReadOnlySpan<T> separator)
        where T : IEquatable<T>
    {
        var index = memory.Span.LastIndexOf(separator);
        if (index < 0)
        {
            return default;
        }

        return memory.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a span of characters into two parts at the last occurrence of a separator.
    /// </summary>
    /// <param name="span">The span of characters to split.</param>
    /// <param name="separator">A span of characters containing the separator to split on.</param>
    /// <param name="comparisonType">The type of string comparison to use to find the separator.</param>
    /// <returns>
    /// If the separator was found, a <see cref="NullableReadOnlySpanPair{T, T}"/> containing the
    /// parts before and after the separator; otherwise, an empty <see cref="NullableReadOnlySpanPair{T, T}"/>.
    /// </returns>
    public static NullableReadOnlySpanPair<char, char> SplitOnceLast(this ReadOnlySpan<char> span, ReadOnlySpan<char> separator,
        StringComparison comparisonType)
    {
        var index = span.LastIndexOf(separator, comparisonType);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a span of characters into two parts at the first occurrence of a separator.
    /// </summary>
    /// <param name="memory">The span of characters to split.</param>
    /// <param name="separator">A span of characters containing the separator to split on.</param>
    /// <param name="comparisonType">The type of string comparison to use to find the separator.</param>
    /// <returns>
    /// If the separator was found, a tuple containing the parts before and after the separator;
    /// otherwise, <see langword="null"/>.
    /// </returns>
    public static (ReadOnlyMemory<char>, ReadOnlyMemory<char>)? SplitOnceLast(this ReadOnlyMemory<char> memory,
        ReadOnlySpan<char> separator, StringComparison comparisonType)
    {
        var index = memory.Span.LastIndexOf(separator, comparisonType);
        if (index < 0)
        {
            return default;
        }

        return memory.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a span of characters into two parts at the last occurrence of a separator.
    /// </summary>
    /// <param name="span">The span of characters to split.</param>
    /// <param name="separator">A span of characters containing the separator to split on.</param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> to use to perform string comparison when finding the
    /// separator.
    /// </param>
    /// <param name="options">The options to use for the string comparison.</param>
    /// <returns>
    /// If the separator was found, a <see cref="NullableReadOnlySpanPair{T, T}"/> containing the
    /// parts before and after the separator; otherwise, an empty <see cref="NullableReadOnlySpanPair{T, T}"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   When using the .Net Standard 2.0 or 2.1 version of this library, this method may allocate
    ///   strings to perform the comparison. This is because the <see cref="CompareInfo"/> class
    ///   does not provide string comparison methods for use with <see cref="ReadOnlySpan{T}"/>.
    /// </para>
    /// </remarks>
    public static NullableReadOnlySpanPair<char, char> SplitOnceLast(this ReadOnlySpan<char> span, ReadOnlySpan<char> separator,
        CultureInfo culture, CompareOptions options = CompareOptions.None)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        var index = culture.CompareInfo.LastIndexOf(span, separator, options);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a span of characters into two parts at the first occurrence of a separator.
    /// </summary>
    /// <param name="memory">The span of characters to split.</param>
    /// <param name="separator">A span of characters containing the separator to split on.</param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> to use to perform string comparison when finding the
    /// separator.
    /// </param>
    /// <param name="options">The options to use for the string comparison.</param>
    /// <returns>
    /// If the separator was found, a tuple containing the parts before and after the separator;
    /// otherwise, <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   When using the .Net Standard 2.0 or 2.1 version of this library, this method may allocate
    ///   strings to perform the comparison. This is because the <see cref="CompareInfo"/> class
    ///   does not provide string comparison methods for use with <see cref="ReadOnlySpan{T}"/>.
    /// </para>
    /// </remarks>
    public static (ReadOnlyMemory<char>, ReadOnlyMemory<char>)? SplitOnceLast(this ReadOnlyMemory<char> memory,
        ReadOnlySpan<char> separator, CultureInfo culture, CompareOptions options = CompareOptions.None)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        var index = culture.CompareInfo.LastIndexOf(memory.Span, separator, options);
        if (index < 0)
        {
            return default;
        }

        return memory.SplitAt(index, separator.Length);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into two parts at the last occurrence of any of the
    /// specified separators.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to split.</param>
    /// <param name="separators">The set of separators to split the span at.</param>
    /// <returns>
    /// If a separator was found, a tuple containing the parts before and after the separator;
    /// otherwise, <see langword="null"/>.
    /// </returns>
    public static NullableReadOnlySpanPair<T, T> SplitOnceLastAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separators)
        where T : IEquatable<T>
    {
        var index = span.LastIndexOfAny(separators);
        if (index < 0)
        {
            return default;
        }

        return span.SplitAt(index, 1);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into two parts at the last occurrence of any of the
    /// specified separators.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="memory">The <see cref="ReadOnlySpan{T}"/> to split.</param>
    /// <param name="separators">The set of separators to split the span at.</param>
    /// <returns>
    /// If a separator was found, a <see cref="NullableReadOnlySpanPair{T, T}"/> containing the
    /// parts before and after the separator; otherwise, an empty <see cref="NullableReadOnlySpanPair{T, T}"/>.
    /// </returns>
    public static (ReadOnlyMemory<T>, ReadOnlyMemory<T>)? SplitOnceLastAny<T>(this ReadOnlyMemory<T> memory, ReadOnlySpan<T> separators)
        where T : IEquatable<T>
    {
        var index = memory.Span.LastIndexOfAny(separators);
        if (index < 0)
        {
            return null;
        }

        return memory.SplitAt(index, 1);
    }

    /// <summary>
    /// Removes a prefix from a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to remove the prefix from.</param>
    /// <param name="prefix">The prefix to move.</param>
    /// <returns>
    /// If <paramref name="span"/> starts with <paramref name="prefix"/>, returns the span without
    /// the prefix; otherwise, returns an empty <see cref="NullableReadOnlySpan{T}"/>.
    /// </returns>
    public static NullableReadOnlySpan<T> StripPrefix<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> prefix)
        where T : IEquatable<T>
    {
        if (span.StartsWith(prefix))
        {
            return span.Slice(prefix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a prefix from a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/> to remove the prefix from.</param>
    /// <param name="prefix">The prefix to move.</param>
    /// <returns>
    /// If <paramref name="memory"/> starts with <paramref name="prefix"/>, returns the memory
    /// without the prefix; otherwise, returns <see langword="null"/>.
    /// </returns>
    public static ReadOnlyMemory<T>? StripPrefix<T>(this ReadOnlyMemory<T> memory, ReadOnlySpan<T> prefix)
        where T : IEquatable<T>
    {
        if (memory.Span.StartsWith(prefix))
        {
            return memory.Slice(prefix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a prefix from a span of characters.
    /// </summary>
    /// <param name="span">The span of characters to remove the prefix from.</param>
    /// <param name="prefix">The prefix to remove.</param>
    /// <param name="comparisonType">The type of string comparison to use to match the prefix.</param>
    /// <returns>
    /// If <paramref name="span"/> starts with <paramref name="prefix"/>, returns the span without
    /// the prefix; otherwise, returns an empty <see cref="NullableReadOnlySpan{T}"/>.
    /// </returns>
    public static NullableReadOnlySpan<char> StripPrefix(this ReadOnlySpan<char> span, ReadOnlySpan<char> prefix,
        StringComparison comparisonType)
    {
        if (span.StartsWith(prefix, comparisonType))
        {
            return span.Slice(prefix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a prefix from a span of characters.
    /// </summary>
    /// <param name="memory">The span of characters to remove the prefix from.</param>
    /// <param name="prefix">The prefix to remove.</param>
    /// <param name="comparisonType">The type of string comparison to use to match the prefix.</param>
    /// <returns>
    /// If <paramref name="memory"/> starts with <paramref name="prefix"/>, returns the memory
    /// without the prefix; otherwise, returns <see langword="null"/>.
    /// </returns>
    public static ReadOnlyMemory<char>? StripPrefix(this ReadOnlyMemory<char> memory, ReadOnlySpan<char> prefix,
        StringComparison comparisonType)
    {
        if (memory.Span.StartsWith(prefix, comparisonType))
        {
            return memory.Slice(prefix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a prefix from a span of characters.
    /// </summary>
    /// <param name="span">The span of characters to remove the prefix from.</param>
    /// <param name="prefix">The prefix to remove.</param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> to use to match the prefix.
    /// </param>
    /// <param name="options">The options to use for the string comparison.</param>
    /// <returns>
    /// If <paramref name="span"/> starts with <paramref name="prefix"/>, returns the span without
    /// the prefix; otherwise, returns an empty <see cref="NullableReadOnlySpan{T}"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   When using the .Net Standard 2.0 or 2.1 version of this library, this method may allocate
    ///   strings to perform the comparison. This is because the <see cref="CompareInfo"/> class
    ///   does not provide string comparison methods for use with <see cref="ReadOnlySpan{T}"/>.
    /// </para>
    /// </remarks>
    public static NullableReadOnlySpan<char> StripPrefix(this ReadOnlySpan<char> span, ReadOnlySpan<char> prefix,
        CultureInfo culture, CompareOptions options = CompareOptions.None)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        if (culture.CompareInfo.IsPrefix(span, prefix, options))
        {
            return span.Slice(prefix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a prefix from a span of characters.
    /// </summary>
    /// <param name="memory">The span of characters to remove the prefix from.</param>
    /// <param name="prefix">The prefix to remove.</param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> to use to match the prefix.
    /// </param>
    /// <param name="options">The options to use for the string comparison.</param>
    /// <returns>
    /// If <paramref name="memory"/> starts with <paramref name="prefix"/>, returns the memory
    /// without the prefix; otherwise, returns <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   When using the .Net Standard 2.0 or 2.1 version of this library, this method may allocate
    ///   strings to perform the comparison. This is because the <see cref="CompareInfo"/> class
    ///   does not provide string comparison methods for use with <see cref="ReadOnlySpan{T}"/>.
    /// </para>
    /// </remarks>
    public static ReadOnlyMemory<char>? StripPrefix(this ReadOnlyMemory<char> memory, ReadOnlySpan<char> prefix,
        CultureInfo culture, CompareOptions options = CompareOptions.None)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        if (culture.CompareInfo.IsPrefix(memory.Span, prefix, options))
        {
            return memory.Slice(prefix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a suffix from a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to remove the suffix from.</param>
    /// <param name="suffix">The suffix to move.</param>
    /// <returns>
    /// If <paramref name="span"/> starts with <paramref name="suffix"/>, returns the span without
    /// the suffix; otherwise, returns an empty <see cref="NullableReadOnlySpan{T}"/>.
    /// </returns>
    public static NullableReadOnlySpan<T> StripSuffix<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> suffix)
        where T : IEquatable<T>
    {
        if (span.EndsWith(suffix))
        {
            return span.Slice(0, span.Length - suffix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a suffix from a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/> to remove the prefix from.</param>
    /// <param name="suffix">The prefix to move.</param>
    /// <returns>
    /// If <paramref name="memory"/> starts with <paramref name="suffix"/>, returns the memory
    /// without the suffix; otherwise, returns <see langword="null"/>.
    /// </returns>
    public static ReadOnlyMemory<T>? StripSuffix<T>(this ReadOnlyMemory<T> memory, ReadOnlySpan<T> suffix)
        where T : IEquatable<T>
    {
        if (memory.Span.EndsWith(suffix))
        {
            return memory.Slice(0, memory.Length - suffix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a suffix from a span of characters.
    /// </summary>
    /// <param name="span">The span of characters to remove the suffix from.</param>
    /// <param name="suffix">The suffix to remove.</param>
    /// <param name="comparisonType">The type of string comparison to use to match the suffix.</param>
    /// <returns>
    /// If <paramref name="span"/> starts with <paramref name="suffix"/>, returns the span without
    /// the suffix; otherwise, returns an empty <see cref="NullableReadOnlySpan{T}"/>.
    /// </returns>
    public static NullableReadOnlySpan<char> StripSuffix(this ReadOnlySpan<char> span, ReadOnlySpan<char> suffix,
        StringComparison comparisonType)
    {
        if (span.EndsWith(suffix, comparisonType))
        {
            return span.Slice(0, span.Length - suffix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a suffix from a span of characters.
    /// </summary>
    /// <param name="memory">The span of characters to remove the suffix from.</param>
    /// <param name="suffix">The suffix to remove.</param>
    /// <param name="comparisonType">The type of string comparison to use to match the suffix.</param>
    /// <returns>
    /// If <paramref name="memory"/> starts with <paramref name="suffix"/>, returns the memory
    /// without the suffix; otherwise, returns <see langword="null"/>.
    /// </returns>
    public static ReadOnlyMemory<char>? StripSuffix(this ReadOnlyMemory<char> memory, ReadOnlySpan<char> suffix,
        StringComparison comparisonType)
    {
        if (memory.Span.EndsWith(suffix, comparisonType))
        {
            return memory.Slice(0, memory.Length - suffix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a suffix from a span of characters.
    /// </summary>
    /// <param name="span">The span of characters to remove the suffix from.</param>
    /// <param name="suffix">The suffix to remove.</param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> to use to match the suffix.
    /// </param>
    /// <param name="options">The options to use for the string comparison.</param>
    /// <returns>
    /// If <paramref name="span"/> starts with <paramref name="suffix"/>, returns the span without
    /// the suffix; otherwise, returns an empty <see cref="NullableReadOnlySpan{T}"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   When using the .Net Standard 2.0 or 2.1 version of this library, this method may allocate
    ///   strings to perform the comparison. This is because the <see cref="CompareInfo"/> class
    ///   does not provide string comparison methods for use with <see cref="ReadOnlySpan{T}"/>.
    /// </para>
    /// </remarks>
    public static NullableReadOnlySpan<char> StripSuffix(this ReadOnlySpan<char> span, ReadOnlySpan<char> suffix,
        CultureInfo culture, CompareOptions options = CompareOptions.None)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        if (culture.CompareInfo.IsSuffix(span, suffix, options))
        {
            return span.Slice(0, span.Length - suffix.Length);
        }

        return default;
    }

    /// <summary>
    /// Removes a suffix from a span of characters.
    /// </summary>
    /// <param name="memory">The span of characters to remove the suffix from.</param>
    /// <param name="suffix">The suffix to remove.</param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> to use to match the suffix.
    /// </param>
    /// <param name="options">The options to use for the string comparison.</param>
    /// <returns>
    /// If <paramref name="memory"/> starts with <paramref name="suffix"/>, returns the memory
    /// without the suffix; otherwise, returns <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   When using the .Net Standard 2.0 or 2.1 version of this library, this method may allocate
    ///   strings to perform the comparison. This is because the <see cref="CompareInfo"/> class
    ///   does not provide string comparison methods for use with <see cref="ReadOnlySpan{T}"/>.
    /// </para>
    /// </remarks>
    public static ReadOnlyMemory<char>? StripSuffix(this ReadOnlyMemory<char> memory, ReadOnlySpan<char> suffix,
        CultureInfo culture, CompareOptions options = CompareOptions.None)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        if (culture.CompareInfo.IsSuffix(memory.Span, suffix, options))
        {
            return memory.Slice(0, memory.Length - suffix.Length);
        }

        return default;
    }


#if !NET5_0_OR_GREATER

    // LastIndexOf with StringComparison is not available in .NET Standard 2.x
    internal static int LastIndexOf(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
    {
        int lastIndex = -1;
        while (true)
        {
            int index = span.IndexOf(value, comparisonType);
            if (index < 0)
            {
                return lastIndex;
            }

            lastIndex += index + 1;
            span = span.Slice(index + 1);
        }
    }

#endif

}
