using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project.Commands
{
    public class MoveLeftCommand : ICommand
    {
        private ICell _cell;

        public MoveLeftCommand(ICell cell)
        {
            _cell = cell;
        }

        public void Execute()
        {
            _cell.TargetX -= 1;

            _cell.IsMoving = true;
        }

        public void Undo()
        {
            _cell.TargetX += 1;

            _cell.IsMovingBack = true;
            _cell.IsMoving = true;
        }
    }
}
