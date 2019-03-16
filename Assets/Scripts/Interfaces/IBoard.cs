using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Observer;

namespace Mathc3Project.Interfaces
{
    public interface IBoard
    {
        int Width { get; set; }
        int Height { get; set; }
        ICell[,] Cells { get; set; }
        ICellRegistrator CellRegistrator { get; set; }

        void Initial();
    }
}
