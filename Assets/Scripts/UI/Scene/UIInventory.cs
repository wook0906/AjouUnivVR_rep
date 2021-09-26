using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIScene
{
    enum GameObjects
    {
        GridPanel
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
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        for (int i = 0; i < 8; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UISceneSelectButton>(gridPanel.transform).gameObject;

            UISceneSelectButton invenItem = item.GetOrAddComponent<UISceneSelectButton>();
            //invenItem.SetInfo($"æ∆¿Ã≈€{i + 1}");
        }
    }
}
