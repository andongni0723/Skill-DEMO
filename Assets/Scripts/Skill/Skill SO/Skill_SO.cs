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
    public float skillCooldown;
    public int skillNum;

    [Header("Skill Type Setting")]  
    public SkillType skillType;
    public Shoot shootVar = new Shoot(); // Shoot UI
    public AOE aoeVar = new AOE(); // AOE UI
    public Point pointVar = new Point(); // Point UI

    [Space(10f)]
    public List<SkillActionDetails> skillActions = new List<SkillActionDetails>();
}