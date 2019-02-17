using Mathc3Project.Interfaces;

namespace Mathc3Project.Commands
{
    public class NoMoveCommand : ICommand
    {
        public string Discription  {
            get { return "Fall";}
        }

        public void Execute()
        {
            
        }

        public void Undo()
        {
            
        }
    }
}