using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
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
            
            _cell.Move();
        }

    }
}