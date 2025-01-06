using System;

namespace Ookii.Common;

/// <summary>
/// Encapsulates a method that returns a <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <typeparam name="TResult">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
/// <returns>
/// The return value of the method that this delegate encapsulates.
/// </returns>
public delegate ReadOnlySpan<TResult> ReadOnlySpanFunc<TResult>();

/// <summary>
/// Encapsulates a method that returns a <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the first argument.</typeparam>
/// <typeparam name="TResult">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
/// <param name="arg">The first argument for the encapsulated method.</param>
/// <returns>
/// The return value of the method that this delegate encapsulates.
/// </returns>
public delegate ReadOnlySpan<TResult> ReadOnlySpanFunc<T, TResult>(T arg);

/// <summary>
/// Encapsulates a method that returns a <see cref="Span{T}"/>.
/// </summary>
/// <typeparam name="TResult">The type of the items in the <see cref="Span{T}"/>.</typeparam>
/// <returns>
/// The return value of the method that this delegate encapsulates.
/// </returns>
public delegate Span<TResult> SpanFunc<TResult>();

/// <summary>
/// Encapsulates a method that returns a <see cref="Span{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the first argument.</typeparam>
/// <typeparam name="TResult">The type of the items in the <see cref="Span{T}"/>.</typeparam>
/// <param name="arg">The first argument for the encapsulated method.</param>
/// <returns>
/// The return value of the method that this delegate encapsulates.
/// </returns>
public delegate Span<TResult> SpanFunc<T, TResult>(T arg);

/// <summary>
/// Encapsulates a method that returns a <see cref="ReadOnlySpanPair{TFirst, TSecond}"/>.
/// </summary>
/// <typeparam name="TResultFirst">
/// The type of the items in the first value of the resulting
/// <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
/// </typeparam>
/// <typeparam name="TResultSecond">
/// The type of the items in the second value of the resulting
/// <see cref="NullableReadOnlySpanPair{TFirst, TSecond}"/>.
/// </typeparam>
/// <returns>
/// The return value of the method that this delegate encapsulates.
/// </returns>
public delegate ReadOnlySpanPair<TResultFirst, TResultSecond> ReadOnlySpanPairFunc<TResultFirst, TResultSecond>();
