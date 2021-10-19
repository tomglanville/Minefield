namespace Minefield.Core.Test
{
    using Minefield.Core.Models;
    using Minefield.Core.Services;
    using Moq;
    using NUnit.Framework;

    public class MinefieldViewModelTests
    {
        Mock<IGridService> _mockGridService;

        MinefieldViewModel _viewmodel;

        [SetUp]
        public void Setup()
        {
            _mockGridService = new Mock<IGridService>(MockBehavior.Default);
            _viewmodel = new MinefieldViewModel(_mockGridService.Object);
        }

        [Test]
        public void WhenViewModelIsCreated_MoveCommandShouldNotBeNull()
        {
            Assert.IsNotNull(_viewmodel.MoveCommand, "MoveCommand is null");
        }

        [Test]
        [TestCase(MovementDirection.Up)]
        [TestCase(MovementDirection.Down)]
        [TestCase(MovementDirection.Left)]
        [TestCase(MovementDirection.Right)]
        public void WhenMoveCommandCalled_GridServiceShouldCallMoveOnce(MovementDirection md)
        {
            // Arrange.

            // Act.
            _viewmodel.MoveCommand.Execute(new DirectionParam(md));

            // Assert.
            _mockGridService.Verify((m => m.Move(md)), Times.Once);
        }
    }
}