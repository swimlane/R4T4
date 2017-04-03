using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Roslyn4T4.Models;

namespace Roslyn4T4
{
    public class SolutionService
    {
        private readonly IEnumerable<CompiledProject> _projects;
        private readonly MSBuildWorkspace _workspace;

        public SolutionService(string solutionPath)
        {
            _workspace = MSBuildWorkspace.Create();
            Solution = _workspace.OpenSolutionAsync(solutionPath).Result;
            _projects = Solution.Projects.Select(p => new CompiledProject(p));
        }

        public Solution Solution { get; }

        #region Public

        public CompiledProject GetProject(string name)
        {
            return _projects.FirstOrDefault(p => p.Project.Name == name);
        }

        #endregion
    }
}