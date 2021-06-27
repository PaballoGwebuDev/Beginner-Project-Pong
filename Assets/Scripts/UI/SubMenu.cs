using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : Menu
{
    private void Awake()
    {
        //gameObject.SetActive(false);
    }
    public override void StartNew()
    {
        gameObject.SetActive(false);
        base.StartNew();
        
        
    }
    public override void Exit()
    {
        base.Exit();
    }


}
