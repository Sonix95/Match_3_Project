using Mathc3Project.Interfaces;

namespace Mathc3Project.Commands
{
    public class FallCommand : ICommand
    {
        private ICell _cell;
        private int _shiftYPosition;

        public FallCommand(ICell cell, int shiftYPosition)
        {
            _cell = cell;
            _cell.SetPrevY();
            _shiftYPosition = shiftYPosition;
        }
        
        public string Discription  {
            get { return "Fall";}
        }

        public void Execute()
        {
            _cell.TargetY -= _shiftYPosition;
            
            _cell.IsFall = true;
            _cell.IsMoving = true;
        }

        public void Undo()
        {
            
        }
    }
}