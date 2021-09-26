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

        //사용자의 현실 위치를 기준으로 가상의 위치를 보정합니다. 플레이어가 생성될때마다 호출됩니다.
        Valve.VR.OpenVR.Chaperone.ResetZeroPose(ETrackingUniverseOrigin.TrackingUniverseStanding);
        Valve.VR.OpenVR.Compositor.SetTrackingSpace(ETrackingUniverseOrigin.TrackingUniverseStanding);
    }

    protected override void MoveDoneCallback()
    {
        base.MoveDoneCallback();
        GameObject.Find("@Scene").GetComponent<BaseScene>().TakeDoneCallback();
    }

    //컨트롤러를 진동시킵니다.
    public void HapticPulse(float duration, float frequency, float amplitude)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.RightHand);
    }

    public void ChangeSmartPhoneScreen()
    {
        smartPhone.GetComponent<MeshRenderer>().materials = mat_MsgScreen;
    }


}
