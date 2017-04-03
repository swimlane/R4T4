using Microsoft.CodeAnalysis;

namespace Roslyn4T4.Models
{
    public class PropertyModel : BaseModel<IPropertySymbol>
    {
        public PropertyModel(IPropertySymbol symbol) : base(symbol)
        {
        }
        #region Overrides of BaseModel<INamedTypeSymbol>

        public override ISymbol ModelType => Symbol.Type;

        #endregion
    }
}