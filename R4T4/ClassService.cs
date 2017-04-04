using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using R4T4.Extensions;
using R4T4.Models;

namespace R4T4
{
    /// <summary>
    /// Class and Interface provider
    /// </summary>
    public class ClassService
    {
        private readonly Dictionary<string, INamedTypeSymbol> _classes = new Dictionary<string, INamedTypeSymbol>();
        private readonly Dictionary<string, List<INamedTypeSymbol>> _bases = new Dictionary<string, List<INamedTypeSymbol>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassService"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public ClassService(CompiledProject project)
        {
            var classes = project.Compilation.SyntaxTrees
                .Select(s => project.Compilation.GetSemanticModel(s))
                .SelectMany(s => s.SyntaxTree.GetRoot()
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .Select(c => (s.GetDeclaredSymbol(c) as INamedTypeSymbol, GetBaseClasses(s, c))))
                .ToImmutableList();

            foreach (var c in classes.Where(c=>c.Item1!=null))
            {
                _classes[c.Item1.GetFullTypeString()] = c.Item1;
                foreach (var namedTypeSymbol in c.Item2)
                {
                    var baseType = namedTypeSymbol.GetFullTypeString();
                    if(!_bases.ContainsKey(baseType))
                        _bases[baseType] = new List<INamedTypeSymbol>();
                    _bases[baseType].Add(c.Item1);
                }
            }
        }

        /// <summary>
        /// Gets the class by it's base type.
        /// </summary>
        /// <param name="fqBaseName">Fully qualified name of the base type.</param>
        /// <returns></returns>
        public IEnumerable<ClassModel> GetByBaseType(string fqBaseName)
        {
            return FindByBaseType(fqBaseName).Select(symbol => new ClassModel(symbol));
        }
        /// <summary>
        /// Gets the class by it's type.
        /// </summary>
        /// <param name="fqName">Name of the fq.</param>
        /// <returns></returns>
        public ClassModel GetByType(string fqName)
        {
            var symbol = FindByType(fqName);
            return symbol!=null ? new ClassModel(symbol) : null;
        }

        internal IEnumerable<INamedTypeSymbol> FindByBaseType(string fqBaseName)
        {
            return _bases.TryGetValue(fqBaseName, out List<INamedTypeSymbol> list)
                ? list
                : Enumerable.Empty<INamedTypeSymbol>();
        }

        internal INamedTypeSymbol FindByType(string name)
        {
            return _classes.TryGetValue(name, out INamedTypeSymbol cls) ? cls : null;
        }

        private static IEnumerable<INamedTypeSymbol> GetBaseClasses(SemanticModel model, BaseTypeDeclarationSyntax type)
        {
            var classSymbol = model.GetDeclaredSymbol(type) as INamedTypeSymbol;
            var returnValue = new List<INamedTypeSymbol>();
            while (classSymbol?.BaseType != null)
            {
                returnValue.Add(classSymbol.BaseType);
                if (!classSymbol.Interfaces.IsEmpty)
                    returnValue.AddRange(classSymbol.Interfaces);
                classSymbol = classSymbol.BaseType;
            }
            return returnValue;
        }
    }
}
