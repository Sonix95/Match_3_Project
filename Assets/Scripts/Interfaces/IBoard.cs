namespace Mathc3Project.Interfaces
{
    public interface IBoard
    {
        int Width { get; set; }
        int Height { get; set; }
        ICell[,] Cells { get; set; }
        ISpawnManager SpawnManager { get; set; }
        bool[,] HollowCells { get; set; }
    }
}
