using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEditor;

public class Test :
{
    public string id;
    public Test(Object obj) : base(obj)
    {

    }

    private void OnApplicationQuit()
    {
        id = string.Empty;
        ApplyModifiedProperties();
    }

}
