using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ICell
    {
        Vector2 Self { get; set; }
        int TargetX { get; set; }
        int TargetY { get; set; }
    }
}
