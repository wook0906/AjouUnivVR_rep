using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI관리에 사용하지만 거의 사용하고 있지 않습니다.
public class UIManager
{
    int order = 0;

    Stack<UIPopup> popupStack = new Stack<UIPopup>();
    UIScene sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root  = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool isNeedSort = true)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (isNeedSort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return Utils.GetOrAddComponent<T>(go);

    }
    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T _sceneUI = Utils.GetOrAddComponent<T>(go);
        sceneUI = _sceneUI;

        go.transform.SetParent(Root.transform);

        return _sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Utils.GetOrAddComponent<T>(go);
        popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;
        UIPopup popup = popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        order--;
    }
    public void ClosePopupUI(UIPopup popup)
    {
        if (popupStack.Count == 0) return;
        if (popupStack.Peek() != popup)
        {
            Debug.Log("close Popup failed");
        }
        ClosePopupUI();
    }
    public void CloseAllPopupUI()
    {
        while (popupStack.Count>0)
        {
            ClosePopupUI();
        }
    }
    public void Clear()
    {
        CloseAllPopupUI();
        sceneUI = null;
    }
}
