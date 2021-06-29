using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>//INHERITANCE
{
    // 
    private static GameManager instance;
    private static bool playerAI = true;
    public static bool PlayerAI {
        get { return playerAI; }
    }
    //string currentLevelName = string.Empty;
    int currentLevelNumber;
    List<AsyncOperation> loadOperations;

    //additional manager support
    //public GameObject[] systemPrefabs;
    //private List<GameObject> instancedSystemPrefabs;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadOperations = new List<AsyncOperation>();
        //instancedSystemPrefabs = new List<GameObject>();
    }
 
    void OnLoadOperationComplete(AsyncOperation asyncOperation)
    {
        if (loadOperations.Contains(asyncOperation))
        {
            loadOperations.Remove(asyncOperation);
            Debug.Log("LOADING");
        }
        Debug.Log("Load Complete");
    }
    void OnUnloadLevelOperationComplete(AsyncOperation asyncOperation)
    {
        Debug.Log("Unload complete");

    }
    public void LoadLevel(int sceneNumber)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Single);
        if (asyncOperation== null)
        {
            Debug.LogError("[GameManager] Unable to load level: " + sceneNumber);
            return;
        }
        asyncOperation.completed += OnLoadOperationComplete;
        loadOperations.Add(asyncOperation);
        currentLevelNumber = sceneNumber;

    }
    public void UnloadLevel(int sceneNumber)
    {
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneNumber);
        if (asyncOperation == null)
        {
            Debug.LogError("[GameManager] Unable to unload level: " + sceneNumber);
            return;
        }
        asyncOperation.completed += OnUnloadLevelOperationComplete;
    }
    public void SetPlayerInput()
    {
        Debug.Log("Setting player AI to false");
        playerAI = false;
    }
}
