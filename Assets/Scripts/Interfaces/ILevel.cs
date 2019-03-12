using Mathc3Project.Classes;

namespace Mathc3Project.Interfaces
{
    public interface ILevel
    {
        int LocationId { get; set; }
        int LevelId { get; set; }
        ILevelTask[] LevelTasks { get; set; }
        int BoardWidth { get; set; }
        int BoardHeight { get; set; }
    }
}