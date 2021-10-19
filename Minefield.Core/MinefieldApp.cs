namespace Minefield.Core
{
    using Minefield.Core.Models;
    using System;

    /// <summary>
    /// Minefield app.
    /// </summary>
    public class MinefieldApp
    {
        /// <summary>
        /// View model.
        /// </summary>
        private MinefieldViewModel ViewModel { get; }

        /// <summary>
        /// Minefield app.
        /// </summary>
        /// <param name="viewModel">Minefield viewmodel.</param>
        public MinefieldApp(MinefieldViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        /// <summary>
        /// Run the app and handle user inputs.
        /// </summary>
        public void Run()
        {
            // Loop and read player key entries.
            while (true)
            {
                // Intercept to prevent key display.
                var key = Console.ReadKey(false).Key;

                // Call move command when a movement arrow is pressed.
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        ViewModel.MoveCommand.Execute(new DirectionParam(MovementDirection.Up));
                        break;

                    case ConsoleKey.DownArrow:
                        ViewModel.MoveCommand.Execute(new DirectionParam(MovementDirection.Down));
                        break;

                    case ConsoleKey.LeftArrow:
                        ViewModel.MoveCommand.Execute(new DirectionParam(MovementDirection.Left));
                        break;

                    case ConsoleKey.RightArrow:
                        ViewModel.MoveCommand.Execute(new DirectionParam(MovementDirection.Right));
                        break;
                }
            }
        }
    }
}