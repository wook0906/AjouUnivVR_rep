using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


//관리자의 화면을 비추는 카메라에 붙어있는 스크립트 입니다.
public class DirectorUI : MonoBehaviour
{
    public static DirectorUI S;

    //Resources폴더에 저장된 분기 선택지를 생성할때 사용하는 오브젝트들 입니다. 해당 오브젝트로 분기 선택지를 생성합니다.
    public List<ChoiceBranchInfo> choiceBranchData;
    
    public Transform verticalLayoutRoot;
    public ChoiceBranchButton choiceBranchPrefab;
    private Button nextBtn;
    //private bool useUserUI;

    //Mask를 On/Off시키는 토글입니다.
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

        //로컬에 마지막으로 조작된 마스크 On/off 상태를 불러옵니다.
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

    //분기 선택지를 생성하는 함수입니다. Resources 폴더 경로변경에 취약합니다.
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

    

    //Next버튼을 누르면 호출됩니다.
    public void OnClickNextBtn()
    {
        Managers.Scene.currentScene.TakeDoneCallback();
    }

    //Mask On Toggle을 조작하면 호출됩니다.
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
