using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;


namespace Roslyn4T4.Models
{
    public class ClassModel : BaseModel<INamedTypeSymbol>
    {
        private List<MethodModel> _methods;
        private HashSet<string> _publicMembers;

        public ClassModel(INamedTypeSymbol symbol) : base(symbol)
        {
        }

        public HashSet<string> PublicMembers => _publicMembers
                                                ?? (_publicMembers = new HashSet<string>(Symbol.MemberNames));

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

        public override ISymbol ModelType => Symbol;

        #endregion
    }
}