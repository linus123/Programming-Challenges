using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Core.TheBlocksProblem;
using FluentAssertions;
using Xunit;

namespace Tests.TheBlocksProblem
{
    public class TheBlocksProblemSolutionTests
    {
        [Fact(DisplayName = "Should work.")]
        public void Test001()
        {
            var solution = new TheBlocksProblemSolution();

            var moves = new string[]
            {
                "2",
                "move 1 onto 0"
            };

            var solutionLines = solution
                .GetSolution(moves)
                .ToArray();

            var expectedSolution = new []
            {
                "0: 1",
                "1:",
            };

            solutionLines.Should().Equal(expectedSolution);
        }

    }
}