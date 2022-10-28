using NUnit.Framework;
using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;
using System;
using Minesweeper.Core;

namespace Tests
{
    [TestFixture]
    internal class GetCurrentField
    {
        private GameProcessor _processor;

        public GetCurrentField()
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

            Assert.AreEqual( GameState.Lose, gameState, $"Expected GameState is {GameState.Lose}");

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
}