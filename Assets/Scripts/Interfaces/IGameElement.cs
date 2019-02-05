using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface IGameElement
    {
        float Size { get; set; }
        Color Color { get; set; }
        string Name { get; set; }
        bool IsUpdate { get; set; }
        Vector3 CurrentPosition { get; set; }
        IBoard Board { get; set; }

        void SetPosition();
        void CustomUpdate();

        void DestroyElement();
    }
}


/////////////////////////////////////////////////////////////////////////////////////////////////////


/*
        Color Color { get; set; }
        string Name { get; set; }
        float localSize { get; set; }
        IBoardManager BoardManager { get; set; }
        Vector3 CurrentPosition { get; set; }
        GameObject Board { get; set; }
    }
}
*/
