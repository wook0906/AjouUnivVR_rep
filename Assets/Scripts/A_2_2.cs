using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_2_2 : BaseScene, ISceneControl
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
        SceneType = Define.Scene.A_2_2;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        actors["MinSu"].Anim.Play("WAIT");
        actors["SeungWook"].Anim.Play("WAIT");

        switch (PlayerPrefs.GetInt("SelectedBranch"))
        {
            case 1:
                Managers.Sound.Play("Clap", Define.Sound.Effect, 0.5f);
                foreach (var item in actors)
                {
                    if (item.Key == "AYun") continue;
                    item.Value.Anim.CrossFade("Clap", 0.1f);
                }
                yield return new WaitForSeconds(3f);//박수 애니메이션 Duration 참고해서 시간 부여

                actors["AYun"].Say("8_a1_1", Define.AnimationLayerType.A_2);
                actors["AYun"].Anim.CrossFade("8_a1_1", 0.1f);
                yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.AYun));
                break;
            case 2:
                actors["AYun"].Say("8_a2_1", Define.AnimationLayerType.A_2);
                actors["AYun"].Anim.CrossFade("8_a2_1", 0.1f);
                DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_2_1);
                yield break;
            default:
                break;
        }

        player.HapticPulse(2f, 200f, 1f);
        player.ChangeSmartPhoneScreen();

        actors["MinSu"].Say("9_1", Define.AnimationLayerType.A_2);
        actors["MinSu"].Anim.CrossFade("9_1", 0.1f);
        actors["AYun"].Anim.CrossFade("LookMinsu", 0.5f);

        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.MinSu));
        
        actors["MinSu"].Anim.CrossFade("WAIT", 0.1f);
        
        Managers.Sound.Play("Clap");
        foreach (var item in actors)
        {
            if (item.Key == "AYun" || item.Key == "MinSu") continue;
            item.Value.Anim.CrossFade("Clap", 0.1f);
        }
        yield return new WaitForSeconds(3f);

        actors["AYun"].Say("10_1", Define.AnimationLayerType.A_2);
        actors["AYun"].Anim.CrossFade("10_1", 0.1f);

        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.AYun));
        DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_2_2); //사용자 행동에따라 관리자가 선택지 선택
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
