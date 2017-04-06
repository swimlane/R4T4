using System.IO;
using R4T4;

namespace Test
{
    class Program
    {
        #region Private

        static void Main(string[] args)
        {
            var solutionFile = Path.GetFullPath("..\\..\\..\\R4T4.sln");
            var solution = new SolutionService(solutionFile);
            var project = solution.GetProject("ExampleClasses");
            var typeService = new ClassService(project);
            var classModel = typeService.GetByType("ExampleClasses.Models.Person");
            var classAttributes = classModel.Attributes;
        }

        #endregion
    }
}