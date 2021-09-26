using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//플레이어를 제외한 모든 캐릭터들에 적용되는 스크립트입니다.
public class CharacterController : BaseController
{
    public GameObject mask;
    private AudioSource audioSource;
    public AudioSource Audio
    {
        get
        {
            return audioSource;
        }
    }

    public override void Init()
    {
        base.Init();
        audioSource = Managers.Resource.Instantiate("Audio",this.transform).GetComponent<AudioSource>();
        audioSource.transform.localPosition = new Vector3(0f, 1.5f, 0f);
        NavAgent.speed = 1.0f;
        NavAgent.acceleration = 3f;
        NavAgent.radius = 0.25f;
        NavAgent.height = 1.7f;

        if (mask)
        {
            if ((Managers.Scene.currentScene.SceneType == Define.Scene.A_3_1 ||
               Managers.Scene.currentScene.SceneType == Define.Scene.A_3_2) &&
               name == "YoungMan") return;
            ShowMask(DirectorUI.S.maskOnToggle.isOn);
        }
    }

    //캐릭터들의 음성파일을 재생합니다. Resources폴더 경로변경에 취약합니다.
    public void Say(string content, Define.AnimationLayerType audioType)
    {
        AudioClip clip = Managers.Resource.Load<AudioClip>($"Sounds/{this.name}/{audioType.ToString()}/{content}");
        audioSource.clip = clip;
        audioSource.Play();
    }
    //캐릭터의 이동이 끝나면 호출됩니다
    protected override void MoveDoneCallback()
    {
        base.MoveDoneCallback();
        Vector3 dir = (this._endLookDir - transform.position).normalized;
        StartCoroutine(SlowLookAt(dir));
        //transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    //캐릭터의 이동이 끝나면 dir 방향을 바라봅니다.
    IEnumerator SlowLookAt(Vector3 dir)
    {
        while (Vector3.Angle(transform.rotation.eulerAngles, dir) > 1f)
        {
            //Debug.Log($"{transform.rotation.eulerAngles}, {Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 01f * Time.deltaTime).eulerAngles}");
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime*2f);
            yield return null;
        }
    }
    public void ShowMask(bool show)
    {
        if (show)
        {
            mask.SetActive(true);
        }
        else
        {
            mask.SetActive(false);
        }
    }
}
