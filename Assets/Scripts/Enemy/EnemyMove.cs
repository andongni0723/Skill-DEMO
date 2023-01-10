using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;

    int step = 0; // 0: null, 1: target left, 2: target right

    [SerializeField]
    private GameObject leftMoveTarget;
    [SerializeField]
    private GameObject rightMoveTarget;

    private void Update()
    {
        AutoMovement();
    }

    public void AutoMovement()
    {
        switch (step)
        {
            case 0:
                step = 1;
                break;
            case 1:
                if (Mathf.Approximately(transform.position.x, leftMoveTarget.transform.position.x)) // If self pos is LEFT point pos
                    step = 2;
                else
                    transform.position = Vector2.MoveTowards(transform.position, leftMoveTarget.transform.position, speed * Time.deltaTime);
                
                break;
            case 2:
                if (Mathf.Approximately(transform.position.x, rightMoveTarget.transform.position.x)) // If self pos is RIGHT point pos
                    step = 1;
                else
                    transform.position = Vector2.MoveTowards(transform.position, rightMoveTarget.transform.position, speed * Time.deltaTime);
                
                break;
        }
    }
}
