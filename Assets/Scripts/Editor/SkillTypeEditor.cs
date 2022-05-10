using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Skill_SO))]
public class SkillTypeEditor : Editor
{
    private SerializedObject editor;//序列化
    private SerializedProperty skillTypeEnum, shootClass, aoeClass, pointClass, skillAction, actionType, skillEffect, skillVfxObj;

    private void OnEnable()
    {
        editor = new SerializedObject(target);

        skillTypeEnum = editor.FindProperty("skillType");
        shootClass = editor.FindProperty("shootVar");
        aoeClass = editor.FindProperty("aoeVar");
        pointClass = editor.FindProperty("pointVar");
        pointClass = editor.FindProperty("pointVar");
        skillAction = editor.FindProperty("skillActions");

        Debug.Log(editor.FindProperty($"skillActions.Array.data[0].actionType").enumValueFlag);
    }

    public override void OnInspectorGUI()
    {
        InitVar();
    }

    public void InitVar()
    {
        EditorGUILayout.PropertyField(editor.FindProperty("skillID"));
        EditorGUILayout.PropertyField(editor.FindProperty("skillName"));
        EditorGUILayout.PropertyField(editor.FindProperty("skillSprite"));
        EditorGUILayout.PropertyField(editor.FindProperty("skillNum"));
        EditorGUILayout.PropertyField(editor.FindProperty("skillCooldown"));
        EditorGUILayout.PropertyField(skillTypeEnum);
        SkillType();

        EditorGUILayout.PropertyField(skillAction);
        SkillActionEffectType();

    }

    public void SkillType()
    {
        switch (skillTypeEnum.enumValueIndex)
        {
            case 0: // Shoot
                EditorGUILayout.PropertyField(shootClass);
                break;
            case 1: // AOE
                EditorGUILayout.PropertyField(aoeClass);
                break;
            case 2: // Point
                EditorGUILayout.PropertyField(pointClass);
                break;
        }
    }

    public void SkillActionEffectType()
    {
        for (int actionListIndex = 0; actionListIndex < editor.FindProperty("skillActions.Array").arraySize; actionListIndex++)
        {
            actionType = editor.FindProperty($"skillActions.Array.data[{actionListIndex}].actionType");
            skillEffect = editor.FindProperty($"skillActions.Array.data[{actionListIndex}].skillEffect");
            skillVfxObj = editor.FindProperty($"skillActions.Array.data[{actionListIndex}].skillVfxObj");
            var myRect = GUILayoutUtility.GetRect(0f, 16f);

            switch (actionType.enumValueIndex)
            {
                case 0: // Effect
                    EditorGUILayout.PropertyField(skillEffect);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(skillVfxObj);
                    EditorGUI.EndDisabledGroup();
                    break;
                case 1:
                    EditorGUILayout.PropertyField(skillVfxObj);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(skillEffect);
                    EditorGUI.EndDisabledGroup();
                    break;
            }
        }

    }
}