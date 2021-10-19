using Minefield.Core.Models;

namespace Minefield.Core.Extensions
{
    /// <summary>
    /// Movement direction extensions.
    /// </summary>
    public static class MovementDirectionExtensions
    {
        /// <summary>
        /// Converts the movement direction into the corresponding arrow symbol.
        /// </summary>
        /// <param name="md">Movement direction.</param>
        /// <returns>Arrow symbol.</returns>
        public static string ToArrowSymbol(this MovementDirection md)
        {
            return md switch
            {
                MovementDirection.Up => "↑",
                MovementDirection.Down => "↓",
                MovementDirection.Left => "←",
                MovementDirection.Right => "→",
                _ => string.Empty,
            };
        }
    }
}
