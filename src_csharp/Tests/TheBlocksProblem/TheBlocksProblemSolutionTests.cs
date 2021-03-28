using System.Linq;
using Core.TheBlocksProblem;
using FluentAssertions;
using Xunit;

namespace Tests.TheBlocksProblem
{
    public class TheBlocksProblemSolutionTests
    {
        [Fact(DisplayName = "Moved single block with 2 blocks.")]
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
                "0: 0 1",
                "1:",
            };

            solutionLines.Should().Equal(expectedSolution);
        }

        [Fact(DisplayName = "Moved single block with 2 blocks.")]
        public void Test002()
        {
            var solution = new TheBlocksProblemSolution();

            var moves = new string[]
            {
                "3",
                "move 1 onto 0",
                "move 2 onto 0"
            };

            var solutionLines = solution
                .GetSolution(moves)
                .ToArray();

            var expectedSolution = new[]
            {
                "0: 0 1 2",
                "1:",
                "2:",
            };

            solutionLines.Should().Equal(expectedSolution);
        }
    }
}