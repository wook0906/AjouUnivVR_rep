using CurvedUI;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    //현재 씬이 어떤씬인지를 구분합니다
    Define.Scene _sceneType = Define.Scene.Unknown;

    public Define.Scene SceneType { get; protected set; }

    //에디터의 Scene 창에서 게임오브젝트로 이벤트들이 일어나야하는 포인트를 지정하고 해당 변수에 저장합니다.
    protected Transform eventPoints;

    //캐릭터들이 생성되면 이 곳에 저장되어 관리됩니다.
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
 

    public virtual void TakeDoneCallback()  //매니저 화면에서 Next버튼을 누르면 해당 함수가 호출되며, 현재 Scene에 대응된 Override된 함수가 실행됍니다.
    {

    }

    public CharacterController GenerateCharacter(Define.CharacterType characterType, Transform spawnPos, Define.AnimationLayerType useAnimLayer, bool usingNavAgent = false)
    //해당 함수를 통해 캐릭터를 생성합니다 Resource폴더의 경로변경에 취약하기때문에 유의하여야 합니다.
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
    //생성된 캐릭터를 관리할 Dictionary에 저장합니다.
    {
        actors.Add(go.name, go.GetComponent<CharacterController>());
    }
}
