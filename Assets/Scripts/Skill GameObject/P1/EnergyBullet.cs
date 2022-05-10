using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnergyBullet : Bullet
{   
    private BoxCollider2D coll;

    private void Awake() {
        coll = GetComponent<BoxCollider2D>();


    }
    private void Update() {
        BulletMove();
    }
}
