using System;

namespace Ookii.Common;

/// <summary>
/// Provides helper methods for the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> structure.
/// </summary>
public static class ReadOnlySpanPair
{
    /// <summary>
    /// Creates a new <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> instance.
    /// </summary>
    /// <typeparam name="TFirst">
    /// The type of the items in the first value of the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </typeparam>
    /// <typeparam name="TSecond">
    /// The type of the items in the second value of the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </typeparam>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <returns>A <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> containing the value.</returns>
    public static ReadOnlySpanPair<TFirst, TSecond> Create<TFirst, TSecond>(ReadOnlySpan<TFirst> first, ReadOnlySpan<TSecond> second) 
        => new (first, second);
}

/// <summary>
/// Represents a pair of <see cref="ReadOnlySpan{T}"/> instances.
/// </summary>
/// <typeparam name="TFirst">
/// The type of the items in the first <see cref="ReadOnlySpan{T}"/>.
/// </typeparam>
/// <typeparam name="TSecond">
/// The type of the items in the second <see cref="ReadOnlySpan{T}"/>.
/// </typeparam>
public readonly ref struct ReadOnlySpanPair<TFirst, TSecond>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>
    /// structure.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    public ReadOnlySpanPair(ReadOnlySpan<TFirst> first, ReadOnlySpan<TSecond> second)
    {
        First = first;
        Second = second;
    }

    /// <summary>
    /// Gets the first value of the pair.
    /// </summary>
    /// <value>
    /// The first <see cref="ReadOnlySpan{T}"/> in the pair.
    /// </value>
    public readonly ReadOnlySpan<TFirst> First { get; }

    /// <summary>
    /// Gets the second value of the pair.
    /// </summary>
    /// <value>
    /// The second <see cref="ReadOnlySpan{T}"/> in the pair.
    /// </value>
    public readonly ReadOnlySpan<TSecond> Second { get; }

    /// <summary>
    /// Deconstructs the pair into two <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <param name="first">Receives the first value.</param>
    /// <param name="second">Receives the second value.</param>
    public void Deconstruct(out ReadOnlySpan<TFirst> first, out ReadOnlySpan<TSecond> second)
    {
        first = First;
        second = Second;
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </summary>
    /// <returns>A string containing the two values in the format "(first, second)".</returns>
    override public string ToString() => $"({First.ToString()}, {Second.ToString()})";
}
