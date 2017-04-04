using Microsoft.CodeAnalysis;

namespace Roslyn4T4.Models
{
    /// <summary>
    /// Parameter model.
    /// </summary>
    /// <seealso cref="Roslyn4T4.Models.BaseModel{Microsoft.CodeAnalysis.IParameterSymbol}" />
    public class ParameterModel : BaseModel<IParameterSymbol>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterModel"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        public ParameterModel(IParameterSymbol symbol) : base(symbol)
        {
        }
        #region Overrides of BaseModel<INamedTypeSymbol>

        /// <summary>
        /// Gets the symbol of the model type.
        /// </summary>
        /// <value>
        /// The ISymbol
        /// </value>
        protected override ISymbol ModelType => Symbol.Type;

        #endregion
    }
}