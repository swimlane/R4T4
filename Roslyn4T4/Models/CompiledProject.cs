using Microsoft.CodeAnalysis;

namespace R4T4.Models
{
    /// <summary>
    /// Project model with lazy cached compilation.
    /// </summary>
    public class CompiledProject
    {
        private Compilation _compilation;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompiledProject"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public CompiledProject(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Gets the compilation.
        /// </summary>
        /// <value>
        /// The compilation.
        /// </value>
        public Compilation Compilation => _compilation ?? (_compilation = Project.GetCompilationAsync().Result);
        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public Project Project { get; }
    }
}