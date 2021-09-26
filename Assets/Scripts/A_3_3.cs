using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_3_3 : BaseScene, ISceneControl
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
        Managers.Sound.Play("ajou_bgm", Define.Sound.Bgm, 0.1f);
        player = Managers.Resource.Instantiate("Player").GetComponent<PlayerController>();
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.CustomerA, eventPoints.Find("CustomerSpawn"), Define.AnimationLayerType.A_3,true).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungSoo, eventPoints.Find("YoungSooSpawn"), Define.AnimationLayerType.A_3).gameObject);
        player.Init();
        player.NavAgent.enabled = false;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;

        StartScene();
    }

    void StartTake()
    {
        SceneType = Define.Scene.A_3_3;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {

        actors["YoungSoo"].Anim.Play("Work");
        actors["CustomerA"].Anim.Play("WAIT");
        yield return StartCoroutine(WaitTakeDone());

        actors["CustomerA"].Anim.CrossFade("WALK",0.1f);
        actors["CustomerA"].SetDestination(GameObject.Find("OrderPoint").transform.position, player.transform.position);
        yield return new WaitUntil(() => Managers.Observer.IsCharacterStopped(Define.CharacterType.CustomerA));
        actors["CustomerA"].Anim.CrossFade("WAIT", 0.5f);
        yield return StartCoroutine(WaitTakeDone());
        
        actors["CustomerA"].Say("2_1",Define.AnimationLayerType.A_3);
        actors["CustomerA"].Anim.CrossFade("2_1", 0.1f);
        yield return new WaitForSeconds(1f);

        DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_3_3);

        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungMan, eventPoints.Find("CustomerSpawn"), Define.AnimationLayerType.A_3, true).gameObject);
     
        actors["YoungMan"].Anim.Play("WALK");
        actors["YoungMan"].SetDestination(GameObject.Find("CustomerB_Dest").transform.position, player.transform.position);
        yield return new WaitUntil(() => Managers.Observer.IsCharacterStopped(Define.CharacterType.YoungMan));

        actors["YoungMan"].Anim.CrossFade("WaitForUser", 0.5f);

        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.CustomerC, eventPoints.Find("CustomerSpawn"), Define.AnimationLayerType.A_3, true).gameObject);
        actors["CustomerC"].Anim.Play("WALK");
        actors["CustomerC"].SetDestination(GameObject.Find("CustomerC_Dest").transform.position, player.transform.position);
        yield return new WaitUntil(() => Managers.Observer.IsCharacterStopped(Define.CharacterType.CustomerC));
        actors["CustomerC"].Anim.CrossFade("WAIT", 0.5f);
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
