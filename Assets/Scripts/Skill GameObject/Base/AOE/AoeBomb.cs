using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillDemo.Enemy;

public class AoeBomb : MonoBehaviour
{
    Animator anim;
    [SerializeField]

    [HideInInspector]
    public float damage;
    
    [SerializeField]
    float waitAnimTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.tag)
        {
            case "Enemy":
                other.GetComponent<EnemyHP>().Damage(damage);
                break;
        }
    }

    public IEnumerator ObjDestroyAnim()
    {
        yield return new WaitForSeconds(waitAnimTime);
        anim.Play("BombDestroy");
    }

    public void AnimEndDestroy()
    {
        Destroy(gameObject);
    }
}
