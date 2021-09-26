using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class A_1_1 : BaseScene, ISceneControl
{ 
    protected bool isEnd = false;
    protected bool isTakeDone = false;
    protected delegate IEnumerator Takes();

    Takes takes;
    PlayerController player;


    // Start is called before the first frame update
    protected override void Init() //해당 씬을 플레이하기 전에 필요한 요소들을 여기서 세팅합니다.
    {
        base.Init();
        Managers.Sound.Play("ajou_bgm", Define.Sound.Bgm, 0.1f);
        player = Managers.Resource.Instantiate("Player").GetComponent<PlayerController>();
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungSoo, eventPoints.Find("YoungSooSpawn"), Define.AnimationLayerType.A_1, false).gameObject);
        player.NavAgent.enabled = true;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;
        player.transform.rotation = Quaternion.LookRotation(eventPoints.Find("PlayerSpawn").forward);
        player.Init();

        StartScene();
    }

    void StartTake()
    {
        SceneType = Define.Scene.A_1_1;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        actors["YoungSoo"].Anim.CrossFade("Working", 0.1f, (int)Define.AnimationLayerType.A_1);
        yield return StartCoroutine(WaitTakeDone());

        player.SetDestination(eventPoints.Find("InFrontOfCoWorker").position, actors["YoungSoo"].transform.position);
        yield return StartCoroutine(WaitTakeDone());
        actors["YoungSoo"].Anim.CrossFade("LookUser", 0.1f, (int)Define.AnimationLayerType.A_1);
        yield return StartCoroutine(WaitTakeDone());
        actors["YoungSoo"].Anim.CrossFade("2_2", 0.1f, (int)Define.AnimationLayerType.A_1);
        actors["YoungSoo"].Say("2_2", Define.AnimationLayerType.A_1);
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungSoo));
        
        DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_1_1);

       
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
