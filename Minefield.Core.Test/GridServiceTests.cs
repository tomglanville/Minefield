namespace Minefield.Core.Test
{
    using Minefield.Core.Models;
    using Minefield.Core.Services;
    using NUnit.Framework;

    public class GridServiceTests
    {
        IGridService _gridService;

        [SetUp]
        public void Setup()
        {
            _gridService = new GridService();
        }

        [Test]
        [TestCase(MovementDirection.Left, 0, 0)]
        [TestCase(MovementDirection.Up, 0, 0)]
        public void WhenOnGridEdge_PlayerDoesntMove(MovementDirection movementDirection, int endX, int endY)
        {
            // Arrange.
            _gridService.Setup(false);
            var startPosition = _gridService.PlayerPosition;

            // Act
            _gridService.Move(movementDirection);

            // Assert.
            Assert.AreEqual(startPosition, (endX, endY));
        }

        [Test]
        [TestCase(MovementDirection.Right, 1, 0)]
        [TestCase(MovementDirection.Down, 0, 1)]
        public void WhenGridSpaceAvailable_PlayerMovesInCorrectDirection(MovementDirection movementDirection, int endX, int endY)
        {
            // Arrange.
            _gridService.Setup(false);
            _gridService.MineLocations = new System.Collections.Generic.List<(int, int)>();

            // Act
            _gridService.Move(movementDirection);

            // Assert.
            Assert.AreEqual(_gridService.PlayerPosition, (endX, endY));
        }

        [Test]
        [TestCase(MovementDirection.Right, 1, 0)]
        [TestCase(MovementDirection.Down, 0, 1)]
        public void WhenMoveOntoMine_LifeIsLost(MovementDirection movementDirection, int mineX, int mineY)
        {
            // Arrange.
            _gridService.Lives = 3;
            _gridService.Setup(false);
            _gridService.MineLocations.Clear();
            _gridService.MineLocations.Add((mineX, mineY));

            // Act
            _gridService.Move(movementDirection);

            // Assert.
            Assert.AreEqual(_gridService.Lives, 2);
        }

        [Test]
        [TestCase(MovementDirection.Right)]
        [TestCase(MovementDirection.Down)]
        public void WhenMoveOnNoMines_NoLivesAreLost(MovementDirection movementDirection)
        {
            // Arrange.
            _gridService.GridDimensions = 8;
            _gridService.Lives = 1;
            _gridService.Setup(false);
            _gridService.MineLocations = new System.Collections.Generic.List<(int, int)>();

            // Act
            _gridService.Move(movementDirection);
            _gridService.Move(movementDirection);
            _gridService.Move(movementDirection);
            _gridService.Move(movementDirection);

            // Assert.
            Assert.AreEqual(_gridService.Lives, 1);
        }

        [Test]
        public void WhenAllLifeLost_GameOver()
        {
            // Arrange.
            _gridService.GridDimensions = 8;
            _gridService.Lives = 3;
            _gridService.Setup(false);
            _gridService.MineLocations.Clear();
            _gridService.MineLocations.Add((0, 1));
            _gridService.MineLocations.Add((0, 2));
            _gridService.MineLocations.Add((0, 3));

            // Act
            _gridService.Move(MovementDirection.Down);
            _gridService.Move(MovementDirection.Down);
            _gridService.Move(MovementDirection.Down);

            // Assert.
            Assert.IsTrue(_gridService.Lives == 0);
            Assert.IsTrue(_gridService.GameOver);
        }

        [Test]
        public void WhenAtEdgeOfGrid_GameOver()
        {
            // Arrange.
            _gridService.GridDimensions = 2;
            _gridService.Setup(false);
            _gridService.MineLocations.Clear();

            // Act
            _gridService.Move(MovementDirection.Right);
            _gridService.Move(MovementDirection.Right);

            // Assert.
            Assert.IsTrue(_gridService.GameOver);
        }

        [Test]
        public void WhenPlayerMoves_MoveCountShouldIncrease()
        {
            // Arrange.
            _gridService.GridDimensions = 8;
            _gridService.Setup(false);
            _gridService.MineLocations.Clear();

            // Act
            _gridService.Move(MovementDirection.Right);
            _gridService.Move(MovementDirection.Right);
            _gridService.Move(MovementDirection.Down);
            _gridService.Move(MovementDirection.Down);
            _gridService.Move(MovementDirection.Right);
            _gridService.Move(MovementDirection.Up);

            // Assert.
            Assert.AreEqual(_gridService.Moves, 6);
        }
    }
}