using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.TheBlocksProblem
{
    public class TheBlocksProblemSolution
    {
        public IEnumerable<string> GetSolution(
            IEnumerable<string> testFileLines)
        {
            var isFirst = true;

            int numberOfBlocks = 0;
            var commands = new List<string>();

            foreach (var line in testFileLines)
            {
                if (isFirst)
                    numberOfBlocks = int.Parse(line);

                commands.Add(line);

                isFirst = false;
            }

            return GetSolution(numberOfBlocks, commands);
        }

        public IEnumerable<string> GetSolution(
            int numberOfBlocks,
            IEnumerable<string> commands)
        {
            var blockSpace = new BlockSpace(numberOfBlocks);

            var moveOntoRegEx = new Regex("move (\\d+) onto (\\d+)");

            foreach (var command in commands)
            {
                var match = moveOntoRegEx.Match(command);

                if (match.Success)
                {
                    var sourceBlockNumber = int.Parse(match.Groups[1].Value);
                    var destBlockNumber = int.Parse(match.Groups[2].Value);

                    blockSpace.MoveOnto(sourceBlockNumber, destBlockNumber);
                }
            }

            return blockSpace.GetLines();
        }

    }
}