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
            Arguments=attributeData.ConstructorArguments.ToLookup(ca => ca.Value, ca => ca.Type.GetFullTypeString());
        }
        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public ILookup<object, string> Arguments { get; set; }
    }
}