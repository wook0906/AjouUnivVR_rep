using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_3_5 : BaseScene, ISceneControl
{
    protected bool isEnd = false;
    protected bool isTakeDone = false;
    protected delegate IEnumerator Takes();
    Takes takes;
    PlayerController player;
    GameObject creditCard;
    public Material[] posMaterials;
    [SerializeField]
    GameObject posMachine;

    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        player = Managers.Resource.Instantiate("Player").GetComponent<PlayerController>();
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.CustomerA, eventPoints.Find("OrderPoint"), Define.AnimationLayerType.A_3).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungMan, eventPoints.Find("CustomerBSpawn"), Define.AnimationLayerType.A_3).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.CustomerC, eventPoints.Find("CustomerCSpawn"), Define.AnimationLayerType.A_3).gameObject);
        EntryCharacterToCurrentScene(GenerateCharacter(Define.CharacterType.YoungSoo, eventPoints.Find("YoungSooSpawn"), Define.AnimationLayerType.A_3).gameObject);
        player.Init();
        player.NavAgent.enabled = false;
        player.transform.position = eventPoints.Find("PlayerSpawn").position;
        creditCard = GameObject.Find("CreditCard");
        posMachine = GameObject.Find("Pos");
        creditCard.SetActive(false);

      

        StartScene();
    }

    void StartTake()
    {
        SceneType = Define.Scene.A_3_5;
        takes = Take1;
        StartCoroutine(takes());
    }

    IEnumerator Take1()
    {
        actors["CustomerA"].Anim.Play("WAIT");
        switch (PlayerPrefs.GetInt("SelectedBranch"))
        {
            case 1:
                actors["YoungSoo"].Say("1_1", Define.AnimationLayerType.A_3);
                actors["YoungSoo"].Anim.Play("1_1");
                break;
            case 2:
                actors["YoungSoo"].Say("2_3", Define.AnimationLayerType.A_3);
                actors["YoungSoo"].Anim.Play("2_3");
                break;
            default:
                break;
        }
        yield return new WaitUntil(() => Managers.Observer.IsCharactersAudioDone(Define.CharacterType.YoungSoo));
        actors["YoungSoo"].Anim.Play("Work");

        yield return StartCoroutine(WaitTakeDone());
        actors["CustomerA"].Say("6_2", Define.AnimationLayerType.A_3);
        actors["CustomerA"].Anim.CrossFade("6_2", 0.1f);
        yield return StartCoroutine(WaitTakeDone());

        Material[] mats = new Material[2];
        mats[0] = posMachine.GetComponent<MeshRenderer>().materials[0];
        mats[1] = posMaterials[0];
        posMachine.GetComponent<MeshRenderer>().materials = mats;
        actors["CustomerA"].Say("8_2", Define.AnimationLayerType.A_3);
        actors["CustomerA"].Anim.CrossFade("8_2", 0.1f);
        yield return StartCoroutine(WaitTakeDone());


        actors["CustomerA"].Say("10_1", Define.AnimationLayerType.A_3);
        actors["CustomerA"].Anim.CrossFade("10_1", 0.1f);
        yield return new WaitForSeconds(2f);
        creditCard.SetActive(true);

        mats[1] = posMaterials[1];
        posMachine.GetComponent<MeshRenderer>().materials = mats;
        yield return StartCoroutine(WaitTakeDone());

        mats[1] = posMaterials[2];
        posMachine.GetComponent<MeshRenderer>().materials = mats;
        actors["CustomerA"].Say("12_2", Define.AnimationLayerType.A_3);
        actors["CustomerA"].Anim.CrossFade("12_2", 0.1f);
        yield return new WaitForSeconds(1f);
        creditCard.SetActive(false);
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
