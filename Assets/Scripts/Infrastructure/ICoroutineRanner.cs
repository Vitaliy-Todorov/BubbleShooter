using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public interface ICoroutineRanner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}