namespace Minefield.Core.Services
{
    using Minefield.Core.Extensions;
    using Minefield.Core.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Grid service.
    /// </summary>
    public class GridService : IGridService
    {
        /// <summary>
        /// The players current position.
        /// </summary>
        private (int positionX, int positionY) _playerPosition = (0, 0);

        /// <summary>
        /// Number of moves.
        /// </summary>
        private int _moves;

        /// <summary>
        /// Gets the player position.
        /// </summary>
        public (int positionX, int positionY) PlayerPosition => _playerPosition;

        /// <summary>
        /// The number of moves.
        /// </summary>
        public int Moves => _moves;

        /// <summary>
        /// How often mines appear as a percentage.
        /// </summary>
        public int MineFrequencyPercentage { get; set; }

        /// <summary>
        /// Number of rows and columns of the grid.
        /// </summary>
        public int GridDimensions { get; set; }

        /// <summary>
        /// List of mine locations.
        /// </summary>
        public IList<(int mineX, int mineY)> MineLocations { get; set; } = new List<(int mineX, int mineY)>();

        /// <summary>
        /// Current number of lives.
        /// </summary>
        public int Lives { get; set; }

        /// <summary>
        /// Game has finished.
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// Grid service.
        /// </summary>
        public GridService()
        {
            // Default settings.
            Lives = 3;
            GridDimensions = 8;
            MineFrequencyPercentage = 10;
        }

        /// <summary>
        /// Setup grid.
        /// </summary>
        /// <param name="showMineLocations">Show mine location spoilers.</param>
        public void Setup(bool showMineLocations)
        {
            Console.WriteLine("Starting game! Grid size " + GridDimensions + "x" + GridDimensions + " - Mine frequency " + MineFrequencyPercentage + "% - Lives:" + Lives);
            SetupMineLocations(showMineLocations);
        }

        /// <summary>
        /// Setup mine locations.
        /// </summary>
        /// <param name="showMineLocations">Show player mine location hints.</param>
        private void SetupMineLocations(bool showMineLocations = false)
        {
            // Loop across grid rows.
            for (int x = 0; x < GridDimensions; x++)
            {
                // Loop down grid column.
                for (int y = 0; y < GridDimensions; y++)
                {
                    // Randomly add a mine based on frequency.
                    var isMine = new Random().Next(0, 100) < MineFrequencyPercentage;

                    if (isMine)
                    {
                        // Add to mine locations.
                        MineLocations.Add((x, y));

                        // Show hint to player.
                        if (showMineLocations)
                            Console.WriteLine("Spoiler: Added mine at (" + x + "," + y + ")");
                    }
                }
            }
        }

        /// <summary>
        /// Move the player.
        /// </summary>
        /// <param name="movementDirection">Movement direction.</param>
        /// <returns>Whether player moved.</returns>
        public bool Move(MovementDirection movementDirection)
        {
            if (GameOver)
                return false;

            var canMove = false;

            // Ensure player doesn't move outside of grid.
            switch (movementDirection)
            {
                case MovementDirection.Up:
                    canMove = (_playerPosition.positionY - 1) >= 0;

                    // Update player positon.
                    if (canMove)
                        _playerPosition.positionY--;
                    break;

                case MovementDirection.Down:
                    canMove = (_playerPosition.positionY + 1) <= GridDimensions;

                    // Update player positon.
                    if (canMove)
                        _playerPosition.positionY++;
                    break;

                case MovementDirection.Left:
                    canMove = (_playerPosition.positionX - 1) >= 0;

                    // Update player positon.
                    if (canMove)
                        _playerPosition.positionX--;
                    break;

                case MovementDirection.Right:
                    canMove = (_playerPosition.positionX + 1) <= GridDimensions;

                    // Update player positon.
                    if (canMove)
                        _playerPosition.positionX++;
                    break;
            }

            if (canMove)
            {
                // Add to moves.
                _moves++;

                // Check win/lose conditions.
                CheckForWinConditionOrMineHit(movementDirection);
                return true;
            }
            else
            {
                // Warn player.
                Console.WriteLine("Unable to move off grid! Current position (" + _playerPosition.positionX + ", " + _playerPosition.positionY + ")");
                return false;
            }
        }

        /// <summary>
        /// Check if player has reached the end of grid or if they have hit a mine.
        /// </summary>
        /// <param name="movementDirection">Movement direction.</param>
        private void CheckForWinConditionOrMineHit(MovementDirection movementDirection)
        {
            // Player has reached the end of the grid.
            var gameWon = _playerPosition.positionX >= GridDimensions;

            if (gameWon)
            {
                var livesText = Lives > 1 ? "lives" : "life";
                Console.WriteLine("Congratulations you reached the end of the grid in " + Moves + " moves! You had " + Lives + " " + livesText + " left.");
                GameOver = true;
                return;
            }

            // Does mine locations contain player position.
            var mineHit = MineLocations.Contains(_playerPosition);

            Console.WriteLine("Pressed " + movementDirection.ToArrowSymbol() + " Current position (" + _playerPosition.positionX + ", " + _playerPosition.positionY + ") - Lives " + Lives + " - Moves " + Moves);

            if (mineHit)
            {
                LoseLife();
            }
        }

        /// <summary>
        /// Player has lost a life.
        /// </summary>
        private void LoseLife()
        {
            var message = "Oops you hit a mine and lost a life!";

            // Remove a life.
            Lives--;

            if (Lives <= 0)
            {
                GameOver = true;
                Console.WriteLine(message);
                Console.WriteLine("GAME OVER!");
            }
            else
            {
                Console.WriteLine(message + " Remaining lives: " + Lives);
            }
        }
    }
}
