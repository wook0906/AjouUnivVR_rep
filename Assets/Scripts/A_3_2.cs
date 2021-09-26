using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_3_2 : BaseScene, ISceneControl
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
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungMan, eventPoints.Find("YoungManSpawn"), Define.AnimationLayerType.A_3).gameObject);
        player.Init();
        player.NavAgent.enabled = false;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;
        actors["YoungMan"].mask.SetActive(true);

        StartScene();
    }

    void StartTake()
    {
        SceneType = Define.Scene.A_3_2;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        switch (PlayerPrefs.GetInt("SelectedBranch"))
        {
            case 1:
                actors["YoungMan"].Say("6_a1_1",Define.AnimationLayerType.A_3);
                actors["YoungMan"].Anim.CrossFade("6_a1_1", 0.1f);
                yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungMan));
                actors["YoungMan"].Anim.CrossFade("WaitForUser",0.5f);
                break;
            case 2:
                actors["YoungMan"].Say("6_a3_1", Define.AnimationLayerType.A_3);
                actors["YoungMan"].Anim.CrossFade("6_a3_1", 0.1f);
                yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungMan));
                Managers.Scene.LoadScene(Define.Scene.StartScene);
                yield break;
            case 3:
                actors["YoungMan"].Say("6_a2_1", Define.AnimationLayerType.A_3);
                actors["YoungMan"].Anim.CrossFade("6_a2_1", 0.1f);
                yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungMan));
                actors["YoungMan"].Anim.CrossFade("WaitForUser", 0.5f);
                DirectorUI.S.CreateChoiceBranch(Define.BranchType.A_3_1);
                yield break;
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
