using Microsoft.CodeAnalysis;

namespace Roslyn4T4.Models
{
    public class CompiledProject
    {
        private Compilation _compilation;

        public CompiledProject(Project project)
        {
            Project = project;
        }

        public Compilation Compilation => _compilation ?? (_compilation = Project.GetCompilationAsync().Result);
        public Project Project { get; }
    }
}