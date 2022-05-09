using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEUI : MonoBehaviour
{
    public int skillNum;
    public float skillMaxRadius;
    public float aoeRadius;

    private Joystick joystick;
    private GameObject circle => transform.GetChild(0).gameObject;
    private GameObject aoe => transform.GetChild(1).gameObject;

    public bool isSetDone = false;

    private void Update()
    {
        if (isSetDone)
        {
            joystick = GameObject.FindGameObjectWithTag("Skill" + skillNum.ToString()).GetComponentInChildren<FloatingJoystick>();

            SetRadius(skillMaxRadius, aoeRadius);
            Look();
        }
    }

    /// <summary>
    /// Set height and width of cicle and shoot
    /// </summary>
    /// <param name="_skillMaxRadius">set cicle x, y and shoot y</param>
    /// <param name="_aoeRaius">set shoot x</param>
    private void SetRadius(float _skillMaxRadius, float _aoeRaius)
    {
        circle.GetComponent<SpriteRenderer>().size = new Vector2(_skillMaxRadius, _skillMaxRadius);
        aoe.GetComponent<SpriteRenderer>().size = new Vector2(_aoeRaius, _aoeRaius);
    }

    /// <summary>
    /// Let shoot arrow look at joystick direction
    /// </summary>
    private void Look()
    {
        circle.SetActive(joystick.transform.GetChild(0).gameObject.activeSelf);
        aoe.SetActive(joystick.transform.GetChild(0).gameObject.activeSelf);

        Vector2 UI = new Vector2(joystick.Direction.x * skillMaxRadius, joystick.Direction.y * skillMaxRadius);
        aoe.transform.localPosition = UI;
    }
}
