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

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

    /// <summary>
    /// <note type="caution">
    /// This method always throws an exception. Use the equality operator
    /// instead.
    /// </note>
    /// Not supported. Throws a <see cref="NotSupportedException"/>.
    /// </summary>
    /// <param name="obj">Not supported.</param>
    /// <returns>Not supported.</returns>
    /// <exception cref="NotSupportedException">
    /// Always thrown by this method.
    /// </exception>
    [Obsolete("Equals() should not be used on ReadOnlySpanPair<TFirst, TSecond>.", true)]
    public override bool Equals(object? obj) => throw new NotSupportedException();

    /// <summary>
    /// <note type="caution">
    /// This method always throws an exception.
    /// </note>
    /// Not supported. Throws a <see cref="NotSupportedException"/>.
    /// </summary>
    /// <returns>Not supported.</returns>
    /// <exception cref="NotSupportedException">
    /// Always thrown by this method.
    /// </exception>
    [Obsolete("GetHashCode() should not be used on ReadOnlySpanPair<TFirst, TSecond>.", true)]
    public override int GetHashCode() => throw new NotSupportedException();

#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

    /// <summary>
    /// Compares two <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> instances for equality.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the values are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(ReadOnlySpanPair<TFirst, TSecond> left, ReadOnlySpanPair<TFirst, TSecond> right)
        => left.First == right.First && left.Second == right.Second;

    /// <summary>
    /// Compares two <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> instances for inequality.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the values are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(ReadOnlySpanPair<TFirst, TSecond> left, ReadOnlySpanPair<TFirst, TSecond> right)
        => left.First != right.First || left.Second != right.Second;

}
