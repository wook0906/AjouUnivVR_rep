using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using UnityEngine.SceneManagement;

//분기 선택지 버튼입니다
public class ChoiceBranchButton : MonoBehaviour
{
    Define.Scene connectedSceneName;

    public void Init(string content, Define.Scene connectedScene)
    {
        connectedSceneName = connectedScene;
        GetComponentInChildren<Text>().text = content;
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    //클릭하면 호출됩니다.
    public void OnClick()
    {
        //if (connectedSceneName == "") return;
        Managers.Scene.LoadScene(connectedSceneName);
        PlayerPrefs.SetInt("SelectedBranch", int.Parse(gameObject.name));
    }
}
