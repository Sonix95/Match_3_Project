using Mathc3Project.Enums;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Command;

namespace Mathc3Project.Classes.Commands
{
    public class FallCommand : ICommand
    {
        private readonly ICell _cell;

        public FallCommand(ICell cell)
        {
            _cell = cell;
        }
        public void Execute()
        {
            _cell.CellState = CellStates.Fall;  
            _cell.Move();
        }

        public void Undo()
        {
            // -- no implementation -- //
        }
        
    }
}
