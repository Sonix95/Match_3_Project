using UnityEngine;

namespace Mathc3Project
{
    public interface ICell
    {
        float Speed { get; set; }
        bool CanFall { get; set; }
        IBoard Board { get; set; }
        Vector3 EndPosition { get; set; }
        int SelfColomn { get; set; }
        int SelfRow { get; set; }
        float Gravity { get; set; }

        void MoveDown();
        void CheckDownCell();
    }
}
