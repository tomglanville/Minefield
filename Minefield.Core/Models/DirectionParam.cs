namespace Minefield.Core.Models
{
    /// <summary>
    /// Direction param.
    /// </summary>
    public class DirectionParam
    {
        /// <summary>
        /// Direction param.
        /// </summary>
        /// <param name="movementDirection">Movement direction.</param>
        public DirectionParam(MovementDirection movementDirection)
        {
            MovementDirection = movementDirection;
        }

        /// <summary>
        /// Movement direction.
        /// </summary>
        public MovementDirection MovementDirection { get; }
    }
}