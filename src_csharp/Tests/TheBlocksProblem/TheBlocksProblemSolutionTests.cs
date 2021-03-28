using System.Linq;
using Core.TheBlocksProblem;
using FluentAssertions;
using Xunit;

namespace Tests.TheBlocksProblem
{
    public class TheBlocksProblemSolutionTests
    {
        [Fact(DisplayName = "Move Onto single block.")]
        public void Test001()
        {
            RunTest(new string[]
            {
                "2",
                "move 1 onto 0",
                "quit",
            }, new[]
            {
                "0: 0 1",
                "1:",
            });
        }

        [Fact(DisplayName = "Move Onto two separate blocks.")]
        public void Test002()
        {
            RunTest(new string[]
            {
                "4",
                "move 1 onto 0",
                "move 3 onto 2",
                "quit"
            }, new[]
            {
                "0: 0 1",
                "1:",
                "2: 2 3",
                "3:",
            });
        }

        [Fact(DisplayName = "Move Onto two blocks block.")]
        public void Test003()
        {
            RunTest(new string[]
            {
                "3",
                "move 1 onto 0",
                "move 2 onto 1",
                "quit"
            }, new[]
            {
                "0: 0 1 2",
                "1:",
                "2:",
            });
        }

        [Fact(DisplayName = "Move Onto two blocks block as insert.")]
        public void Test004()
        {
            RunTest(new string[]
            {
                "3",
                "move 1 onto 0",
                "move 2 onto 0",
                "quit"
            }, new[]
            {
                "0: 0 2 1",
                "1:",
                "2:",
            });
        }

        [Fact(DisplayName = "Move Onto blocks with a slide down.")]
        public void Test005()
        {
            RunTest(new string[]
            {
                "4",
                "move 1 onto 0",
                "move 2 onto 0",
                "move 0 onto 3",
                "quit"
            }, new[]
            {
                "0: 2 1",
                "1:",
                "2:",
                "3: 3 0",
            });
        }

        [Fact(DisplayName = "Move Over should work with two blocks.")]
        public void Test006()
        {
            RunTest(new string[]
            {
                "2",
                "move 1 over 0",
                "quit"
            }, new[]
            {
                "0: 0 1",
                "1:",
            });
        }

        private static void RunTest(string[] moves, string[] expectedSolution)
        {
            var solution = new TheBlocksProblemSolution();

            var solutionLines = solution
                .GetSolution(moves)
                .ToArray();

            solutionLines.Should().Equal(expectedSolution);
        }
    }
}