using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shoot
{
    public float shootHeight;
    public float shootWidth;
}

[System.Serializable]
public class AOE
{
    public float aoeSkillMaxRadius;
    public float aoeRadius;
}

[System.Serializable]
public class Point
{
    public float pointSkillMaxRadius;
}

[System.Serializable]
public class EffectDatails
{
    public Effect effect;
    public float effectDuration;
    public EffectTarget effectTarget;
}

[System.Serializable]
public class SkillActionDetails
{
    public float startTime;

    [Header("Skill Action Type")]
    public ActionType actionType;


    [Header("Effect Setting")]
    [Space(10)]
    public List<EffectDatails> skillEffect = new List<EffectDatails>();
    

    [Header("New Object Setting")]
    [Space(10)]
    public GameObject skillVfxObj;
    public float skillDamage;


}