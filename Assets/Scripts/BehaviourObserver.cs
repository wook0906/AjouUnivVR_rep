using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//actors Dictionary�� ��� ĳ���͵��� ���¸� �����մϴ�.
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

    //��� ĳ���Ͱ� �̵��� ������ ��������?
    public bool IsAllCharactersStopped()
    {
        foreach (var item in currentScene.actors.Values)
        {
            if (item.State != Define.State.Idle)
                return false;
        }
        return true;
    }
    //�ش� ĳ���Ͱ� �̵��� ������ ��������?
    public bool IsCharacterStopped(Define.CharacterType charType)
    {
        if (currentScene.actors[charType.ToString()].State != Define.State.Idle)
            return false;
        return true;
    }
    
    
    //���� ĳ������ ���� ��������?
    public bool IsCharactersAudioDone(Define.CharacterType characterType)
    {
        
        if (currentScene.actors[characterType.ToString()].Audio.isPlaying)
            return false;
        else
            return true;
    }
    //��� ĳ������ ���� ��������?
    public bool IsAllCharactersAudioDone()
    {
        foreach (var item in currentScene.actors.Values)
        {
            if (item.Audio.isPlaying) return false;
        }
        return true;
    }
}