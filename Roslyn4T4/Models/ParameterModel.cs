using Microsoft.CodeAnalysis;

namespace Roslyn4T4.Models
{
    public class ParameterModel : BaseModel<IParameterSymbol>
    {
        public ParameterModel(IParameterSymbol symbol) : base(symbol)
        {
        }
        #region Overrides of BaseModel<INamedTypeSymbol>

        public override ISymbol ModelType => Symbol.Type;

        #endregion
    }
}