using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneSelectUI : UIScene
{
    enum GameObjects
    {
        Content
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GameObject contentPanel = Get<GameObject>((int)GameObjects.Content);
        foreach (Transform child in contentPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        //Define 클래스에 정의되어있는 Enum Scene의 정보로 버튼을 생성합니다.
        for (Define.Scene i = Define.Scene.A_1_1; i < Define.Scene.End; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UISceneSelectButton>(contentPanel.transform).gameObject;

            UISceneSelectButton sceneButton = item.GetOrAddComponent<UISceneSelectButton>();
            sceneButton.SetInfo(i);
        }
    }
}
