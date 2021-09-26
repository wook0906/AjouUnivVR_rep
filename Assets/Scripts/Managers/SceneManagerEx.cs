using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneManagerEx
{
    public BaseScene currentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type)
    {
        //Managers.Clear();

        SteamVR_LoadLevel.Begin(type.ToString());
    }
    string GetSceneName(Define.Scene type)
    {
        return System.Enum.GetName(typeof(Define.Scene), type);
    }
    public void Clear()
    {
        //if(currentScene)
        //    currentScene.Clear();
    }
    
}
