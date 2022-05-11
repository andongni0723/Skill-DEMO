using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBullet : Bullet
{   
    private void Update() {
        BulletMove();
        OverSkillLenthDestroyObj();
    }
}
