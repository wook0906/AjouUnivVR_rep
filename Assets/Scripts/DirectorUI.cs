using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


//�������� ȭ���� ���ߴ� ī�޶� �پ��ִ� ��ũ��Ʈ �Դϴ�.
public class DirectorUI : MonoBehaviour
{
    public static DirectorUI S;

    //Resources������ ����� �б� �������� �����Ҷ� ����ϴ� ������Ʈ�� �Դϴ�. �ش� ������Ʈ�� �б� �������� �����մϴ�.
    public List<ChoiceBranchInfo> choiceBranchData;
    
    public Transform verticalLayoutRoot;
    public ChoiceBranchButton choiceBranchPrefab;
    private Button nextBtn;
    //private bool useUserUI;

    //Mask�� On/Off��Ű�� ����Դϴ�.
    public Toggle maskOnToggle;

    

    private void Awake()
    {
        if (!S)
        {
            S = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        //���ÿ� ���������� ���۵� ����ũ On/off ���¸� �ҷ��ɴϴ�.
        if (PlayerPrefs.HasKey("Mask"))
        {
            if(PlayerPrefs.GetInt("Mask") == 0)
            {
                maskOnToggle.isOn = false;
            }
            else
            {
                maskOnToggle.isOn = true;
            }
        }
    }
    private void Start()
    {
        nextBtn = GetComponentInChildren<Button>();
        nextBtn.onClick.AddListener(OnClickNextBtn);
    }

    //�б� �������� �����ϴ� �Լ��Դϴ�. Resources ���� ��κ��濡 ����մϴ�.
    public void CreateChoiceBranch(Define.BranchType take)
    {
        foreach (var branchData in choiceBranchData)
        {
            if(branchData.name == take.ToString())
            {
                for (int i = 0; i < branchData.branches.Count; i++)
                {
                    ChoiceBranchButton choiceBranchInstance = 
                        Managers.Resource.Instantiate("UI/SubItem/ChoiceBranch", verticalLayoutRoot).GetComponent<ChoiceBranchButton>();
                    choiceBranchInstance.Init(branchData.branches[i], branchData.connectedScene[i]);
                    choiceBranchInstance.name = (i+1).ToString();
                }
                break;
            }
        }
    }

    

    //Next��ư�� ������ ȣ��˴ϴ�.
    public void OnClickNextBtn()
    {
        Managers.Scene.currentScene.TakeDoneCallback();
    }

    //Mask On Toggle�� �����ϸ� ȣ��˴ϴ�.
    public void OnMaskOnToggle()
    {
        foreach (var item in Managers.Scene.currentScene.actors)
        {
            if ((Managers.Scene.currentScene.SceneType == Define.Scene.A_3_1 ||
                Managers.Scene.currentScene.SceneType == Define.Scene.A_3_2) &&
                item.Key == "YoungMan") continue;

            if(item.Value.mask != null)
                item.Value.ShowMask(maskOnToggle.isOn);
        }
        if(maskOnToggle.isOn)
            PlayerPrefs.SetInt("Mask",1);
        else
            PlayerPrefs.SetInt("Mask", 0);
    }
   
}
