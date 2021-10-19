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
            switch (md)
            {
                case MovementDirection.Up:
                    return "↑";

                case MovementDirection.Down:
                    return "↓";

                case MovementDirection.Left:
                    return "←";

                case MovementDirection.Right:
                    return "→";

                default:
                    return string.Empty;
            }
        }
    }
}
