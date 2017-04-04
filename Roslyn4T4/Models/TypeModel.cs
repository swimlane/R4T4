using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslyn4T4.Extensions;

namespace Roslyn4T4.Models
{
    /// <summary>
    /// .Net type model.
    /// </summary>
    public class TypeModel
    {
        private string _fullType;
        /// <summary>
        /// Gets the full type.
        /// </summary>
        /// <value>
        /// The full type.
        /// </value>
        public string FullType => _fullType ?? (_fullType = Symbol.GetFullTypeString());

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeModel"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
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

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public string Namespace => Symbol.GetFullNamespace();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => Symbol.Name;

        /// <summary>
        /// Gets the inner types of generic type.
        /// </summary>
        /// <value>
        /// The inner types.
        /// </value>
        public List<TypeModel> InnerTypes { get; } = new List<TypeModel>();

        /// <summary>
        /// Gets a value indicating whether this instance is generics.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is generics; otherwise, <c>false</c>.
        /// </value>
        public bool IsGenerics { get; }

        /// <summary>
        /// Gets the symbol.
        /// </summary>
        /// <value>
        /// The symbol.
        /// </value>
        public ISymbol Symbol { get;  }
    }
}