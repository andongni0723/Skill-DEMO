using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSlowly : EffectBase
{
    public GameObject slowlyVfxObj;
    public GameObject slowlyVfxSetPos;

    public float setVfxObjTime;
    public float speedSlowlyVaulePercent;

    private float vfxTimer;
    private float originTargetSpeed;

    public override void Awake()
    {
        base.Awake();
        EffectAction();
    }

    public override void Update()
    {
        base.Update();
        NewVfxObj();
    }
    private void NewVfxObj()
    {
        vfxTimer += Time.deltaTime;

        if (vfxTimer >= setVfxObjTime)
        {
            Instantiate(slowlyVfxObj, slowlyVfxSetPos.transform.position, Quaternion.identity);
            vfxTimer = 0;
        }
    }

    public override void EffectAction()
    {
        //// Target Player
        // originTargetSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().speed;
        // GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().speed *= speedSlowlyVaulePercent / 100;

        // Target Enemy
        originTargetSpeed = effectTarget.GetComponent<EnemyMove>().speed;
        effectTarget.GetComponent<EnemyMove>().speed *= speedSlowlyVaulePercent / 100;
    }

    public override void BeforeDestroyEffect()
    {
        effectTarget.GetComponent<EnemyMove>().speed = originTargetSpeed;
    }
}
