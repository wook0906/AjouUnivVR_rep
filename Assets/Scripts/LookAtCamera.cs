using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//targetCamera를 쳐다보게 하는 스크립트입니다.
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
