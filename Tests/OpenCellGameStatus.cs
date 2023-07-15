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

        //Commented ROW for TeamCity Run check


        [TestCase(1, 1)]
        public void T1_CheckLooseGameStatus_OpenCellWithMine_GameStatusIsLoose(int xCoordinate, int yCoordinate)
        {
            var gameState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(GameState.Lose, gameState, $"Expected GameState is {GameState.Lose}");
        }

        [TestCase(0, 1)]
        public void T2_CheckActiveGameStatus_OpenCellWithNoMine_GameStatusIsActive(int xCoordinate, int yCoordinate)
        {
            var gameState = _processor.Open(xCoordinate, yCoordinate);

            Assert.AreEqual(GameState.Active, gameState, $"Expected GameState is {GameState.Active}");
        }


        [TestCase(0, 0)]
        public void T3_CheckWinGameStatus_OpenAllCellsAllMines_GameStatusWin(int xCoordinate, int yCoordinate)
        {

            var gameState = _processor.Open(xCoordinate, yCoordinate);
            gameState = _processor.Open(xCoordinate + 1, yCoordinate);
            gameState = _processor.Open(xCoordinate, yCoordinate + 1);

            Assert.AreEqual(GameState.Win, gameState, $"Expected GameState is {GameState.Win}");

        }

        [TestCase(0, 0)]
        //[Ignore("Need to find out how to work with thrown from method exception")]
        public void T4_CheckWinGameStatus_TryToOpenCell_InvalidOperationException(int xCoordinate, int yCoordinate)
        {
            var gameState = _processor.Open(xCoordinate, yCoordinate);
            gameState = _processor.Open(xCoordinate + 1, yCoordinate);
            gameState = _processor.Open(xCoordinate, yCoordinate + 1);
            // gameState = _processor.Open(xCoordinate, yCoordinate);

            Assert.Throws<InvalidOperationException>(() => _processor.Open(xCoordinate, yCoordinate));

        }

        //[Test]
        //public void Failed()
        //{
        //    throw new Exception();
        //}

        //[Test]
        //public void Flaky1()
        //{
        //    if (DateTime.Now.Millisecond % 2 == 0)
        //    throw new Exception();
        //}

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
        public void T5_CheckPiontStateStatus_CheckStatusOfClosedCell_FieldPointStateClose(int xCoordinate, int yCoordinate)
        {
            var fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Close, fullFieldState[xCoordinate, yCoordinate]);

        }

        [TestCase(5, 5)]
        public void T6_CheckPiontStateStatus_OpenCellNoNeighbours_CellPointStateNoNeighbours(int xCoordinate, int yCoordinate)
        {

            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors0, fullFieldState[xCoordinate, yCoordinate]);

        }

        [TestCase(5, 0)]
        public void T7_CheckPiontStateStatus_OpenCellOneNeighbour_CellPointStateOneNeighbour(int xCoordinate, int yCoordinate)
        {
            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors1, fullFieldState[yCoordinate, xCoordinate]);

        }

        [TestCase(4, 2)]
        public void T8_CheckPiontStateStatus_OpenCellFourNeighbours_CellPointStateFourNeighbours(int xCoordinate, int yCoordinate)
        {

            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors4, fullFieldState[yCoordinate, xCoordinate]);

        }

        [TestCase(2, 2)]
        public void T9_CheckPiontStateStatus_OpenCellMaxNeighbours_CellPointStateEightNeighbours(int xCoordinate, int yCoordinate)
        {
            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors8, fullFieldState[yCoordinate, xCoordinate]);

        }

        [TestCase(5, 1)]
        public void T10_CheckPiontStateStatus_OpenCellWithMine_CellPointStateMine(int xCoordinate, int yCoordinate)
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
        public void T11_CheckPiontStateStatus_OpenCellFiveNeighbours_CellPointStateFiveNeighbours(int xCoordinate, int yCoordinate)
        {
            var fullFieldState = _processor.GetCurrentField();
            _processor.Open(xCoordinate, yCoordinate);
            fullFieldState = _processor.GetCurrentField();

            Assert.AreEqual(PointState.Neighbors5, fullFieldState[xCoordinate, yCoordinate]);
        }

    }



}