using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;

    /// <summary>
    /// Basic bullet move
    /// </summary>
    public void BulletMove()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }
}
