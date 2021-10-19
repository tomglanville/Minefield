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
        /// Gives the player some hints!
        /// </summary>
        private bool _showMineLocations;

        /// <summary>
        /// The players current position coordinates.
        /// </summary>
        private (int, int) _playerPosition = (0, 0);

        /// <summary>
        /// Number of moves.
        /// </summary>
        private int _moves;

        /// <summary>
        /// Gets the player position.
        /// </summary>
        public (int, int) PlayerPosition => _playerPosition;

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
        /// List of mine location coordinates
        /// </summary>
        public List<(int, int)> MineLocations { get; set; } = new List<(int, int)>();

        /// <summary>
        /// Current number of lives.
        /// </summary>
        public int Lives { get; set; }

        /// <summary>
        /// Game has finished.
        /// </summary>
        public bool GameOver { get; set; }

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
            _showMineLocations = showMineLocations;

            SetupMineLocations();
        }

        /// <summary>
        /// Setup mine locations.
        /// </summary>
        private void SetupMineLocations()
        {
            // Loop across grid rows.
            for (int i = 0; i < GridDimensions; i++)
            {
                // Loop down grid column.
                for (int j = 0; j < GridDimensions; j++)
                {
                    // Randomly add a mine based on frequency.
                    var isMine = new Random().Next(0, 100) < MineFrequencyPercentage;

                    if (isMine)
                    {
                        // Add to mine locations.
                        MineLocations.Add((i, j));

                        // Show hint to player.
                        if (_showMineLocations)
                            Console.WriteLine("Spoiler: Added mine at (" + i + "," + j + ")");
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

            var canMove = true;

            // Ensure player doesn't move outside of grid.
            switch (movementDirection)
            {
                case MovementDirection.Up:
                    canMove = (_playerPosition.Item2 - 1) >= 0;

                    // Update player positon.
                    if (canMove)
                        _playerPosition.Item2--;
                    break;

                case MovementDirection.Down:
                    canMove = (_playerPosition.Item2 + 1) <= GridDimensions;

                    // Update player positon.
                    if (canMove)
                        _playerPosition.Item2++;
                    break;

                case MovementDirection.Left:
                    canMove = (_playerPosition.Item1 - 1) >= 0;

                    // Update player positon.
                    if (canMove)
                        _playerPosition.Item1--;
                    break;

                case MovementDirection.Right:
                    canMove = (_playerPosition.Item1 + 1) <= GridDimensions;

                    // Update player positon.
                    if (canMove)
                        _playerPosition.Item1++;
                    break;
            }

            if (canMove)
            {
                _moves++;

                // Check win/lose conditions.
                CheckForWinConditionOrMineHit(movementDirection);
                return true;
            }
            else
            {
                // Warn player.
                Console.WriteLine("Unable to move off grid! Current position (" + _playerPosition.Item1 + ", " + _playerPosition.Item2 + ")");
                return false;
            }
        }

        /// <summary>
        /// Check if player has reached the end of grid or if they have hit a mine.
        /// </summary>
        /// <param name="movementDirection">Movement direction.</param>
        private void CheckForWinConditionOrMineHit(MovementDirection movementDirection)
        {
            var gameWon = _playerPosition.Item1 >= GridDimensions;

            if (gameWon)
            {
                var livesText = Lives > 1 ? "lives" : "life";
                Console.WriteLine("Congratulations you reached the end of the grid in " + Moves + " moves! You had " + Lives + " " + livesText + " left.");
                GameOver = true;
                return;
            }

            // Mine locations contains player location.
            var mineHit = MineLocations.Contains(_playerPosition);

            Console.WriteLine("Pressed " + movementDirection.ToArrowSymbol() + " Current position (" + _playerPosition.Item1 + ", " + _playerPosition.Item2 + ") - Lives " + Lives + " - Moves " + Moves);

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
            Lives--;

            var message = "Oops you hit a mine and lost a life!";

            if (Lives == 0)
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
