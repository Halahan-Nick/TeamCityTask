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
        private GameProcessor _processor;

        public GameProcessorTest()
        {
        }

        [OneTimeSetUp]
        public void SetupForGameStatus()
        {
            var testField = new bool[2, 2];
            testField[1, 1] = true;
            _processor = new GameProcessor(testField);
        }

        [OneTimeTearDown]
        public void PostCondition()
        {
            _processor = null;
        }


        [Test]
        public void CheckLooseGameStatus_OpenCellWithMine_GameStatusIsLoose()
        {
            var xCoordinate = 1;
            var yCoordinate = 1;
            var fieldState = _processor.Open(xCoordinate, yCoordinate);

            if (fieldState != GameState.Lose)
            {
                throw new("Game status is Incorrect");
            }
        }

        [Test]
        public void CheckActiveGameStatus_OpenCellWithNoMine_GameStatusIsActive()
        {
            var xCoordinate = 0;
            var yCoordinate = 1;
            var fieldState = _processor.Open(xCoordinate, yCoordinate);

            if (fieldState != GameState.Active)
            {
                throw new("Game status is Incorrect");
            }
        }


        [Test]
        public void CheckWinGameStatus_OpenAllCellsAllMines_GameStatusWin()
        {
            var xCoordinate = 0;
            var yCoordinate = 0;
            var fieldState = _processor.Open(xCoordinate, yCoordinate);
                fieldState = _processor.Open(xCoordinate+1, yCoordinate);
                fieldState = _processor.Open(xCoordinate, yCoordinate+1);

            if (fieldState != GameState.Win)
            {
                throw new("Game status is Incorrect");
            }
        }
        [Test]
        public void CheckWinGameStatus_FindAllMines_GameStatusWin()
        {
            var xCoordinate = 0;
            var yCoordinate = 1;
            var fieldState = _processor.Open(xCoordinate, yCoordinate);

            if (fieldState != GameState.Win)
            {
                throw new("Game status is Incorrect");
            }
        }

    }
}