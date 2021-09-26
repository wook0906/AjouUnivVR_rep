using UnityEngine;
using Valve.VR;

public class PlayerController : BaseController
{
    public Transform desiredHeadPosition;
    public Transform steamCamera;
    public GameObject smartPhone;
    public Material[] mat_MsgScreen;
    public GameObject receipt;
    public SteamVR_Action_Vibration hapticAction;


    // Update is called once per frame
    public override void Init()
    {
        base.Init();

        QualitySettings.antiAliasing = 2;

        //������� ���� ��ġ�� �������� ������ ��ġ�� �����մϴ�. �÷��̾ �����ɶ����� ȣ��˴ϴ�.
        Valve.VR.OpenVR.Chaperone.ResetZeroPose(ETrackingUniverseOrigin.TrackingUniverseStanding);
        Valve.VR.OpenVR.Compositor.SetTrackingSpace(ETrackingUniverseOrigin.TrackingUniverseStanding);
    }

    protected override void MoveDoneCallback()
    {
        base.MoveDoneCallback();
        GameObject.Find("@Scene").GetComponent<BaseScene>().TakeDoneCallback();
    }

    //��Ʈ�ѷ��� ������ŵ�ϴ�.
    public void HapticPulse(float duration, float frequency, float amplitude)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.RightHand);
    }

    public void ChangeSmartPhoneScreen()
    {
        smartPhone.GetComponent<MeshRenderer>().materials = mat_MsgScreen;
    }


}
