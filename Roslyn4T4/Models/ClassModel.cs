using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;


namespace Roslyn4T4.Models
{
    /// <summary>
    /// Model for classes and interfaces
    /// </summary>
    public class ClassModel : BaseModel<INamedTypeSymbol>
    {
        private List<MethodModel> _methods;
        private HashSet<string> _publicMembers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassModel"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        public ClassModel(INamedTypeSymbol symbol) : base(symbol)
        {
        }

        /// <summary>
        /// Gets the public member names.
        /// </summary>
        /// <value>
        /// The public member namess.
        /// </value>
        public HashSet<string> PublicMembers => _publicMembers
                                                ?? (_publicMembers = new HashSet<string>(Symbol.MemberNames));

        /// <summary>
        /// Gets the public class methods.
        /// </summary>
        /// <value>
        /// The methods.
        /// </value>
        public List<MethodModel> Methods
        {
            get
            {
                return _methods ?? (_methods = Symbol.GetMembers()
                           .Where(m => m.Kind == SymbolKind.Method)
                           .Cast<IMethodSymbol>()
                           .Where(s => PublicMembers.Contains(s.Name))
                           .Select(ms => new MethodModel(ms))
                           .ToList());
            }
        }

        /// <summary>
        /// Gets the public class properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public List<PropertyModel> Properties
        {
            get
            {
                return Symbol.GetMembers()
                    .Where(m => m.Kind == SymbolKind.Property)
                    .Cast<IPropertySymbol>()
                    .Where(ps => PublicMembers.Contains(ps.Name))
                    .Select(s => new PropertyModel(s))
                    .ToList();
            }
        }

        #region Overrides of BaseModel<INamedTypeSymbol>

        /// <summary>
        /// Gets the symbol of the model type.
        /// </summary>
        /// <value>
        /// The ISymbol
        /// </value>
        protected override ISymbol ModelType => Symbol;

        #endregion
    }
}