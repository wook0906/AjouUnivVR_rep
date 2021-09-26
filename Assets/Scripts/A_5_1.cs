using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class A_5_1 : BaseScene, ISceneControl
{
    protected bool isTakeDone = false;
    protected delegate IEnumerator Takes();
    Takes takes;
    PlayerController player;
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        player = Managers.Resource.Instantiate("Player").GetComponent<PlayerController>();
        player.Init();
        player.transform.rotation = Quaternion.LookRotation(eventPoints.Find("PlayerSpawn").forward);
        player.transform.position = eventPoints.Find("PlayerSpawn").position;

        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.AYun, eventPoints.Find("AYunSpawn"), Define.AnimationLayerType.A_5).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.MinSu, eventPoints.Find("MinSuSpawn"), Define.AnimationLayerType.A_5).gameObject);

        Managers.Sound.Play("ajou_bgm", Define.Sound.Bgm, 0.1f);

        StartScene();
    }

    void StartTake()
    {
        SceneType = Define.Scene.A_5_1;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        actors["AYun"].Anim.Play("POS_Work", (int)Define.AnimationLayerType.A_5);
        yield return StartCoroutine(WaitTakeDone());
        actors["AYun"].Anim.CrossFade("1_1", 0.1f, (int)Define.AnimationLayerType.A_5);
        actors["AYun"].Say("1_1",Define.AnimationLayerType.A_5);
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.AYun));
        actors["AYun"].Anim.CrossFade("WaitForUserAnswer",0.5f, (int)Define.AnimationLayerType.A_5);
        yield return StartCoroutine(WaitTakeDone());


        actors["AYun"].Anim.CrossFade("3_2", 0.1f, (int)Define.AnimationLayerType.A_5);
        actors["AYun"].Say("3_2",Define.AnimationLayerType.A_5);
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.AYun));

        Managers.Scene.LoadScene(SceneType + 1);

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
