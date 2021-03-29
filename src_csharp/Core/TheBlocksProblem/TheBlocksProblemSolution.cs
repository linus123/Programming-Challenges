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
            var moveOverRegEx = new Regex("move (\\d+) over (\\d+)");
            var pileOntoRegEx = new Regex("pile (\\d+) onto (\\d+)");

            foreach (var command in commands)
            {
                if (command == "quit")
                    break;

                var moveOntoMatch = moveOntoRegEx.Match(command);

                if (moveOntoMatch.Success)
                {
                    var sourceBlockNumber = int.Parse(moveOntoMatch.Groups[1].Value);
                    var destBlockNumber = int.Parse(moveOntoMatch.Groups[2].Value);

                    blockSpace.MoveOnto(sourceBlockNumber, destBlockNumber);
                }

                var moveOverMatch = moveOverRegEx.Match(command);

                if (moveOverMatch.Success)
                {
                    var sourceBlockNumber = int.Parse(moveOverMatch.Groups[1].Value);
                    var destBlockNumber = int.Parse(moveOverMatch.Groups[2].Value);

                    blockSpace.MoveOver(sourceBlockNumber, destBlockNumber);
                }

                var pileOntoMatch = pileOntoRegEx.Match(command);

                if (pileOntoMatch.Success)
                {
                    var sourceBlockNumber = int.Parse(pileOntoMatch.Groups[1].Value);
                    var destBlockNumber = int.Parse(pileOntoMatch.Groups[2].Value);

                    blockSpace.PileOnto(sourceBlockNumber, destBlockNumber);
                }
            }

            return blockSpace.GetLines();
        }

    }
}