using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_4_1 : BaseScene, ISceneControl
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
        Managers.Sound.Play("ajou_bgm", Define.Sound.Bgm, 0.05f);
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
        SceneType = Define.Scene.A_4_1;
        takes = Take1;
        StartCoroutine(takes());
    }
    void Update()
    {

            
    }

    IEnumerator Take1()
    {
        actors["YoungSoo"].Anim.Play("WAIT");
        actors["SeungWook"].Anim.Play("WAIT");
        yield return StartCoroutine(WaitTakeDone());
        
        actors["YoungSoo"].Say("2_1",Define.AnimationLayerType.A_4);
        actors["YoungSoo"].Anim.CrossFade("2_1", 0.1f);
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungSoo));
        actors["YoungSoo"].Anim.CrossFade("WAIT", 0.5f);

        DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_4_1);
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
