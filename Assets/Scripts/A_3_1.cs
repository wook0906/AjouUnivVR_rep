using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_3_1 : BaseScene, ISceneControl
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
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungMan, eventPoints.Find("YoungManSpawn"), Define.AnimationLayerType.A_3,true).gameObject);
        player.NavAgent.enabled = false;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;
        player.Init();

        StartScene();
    }

    void StartTake()
    {
        SceneType = Define.Scene.A_3_1;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        actors["YoungMan"].NavAgent.speed /= 2f;
        actors["YoungMan"].Anim.CrossFade("WaitForUser", 0.5f);
        yield return StartCoroutine(WaitTakeDone());

        actors["YoungMan"].Anim.CrossFade("WALK", 0.5f);
        actors["YoungMan"].SetDestination(GameObject.Find("OrderPoint").transform.position, player.transform.position);
        yield return new WaitUntil(() => Managers.Observer.IsCharacterStopped(Define.CharacterType.YoungMan));
        actors["YoungMan"].Anim.CrossFade("WaitForUser", 0.5f);

        actors["YoungMan"].Say("2_2",Define.AnimationLayerType.A_3);
        actors["YoungMan"].Anim.CrossFade("2_2", 0.1f);
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungMan));
        actors["YoungMan"].Anim.CrossFade("WaitForUser", 0.5f);
        yield return StartCoroutine(WaitTakeDone());

        actors["YoungMan"].Say("4_1_1", Define.AnimationLayerType.A_3);
        actors["YoungMan"].Anim.CrossFade("4_1_1", 0.1f);
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungMan));
        actors["YoungMan"].ShowMask(true);

        actors["YoungMan"].Say("4_2_1", Define.AnimationLayerType.A_3);
        actors["YoungMan"].Anim.CrossFade("4_2_1", 0.1f);
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungMan));
        actors["YoungMan"].Anim.CrossFade("6_a1_2", 0.5f);

        DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_3_1);
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
