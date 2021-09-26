using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�÷��̾ ������ ��� ĳ���͵鿡 ����Ǵ� ��ũ��Ʈ�Դϴ�.
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

    //ĳ���͵��� ���������� ����մϴ�. Resources���� ��κ��濡 ����մϴ�.
    public void Say(string content, Define.AnimationLayerType audioType)
    {
        AudioClip clip = Managers.Resource.Load<AudioClip>($"Sounds/{this.name}/{audioType.ToString()}/{content}");
        audioSource.clip = clip;
        audioSource.Play();
    }
    //ĳ������ �̵��� ������ ȣ��˴ϴ�
    protected override void MoveDoneCallback()
    {
        base.MoveDoneCallback();
        Vector3 dir = (this._endLookDir - transform.position).normalized;
        StartCoroutine(SlowLookAt(dir));
        //transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    //ĳ������ �̵��� ������ dir ������ �ٶ󺾴ϴ�.
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
