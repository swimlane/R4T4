using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslyn4T4.Extensions
{
    public static class TypeExtension
    {
        public static string GetFullNamespace(this ISymbol symbol)
        {
            var ns = symbol.ContainingNamespace;
            if (string.IsNullOrEmpty(ns?.Name))
                return null;

            var full = ns.GetFullNamespace();
            return full == null ? ns.Name : $"{full}.{ns.Name}";
        }

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
