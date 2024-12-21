using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ookii.Common;

/// <summary>
/// Provides helper methods for the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> structure.
/// </summary>
/// <threadsafety static="true" instance="false"/>
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
        => new(first, second);
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
/// <threadsafety static="true" instance="false"/>
public readonly ref struct ReadOnlySpanPair<TFirst, TSecond>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>
    /// structure.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpanPair(ReadOnlySpan<TFirst> first, ReadOnlySpan<TSecond> second)
    {
        Item1 = first;
        Item2 = second;
    }

    /// <summary>
    /// Gets the first value of the pair.
    /// </summary>
    /// <value>
    /// The first <see cref="ReadOnlySpan{T}"/> in the pair.
    /// </value>
    public readonly ReadOnlySpan<TFirst> Item1
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    /// <summary>
    /// Gets the second value of the pair.
    /// </summary>
    /// <value>
    /// The second <see cref="ReadOnlySpan{T}"/> in the pair.
    /// </value>
    public readonly ReadOnlySpan<TSecond> Item2
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    /// <summary>
    /// Deconstructs the pair into two <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <param name="first">Receives the first value.</param>
    /// <param name="second">Receives the second value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out ReadOnlySpan<TFirst> first, out ReadOnlySpan<TSecond> second)
    {
        first = Item1;
        second = Item2;
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </summary>
    /// <returns>A string containing the two values in the format "(first, second)".</returns>
    override public string ToString() => $"({Item1.ToString()}, {Item2.ToString()})";

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
    [EditorBrowsable(EditorBrowsableState.Never)]
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
    [EditorBrowsable(EditorBrowsableState.Never)]
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
        => left.Item1 == right.Item1 && left.Item2 == right.Item2;

    /// <summary>
    /// Compares two <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> instances for inequality.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the values are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(ReadOnlySpanPair<TFirst, TSecond> left, ReadOnlySpanPair<TFirst, TSecond> right)
        => left.Item1 != right.Item1 || left.Item2 != right.Item2;

}
