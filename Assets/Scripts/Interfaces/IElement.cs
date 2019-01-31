using UnityEngine;

namespace Mathc3Project
{
    public interface IElement
    {
        string Name { get; set; }
        float localSize { get; set; }
        IBoardManager BoardManager { get; set; }
        Vector3 CurrentPosition { get; set; }
    }
}
