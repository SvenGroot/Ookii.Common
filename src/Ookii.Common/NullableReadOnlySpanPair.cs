using System;

namespace Ookii.Common;

/// <summary>
/// Provides helper methods for <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
/// </summary>
public static class NullableReadOnlySpanPair
{
    /// <summary>
    /// Creates a new <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> from the specified values.
    /// </summary>
    /// <typeparam name="TFirst">
    /// The type of the items in the first value of the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </typeparam>
    /// <typeparam name="TSecond">
    /// The type of the items in the second value of the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </typeparam>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <returns>A <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> containing the value.</returns>
    public static NullableReadOnlySpanPair<TFirst, TSecond> Create<TFirst, TSecond>(ReadOnlySpan<TFirst> first, ReadOnlySpan<TSecond> second)
        => new(first, second);
}

/// <summary>
/// Represents a <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> that may or may not have a value.
/// </summary>
/// <typeparam name="TFirst">
/// The type of the items in the first value of the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
/// </typeparam>
/// <typeparam name="TSecond">
/// The type of the items in the second value of the <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
/// </typeparam>
public readonly ref struct NullableReadOnlySpanPair<TFirst, TSecond>
{
    /// <summary>
    /// A function that maps a <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> to another <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </summary>
    /// <typeparam name="TResultFirst">
    /// The type of the items in the first value of the resulting
    /// <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </typeparam>
    /// <typeparam name="TResultSecond">
    /// The type of the items in the second value of the resulting
    /// <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </typeparam>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public delegate ReadOnlySpanPair<TResultFirst, TResultSecond> MapReadOnlyFunc<TResultFirst, TResultSecond>(ReadOnlySpanPair<TFirst, TSecond> value);

    /// <summary>
    /// A function that maps a <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> to a <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the resulting value.</typeparam>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public delegate TResult MapStructFunc<TResult>(ReadOnlySpanPair<TFirst, TSecond> value)
        where TResult: struct;

    /// <summary>
    /// A function that maps a <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> to a <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the resulting value.</typeparam>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public delegate TResult MapClassFunc<TResult>(ReadOnlySpanPair<TFirst, TSecond> value)
        where TResult : class;

    private readonly ReadOnlySpanPair<TFirst, TSecond> _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>
    /// structure.
    /// </summary>
    /// <param name="value">The value that the structure will contain.</param>
    public NullableReadOnlySpanPair(ReadOnlySpanPair<TFirst, TSecond> value)
    {
        _value = value;
        HasValue = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>
    /// structure using the specified values.
    /// </summary>
    /// <param name="first">The first value of the pair.</param>
    /// <param name="second">The second value of the pair.</param>
    public NullableReadOnlySpanPair(ReadOnlySpan<TFirst> first, ReadOnlySpan<TSecond> second)
        : this(new ReadOnlySpanPair<TFirst, TSecond>(first, second))
    {
    }

    /// <summary>
    /// Gets the contained value.
    /// </summary>
    /// <value>
    /// The contained <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </value>
    /// <exception cref="InvalidOperationException">
    /// <see cref="HasValue"/> is <see langword="false"/>.
    /// </exception>
    public ReadOnlySpanPair<TFirst, TSecond> Value 
        => HasValue ? _value : throw new InvalidOperationException(Properties.Resources.EmptyNullableReadOnlySpanPair);

    /// <summary>
    /// Gets a value that indicates whether this instance contains a value.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if this instance contains a value; otherwise,
    /// <see langword="false"/>.
    /// </value>
    public bool HasValue { get; }

    /// <summary>
    /// Gets the contained value.
    /// </summary>
    /// <param name="value">
    /// When this method returns, if the <see cref="HasValue"/> property is <see langword="true"/>,
    /// contains the value of the <see cref="Value"/> property; otherwise, an empty
    /// <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </param>
    /// <returns>
    /// The value of the <see cref="HasValue"/> property.
    /// </returns>
    public bool TryGetValue(out ReadOnlySpanPair<TFirst, TSecond> value)
    {
        value = _value;
        return HasValue;
    }

    /// <summary>
    /// Gets the contained value.
    /// </summary>
    /// <param name="first">
    /// When this method returns, if the <see cref="HasValue"/> property is <see langword="true"/>,
    /// contains the first value of the <see cref="Value"/> property; otherwise, an empty
    /// <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </param>
    /// <param name="second">
    /// When this method returns, if the <see cref="HasValue"/> property is <see langword="true"/>,
    /// contains the second value of the <see cref="Value"/> property; otherwise, an empty
    /// <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </param>
    /// <returns>
    /// The value of the <see cref="HasValue"/> property.
    /// </returns>
    public bool TryGetValue(out ReadOnlySpan<TFirst> first, out ReadOnlySpan<TSecond> second)
    {
        first = _value.Item1;
        second = _value.Item2;
        return HasValue;
    }

    /// <summary>
    /// Converts a <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> to a pair of 
    /// <see cref="NullableReadOnlySpan{T}"/>. values.
    /// </summary>
    /// <param name="first">
    /// If <see cref="HasValue"/> is <see langword="true"/>, receives the first value, otherwise
    /// an empty <see cref="NullableReadOnlySpan{T}"/>.
    /// </param>
    /// <param name="second">
    /// If <see cref="HasValue"/> is <see langword="true"/>, receives the second value, otherwise
    /// an empty <see cref="NullableReadOnlySpan{T}"/>.
    /// </param>
    public void Unzip(out NullableReadOnlySpan<TFirst> first, out NullableReadOnlySpan<TSecond> second)
    {
        if (HasValue)
        {
            first = _value.Item1;
            second = _value.Item2;
        }
        else
        {
            first = default;
            second = default;
        }
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </summary>
    /// <returns>
    /// If <see cref="HasValue"/> is <see langword="true"/>, a string representation of the
    /// <see cref="Value"/> property; otherwise, an empty string ("").
    /// </returns>
    public override string ToString() => HasValue ? _value.ToString() : string.Empty;

    /// <summary>
    /// Returns the contained value, or the specified default value if there is no value.
    /// </summary>
    /// <param name="defaultValue">
    /// The value to return if this <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> is empty.
    /// </param>
    /// <returns>
    /// If the <see cref="HasValue"/> property is <see langword="true"/>, the <see cref="Value"/>
    /// property; otherwise, <paramref name="defaultValue"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   This method provides an alternative to using the <c>??</c> operator, which does not work
    ///   with the <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> structure.
    /// </para>
    /// <para>
    ///   If the value used for <paramref name="defaultValue"/> is expensive to construct, consider
    ///   using the <see cref="GetValueOrElse(ReadOnlySpanPairFunc{TFirst, TSecond})"/> overload
    ///   instead.
    /// </para>
    /// </remarks>
    public ReadOnlySpanPair<TFirst, TSecond> GetValueOrDefault(ReadOnlySpanPair<TFirst, TSecond> defaultValue = default)
        => HasValue ? _value : defaultValue;

    /// <summary>
    /// Returns the contained value, or the value returned by the specified function if there is no value.
    /// </summary>
    /// <param name="defaultValueFunc">
    /// The function that produces a value if this <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> is empty.
    /// </param>
    /// <returns>
    /// If the <see cref="HasValue"/> property is <see langword="true"/>, the <see cref="Value"/>
    /// property; otherwise, the value returned by <paramref name="defaultValueFunc"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   The function <paramref name="defaultValueFunc"/> is only called if the <see cref="HasValue"/>
    ///   property is <see langword="false"/>.
    /// </para>
    /// </remarks>
    public ReadOnlySpanPair<TFirst, TSecond> GetValueOrElse(ReadOnlySpanPairFunc<TFirst, TSecond> defaultValueFunc)
        => HasValue ? _value : defaultValueFunc();

    /// <summary>
    /// Maps a <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> to another value by applying a function to the
    /// contained value, or returns an empty value if there is no value.
    /// </summary>
    /// <typeparam name="TResultFirst">
    /// The type of the items in the first value of the resulting
    /// <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </typeparam>
    /// <typeparam name="TResultSecond">
    /// The type of the items in the second value of the resulting
    /// <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </typeparam>
    /// <param name="mapFunc">The function to apply to the contained value.</param>
    /// <returns>
    /// If the <see cref="HasValue"/> property is <see langword="true"/>, the result of applying
    /// <paramref name="mapFunc"/> to the <see cref="Value"/> property; otherwise, an empty
    /// <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   The function <paramref name="mapFunc"/> is only called if the <see cref="HasValue"/>
    ///   property is <see langword="true"/>.
    /// </para>
    /// </remarks>
    public NullableReadOnlySpanPair<TResultFirst, TResultSecond> Map<TResultFirst, TResultSecond>(MapReadOnlyFunc<TResultFirst, TResultSecond> mapFunc)
        => HasValue ? new NullableReadOnlySpanPair<TResultFirst, TResultSecond>(mapFunc(Value)) : default;

    /// <summary>
    /// Maps a <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> to another value by applying a function to the
    /// contained value, or returns an empty value if there is no value.
    /// </summary>
    /// <typeparam name="TResult">
    /// The result type of the map operation.
    /// </typeparam>
    /// <param name="mapFunc">The function to apply to the contained value.</param>
    /// <returns>
    /// If the <see cref="HasValue"/> property is <see langword="true"/>, the result of applying
    /// <paramref name="mapFunc"/> to the <see cref="Value"/> property; otherwise,
    /// <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   The function <paramref name="mapFunc"/> is only called if the <see cref="HasValue"/>
    ///   property is <see langword="true"/>.
    /// </para>
    /// </remarks>
    public TResult? Map<TResult>(MapStructFunc<TResult> mapFunc)
        where TResult : struct
        => HasValue ? mapFunc(Value) : null;

    /// <inheritdoc cref="Map{TResult}(MapStructFunc{TResult})"/>
    public TResult? Map<TResult>(MapClassFunc<TResult> mapFunc)
        where TResult : class
        => HasValue ? mapFunc(Value) : null;

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
    [Obsolete("Equals() should not be used on NullableReadOnlySpanPair<TFirst, TSecond>.", true)]
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
    [Obsolete("GetHashCode() should not be used on NullableReadOnlySpanPair<TFirst, TSecond>.", true)]
    public override int GetHashCode() => throw new NotSupportedException();

#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

    /// <summary>
    /// Implicitly converts a <see cref="ReadOnlySpanPair{TFirst, TSecond}"/> to a <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </summary>
    /// <param name="value">The value that the <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> will contain.</param>
    /// <returns>A <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> that contains the specified value.</returns> 
    public static implicit operator NullableReadOnlySpanPair<TFirst, TSecond>(ReadOnlySpanPair<TFirst, TSecond> value) => new(value);

    /// <summary>
    /// Explicitly converts a <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> to a <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
    /// </summary>
    /// <param name="value">The <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> to convert.</param>
    /// <returns>The value of the <see cref="Value"/> property.</returns>
    /// <exception cref="InvalidOperationException">
    /// <see cref="HasValue"/> is <see langword="false"/>.
    /// </exception>
    public static explicit operator ReadOnlySpanPair<TFirst, TSecond>(NullableReadOnlySpanPair<TFirst, TSecond> value) => value.Value;

    /// <summary>
    /// Compares two <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> instances for equality.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the values are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(NullableReadOnlySpanPair<TFirst, TSecond> left, NullableReadOnlySpanPair<TFirst, TSecond> right)
        => left.HasValue ? right.HasValue && left._value == right._value : !right.HasValue;

    /// <summary>
    /// Compares two <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/> instances for inequality.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the values are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(NullableReadOnlySpanPair<TFirst, TSecond> left, NullableReadOnlySpanPair<TFirst, TSecond> right)
        => left.HasValue ? !right.HasValue || left._value != right._value : right.HasValue;
}
