using System.Linq;
using Microsoft.CodeAnalysis;
using Roslyn4T4.Extensions;

namespace Roslyn4T4.Models
{
    public abstract class BaseModel<T> where T : ISymbol
    {
        private ILookup<string, AttributeModel> _attributes;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        // ReSharper disable once InconsistentNaming        
        protected TypeModel _type;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        public BaseModel(T symbol)
        {
            Symbol = symbol;
        }

        public T Symbol { get; }

        public ILookup<string, AttributeModel> Attributes => _attributes
                                                             ?? (_attributes = Symbol.GetAttributes()
                                                                 .ToLookup(
                                                                     ad => ad.AttributeClass.GetFullTypeString(),
                                                                     ad => new AttributeModel(ad)));

        public string Name => Symbol.Name;

        public virtual TypeModel Type => _type ?? (_type = new TypeModel(ModelType));
        public abstract ISymbol ModelType { get; } 
    }
}