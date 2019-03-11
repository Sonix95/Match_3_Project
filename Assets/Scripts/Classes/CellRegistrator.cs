using Mathc3Project.Classes.Cells;
using Mathc3Project.Interfaces;
using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Classes
{
    public class CellRegistrator : ICellRegistrator
    {
        private INotifier _gameplayNotifier;
        private IUpdateManager _updateManager;

        public CellRegistrator(INotifier gameplayNotifier, IUpdateManager updateManager)
        {
            _gameplayNotifier = gameplayNotifier;
            _updateManager = updateManager;
        }

        public void RegistrateNormalCell(NormalCell cell)
        {
            cell.Notifier = _gameplayNotifier;
            _updateManager.AddUpdatable(cell);
        }
    }
}