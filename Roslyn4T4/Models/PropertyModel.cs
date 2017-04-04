using Microsoft.CodeAnalysis;

namespace R4T4.Models
{
    /// <summary>
    /// Classs property model.
    /// </summary>
    /// <seealso cref="Roslyn4T4.Models.BaseModel{Microsoft.CodeAnalysis.IPropertySymbol}" />
    public class PropertyModel : BaseModel<IPropertySymbol>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyModel"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        public PropertyModel(IPropertySymbol symbol) : base(symbol)
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