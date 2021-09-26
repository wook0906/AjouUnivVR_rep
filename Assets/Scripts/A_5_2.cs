using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class A_5_2 : BaseScene, ISceneControl
{
    protected bool isTakeDone = false;
    protected delegate IEnumerator Takes();
    Takes takes;
    PlayerController player;
    protected override void Init()
    {
        base.Init();
        player = Managers.Resource.Instantiate("Player").GetComponent<PlayerController>();
        player.Init();
        player.transform.rotation = Quaternion.LookRotation(eventPoints.Find("PlayerSpawn").forward);
        player.transform.position = eventPoints.Find("PlayerSpawn").position;

        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.AYun, eventPoints.Find("AYunSpawn"), Define.AnimationLayerType.A_5).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.MinSu, eventPoints.Find("MinSuSpawn"), Define.AnimationLayerType.A_5).gameObject);
        
        
        StartScene();
    }

    void StartTake()
    {
        SceneType = Define.Scene.A_5_2;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        actors["AYun"].Anim.CrossFade("POS_Work", 0.5f, (int)Define.AnimationLayerType.A_5);

        GetComponent<PlayableDirector>().Play();

        yield return StartCoroutine(WaitTakeDone());

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
