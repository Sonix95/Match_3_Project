public interface IBorderGenerator
{
    int BoardRowCount { get; set; }
    int BoardColumnCount { get; set; }    

    void CreateEmptyBoard();
    
}
