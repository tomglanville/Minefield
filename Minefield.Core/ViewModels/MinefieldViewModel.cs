namespace Minefield.Core
{
    using Minefield.Core.Models;
    using Minefield.Core.Services;
    using Prism.Commands;

    /// <summary>
    /// Minefield view model.
    /// </summary>
    public class MinefieldViewModel
    {
        /// <summary>
        /// Mine field view model.
        /// </summary>
        /// <param name="gridService">Grid service.</param>
        public MinefieldViewModel(IGridService gridService)
        {
            GridService = gridService;
            MoveCommand = new DelegateCommand<DirectionParam>(Move);

            // Set custom difficulty settings here.
            GridService.Lives = 3;
            GridService.GridDimensions = 8;
            GridService.MineFrequencyPercentage = 10;

            // Setup grid.
            // Toggle showMineLocations to show/hide hints here.
            GridService.Setup(showMineLocations: true);
        }

        /// <summary>
        /// Move command.
        /// </summary>
        public DelegateCommand<DirectionParam> MoveCommand { get; }

        /// <summary>
        /// Grid service.
        /// </summary>
        public IGridService GridService { get; }

        /// <summary>
        /// Move player.
        /// </summary>
        /// <param name="param">The direction.</param>
        public void Move(DirectionParam param)
        {
            GridService.Move(param.MovementDirection);
        }
    }
}