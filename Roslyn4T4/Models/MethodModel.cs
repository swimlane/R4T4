using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslyn4T4.Models
{
    /// <summary>
    /// Method model.
    /// </summary>
    public class MethodModel : BaseModel<IMethodSymbol>
    {
        private Dictionary<string, ParameterModel> _parameters;
        private string _returnType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodModel"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        public MethodModel(IMethodSymbol symbol) : base(symbol)
        {
        }

        #region Overrides of BaseModel<INamedTypeSymbol>

        /// <summary>
        /// Gets the symbol of the model type.
        /// </summary>
        /// <value>
        /// The ISymbol
        /// </value>
        protected override ISymbol ModelType => Symbol.ReturnType;

        #endregion

        /// <summary>
        /// Gets the method parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public Dictionary<string, ParameterModel> Parameters => _parameters ??
                                                                (_parameters = Symbol.Parameters.ToDictionary(
                                                                    ps => ps.Name,
                                                                    ps => new ParameterModel(ps)));
    }
}