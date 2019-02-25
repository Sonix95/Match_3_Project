using Mathc3Project.Interfaces;

namespace Mathc3Project.Commands
{
    public class FallCommand : ICommand
    {
        private readonly ICell _cell;
        private readonly int _newTargetY;
        private readonly IBoard _board;

        public FallCommand(ICell cell, int newTargetY, IBoard board)
        {
            _cell = cell;
            _newTargetY = newTargetY;
            _board = board;
        }
        public void Execute()
        {
            _cell.TargetY = _newTargetY;
            _board.Cells[_cell.TargetX, _cell.TargetY] = _cell;
            _cell.IsFall = true;
            _cell.IsMoving = true;
        }

        public void Undo()
        {
            // ----
        }
    }
}