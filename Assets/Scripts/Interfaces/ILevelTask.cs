using UnityEngine;
using UnityEngine.UI;

namespace Mathc3Project.Interfaces
{
    public interface ILevelTask
    {
        string ElementName { get; }
        int Count { get; set; }
        Sprite SpriteElement { get; }
        bool Completed { get; }
    }
}
