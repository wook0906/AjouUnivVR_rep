using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_3_6 : BaseScene, ISceneControl
{
    protected bool isEnd = false;
    protected bool isTakeDone = false;
    protected delegate IEnumerator Takes();
    Takes takes;
    PlayerController player;
    public GameObject completedMenu;

    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        player = Managers.Resource.Instantiate("Player").GetComponent<PlayerController>();
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.CustomerA, eventPoints.Find("CustomerASpawn"), Define.AnimationLayerType.A_3, true).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.CustomerC, eventPoints.Find("CustomerCSpawn"), Define.AnimationLayerType.A_3).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungSoo, eventPoints.Find("YoungSooSpawn"), Define.AnimationLayerType.A_3).gameObject);
        player.Init();
        player.NavAgent.enabled = false;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;
        completedMenu.SetActive(true);

   
        StartScene();
    }

    void StartTake()
    {
        SceneType = Define.Scene.A_3_6;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        actors["YoungSoo"].Anim.Play("Work");
        actors["CustomerC"].Anim.Play("Sit");

        actors["CustomerA"].Anim.Play("WALK");
        actors["CustomerA"].SetDestination(GameObject.Find("OrderPoint").transform.position, player.transform.position);
        yield return new WaitUntil(() => Managers.Observer.IsCharacterStopped(Define.CharacterType.CustomerA));
        actors["CustomerA"].Anim.Play("WAIT");

        yield return StartCoroutine(WaitTakeDone());

        actors["CustomerA"].Say("4_2", Define.AnimationLayerType.A_3);
        actors["CustomerA"].Anim.CrossFade("4_2", 0.1f);
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.CustomerA));
        player.receipt.SetActive(true);
        player.HapticPulse(2f, 200f, 0.5f);
        actors["CustomerA"].Anim.Play("WAIT2");

        DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_3_6);
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


