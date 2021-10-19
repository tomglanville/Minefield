namespace Minefield.Core.Services
{
    using Minefield.Core.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Grid service interface.
    /// </summary>
    public interface IGridService
    {
        /// <summary>
        /// Move the player by the movement direction.
        /// </summary>
        /// <param name="movementDirection">Movement direction.</param>
        /// <returns>Whether the player moved.</returns>
        bool Move(MovementDirection movementDirection);

        /// <summary>
        /// Setup the grid.
        /// </summary>
        /// <param name="showMineLocations">Show mine locations.</param>
        void Setup(bool showMineLocations);

        /// <summary>
        /// Mine locations.
        /// </summary>
        List<(int, int)> MineLocations { get; set; }

        /// <summary>
        /// Player lives.
        /// </summary>
        int Lives { get; set; }

        /// <summary>
        /// The number of moves.
        /// </summary>
        public int Moves { get; }

        /// <summary>
        /// Number of rows and columns of the grid.
        /// </summary>
        int GridDimensions { get; set; }

        /// <summary>
        /// How often mines appear as a percentage.
        /// </summary>
        int MineFrequencyPercentage { get; set; }

        /// <summary>
        /// Player position.
        /// </summary>
        (int, int) PlayerPosition { get; }

        /// <summary>
        /// Indicates whether the game has finished.
        /// </summary>
        bool GameOver { get; set; }
    }
}