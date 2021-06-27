using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Menu : MonoBehaviour
{
   
    public virtual void StartNew()
    {
  
        GameManager.Instance.LoadLevel(2);
    }

    public virtual void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }
  
    public void SetPlayerAI()
    {
        GameManager.Instance.SetPlayerInput();
    }
}
