using System.Linq;
using Microsoft.CodeAnalysis;
using R4T4.Extensions;

namespace R4T4.Models
{
    /// <summary>
    /// Attributes model.
    /// </summary>
    public class AttributeModel
    {
        private AttributeData _attributeData;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeModel"/> class.
        /// </summary>
        /// <param name="attributeData">The attribute data.</param>
        public AttributeModel(AttributeData attributeData)
        {
            _attributeData = attributeData;
            Arguments=GetArgumentModel(attributeData);
        }

        private static ILookup<string, object> GetArgumentModel(AttributeData attributeData)
        {
            var para = attributeData.AttributeConstructor.Parameters;
           return Enumerable.Range(0, para.Length)
                .ToLookup(i => para[i].Name, i => GetValue(attributeData.ConstructorArguments[i]));
        }

        private static object GetValue(TypedConstant constant)
        {
            return constant.Kind == TypedConstantKind.Array ? constant.Values.Select(GetValue).ToList() : constant.Value;
        }
        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public ILookup<string, object> Arguments { get; set; }
    }
}