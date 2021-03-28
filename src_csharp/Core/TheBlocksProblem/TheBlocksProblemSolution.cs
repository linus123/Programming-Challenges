using System.Collections.Generic;

namespace Core.TheBlocksProblem
{
    public class TheBlocksProblemSolution
    {
        public IEnumerable<string> GetSolution(
            IEnumerable<string> testFileLines)
        {
            foreach (var testFileLine in testFileLines)
            {
                yield return "";
            }
        }
    }
}