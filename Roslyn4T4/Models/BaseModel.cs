using System.Linq;
using Microsoft.CodeAnalysis;
using Roslyn4T4.Extensions;

namespace Roslyn4T4.Models
{
    /// <summary>
    /// Base model for Code models derived from ISymbol
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseModel<T> where T : ISymbol
    {
        private ILookup<string, AttributeModel> _attributes;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        // ReSharper disable once InconsistentNaming        
        protected TypeModel _type;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModel{T}"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        public BaseModel(T symbol)
        {
            Symbol = symbol;
        }

        /// <summary>
        /// Gets the base symbol for this model.
        /// </summary>
        /// <value>
        /// The symbol.
        /// </value>
        public T Symbol { get; }

        /// <summary>
        /// Gets the attributes collection.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public ILookup<string, AttributeModel> Attributes => _attributes
                                                             ?? (_attributes = Symbol.GetAttributes()
                                                                 .ToLookup(
                                                                     ad => ad.AttributeClass.GetFullTypeString(),
                                                                     ad => new AttributeModel(ad)));

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => Symbol.Name;

        /// <summary>
        /// Gets the model type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        /// <remarks>For classes - it's type, for methods it's return type, for metod parametes their type etc</remarks>
        public virtual TypeModel Type => _type ?? (_type = new TypeModel(ModelType));

        /// <summary>
        /// Gets the symbol of the model type.
        /// </summary>
        /// <value>
        /// The ISymbol
        /// </value>
        protected abstract ISymbol ModelType { get; } 
    }
}