using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class GameProcessorTest
    {
        [Test]

        public GameState Open(int x, int y)
        {
            if (GameState != GameState.Active)
                throw new InvalidOperationException($"Game status is {GameState}");

            var targetCell = _field[y, x];

            if (targetCell.IsOpen)
                return GameState;

            targetCell.IsOpen = true;

            if (targetCell.IsMine)
            {
                GameState = GameState.Lose;
            }
            else
            {
                for (var row = Math.Max(0, y - 1); row <= Math.Min(_field.GetLength(0) - 1, y + 1); row++)
                {
                    for (var column = Math.Max(0, x - 1); column <= Math.Min(_field.GetLength(1) - 1, x + 1); column++)
                    {
                        Point neighbor = _field[row, column];

                        if (neighbor.IsMine)
                        {
                            targetCell.MineNeighborsCount++;
                        }
                    }
                }

                if (targetCell.MineNeighborsCount == 0)
                {
                    for (var row = Math.Max(0, y - 1); row <= Math.Min(_field.GetLength(0) - 1, y + 1); row++)
                    {
                        for (var column = Math.Max(0, x - 1); column <= Math.Min(_field.GetLength(1) - 1, x + 1); column++)
                        {
                            Open(column, row);
                        }
                    }
                }

                openCount++;

                if (openCount + mineCount == totalCount)
                {
                    GameState = GameState.Win;
                }
            }

            return GameState;
        }
        public void FirstTest_CheckOpen_IsOpened()
        {
            var field = FieldGenerator.GetRandomField(settings.Width, settings.Height, settings.Mines);

            var gameProcessor = new GameProcessor(field);

            var currentField = gameProcessor.GetCurrentField();
            throw new Exception("Error");
        }

    }
}
