using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//targetCamera�� �Ĵٺ��� �ϴ� ��ũ��Ʈ�Դϴ�.
public class LookAtCamera : MonoBehaviour
{
    Transform targetCamera;

    private void Start()
    {
        targetCamera = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(2*transform.position - targetCamera.position);
    }
}
