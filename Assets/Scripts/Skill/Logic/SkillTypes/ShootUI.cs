using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUI : MonoBehaviour
{
    public int skillNum;
    public float height;
    public float width;

    private Joystick joystick;
    private GameObject circle => transform.GetChild(0).gameObject;
    private GameObject shoot => transform.GetChild(1).gameObject;

    bool isSetDone = false;


    private void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("Skill" + skillNum.ToString()).GetComponentInChildren<FloatingJoystick>();
    }

    private void Update()
    {
        SetRadius(height, width);
        Look();
    }

    /// <summary>
    /// Set height and width of cicle and shoot
    /// </summary>
    /// <param name="_height">set cicle x, y and shoot y</param>
    /// <param name="_width">set shoot x</param>
    private void SetRadius(float _height, float _width)
    {
        circle.GetComponent<SpriteRenderer>().size = new Vector2(_height, _height);
        shoot.GetComponent<SpriteRenderer>().size = new Vector2(_width, _height);
    }

    /// <summary>
    /// Let shoot arrow look at joystick direction
    /// </summary>
    private void Look()
    {
        shoot.SetActive(joystick.transform.GetChild(0).gameObject.activeSelf);
        circle.SetActive(joystick.transform.GetChild(0).gameObject.activeSelf);

        float angle = Mathf.Atan2(joystick.Direction.y, joystick.Direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
