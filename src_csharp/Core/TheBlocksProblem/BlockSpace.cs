using System;
using System.Collections;
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
            var destLocation = FindBlock(destBlockNumber);

            RemoveBlock(sourceBlockNumber);

            InsertBlock(sourceBlockNumber, destLocation);
        }

        private void InsertBlock(int sourceBlockNumber, BlockLocation destLocation)
        {
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

        private void RemoveBlock(int sourceBlockNumber)
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
    }
}