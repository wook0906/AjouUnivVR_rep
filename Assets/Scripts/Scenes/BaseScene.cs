using CurvedUI;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    //���� ���� ��������� �����մϴ�
    Define.Scene _sceneType = Define.Scene.Unknown;

    public Define.Scene SceneType { get; protected set; }

    //�������� Scene â���� ���ӿ�����Ʈ�� �̺�Ʈ���� �Ͼ���ϴ� ����Ʈ�� �����ϰ� �ش� ������ �����մϴ�.
    protected Transform eventPoints;

    //ĳ���͵��� �����Ǹ� �� ���� ����Ǿ� �����˴ϴ�.
    public Dictionary<string, CharacterController> actors = new Dictionary<string, CharacterController>();

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(CurvedUIEventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";

        eventPoints = GameObject.Find("EventPoints").transform;

        Managers.Observer.Init();
    }
 

    public virtual void TakeDoneCallback()  //�Ŵ��� ȭ�鿡�� Next��ư�� ������ �ش� �Լ��� ȣ��Ǹ�, ���� Scene�� ������ Override�� �Լ��� �����ϴϴ�.
    {

    }

    public CharacterController GenerateCharacter(Define.CharacterType characterType, Transform spawnPos, Define.AnimationLayerType useAnimLayer, bool usingNavAgent = false)
    //�ش� �Լ��� ���� ĳ���͸� �����մϴ� Resource������ ��κ��濡 ����ϱ⶧���� �����Ͽ��� �մϴ�.
    {
        CharacterController character;
        character = Managers.Resource.Instantiate(characterType.ToString()).GetComponent<CharacterController>();
        character.NavAgent.enabled = usingNavAgent;
        character.transform.position = spawnPos.position;
        character.transform.rotation = Quaternion.LookRotation(spawnPos.forward);
        character.Anim.SetLayerWeight((int)useAnimLayer, 1f);
        character.Anim.SetLayerWeight(0, 0f);
        return character;
    }

    public void EntryCharacterToCurrentScene(GameObject go)
    //������ ĳ���͸� ������ Dictionary�� �����մϴ�.
    {
        actors.Add(go.name, go.GetComponent<CharacterController>());
    }
}
