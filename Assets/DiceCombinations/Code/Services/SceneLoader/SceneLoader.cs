using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DiceCombinations.Code.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadScene(string sceneName, Action onLoaded = null)
        {
            AsyncOperation loadSceneAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
            loadSceneAsyncOperation.completed += operation => onLoaded?.Invoke();
        }
    }
}