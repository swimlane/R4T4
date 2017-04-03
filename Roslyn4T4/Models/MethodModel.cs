using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslyn4T4.Extensions;

namespace Roslyn4T4.Models
{
    public class MethodModel : BaseModel<IMethodSymbol>
    {
        private Dictionary<string, ParameterModel> _parameters;
        private string _returnType;

        public MethodModel(IMethodSymbol symbol) : base(symbol)
        {
        }

        #region Overrides of BaseModel<INamedTypeSymbol>

        public override ISymbol ModelType => Symbol.ReturnType;

        #endregion

        public Dictionary<string, ParameterModel> Parameters => _parameters ??
                                                                (_parameters = Symbol.Parameters.ToDictionary(
                                                                    ps => ps.Name,
                                                                    ps => new ParameterModel(ps)));
    }
}