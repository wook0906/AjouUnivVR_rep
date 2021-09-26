using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_2_1 : BaseScene, ISceneControl
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
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungSoo, eventPoints.Find("YoungSooSpawn"), Define.AnimationLayerType.A_2).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.SeungWook, eventPoints.Find("SeungWookSpawn"), Define.AnimationLayerType.A_2).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.AYun, eventPoints.Find("AYunSpawn"), Define.AnimationLayerType.A_2).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.MinSu, eventPoints.Find("MinSuSpawn"), Define.AnimationLayerType.A_2).gameObject);
        player.NavAgent.enabled = false;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;
        player.transform.rotation = Quaternion.LookRotation(eventPoints.Find("PlayerSpawn").forward);
        player.smartPhone.SetActive(true);
        player.Init();
        StartScene();
    }
    void StartTake()
    {
        SceneType = Define.Scene.A_2_1;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        yield return StartCoroutine(WaitTakeDone());

        actors["AYun"].Say("1_1", Define.AnimationLayerType.A_2);
        actors["AYun"].Anim.CrossFade("1_1", 0.1f);

        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.AYun));

        actors["AYun"].Anim.CrossFade("WAIT", 0.5f);
        actors["SeungWook"].Say("2_2", Define.AnimationLayerType.A_2);
        actors["SeungWook"].Anim.CrossFade("2_2", 0.1f);
        actors["YoungSoo"].Say("2_2", Define.AnimationLayerType.A_2);
        actors["YoungSoo"].Anim.CrossFade("2_2", 0.1f);
        actors["MinSu"].Say("2_1", Define.AnimationLayerType.A_2);
        actors["MinSu"].Anim.CrossFade("2_1", 0.1f);

        yield return new WaitUntil(() => Managers.Observer.IsAllCharactersAudioDone());

        actors["SeungWook"].Anim.CrossFade("LookAYun", 0.5f);
        actors["YoungSoo"].Anim.CrossFade("WAIT", 0.5f);
        actors["MinSu"].Anim.CrossFade("LookAYun", 0.5f);
        actors["AYun"].Say("3_2", Define.AnimationLayerType.A_2);
        actors["AYun"].Anim.CrossFade("3_2", 0.1f);

        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.AYun));

        actors["MinSu"].Anim.CrossFade("WAIT", 0.5f);
        actors["SeungWook"].Say("4_1", Define.AnimationLayerType.A_2);
        actors["SeungWook"].Anim.CrossFade("4_1", 0.1f);

        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.SeungWook));

        Managers.Sound.Play("Clap",Define.Sound.Effect,0.5f);
        foreach (var item in actors)
        {
            if (item.Key == "SeungWook" || item.Key == "AYun") continue;
            item.Value.Anim.CrossFade("Clap", 0.1f);
        }
        yield return new WaitForSeconds(3f);

        actors["SeungWook"].Anim.CrossFade("WAIT", 0.5f);
        actors["AYun"].Say("5_1", Define.AnimationLayerType.A_2);
        actors["AYun"].Anim.CrossFade("5_1", 0.1f);

        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.AYun));

        actors["AYun"].Anim.CrossFade("WAIT", 0.5f);
        actors["YoungSoo"].Say("6_1", Define.AnimationLayerType.A_2);
        actors["YoungSoo"].Anim.CrossFade("6_1", 0.1f);

        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungSoo));
        actors["AYun"].Anim.CrossFade("ShakeHead", 0.1f);

        Managers.Sound.Play("Clap", Define.Sound.Effect, 0.5f);
        foreach (var item in actors)
        {
            if (item.Key == "YoungSoo" || item.Key == "AYun") continue;
            item.Value.Anim.CrossFade("Clap", 0.1f);
        }
        yield return new WaitForSeconds(3f);
        actors["YoungSoo"].Anim.CrossFade("WAIT", 0.5f);
        actors["AYun"].Anim.CrossFade("WaitUser", 0.5f);
        DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_2_1);
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
