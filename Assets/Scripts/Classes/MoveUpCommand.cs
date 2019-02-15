using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
{
    public class MoveUpCommand : ICommand
    {
        private ICell _cell;

        public MoveUpCommand(ICell cell)
        {
            _cell = cell;
        }
        
        public void Execute()
        {
            _cell.TargetY += 1;
            _cell.IsMoving = true;
            
            _cell.Move();
        }

    }
}