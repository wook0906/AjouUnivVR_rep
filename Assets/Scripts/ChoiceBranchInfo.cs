using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceBranchData", menuName = "Scriptable Object/ ChoiceBranchData")]

//Assets/Resources/ChoiceBranchDatas에 저장되어있는 스크립팅이 가능한 오브젝트입니다.
public class ChoiceBranchInfo : ScriptableObject
{
    public List<string> branches;
    public List<Define.Scene> connectedScene;
}
