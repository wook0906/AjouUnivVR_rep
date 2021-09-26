using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//actors Dictionary의 모든 캐릭터들의 상태를 관찰합니다.
public class BehaviourObserver
{
    BaseScene currentScene;

    public void Init()
    {
        currentScene = GameObject.Find("@Scene").GetComponent<BaseScene>();
    }
    
    public BaseController[] GetStaffs()
    {
        List<BaseController> tmpList = new List<BaseController>();
        foreach (var item in currentScene.actors.Values)
        {
            if (item.GetComponent<PlayerController>()) continue;
            tmpList.Add(item);
        }
        return tmpList.ToArray();
    }

    //모든 캐릭터가 이동을 종료한 상태인지?
    public bool IsAllCharactersStopped()
    {
        foreach (var item in currentScene.actors.Values)
        {
            if (item.State != Define.State.Idle)
                return false;
        }
        return true;
    }
    //해당 캐릭터가 이동을 종료한 상태인지?
    public bool IsCharacterStopped(Define.CharacterType charType)
    {
        if (currentScene.actors[charType.ToString()].State != Define.State.Idle)
            return false;
        return true;
    }
    
    
    //단일 캐릭터의 말이 끝났는지?
    public bool IsCharactersAudioDone(Define.CharacterType characterType)
    {
        
        if (currentScene.actors[characterType.ToString()].Audio.isPlaying)
            return false;
        else
            return true;
    }
    //모든 캐릭터의 말이 끝났는지?
    public bool IsAllCharactersAudioDone()
    {
        foreach (var item in currentScene.actors.Values)
        {
            if (item.Audio.isPlaying) return false;
        }
        return true;
    }
}