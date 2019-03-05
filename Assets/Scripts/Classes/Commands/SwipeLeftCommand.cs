using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Command;

namespace Mathc3Project.Classes.Commands
{
    public class SwipeLeftCommand : ICommand
    {
        private ICell _cell;

        public SwipeLeftCommand(ICell cell)
        {
            _cell = cell;
        }

        public void Execute()
        {
            _cell.TargetX -= 1;
            _cell.CellState = CellStates.Swipe;
            _cell.Move();
        }

        public void Undo()
        {
            _cell.TargetX += 1;
            _cell.CellState = CellStates.Back;
            _cell.Move();
        }
    }
}
