using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_3_7 : BaseScene, ISceneControl
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
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.CustomerA, eventPoints.Find("OrderPoint"), Define.AnimationLayerType.A_3).gameObject);
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
        SceneType = Define.Scene.A_3_7;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        actors["YoungSoo"].Anim.Play("Work");

        switch (PlayerPrefs.GetInt("SelectedBranch"))
        {
            case 1:
                actors["CustomerA"].Say("6_a1_2", Define.AnimationLayerType.A_3);
                actors["CustomerA"].Anim.CrossFade("6_a1_2", 0.1f);
                break;
            case 2:
                actors["CustomerA"].Say("6_a2_1", Define.AnimationLayerType.A_3);
                actors["CustomerA"].Anim.CrossFade("6_a2_1", 0.1f);
                break;
            default:
                break;
        }
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.CustomerA));

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


