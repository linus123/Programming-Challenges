using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.TheBlocksProblem
{
    public class TheBlocksProblemSolution
    {
        const int EmptySlot = -1;

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
            var blockSpace = new int[numberOfBlocks, 25];

            for (int i = 0; i < numberOfBlocks; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    blockSpace[i, j] = EmptySlot;
                }
            }

            for (int i = 0; i < numberOfBlocks; i++)
            {
                blockSpace[i, 0] = i;
            }

            var moveOntoRegEx = new Regex("move (\\d+) onto (\\d+)");

            foreach (var command in commands)
            {
                var match = moveOntoRegEx.Match(command);

                if (match.Success)
                {
                    var sourceBlockNumber = int.Parse(match.Groups[1].Value);
                    var destBlockNumber = int.Parse(match.Groups[2].Value);

                    var sourceLocation = FindBlock(sourceBlockNumber, numberOfBlocks, blockSpace);
                    var destLocation = FindBlock(destBlockNumber, numberOfBlocks, blockSpace);

                    blockSpace[destLocation.Row, destLocation.Col + 1] = sourceBlockNumber;
                    blockSpace[sourceLocation.Row, sourceLocation.Col] = EmptySlot;
                }
            }

            for (int i = 0; i < numberOfBlocks; i++)
            {
                var foo = $"{i}:";

                for (int j = 0; j < 25; j++)
                {
                    if (blockSpace[i, j] != EmptySlot)
                        foo += $" {blockSpace[i, j]}";
                }

                yield return foo;
            }
        }

        private Location FindBlock(
            int blockNumber,
            int numberOfBlocks,
            int[,] blockSpace)
        {
            for (int i = 0; i < numberOfBlocks; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (blockSpace[i, j] == blockNumber)
                        return new Location()
                        {
                            Row = i,
                            Col = j
                        };
                }
            }

            throw new Exception("Block not found.");
        }

        public struct Location
        {
            public int Row { get; set; }
            public int Col { get; set; }
        }
    }
}