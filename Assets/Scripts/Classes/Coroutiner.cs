using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project.Classes
{
    public class Coroutiner : MonoBehaviour, ICoroutiner
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
    }
}
