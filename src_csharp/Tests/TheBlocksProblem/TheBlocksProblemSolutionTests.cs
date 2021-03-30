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

        [Fact(DisplayName = "Pile Onto basic.")]
        public void Test0010()
        {
            RunTest(new string[]
            {
                "4",
                "move 1 onto 0",
                "move 2 onto 0",
                "quit"
            }, new[]
            {
                "0: 0 2 1",
                "1:",
                "2:",
                "3: 3",
            });

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

            RunTest(new string[]
            {
                "4",
                "move 1 onto 0",
                "move 2 onto 0",
                "move 0 onto 3",
                "pile 3 onto 2",
                "quit"
            }, new[]
            {
                "0: 2 3 0 1",
                "1:",
                "2:",
                "3:",
            });
        }

        [Fact(DisplayName = "Pile Onto basic 2.")]
        public void Test0014()
        {
            RunTest(new string[]
            {
                "5",
                "move 1 onto 0",
                "move 2 onto 0",
                "quit"
            }, new[]
            {
                "0: 0 2 1",
                "1:",
                "2:",
                "3: 3",
                "4: 4",
            });

            RunTest(new string[]
            {
                "5",
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
                "4: 4",
            });

            RunTest(new string[]
            {
                "5",
                "move 1 onto 0",
                "move 2 onto 0",
                "move 0 onto 3",
                "move 4 onto 2",
                "quit"
            }, new[]
            {
                "0: 2 4 1",
                "1:",
                "2:",
                "3: 3 0",
                "4:",
            });

            RunTest(new string[]
            {
                "5",
                "move 1 onto 0",
                "move 2 onto 0",
                "move 0 onto 3",
                "move 4 onto 2",
                "pile 3 onto 2",
                "quit"
            }, new[]
            {
                "0: 2 3 0 4 1",
                "1:",
                "2:",
                "3:",
                "4:",
            });
        }

        [Fact(DisplayName = "Pile over should work.")]
        public void Test0020()
        {
            RunTest(new string[]
            {
                "5",
                "move 1 onto 0",
                "move 2 onto 0",
                "quit"
            }, new[]
            {
                "0: 0 2 1",
                "1:",
                "2:",
                "3: 3",
                "4: 4",
            });

            RunTest(new string[]
            {
                "5",
                "move 1 onto 0",
                "move 2 onto 0",
                "move 3 over 4",
                "quit"
            }, new[]
            {
                "0: 0 2 1",
                "1:",
                "2:",
                "3:",
                "4: 4 3",
            });

            RunTest(new string[]
            {
                "5",
                "move 1 onto 0",
                "move 2 onto 0",
                "move 3 over 4",
                "pile 4 over 0",
                "quit"
            }, new[]
            {
                "0: 0 2 1 4 3",
                "1:",
                "2:",
                "3:",
                "4:",
            });
        }

        [Fact(DisplayName = "Should ignore command when given blocks are the same.")]
        public void Test030()
        {
            RunTest(new string[]
            {
                "5",
                "move 1 onto 1",
                "pile 0 onto 0",
                "move 4 over 4",
                "quit"
            }, new[]
            {
                "0: 0",
                "1: 1",
                "2: 2",
                "3: 3",
                "4: 4",
            });
        }

        [Fact(DisplayName = "Should ignore command blocs are in the same stack.")]
        public void Test031()
        {
            RunTest(new string[]
            {
                "5",
                "move 1 onto 0",
                "move 2 onto 0",
                "move 3 over 4",
                "move 0 over 1",
                "pile 2 over 1",
                "pile 1 onto 2",
                "quit"
            }, new[]
            {
                "0: 0 2 1",
                "1:",
                "2:",
                "3:",
                "4: 4 3",
            });
        }

        [Fact(DisplayName = "Sample problem.")]
        public void Test050()
        {
            RunTest(new string[]
            {
                "10",
                "move 9 onto 1",
                "move 8 over 1",
                "move 7 over 1",
                "move 6 over 1",
                "pile 8 over 6",
                "pile 8 over 5",
                "move 2 over 1",
                "move 4 over 9",
                "quit"
            }, new[]
            {
                "0: 0",
                "1: 1 9 2 4",
                "2:",
                "3: 3",
                "4:",
                "5: 5 8 7 6",
                "6:",
                "7:",
                "8:",
                "9:",
            });
        }

        // [Fact(DisplayName = "Sample problem 2.")]
        // public void Test051()
        // {
        //     RunTest(new string[]
        //     {
        //         "24",
        //         "move 2 onto 1",
        //         "move 5 over 1",
        //         "move 3 onto 2",
        //         "move 23 over 0",
        //         "pile 22 over 17",
        //         "move 4 onto 3",
        //         "move 5 over 1",
        //         "move 23 onto 3",
        //         "pile 1 over 10",
        //         "move 9 over 8",
        //         "move 11 over 8",
        //         "pile 3 over 8",
        //         "pile 8 over 3",
        //         "move 20 over 19",
        //         "pile 19 over 18",
        //         "pile 18 onto 15",
        //         "move 15 over 3",
        //         "pile 20 onto 19",
        //         "pile 19 onto 18",
        //         "pile 18 over 17",
        //         "quit",
        //     }, new[]
        //     {
        //         "0: 0",
        //         "1:",
        //         "2:",
        //         "3:",
        //         "4: 4",
        //         "5: 5",
        //         "6: 6",
        //         "7: 7",
        //         "8: 8 9 11 3 23 15",
        //         "9:",
        //         "10: 10 1 2",
        //         "11:",
        //         "12: 12",
        //         "13: 13",
        //         "14: 14",
        //         "15:",
        //         "16: 16",
        //         "17: 17 22 18 19 20",
        //         "18:",
        //         "19:",
        //         "20:",
        //         "21: 21",
        //         "22:",
        //         "23:",
        //     });
        // }

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