using NUnit.Framework;
using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;
using System;
using Minesweeper.Core;

namespace Tests
{
    [TestFixture]
    internal class GameProcessorTest
    {
        [Test]
        public void CheckLooseGameStatus_OpenCellWithMine_GameStatusIsLoose()
        {
            var testField = new bool[2, 2];
            testField[1, 1] = true;

            var gameProcessor = new GameProcessor(testField);

            var xCoordinate = 1;
            var yCoordinate = 1;
            var fieldState = gameProcessor.Open(xCoordinate, yCoordinate);

            if (fieldState != GameState.Lose)
            {
                throw new("Game status is Incorrect");
            }
        }

        [Test]
        public void CheckActiveGameStatus_OpenCellWithNoMine_GameStatusIsActive()
        {
            var testField = new bool[2, 2];
            testField[2, 2] = true;
            testField[1, 2] = true;

            var gameProcessor = new GameProcessor(testField);

            var xCoordinate = 1;
            var yCoordinate = 1;
            var fieldState = gameProcessor.Open(xCoordinate, yCoordinate);

            if (fieldState != GameState.Active)
            {
                throw new("Game status is Incorrect");
            }
        }
        [Test]
        public void CheckWinGameStatus_OpenCellWithMine_GameStatusWin()
        {
            var testField = new bool[1, 2];
            testField[0, 1] = true;

            var gameProcessor = new GameProcessor(testField);

            var xCoordinate = 0;
            var yCoordinate = 1;
            var fieldState = gameProcessor.Open(xCoordinate, yCoordinate);

            if (fieldState != GameState.Win)
            {
                throw new("Game status is Incorrect");
            }
        }

        //if (fieldState == GameState.Active)
        //{
        //    throw new($"Game status is {fieldState}");
        //}

        //if (fieldState == GameState.Win)
        //{
        //    throw new($"Game status is {fieldState}");
        //}

    }
}