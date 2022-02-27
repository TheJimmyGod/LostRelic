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

    private void Start()
    {
        AudioManager.Instance.musicSource.clip = AudioManager.Instance.LevelOne;
        AudioManager.Instance.musicSource.Play();
    }

    private int currentBuildIndex = 0;
    public static IEnumerator ReturnCurrentLevel()
    {
        yield return SceneManager.LoadSceneAsync(mInstance.currentBuildIndex);
    }

    public static IEnumerator GoNextLevel()
    {
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.musicSource.clip = AudioManager.Instance.LevelTwo;
        AudioManager.Instance.musicSource.Play();
        mInstance.currentBuildIndex++;
        yield return SceneManager.LoadSceneAsync(mInstance.currentBuildIndex);
    }

    public void Exit()
    {
        AudioManager.Instance.musicSource.Stop();
        Application.Quit();
    }
}
