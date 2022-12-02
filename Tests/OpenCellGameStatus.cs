using NUnit.Framework;
using Minesweeper.Core.Enums;
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


        [TestCase(1, 1)]
        public void CheckLooseGameStatus_OpenCellWithMine_GameStatusIsLoose(int xCoordinate, int yCoordinate)
        {
            var gameState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(GameState.Lose, gameState, $"Expected GameState is {GameState.Lose}");
        }

        [TestCase(0, 1)]
        public void CheckActiveGameStatus_OpenCellWithNoMine_GameStatusIsActive(int xCoordinate, int yCoordinate)
        {
            var gameState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(GameState.Active, gameState, $"Expected GameState is {GameState.Active}");
        }


        [TestCase(0, 0)]
        public void CheckWinGameStatus_OpenAllCellsAllMines_GameStatusWin(int xCoordinate, int yCoordinate)
        {

            var gameState = _processor.Open(xCoordinate, yCoordinate);
            gameState = _processor.Open(xCoordinate + 1, yCoordinate);
            gameState = _processor.Open(xCoordinate, yCoordinate + 1);

            Assert.AreEqual(GameState.Win, gameState, $"Expected GameState is {GameState.Win}");

        }

        [TestCase(0, 0)]
        //[Ignore("Need to find out how to work with thrown from method exception")]
        public void CheckWinGameStatus_TryToOpenCell_InvalidOperationException(int xCoordinate, int yCoordinate)
        {
            var gameState = _processor.Open(xCoordinate, yCoordinate);
            gameState = _processor.Open(xCoordinate + 1, yCoordinate);
            gameState = _processor.Open(xCoordinate, yCoordinate + 1);
            // gameState = _processor.Open(xCoordinate, yCoordinate);

            Assert.Throws<InvalidOperationException>(() => _processor.Open(xCoordinate, yCoordinate));

        }

    }

    [TestFixture]
    internal class GetCurrentFieldStatus
    {
        private GameProcessor _processor;

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

        }

        [TestCase(0, 0)]
        public void CheckPiontStateStatus_CheckStatusOfClosedCell_FieldPointStateClose(int xCoordinate, int yCoordinate)
        {
            var fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Close, fullFieldState[xCoordinate, yCoordinate]);

        }

        [TestCase(5, 5)]
        public void CheckPiontStateStatus_OpenCellNoNeighbours_CellPointStateNoNeighbours(int xCoordinate, int yCoordinate)
        {

            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors0, fullFieldState[xCoordinate, yCoordinate]);

        }

        [TestCase(5, 0)]
        public void CheckPiontStateStatus_OpenCellOneNeighbour_CellPointStateOneNeighbour(int xCoordinate, int yCoordinate)
        {
            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors1, fullFieldState[yCoordinate, xCoordinate]);

        }

        [TestCase(4, 2)]
        public void CheckPiontStateStatus_OpenCellFourNeighbours_CellPointStateFourNeighbours(int xCoordinate, int yCoordinate)
        {

            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors4, fullFieldState[yCoordinate, xCoordinate]);

        }

        [TestCase(2, 2)]
        public void CheckPiontStateStatus_OpenCellMaxNeighbours_CellPointStateEightNeighbours(int xCoordinate, int yCoordinate)
        {
            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors8, fullFieldState[yCoordinate, xCoordinate]);

        }

        [TestCase(5, 1)]
        public void CheckPiontStateStatus_OpenCellWithMine_CellPointStateMine(int xCoordinate, int yCoordinate)
        {
            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Mine, fullFieldState[yCoordinate, xCoordinate]);

        }

    }

    [TestFixture]
    internal class GetRectangleFieldStatus
    {
        private GameProcessor _processor;

        public GetRectangleFieldStatus()
        {
        }

        [SetUp]
        public void SetupForGameStatus()
        {
            var testField = new bool[3, 10];
            testField[0, 0] = true;
            testField[0, 1] = true;
            testField[0, 2] = true;
            testField[1, 1] = true;
            testField[2, 0] = true;
            testField[2, 1] = true;
            testField[2, 2] = true;

            _processor = new GameProcessor(testField);
        }

        [TestCase(1, 0)]
        public void CheckPiontStateStatus_OpenCellFiveNeighbours_CellPointStateFiveNeighbours(int xCoordinate, int yCoordinate)
        {
            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors5, fullFieldState[xCoordinate, yCoordinate]);
        }
    }



}