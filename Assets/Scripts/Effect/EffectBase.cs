using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    public GameObject effectIcon;
    public GameObject effectTarget;

    public float effectTime;

    private float timer;

    public bool isSetDone;

    public virtual void Awake()
    {
        effectIcon.SetActive(true);
    }

    public virtual void Update()
    {
        TimeToDestroy();
    }

    public void TimeToDestroy()
    {
        timer += Time.deltaTime;
        print("time");

        if (timer >= effectTime)
        {
            BeforeDestroyEffect();
            Destroy(gameObject);
        }
    }

    public virtual void EffectAction() { }

    public virtual void BeforeDestroyEffect() { }
}
