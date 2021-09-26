using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_4_2 : BaseScene, ISceneControl
{
    protected bool isEnd = false;
    protected bool isTakeDone = false;
    protected delegate IEnumerator Takes();
    Takes takes;
    PlayerController player;

    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        player = Managers.Resource.Instantiate("Player").GetComponent<PlayerController>();
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungSoo, eventPoints.Find("YoungSooSpawn"), Define.AnimationLayerType.A_4).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.SeungWook, eventPoints.Find("SeungWookSpawn"), Define.AnimationLayerType.A_4).gameObject);
        player.NavAgent.enabled = false;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;
        player.transform.rotation = Quaternion.LookRotation(eventPoints.Find("PlayerSpawn").forward);
        player.Init();
        StartScene();
    }
    void StartTake()
    {
        SceneType = Define.Scene.A_4_2;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        switch (PlayerPrefs.GetInt("SelectedBranch"))
        {
            case 1:
                actors["SeungWook"].Say("4_2", Define.AnimationLayerType.A_4);
                actors["SeungWook"].Anim.CrossFade("4_2", 0.1f);
                yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.SeungWook));
                actors["SeungWook"].Anim.CrossFade("WAIT", 0.5f);
                break;
            default:
                break;
        }
        
        DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_4_2);
    }

    IEnumerator WaitTakeDone()
    {
        yield return new WaitUntil(() => isTakeDone);
        isTakeDone = false;
    }
    public void StartScene()
    {
        StartTake();
    }

    public override void TakeDoneCallback()
    {
        isTakeDone = true;
    }
}
