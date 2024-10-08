using System;

namespace Ookii.Common;

/// <summary>
/// Encapsulates a method that returns a <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
/// <returns>
/// The return value of the method that this delegate encapsulates.
/// </returns>
public delegate ReadOnlySpan<T> ReadOnlySpanFunc<T>();

/// <summary>
/// Encapsulates a method that returns a <see cref="Span{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the items in the <see cref="Span{T}"/>.</typeparam>
/// <returns>
/// The return value of the method that this delegate encapsulates.
/// </returns>
public delegate Span<T> SpanFunc<T>();
