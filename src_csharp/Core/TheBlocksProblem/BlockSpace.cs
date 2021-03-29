using System;
using System.Collections.Generic;

namespace Core.TheBlocksProblem
{
    public class BlockSpace
    {
        private int _numberOfBlocks;
        private int[,] _blockSpace;

        public const int EmptySlot = -1;

        public BlockSpace(
            int numberOfBlocks)
        {
            _numberOfBlocks = numberOfBlocks;
            _blockSpace = new int[numberOfBlocks, 25];

            for (int i = 0; i < numberOfBlocks; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    _blockSpace[i, j] = EmptySlot;
                }
            }

            for (int i = 0; i < numberOfBlocks; i++)
            {
                _blockSpace[i, 0] = i;
            }
        }

        public BlockLocation FindBlock(
            int blockNumber)
        {
            for (int i = 0; i < _numberOfBlocks; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (_blockSpace[i, j] == blockNumber)
                        return new BlockLocation()
                        {
                            RowIndex = i,
                            ColumnIndex = j
                        };
                }
            }

            throw new Exception("Block not found.");
        }

        public IEnumerable<string> GetLines()
        {
            for (int i = 0; i < _numberOfBlocks; i++)
            {
                var line = $"{i}:";

                for (int j = 0; j < 25; j++)
                {
                    if (_blockSpace[i, j] == EmptySlot)
                        break;

                    line += $" {_blockSpace[i, j]}";
                }

                yield return line;
            }
        }

        public void MoveOnto(
            int sourceBlockNumber,
            int destBlockNumber)
        {
            if (IsMoveNotValid(sourceBlockNumber, destBlockNumber))
                return;

            RemoveSingleBlock(sourceBlockNumber);

            InsertAboveBlock(sourceBlockNumber, destBlockNumber);
        }

        public void PileOnto(
            int sourceBlockNumber,
            int destBlockNumber)
        {
            if (IsMoveNotValid(sourceBlockNumber, destBlockNumber))
                return;

            var removedBlocks = RemoveStack(sourceBlockNumber);

            InsertAboveBlock(removedBlocks, destBlockNumber);
        }

        public void MoveOver(
            int sourceBlockNumber,
            int destBlockNumber)
        {
            if (IsMoveNotValid(sourceBlockNumber, destBlockNumber))
                return;

            RemoveSingleBlock(sourceBlockNumber);

            DropOnTop(sourceBlockNumber, destBlockNumber);
        }

        public void PileOver(
            int sourceBlockNumber,
            int destBlockNumber)
        {
            if (IsMoveNotValid(sourceBlockNumber, destBlockNumber))
                return;

            var removedBlocks = RemoveStack(sourceBlockNumber);

            var deskBlockNumber = FindBlock(destBlockNumber);

            var lastNonEmptyIndex = FindLastNonEmptyIndexForRow(deskBlockNumber.RowIndex);

            for (int i = 0; i < removedBlocks.Length; i++)
            {
                _blockSpace[deskBlockNumber.RowIndex, lastNonEmptyIndex + 1 + i] = removedBlocks[i];
            }
        }

        private bool IsMoveNotValid(
            int sourceBlockNumber,
            int destBlockNumber)
        {
            if (sourceBlockNumber == destBlockNumber)
                return true;

            var sourceLocation = FindBlock(sourceBlockNumber);
            var destLocation = FindBlock(destBlockNumber);

            return sourceLocation.RowIndex == destLocation.RowIndex;
        }

        // **

        private int[] RemoveStack(
            int sourceBlockNumber)
        {
            var sourceLocation = FindBlock(sourceBlockNumber);

            var removedBlocks = new List<int>();

            var i = sourceLocation.ColumnIndex;

            while (_blockSpace[sourceLocation.RowIndex, i] != EmptySlot)
            {
                removedBlocks.Add(_blockSpace[sourceLocation.RowIndex, i]);

                _blockSpace[sourceLocation.RowIndex, i] = EmptySlot;

                i++;
            }

            return removedBlocks.ToArray();
        }

        private void DropOnTop(int sourceBlockNumber, int destBlockNumber)
        {
            var destLocation = FindBlock(destBlockNumber);

            var i = destLocation.ColumnIndex + 1;

            while (_blockSpace[destLocation.RowIndex, i] != EmptySlot)
            {
                i++;
            }

            _blockSpace[destLocation.RowIndex, i] = sourceBlockNumber;
        }

        private void InsertAboveBlock(int sourceBlockNumber, int destBlockNumber)
        {
            var destLocation = FindBlock(destBlockNumber);

            if (_blockSpace[destLocation.RowIndex, destLocation.ColumnIndex + 1] == EmptySlot)
            {
                _blockSpace[destLocation.RowIndex, destLocation.ColumnIndex + 1] = sourceBlockNumber;
            }
            else
            {
                // slide blocks up
                for (int i = 23; i >= (destLocation.ColumnIndex + 1); i--)
                {
                    _blockSpace[destLocation.RowIndex, i + 1] = _blockSpace[destLocation.RowIndex, i];
                }

                _blockSpace[destLocation.RowIndex, destLocation.ColumnIndex + 1] = sourceBlockNumber;
            }
        }

        private void InsertAboveBlock(int[] sourceBlockNumbers, int destBlockNumber)
        {
            var destLocation = FindBlock(destBlockNumber);

            InsertSpace(destLocation.RowIndex, destLocation.ColumnIndex + 1, sourceBlockNumbers.Length);

            for (int i = 0; i < sourceBlockNumbers.Length; i++)
            {
                _blockSpace[destLocation.RowIndex, destLocation.ColumnIndex + 1 + i] = sourceBlockNumbers[i];
            }
        }

        private void InsertSpace(int rowIndex, int startColIndex, int spaceCount)
        {
            for (int i = 24 - spaceCount; i >= startColIndex; i--)
            {
                _blockSpace[rowIndex, i + spaceCount] = _blockSpace[rowIndex, i];

                _blockSpace[rowIndex, i] = EmptySlot;
            }
        }

        private int FindLastNonEmptyIndexForRow(
            int rowIndex)
        {
            for (int i = 24; i >= 0; i--)
            {
                if (_blockSpace[rowIndex, i] != EmptySlot)
                    return i;
            }

            throw new Exception("invalid");
        }

        private void RemoveSingleBlock(int sourceBlockNumber)
        {
            var sourceLocation = FindBlock(sourceBlockNumber);

            if (_blockSpace[sourceLocation.RowIndex, sourceLocation.ColumnIndex + 1] == EmptySlot)
            {
                _blockSpace[sourceLocation.RowIndex, sourceLocation.ColumnIndex] = EmptySlot;
            }
            else
            {
                // slide blocks down

                for (int i = sourceLocation.ColumnIndex; i < 24; i++)
                {
                    _blockSpace[sourceLocation.RowIndex, i] = _blockSpace[sourceLocation.RowIndex, i + 1];
                }
            }
        }

        public override string ToString()
        {
            return string.Join(" | ", GetLines());
        }
    }
}