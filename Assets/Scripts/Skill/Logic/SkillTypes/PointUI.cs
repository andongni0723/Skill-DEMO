using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    public int skillNum;
    public float skillMaxRadius;

    private Joystick joystick;
    private GameObject circle => transform.GetChild(0).gameObject;
    private GameObject point => transform.GetChild(1).gameObject;
    private GameObject pointJoystick => transform.GetChild(2).gameObject;

    public bool isSetDone = false;
    [SerializeField]
    private List<GameObject> enemys = new List<GameObject>();

    [SerializeField]
    private GameObject targetEnemy;

    private void Update()
    {
        if (isSetDone)
        {
            joystick = GameObject.FindGameObjectWithTag("Skill" + skillNum.ToString()).GetComponentInChildren<FloatingJoystick>();

            SetRadius(skillMaxRadius);
            Look();
        }
    }

    /// <summary>
    /// Set height and width of cicle and shoot
    /// </summary>
    /// <param name="_skillMaxRadius">set cicle x, y and shoot y</param>
    /// <param name="_aoeRaius">set shoot x</param>
    private void SetRadius(float _skillMaxRadius)
    {
        circle.GetComponent<SpriteRenderer>().size = new Vector2(_skillMaxRadius, _skillMaxRadius);
    }

    /// <summary>
    /// Let shoot arrow look at joystick direction
    /// </summary>
    private void Look()
    {
        circle.SetActive(joystick.transform.GetChild(0).gameObject.activeSelf);
        point.SetActive(joystick.transform.GetChild(0).gameObject.activeSelf);
        pointJoystick.SetActive(joystick.transform.GetChild(0).gameObject.activeSelf);

        // point joystick
        Vector2 UI = new Vector2(joystick.Direction.x * skillMaxRadius, joystick.Direction.y * skillMaxRadius);
        pointJoystick.transform.localPosition = UI;

        /// point UI on screen

        // Logic
        enemys.Clear();
        targetEnemy = null;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) <= skillMaxRadius)
            {
                enemys.Add(enemy);
            }          
        }

        if (enemys.Count > 1)
        {
            float lastDistance = 100;
            float nowDistance = 0;

            foreach (GameObject enemy in enemys)
            {
                // Get enemy distance
                nowDistance = Vector2.Distance(pointJoystick.transform.position, enemy.transform.position);
                print(nowDistance);
                if (nowDistance < lastDistance) targetEnemy = enemy;
                
                //Init
                lastDistance = nowDistance;
                nowDistance = 0;
            }
        }
        else if(enemys.Count == 1)
        {
            targetEnemy = enemys[0];
        }

        // Show on Screen
        if(targetEnemy != null)
            point.transform.position = targetEnemy.transform.position;
        else
            point.transform.position = pointJoystick.transform.position;
    }
}
