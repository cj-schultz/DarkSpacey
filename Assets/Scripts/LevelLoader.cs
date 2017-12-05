using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    private int currentLevel;    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        currentLevel = int.Parse(currentScene.Substring(currentScene.Length - 1));
        Debug.Log(currentLevel);     
    }

    public void LoadNextLevel()
    {
        if(currentLevel < SceneManager.sceneCountInBuildSettings)
        {
            string sceneToLoad = "Level" + ++currentLevel;
            SceneManager.LoadScene(sceneToLoad);            
        }                
    }

    public void LoadPreviousLevel()
    {
        if (currentLevel > 1)
        {
            string sceneToLoad = "Level" + --currentLevel;
            SceneManager.LoadScene(sceneToLoad);
        }                
    }
}
