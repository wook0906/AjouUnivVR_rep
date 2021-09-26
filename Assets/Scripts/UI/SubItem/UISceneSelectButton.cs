using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//StartScene���� ������ �̵��ϱ� ���� ��ư�Դϴ�
public class UISceneSelectButton : UIBase
{
    enum Buttons
    {
        UISceneSelectButton
    }
    enum Texts
    {
        title
    }

    Define.Scene sceneType;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Get<Button>((int)Buttons.UISceneSelectButton).onClick.AddListener(OnClickButton);
        Get<Text>((int)Texts.title).text = this.sceneType.ToString();
    }
    public void OnClickButton()
    {
        Managers.Scene.LoadScene(sceneType);
    }
    public void SetInfo(Define.Scene sceneType)
    {
        this.sceneType = sceneType;
    }
}
