using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ICell
    {
        bool IsMoving { get; set; }
        Vector2 Self { get; set; }
        int TargetX { get; set; }
        int TargetY { get; set; }
        int PrevTargetX { get; set; }
        int PrevTargetY { get; set; }
        bool IsMatched{ get; set; }
        string Name { get; set; }
        GameObject CurrentGameObject{ get; set; }

        void Move();
    }
}
