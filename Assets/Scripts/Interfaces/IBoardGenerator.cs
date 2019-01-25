namespace Mathc3Project
{
    public interface IBorderGenerator
    {
        int BoardColumnCount { get; set; }
        int BoardRowCount { get; set; }
        float CellSize { get; set; }
        bool FillFakeElements { get; set; }

        void CreateEmptyBoard();
    }
}
