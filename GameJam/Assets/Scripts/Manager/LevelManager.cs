using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager mInstance;
    public static LevelManager Instance { get { return mInstance; } }
    private void Awake()
    {
        if (mInstance != null && mInstance != this)
            Destroy(gameObject);
        else
            mInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private int currentBuildIndex = 0;
    public static IEnumerator ReturnCurrentLevel()
    {
        yield return SceneManager.LoadSceneAsync(mInstance.currentBuildIndex);
    }

    public static IEnumerator GoNextLevel()
    {
        mInstance.currentBuildIndex++;
        yield return SceneManager.LoadSceneAsync(mInstance.currentBuildIndex);
    }
}
