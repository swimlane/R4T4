using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Roslyn4T4.Models;

namespace Roslyn4T4
{
    /// <summary>
    /// Service for working with solutions and projects
    /// </summary>
    public class SolutionService
    {
        private readonly IEnumerable<CompiledProject> _projects;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionService"/> class.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        public SolutionService(string solutionPath)
        {
            var workspace = MSBuildWorkspace.Create();
            Solution = workspace.OpenSolutionAsync(solutionPath).Result;
            _projects = Solution.Projects.Select(p => new CompiledProject(p));
        }

        /// <summary>
        /// Gets the solution.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
        public Solution Solution { get; }

        #region Public

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public CompiledProject GetProject(string name)
        {
            return _projects.FirstOrDefault(p => p.Project.Name == name);
        }

        #endregion
    }
}