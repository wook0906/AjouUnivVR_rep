using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum AnimationLayerType
    {
        A_1,
        A_2,
        A_3,
        A_4,
        A_5,
    }
    public enum CharacterType
    {
        AYun,
        CustomerA,
        CustomerB,
        CustomerC,
        MinSu,
        SeungWook,
        YoungMan,
        YoungSoo
    }
    public enum BranchType
    {
        A_1_1,
        A_2_1,
        A_2_2,
        A_2_3,
        A_3_1,
        A_3_3,
        A_3_4,
        A_3_6,
        A_4_1,
        A_4_2
    }
    public enum State
    {
        Idle,
        Moving,
    }
    public enum Emotion
    {
        None
    }
    public enum Scene
    {
        Unknown,
        StartScene,
        A_1_1,
        A_1_2,
        A_2_1,
        A_2_2,
        A_2_3,
        A_2_4,
        A_3_1,
        A_3_2,
        A_3_3,
        A_3_4,
        A_3_5,
        A_3_6,
        A_3_7,
        A_4_1,
        A_4_2,
        A_4_3,
        A_5_1,
        A_5_2,
        A_5_3,
        End,
    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum MouseEvent
    {
        Press,
        Click
    }
    public enum CameraMode
    {
        QuaterView,
    }
}
