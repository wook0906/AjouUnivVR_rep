using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_1_2 : BaseScene, ISceneControl
{
    protected bool isEnd = false;
    protected bool isTakeDone = false;
    protected delegate IEnumerator Takes();

    Takes takes;

    PlayerController player;
    CharacterController coWorker;


    protected override void Init()
    {
        base.Init();
        player = Managers.Resource.Instantiate("Player").GetComponent<PlayerController>();
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungSoo, eventPoints.Find("YoungSooSpawn"), Define.AnimationLayerType.A_1, false).gameObject);
        actors["YoungSoo"].Anim.Play("WaitForUser", (int)Define.AnimationLayerType.A_1);
        player.NavAgent.enabled = true;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;
        player.transform.rotation = Quaternion.LookRotation(eventPoints.Find("PlayerSpawn").forward);
        player.Init();
        StartScene();
    }


    void StartTake()
    {
        SceneType = Define.Scene.A_1_2;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        switch (PlayerPrefs.GetInt("SelectedBranch"))       
        {
            case 1:
                actors["YoungSoo"].Say("4_a1_2", Define.AnimationLayerType.A_1);
                actors["YoungSoo"].Anim.CrossFade("4_a1_2", 0.1f, (int)Define.AnimationLayerType.A_1);
                yield return new WaitUntil(()=>Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungSoo));
                break;
            case 2:
                actors["YoungSoo"].Say("4_a2_2", Define.AnimationLayerType.A_1);
                actors["YoungSoo"].Anim.CrossFade("4_a2_2", 0.1f, (int)Define.AnimationLayerType.A_1);
                yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungSoo));

                break;
            case 3:
                actors["YoungSoo"].Say("4_a3_1", Define.AnimationLayerType.A_1);
                actors["YoungSoo"].Anim.CrossFade("4_a3_1", 0.1f, (int)Define.AnimationLayerType.A_1);
                yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungSoo));
                DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_1_1);
                yield break;
            default:
                break;
        }

        Managers.Scene.LoadScene(SceneType + 1);
    }


    IEnumerator WaitTakeDone()
    {
        yield return new WaitUntil(() => isTakeDone);
        isTakeDone = false;
    }
    public override void TakeDoneCallback()
    {
        isTakeDone = true;
    }

    public void StartScene()
    {
        StartTake();
    }


}
