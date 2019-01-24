public interface IBoard
{
    int BoardRowCount { get; set; }
    int BoardColumnCount { get; set; }

    void InitilalBoard();
}
