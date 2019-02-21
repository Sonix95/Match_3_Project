using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project.Commands
{
    public class MoveUpCommand : ICommand
    {
        private ICell _cell;

        public MoveUpCommand(ICell cell)
        {
            _cell = cell;
            _cell.SetPrevY();
        }

        public void Execute()
        {
            _cell.TargetY += 1;

            _cell.IsMoving = true;
        }

        public void Undo()
        {
            _cell.TargetY -= 1;

            _cell.IsMovingBack = true;
            _cell.IsMoving = true;
        }
    }
}
