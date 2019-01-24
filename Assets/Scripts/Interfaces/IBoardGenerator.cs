namespace Mathc3Project
{
    public interface IBorderGenerator
    {
        int BoardColumnCount { get; set; }
        int BoardRowCount { get; set; }

        void CreateEmptyBoard();
    }
}
