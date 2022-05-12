using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action SetSkillDone;
    public static void CallSetSkillDone()
    {
        SetSkillDone?.Invoke();
    }

    public static event Action SetInitSkillObjDone;
    public static void CallSSetInitSkillObjDone()
    {
        SetInitSkillObjDone?.Invoke();
    }

    public static event Action<Quaternion, Vector3> SaveSkillShowUIData;
    public static void CallSaveSkillShowUIData(Quaternion rotation, Vector3 pos)
    {
        SaveSkillShowUIData?.Invoke(rotation, pos);
    }
}
