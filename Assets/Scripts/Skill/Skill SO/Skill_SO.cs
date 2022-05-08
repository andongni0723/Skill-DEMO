using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill/skill")]
public class Skill_SO : ScriptableObject
{
    [Header("UI Details")]
    public string skillID;
    public string skillName;
    public Sprite skillSprite;

    [Header("Game Details")]
    public SkillType skillType;
    public float skillCooldown;

    [Space(4f)]
    public float width;
    public float height;

    public UnityEvent OnSkill;
}