using System.Linq;
using Microsoft.CodeAnalysis;
using Roslyn4T4.Extensions;

namespace Roslyn4T4.Models
{
    public class AttributeModel
    {
        private AttributeData _attributeData;

        public AttributeModel(AttributeData attributeData)
        {
            _attributeData = attributeData;
            Arguments=attributeData.ConstructorArguments.ToLookup(ca => ca.Value, ca => ca.Type.GetFullTypeString());
        }
        public ILookup<object, string> Arguments { get; set; }
    }
}