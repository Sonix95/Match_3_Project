using System.Collections;
using UnityEngine;

namespace Mathc3Project.Interfaces
{
    public interface ICoroutiner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
