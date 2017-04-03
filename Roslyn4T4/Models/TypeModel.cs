using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslyn4T4.Extensions;

namespace Roslyn4T4.Models
{
    public class TypeModel
    {
        private string _fullType;
        public string FullType => _fullType ?? (_fullType = Symbol.GetFullTypeString());

        public TypeModel(ISymbol symbol)
        {
            Symbol = symbol;
            if (symbol is INamedTypeSymbol namedSymbol)
            {
                var arguments = namedSymbol.TypeArguments;
                if (!arguments.IsEmpty)
                {
                    IsGenerics = true;
                    InnerTypes = arguments.Select(a => new TypeModel(a)).ToList();
                }
            }
        }

        public string Namespace => Symbol.GetFullNamespace();

        public string Name => Symbol.Name;

        public List<TypeModel> InnerTypes { get; set; } = new List<TypeModel>();

        public bool IsGenerics { get; set; }

        public ISymbol Symbol { get; set; }
    }
}