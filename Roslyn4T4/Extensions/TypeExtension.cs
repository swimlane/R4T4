using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslyn4T4.Extensions
{
    /// <summary>
    /// Extension methods for types and classes.
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Gets the fully qualified namespace of type.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public static string GetFullNamespace(this ISymbol symbol)
        {
            var ns = symbol.ContainingNamespace;
            if (string.IsNullOrEmpty(ns?.Name))
                return null;

            var full = ns.GetFullNamespace();
            return full == null ? ns.Name : $"{full}.{ns.Name}";
        }

        /// <summary>
        /// Gets the fully qualified type string.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="incNs">if set to <c>true</c> Include full namespace for type.</param>
        /// <returns></returns>
        public static string GetFullTypeString(this ISymbol symbol, bool incNs = true)
        {
            var name = symbol.Name;
            if (!(symbol is INamedTypeSymbol)) return name;
            var arguments = ((INamedTypeSymbol)symbol).TypeArguments;
            if (arguments.Length <= 0) return incNs ? $"{GetFullNamespace(symbol)}.{name}" : name;

            var types = string.Join(", ", arguments.Select(ta => GetFullTypeString(ta, incNs)));
            return $"{name}<{types}>";
        }
    }
}
