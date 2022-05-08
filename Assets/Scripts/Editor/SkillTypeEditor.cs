using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Skill_SO))]
public class SkillTypeEditor : Editor
{
    private SerializedObject editor;//序列化
    private SerializedProperty skillTypeEnum, shootClass, aoeClass, pointClass;

    private void OnEnable()
    {
        editor = new SerializedObject(target);

        skillTypeEnum = editor.FindProperty("skillType");
        shootClass = editor.FindProperty("shootVar");
        aoeClass = editor.FindProperty("aoeVar");
        pointClass = editor.FindProperty("pointVar");
    }

    public override void OnInspectorGUI()
    {
        editor.Update();

        InitVar();

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

        editor.ApplyModifiedProperties();
    }

    public void InitVar()
    {
        EditorGUILayout.PropertyField(editor.FindProperty("skillID"));
        EditorGUILayout.PropertyField(editor.FindProperty("skillName"));
        EditorGUILayout.PropertyField(editor.FindProperty("skillSprite"));
        EditorGUILayout.PropertyField(editor.FindProperty("skillNum"));
        EditorGUILayout.PropertyField(editor.FindProperty("skillCooldown"));
        EditorGUILayout.PropertyField(skillTypeEnum);
    }
}