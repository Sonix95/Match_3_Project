using System;
using System.Collections.Generic;
using Mathc3Project.Interfaces;

namespace Mathc3Project.Commands
{
    public class MacroCommand : ICommand
    {
        private ICommand[] _commands;

        public MacroCommand(ICommand[] commands)
        {
            _commands = commands;
        }
        
        public void Execute()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            foreach (var command in _commands)
            {
                command.Undo();
            }
        }
    }
}