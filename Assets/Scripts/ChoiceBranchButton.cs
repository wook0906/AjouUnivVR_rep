using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using UnityEngine.SceneManagement;

//�б� ������ ��ư�Դϴ�
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
    //Ŭ���ϸ� ȣ��˴ϴ�.
    public void OnClick()
    {
        //if (connectedSceneName == "") return;
        Managers.Scene.LoadScene(connectedSceneName);
        PlayerPrefs.SetInt("SelectedBranch", int.Parse(gameObject.name));
    }
}
