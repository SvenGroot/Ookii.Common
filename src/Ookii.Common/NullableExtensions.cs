using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ookii.Common;

/// <summary>
/// Provides extension methods for the <see cref="Nullable{T}"/> structure, and for nullable
/// reference types.
/// </summary>
public static class NullableExtensions
{
    /// <summary>
    /// Encapsulates a method that maps a value type to a reference type.
    /// </summary>
    /// <typeparam name="T">The type of the value type.</typeparam>
    /// <typeparam name="TResult">The type of the resulting reference type.</typeparam>
    /// <returns>
    /// The return value of the method that this delegate encapsulates.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   This delegate exists because the <see cref="Map{T, TResult}(T?, StructToClassFunc{T, TResult})"/>
    ///   method must have different argument types than the <see cref="Map{T, TResult}(T?, Func{T, TResult})"/>
    ///   method.
    /// </para>
    /// </remarks>
    public delegate TResult StructToClassFunc<T, TResult>(T value)
        where T : struct
        where TResult : class;

    /// <summary>
    /// Encapsulates a method that maps a reference type to a value type.
    /// </summary>
    /// <typeparam name="T">The type of the reference type.</typeparam>
    /// <typeparam name="TResult">The type of the resulting value type.</typeparam>
    /// <returns>
    /// The return value of the method that this delegate encapsulates.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   This delegate exists because the <see cref="Map{T, TResult}(T, ClassToStructFunc{T, TResult})"/>
    ///   method must have different argument types than the <see cref="Map{T, TResult}(T?, Func{T, TResult})"/>
    ///   method.
    /// </para>
    /// </remarks>
    public delegate TResult ClassToStructFunc<T, TResult>(T value)
        where T : class
        where TResult : struct;

    /// <summary>
    /// Applies a mapping function a value that may be <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="self">The value to map. May be <see langword="null"/>.</param>
    /// <param name="map">The function to apply to the value.</param>
    /// <returns>
    /// If <paramref name="self"/> is not <see langword="null"/>, the result of applying
    /// <paramref name="map"/> to <paramref name="self"/>; otherwise, <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   The <paramref name="map"/> function is only called if <paramref name="self"/> is not
    ///   <see langword="null"/>.
    /// </para>
    /// </remarks>
    public static TResult? Map<T, TResult>(this T? self, Func<T, TResult> map)
        where T : struct
        where TResult : struct
        => self is T value ? map(value) : null;

    /// <inheritdoc cref = "Map{T, TResult}(T?, Func{T, TResult})" />
    public static TResult? Map<T, TResult>(this T? self, StructToClassFunc<T, TResult> map)
        where T : struct
        where TResult : class
        => self is T value ? map(value) : null;

    /// <inheritdoc cref = "Map{T, TResult}(T?, Func{T, TResult})" />
    public static TResult? Map<T, TResult>(this T? self, ClassToStructFunc<T, TResult> map)
        where T : class
        where TResult : struct
        => self is T value ? map(value) : null;

    /// <inheritdoc cref = "Map{T, TResult}(T?, Func{T, TResult})" />
    public static TResult? Map<T, TResult>(this T? self, Func<T, TResult> map)
        where T : class
        where TResult : class
        => self is T value ? map(value) : null;

    /// <summary>
    /// Applies a mapping function a value that may be <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TResult">
    /// The type of the items in the resulting <see cref="NullableReadOnlySpan{T}"/>.
    /// </typeparam>
    /// <param name="self">The value to map. May be <see langword="null"/>.</param>
    /// <param name="map">The function to apply to the value.</param>
    /// <returns>
    /// If <paramref name="self"/> is not <see langword="null"/>, the result of applying
    /// <paramref name="map"/> to <paramref name="self"/>; otherwise, <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   The <paramref name="map"/> function is only called if <paramref name="self"/> is not
    ///   <see langword="null"/>.
    /// </para>
    /// </remarks>
    public static NullableReadOnlySpan<TResult> Map<T, TResult>(this T? self, ReadOnlySpanFunc<T, TResult> map)
        where T : struct
        => self is T value ? new NullableReadOnlySpan<TResult>(map(value)) : default;

    /// <inheritdoc cref="Map{T, TResult}(T?, ReadOnlySpanFunc{T, TResult})"/>
    public static NullableReadOnlySpan<U> Map<T, U>(this T? self, ReadOnlySpanFunc<T, U> map)
        where T : class
        => self is T value ? new NullableReadOnlySpan<U>(map(value)) : default;

    /// <summary>
    /// Applies a mapping function a value that may be <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TResult">
    /// The type of the items in the resulting <see cref="NullableSpan{T}"/>.
    /// </typeparam>
    /// <param name="self">The value to map. May be <see langword="null"/>.</param>
    /// <param name="map">The function to apply to the value.</param>
    /// <returns>
    /// If <paramref name="self"/> is not <see langword="null"/>, the result of applying
    /// <paramref name="map"/> to <paramref name="self"/>; otherwise, <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    ///   The <paramref name="map"/> function is only called if <paramref name="self"/> is not
    ///   <see langword="null"/>.
    /// </para>
    /// </remarks>
    public static NullableSpan<TResult> Map<T, TResult>(this T? self, SpanFunc<T, TResult> map)
        where T : struct
        => self is T value ? new NullableSpan<TResult>(map(value)) : default;

    /// <inheritdoc cref="Map{T, TResult}(T?, SpanFunc{T, TResult})"/>
    public static NullableSpan<TResult> Map<T, TResult>(this T? self, SpanFunc<T, TResult> map)
        where T : class
        => self is T value ? new NullableSpan<TResult>(map(value)) : default;
}
