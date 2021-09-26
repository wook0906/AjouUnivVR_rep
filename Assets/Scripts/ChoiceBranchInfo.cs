using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceBranchData", menuName = "Scriptable Object/ ChoiceBranchData")]

//Assets/Resources/ChoiceBranchDatas�� ����Ǿ��ִ� ��ũ������ ������ ������Ʈ�Դϴ�.
public class ChoiceBranchInfo : ScriptableObject
{
    public List<string> branches;
    public List<Define.Scene> connectedScene;
}
