using CurvedUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//사용하지 않는 스크립트입니다.
public class InputManager
{
    public Action keyAction = null;
    public Action<Define.MouseEvent> mouseAction = null;

    bool _mousePressed = false;

    public void OnUpdate()
    {
        if (CurvedUIEventSystem.current.IsPointerOverGameObject()) return;

        if (Input.anyKey && keyAction != null)
            keyAction.Invoke();

        if (mouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                mouseAction.Invoke(Define.MouseEvent.Press);
                _mousePressed = true;
            }
            else
            {
                if (_mousePressed)
                    mouseAction.Invoke(Define.MouseEvent.Click);
                _mousePressed = false;
            }
        }
    }

    public void Clear()
    {
        keyAction = null;
        mouseAction = null;
    }

    public void SwitchInput()
    {

    }
    
}
