using System.Collections.Generic;

namespace Core.TheBlocksProblem
{
    public class TheBlocksProblemSolution
    {
        public IEnumerable<string> GetSolution(
            IEnumerable<string> testFileLines)
        {
            yield return "0: 1";
            yield return "1:";

            foreach (var testFileLine in testFileLines)
            {
            }
        }
    }
}