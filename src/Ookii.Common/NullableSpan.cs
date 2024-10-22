using System;

namespace Ookii.Common;

/// <summary>
/// Represents a <see cref="Span{T}"/> that may or may not have a value.
/// </summary>
/// <typeparam name="T">The type of the items in the <see cref="Span{T}"/>.</typeparam>
public readonly ref struct NullableSpan<T>
{
    /// <summary>
    /// A function that maps a <see cref="Span{T}"/> to another <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the items in the resulting <see cref="Span{T}"/>.</typeparam>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public delegate ReadOnlySpan<TResult> MapReadOnlyFunc<TResult>(Span<T> value);

    /// <summary>
    /// A function that maps a <see cref="Span{T}"/> to another <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the items in the resulting <see cref="Span{T}"/>.</typeparam>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public delegate Span<TResult> MapSpanFunc<TResult>(Span<T> value);

    /// <summary>
    /// A function that maps a <see cref="Span{T}"/> to a <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the resulting value.</typeparam>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public delegate TResult MapStructFunc<TResult>(Span<T> value)
        where TResult: struct;

    /// <summary>
    /// A function that maps a <see cref="Span{T}"/> to a <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the resulting value.</typeparam>
    /// <param name="value">The value to map.</param>
    /// <returns>The mapped value.</returns>
    public delegate TResult MapClassFunc<TResult>(Span<T> value)
        where TResult : class;

    private readonly Span<T> _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="NullableSpan{T}"/> structure.
    /// </summary>
    /// <param name="value">The value that the structure will contain.</param>
    public NullableSpan(Span<T> value)
    {
        _value = value;
        HasValue = true;
    }

    /// <summary>
    /// Gets the contained value.
    /// </summary>
    /// <value>
    /// The contained <see cref="Span{T}"/>.
    /// </value>
    /// <exception cref="InvalidOperationException">
    /// <see cref="HasValue"/> is <see langword="false"/>.
    /// </exception>
    public Span<T> Value => HasValue ? _value : throw new InvalidOperationException();

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
    /// <see cref="Span{T}"/>.
    /// </param>
    /// <returns>
    /// The value of the <see cref="HasValue"/> property.
    /// </returns>
    public bool TryGetValue(out Span<T> value)
    {
        value = _value;
        return HasValue;
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="NullableSpan{T}"/>.
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
    /// The value to return if this <see cref="NullableSpan{T}"/> is empty.
    /// </param>
    /// <returns>
    /// If the <see cref="HasValue"/> property is <see langword="true"/>, the <see cref="Value"/>
    /// property; otherwise, <paramref name="defaultValue"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   This method provides an alternative to using the <c>??</c> operator, which does not work
    ///   with the <see cref="NullableSpan{T}"/> structure.
    /// </para>
    /// <para>
    ///   If the value used for <paramref name="defaultValue"/> is expensive to construct, consider
    ///   using the <see cref="GetValueOrDefault(Span{T})"/> overload instead.
    /// </para>
    /// </remarks>
    public Span<T> GetValueOrDefault(Span<T> defaultValue = default) => HasValue ? _value : defaultValue;

    /// <summary>
    /// Returns the contained value, or the value returned by the specified function if there is no value.
    /// </summary>
    /// <param name="defaultValueFunc">
    /// The function that produces a value if this <see cref="NullableSpan{T}"/> is empty.
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
    public Span<T> GetValueOrElse(SpanFunc<T> defaultValueFunc) => HasValue ? _value : defaultValueFunc();

    /// <summary>
    /// Maps a <see cref="NullableSpan{T}"/> to another value by applying a function to the
    /// contained value, or returns an empty value if there is no value.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the items in the resulting <see cref="NullableReadOnlySpan{T}"/>.
    /// </typeparam>
    /// <param name="mapFunc">The function to apply to the contained value.</param>
    /// <returns>
    /// If the <see cref="HasValue"/> property is <see langword="true"/>, the result of applying
    /// <paramref name="mapFunc"/> to the <see cref="Value"/> property; otherwise, an empty
    /// <see cref="NullableSpan{T}"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   The function <paramref name="mapFunc"/> is only called if the <see cref="HasValue"/>
    ///   property is <see langword="true"/>.
    /// </para>
    /// </remarks>
    public NullableReadOnlySpan<TResult> Map<TResult>(MapReadOnlyFunc<TResult> mapFunc)
        => HasValue ? new NullableReadOnlySpan<TResult>(mapFunc(Value)) : default;

    /// <summary>
    /// Maps a <see cref="NullableSpan{T}"/> to another value by applying a function to the
    /// contained value, or returns an empty value if there is no value.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the items in the resulting <see cref="NullableSpan{T}"/>.
    /// </typeparam>
    /// <param name="mapFunc">The function to apply to the contained value.</param>
    /// <returns>
    /// If the <see cref="HasValue"/> property is <see langword="true"/>, the result of applying
    /// <paramref name="mapFunc"/> to the <see cref="Value"/> property; otherwise, an empty
    /// <see cref="NullableSpan{T}"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   The function <paramref name="mapFunc"/> is only called if the <see cref="HasValue"/>
    ///   property is <see langword="true"/>.
    /// </para>
    /// </remarks>
    public NullableSpan<TResult> Map<TResult>(MapSpanFunc<TResult> mapFunc) => HasValue ? new NullableSpan<TResult>(mapFunc(Value)) : default;

    /// <summary>
    /// Maps a <see cref="NullableSpan{T}"/> to another value by applying a function to the
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
    [Obsolete("Equals() should not be used on NullableSpan<T>.", true)]
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
    [Obsolete("GetHashCode() should not be used on NullableSpan<T>.", true)]
    public override int GetHashCode() => throw new NotSupportedException();

#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

    /// <summary>
    /// Implicitly converts a <see cref="Span{T}"/> to a <see cref="NullableSpan{T}"/>.
    /// </summary>
    /// <param name="value">The value that the <see cref="NullableSpan{T}"/> will contain.</param>
    /// <returns>A <see cref="NullableSpan{T}"/> that contains the specified value.</returns> 
    public static implicit operator NullableSpan<T>(Span<T> value) => new(value);

    /// <summary>
    /// Explicitly converts a <see cref="NullableSpan{T}"/> to a <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="value">The <see cref="NullableSpan{T}"/> to convert.</param>
    /// <returns>The value of the <see cref="Value"/> property.</returns>
    /// <exception cref="InvalidOperationException">
    /// <see cref="HasValue"/> is <see langword="false"/>.
    /// </exception>
    public static explicit operator Span<T>(NullableSpan<T> value) => value.Value;

    /// <summary>
    /// Compares two <see cref="NullableReadOnlySpan{T}"/> instances for equality.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the values are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(NullableSpan<T> left, NullableSpan<T> right)
        => left.HasValue ? right.HasValue && left._value == right._value : !right.HasValue;

    /// <summary>
    /// Compares two <see cref="NullableReadOnlySpan{T}"/> instances for inequality.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the values are not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(NullableSpan<T> left, NullableSpan<T> right)
        => left.HasValue ? !right.HasValue || left._value != right._value : right.HasValue;
}
