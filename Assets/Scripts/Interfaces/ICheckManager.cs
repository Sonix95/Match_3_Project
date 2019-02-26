namespace Mathc3Project.Interfaces
{
    public interface ICheckManager
    {
        IBoard Board { get; set; }

        void CheckCell(ICell cell);
        bool SimpleCheck(ICell cell);
    }
}