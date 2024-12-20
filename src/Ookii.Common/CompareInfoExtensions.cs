#if !NET5_0_OR_GREATER

using System;
using System.Globalization;

namespace Ookii.Common;

// Workaround for CompareInfo methods that are not available in .NET Standard 2.x.
internal static class CompareInfoExtensions
{
    public static int IndexOf(this CompareInfo compareInfo, ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options)
    {
        if (options == CompareOptions.Ordinal)
        {
            return source.IndexOf(value, StringComparison.Ordinal);
        }

        if (options == CompareOptions.OrdinalIgnoreCase)
        {
            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase);
        }

        if (compareInfo == CultureInfo.CurrentCulture.CompareInfo)
        {
            if (options == CompareOptions.None)
            {
                return source.IndexOf(value, StringComparison.CurrentCulture);
            }

            if (options == CompareOptions.IgnoreCase)
            {
                return source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        if (compareInfo == CultureInfo.InvariantCulture.CompareInfo)
        {
            if (options == CompareOptions.None)
            {
                return source.IndexOf(value, StringComparison.InvariantCulture);
            }

            if (options == CompareOptions.IgnoreCase)
            {
                return source.IndexOf(value, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        // Fall back to allocating strings if we can't map to a known StringComparison value.
        return compareInfo.IndexOf(source.ToString(), value.ToString(), options);
    }

    public static int LastIndexOf(this CompareInfo compareInfo, ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options)
    {
        if (options == CompareOptions.Ordinal)
        {
            return source.LastIndexOf(value, StringComparison.Ordinal);
        }

        if (options == CompareOptions.OrdinalIgnoreCase)
        {
            return source.LastIndexOf(value, StringComparison.OrdinalIgnoreCase);
        }

        if (compareInfo == CultureInfo.CurrentCulture.CompareInfo)
        {
            if (options == CompareOptions.None)
            {
                return source.LastIndexOf(value, StringComparison.CurrentCulture);
            }

            if (options == CompareOptions.IgnoreCase)
            {
                return source.LastIndexOf(value, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        if (compareInfo == CultureInfo.InvariantCulture.CompareInfo)
        {
            if (options == CompareOptions.None)
            {
                return source.LastIndexOf(value, StringComparison.InvariantCulture);
            }

            if (options == CompareOptions.IgnoreCase)
            {
                return source.LastIndexOf(value, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        // Fall back to allocating strings if we can't map to a known StringComparison value.
        return compareInfo.LastIndexOf(source.ToString(), value.ToString(), options);
    }

    public static bool IsPrefix(this CompareInfo compareInfo, ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options)
    {
        if (options == CompareOptions.Ordinal)
        {
            return source.StartsWith(value, StringComparison.Ordinal);
        }

        if (options == CompareOptions.OrdinalIgnoreCase)
        {
            return source.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        if (compareInfo == CultureInfo.CurrentCulture.CompareInfo)
        {
            if (options == CompareOptions.None)
            {
                return source.StartsWith(value, StringComparison.CurrentCulture);
            }

            if (options == CompareOptions.IgnoreCase)
            {
                return source.StartsWith(value, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        if (compareInfo == CultureInfo.InvariantCulture.CompareInfo)
        {
            if (options == CompareOptions.None)
            {
                return source.StartsWith(value, StringComparison.InvariantCulture);
            }

            if (options == CompareOptions.IgnoreCase)
            {
                return source.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        // Fall back to allocating strings if we can't map to a known StringComparison value.
        return compareInfo.IsPrefix(source.ToString(), value.ToString(), options);
    }

    public static bool IsSuffix(this CompareInfo compareInfo, ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options)
    {
        if (options == CompareOptions.Ordinal)
        {
            return source.EndsWith(value, StringComparison.Ordinal);
        }

        if (options == CompareOptions.OrdinalIgnoreCase)
        {
            return source.EndsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        if (compareInfo == CultureInfo.CurrentCulture.CompareInfo)
        {
            if (options == CompareOptions.None)
            {
                return source.EndsWith(value, StringComparison.CurrentCulture);
            }

            if (options == CompareOptions.IgnoreCase)
            {
                return source.EndsWith(value, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        if (compareInfo == CultureInfo.InvariantCulture.CompareInfo)
        {
            if (options == CompareOptions.None)
            {
                return source.EndsWith(value, StringComparison.InvariantCulture);
            }

            if (options == CompareOptions.IgnoreCase)
            {
                return source.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        // Fall back to allocating strings if we can't map to a known StringComparison value.
        return compareInfo.IsSuffix(source.ToString(), value.ToString(), options);
    }
}

#endif
