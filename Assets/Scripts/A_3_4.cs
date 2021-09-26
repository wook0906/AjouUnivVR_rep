using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_3_4 : BaseScene, ISceneControl
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
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.CustomerA, eventPoints.Find("OrderPoint"), Define.AnimationLayerType.A_3).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungMan, eventPoints.Find("CustomerBSpawn"), Define.AnimationLayerType.A_3).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.CustomerC, eventPoints.Find("CustomerCSpawn"), Define.AnimationLayerType.A_3).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungSoo, eventPoints.Find("YoungSooSpawn"), Define.AnimationLayerType.A_3).gameObject);
        player.Init();
        player.NavAgent.enabled = false;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;



        StartScene();
    }

    void StartTake()
    {
        SceneType = Define.Scene.A_3_4;
        takes = Take1;
        StartCoroutine(takes());
    }


    IEnumerator Take1()
    {
        actors["YoungSoo"].Anim.Play("Work");
        actors["CustomerA"].Anim.Play("WAIT");

        switch (PlayerPrefs.GetInt("SelectedBranch"))
        {
            case 1:
                actors["CustomerA"].Say("4_a1_1", Define.AnimationLayerType.A_3);
                actors["CustomerA"].Anim.CrossFade("4_a1_1", 0.1f);
                break;
            case 2:
                actors["CustomerA"].Say("4_a2_2", Define.AnimationLayerType.A_3);
                actors["CustomerA"].Anim.CrossFade("4_a2_2", 0.1f);
                break;
            default:
                break;
        }
        yield return new WaitUntil(()=>Managers.Observer.IsCharactersAudioDone(Define.CharacterType.CustomerA));

        DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_3_4);
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
