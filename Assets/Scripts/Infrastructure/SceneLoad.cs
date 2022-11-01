using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure
{
    public class SceneLoad
    {
        private readonly ICoroutineRanner _coroutineRanner;

        public SceneLoad(ICoroutineRanner coroutineRanner)
        {
            _coroutineRanner = coroutineRanner;
        }

        public void Load(string nextName, Action onLoader = null) =>
            _coroutineRanner.StartCoroutine(LoadScene(nextName, onLoader));

        private IEnumerator LoadScene(string nextName, Action onLoader)
        {
            /*if (SceneManager.GetActiveScene().name == nextName)
                yield break;*/

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextName);

            while (!waitNextScene.isDone)
                yield return null;

            onLoader?.Invoke();
        }
    }
}