using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_2_4 : BaseScene, ISceneControl
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
        SceneType = Define.Scene.A_2_4;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        player.ChangeSmartPhoneScreen();
        actors["MinSu"].Anim.Play("WAIT");
        actors["SeungWook"].Anim.Play("WAIT");

        switch (PlayerPrefs.GetInt("SelectedBranch"))
        {
            case 1:
                actors["AYun"].Say("14_a1_2", Define.AnimationLayerType.A_2);
                actors["AYun"].Anim.CrossFade("14_a1_2", 0.1f);
                yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.AYun));
                break;
            case 2:
                actors["AYun"].Say("14_a2_2", Define.AnimationLayerType.A_2);
                actors["AYun"].Anim.CrossFade("14_a2_2", 0.1f);
                yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.AYun));
                break;
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
    public void StartScene()
    {
        StartTake();
    }

    public override void TakeDoneCallback()
    {
        isTakeDone = true;
    }

}
