﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ookii.Common;

public static partial class MemoryExtensions
{
    /// <summary>
    /// Enumerator used by the <see cref="Split(ReadOnlyMemory{char}, ReadOnlySpan{char}, StringSplitOptions)"/> method.
    /// </summary>
    public ref struct SplitMemoryEnumerator
    {
        private ReadOnlyMemory<char>? _remaining;
        private ReadOnlyMemory<char> _current = default;
        private readonly ReadOnlySpan<char> _separator;
        private readonly StringSplitOptions _options;

        internal SplitMemoryEnumerator(ReadOnlyMemory<char> value, ReadOnlySpan<char> separator, StringSplitOptions options)
        {
            _remaining = value;
            _separator = separator;
            _options = options;
        }

        /// <summary>
        /// Gets the current memory region segment in the enumeration.
        /// </summary>
        /// <value>
        /// The current segment, or an undefined value if <see cref="MoveNext"/> was not yet
        /// called or returned <see langword="false"/>.
        /// </value>
        public readonly ReadOnlyMemory<char> Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _current;
        }

        /// <summary>
        /// Advances the enumerator to the next segment in the memory region.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the enumerator was successfully advanced to the next segment;
        /// <see langword="false"/> if the enumerator has passed the end of the memory region.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            do
            {
                if (_remaining is not ReadOnlyMemory<char> remaining)
                {
                    return false;
                }

                var index = remaining.Span.IndexOf(_separator);
                if (index == -1)
                {
                    _current = remaining;
                    _remaining = default;
                }
                else
                {
                    (_current, var newRemaining) = remaining.SplitAt(index, _separator.Length);
                    _remaining = newRemaining;
                }

#if NET6_0_OR_GREATER
                if (_options.HasFlag(StringSplitOptions.TrimEntries))
                {
                    _current = _current.Trim();
                }
#endif

            } while (_current.IsEmpty && _options.HasFlag(StringSplitOptions.RemoveEmptyEntries));

            return true;
        }
    }

    /// <summary>
    /// Enumerable used by the <see cref="Split(ReadOnlyMemory{char}, ReadOnlySpan{char}, StringSplitOptions)"/> method.
    /// </summary>
    public readonly ref struct SplitMemoryEnumerable
    {
        private readonly ReadOnlyMemory<char> _value;
        private readonly ReadOnlySpan<char> _separator;
        private readonly StringSplitOptions _options;

        internal SplitMemoryEnumerable(ReadOnlyMemory<char> value, ReadOnlySpan<char> separator, StringSplitOptions options)
        {
            _value = value;
            _separator = separator;
            _options = options;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the split segments of the memory region.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the segments.</returns>
        public readonly SplitMemoryEnumerator GetEnumerator() => new(_value, _separator, _options);
    }

    /// <summary>
    /// Splits a string memory region into segments using the specified separator.
    /// </summary>
    /// <param name="value">The memory region to split.</param>
    /// <param name="separator">The separator that delimits the span segments.</param>
    /// <param name="options">
    /// A bitwise combination of the enumeration values that specifies whether to trim substrings
    /// and include empty substrings.
    /// </param>
    /// <returns>
    /// An object that can be used to enumerate the split segments.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   This method is similar to <see cref="string.Split(string, StringSplitOptions)"/>, but
    ///   operates on memory regions. It allows you to split a memory region into segments, and
    ///   iterate the segments using a <c>foreach</c> loop, without allocating any memory.
    /// </para>
    /// <para>
    ///   Because the returned object is a <c>ref struct</c>, it implements the enumerable pattern,
    ///   but does not implement the <see cref="IEnumerable{T}"/> interface. This means that while
    ///   it can be used with <c>foreach</c>, it cannot be used with LINQ methods or other places
    ///   that expect an <see cref="IEnumerable{T}"/>.
    /// </para>
    /// </remarks>
    public static SplitMemoryEnumerable Split(this ReadOnlyMemory<char> value, ReadOnlySpan<char> separator, StringSplitOptions options = StringSplitOptions.None)
    {
#if NET6_0_OR_GREATER
        const StringSplitOptions validOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
#else
        const StringSplitOptions validOptions = StringSplitOptions.RemoveEmptyEntries;
#endif

        if ((options & ~validOptions) != 0)
        {
            throw new ArgumentException(Properties.Resources.InvalidSplitFlags, nameof(options));
        }

        return new(value, separator, options);
    }
}
