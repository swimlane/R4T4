using System;

namespace ExampleClasses.Attributes
{
    public class TagAttribute : Attribute
    {
        public TagAttribute(params string[] tags)
        {
            Tags = tags;
        }

        public string[] Tags { get; }
    }
}