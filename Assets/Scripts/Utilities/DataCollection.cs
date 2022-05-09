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
public class SkillActionDetails
{
    public float startTime;

    public ActionType actionType;
    public List<Effect> skillEffect = new List<Effect>();

    public GameObject skillVfxObj;
    public float effectDuration;
}