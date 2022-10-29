using NUnit.Framework;
using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;
using System;
using Minesweeper.Core;

namespace Tests
{
    [TestFixture]
    internal class OpenCellGameStatus
    {
        private GameProcessor _processor;

        public OpenCellGameStatus()
        {
        }

        [SetUp]
        public void SetupForGameStatus()
        {
            var testField = new bool[2, 2];
            testField[1, 1] = true;
            _processor = new GameProcessor(testField);
        }


        [Test]
        public void CheckLooseGameStatus_OpenCellWithMine_GameStatusIsLoose()
        {
            var xCoordinate = 1;
            var yCoordinate = 1;
            var gameState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(GameState.Lose, gameState, $"Expected GameState is {GameState.Lose}");

            //if (gameState != GameState.Lose)
            //{
            //    throw new("Game status is Incorrect");
            //}
        }

        [Test]
        public void CheckActiveGameStatus_OpenCellWithNoMine_GameStatusIsActive()
        {
            var xCoordinate = 0;
            var yCoordinate = 1;
            var gameState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(GameState.Active, gameState, $"Expected GameState is {GameState.Active}");
        }


        [Test]
        public void CheckWinGameStatus_OpenAllCellsAllMines_GameStatusWin()
        {
            var xCoordinate = 0;
            var yCoordinate = 0;
            var gameState = _processor.Open(xCoordinate, yCoordinate);
            gameState = _processor.Open(xCoordinate + 1, yCoordinate);
            gameState = _processor.Open(xCoordinate, yCoordinate + 1);

            Assert.AreEqual(GameState.Win, gameState, $"Expected GameState is {GameState.Win}");

        }

        [Test]
        [Ignore("Need to find out how to work with thrown from method exception")]
        public void CheckWinGameStatus_TryToOpenCell_InvalidOperationException()
        {
            var xCoordinate = 0;
            var yCoordinate = 0;
            var gameState = GameState.Win;

            //var fieldState = _processor.Open(xCoordinate, yCoordinate);
            //Assert.Throws<InvalidOperationException>(()=> GameState.Open($"Game status is {GameState.Lose}"));

        }

    }

    [TestFixture]
    internal class GetCurrentFieldStatus
    {
        private GameProcessor _processor;

        private PointState[,] fullFieldState;

        public GetCurrentFieldStatus()
        {
        }

        [SetUp]
        public void SetupForGameStatus()
        {
            var testField = new bool[6, 6];
            testField[1, 1] = true;
            testField[1, 2] = true;
            testField[1, 3] = true;
            testField[1, 5] = true;
            testField[2, 1] = true;
            testField[2, 3] = true;
            testField[3, 1] = true;
            testField[3, 2] = true;
            testField[3, 3] = true;
            _processor = new GameProcessor(testField);
            //_fieldstate = new PointState();
            
        }

        [Test]
        public void CheckPiontStateStatus_OpenCellWithMaxMines_pFieldPointStateClose()
        {
            var xCoordinate = 0;
            var yCoordinate = 0;
            var fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Close, fullFieldState[xCoordinate, yCoordinate]);

        }

        [Test]
        public void CheckPiontStateStatus_OpenCellNoNeighbours_CellPointStateNoNeighbours()
        {
            var xCoordinate = 5;
            var yCoordinate = 5;
            var fullFieldState = _processor.GetCurrentField();
            var fieldState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(PointState.Neighbors0, fullFieldState[xCoordinate, yCoordinate]);

        }

        [Test]
        public void CheckPiontStateStatus_OpenCellOneNeighbour_CellPointStateOneNeighbour()
        {
            var xCoordinate = 0;
            var yCoordinate = 5;
            var fullFieldState = _processor.GetCurrentField();
            var fieldState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(PointState.Neighbors1, fullFieldState[xCoordinate, yCoordinate]);

        }

        [Test]
        public void CheckPiontStateStatus_OpenCellFourNeighbours_CellPointStateFourNeighbours()
        {
            var xCoordinate = 2;
            var yCoordinate = 4;
            var fullFieldState = _processor.GetCurrentField();
            var fieldState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(PointState.Neighbors4, fullFieldState[xCoordinate, yCoordinate]);

        }

        [Test]
        public void CheckPiontStateStatus_OpenCellFiveNeighbours_CellPointStateFiveNeighbours()
        {
            var xCoordinate = 0;
            var yCoordinate = 5;
            var fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Close, fullFieldState[xCoordinate, yCoordinate]);

        }

        [Test]
        public void CheckPiontStateStatus_OpenCellWithMine_CellPointStateMine()
        {
            var xCoordinate = 1;
            var yCoordinate = 1;
            var fullFieldState = _processor.GetCurrentField();
            var fieldState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(PointState.Mine, fullFieldState[xCoordinate, yCoordinate]);

        }

    }
}