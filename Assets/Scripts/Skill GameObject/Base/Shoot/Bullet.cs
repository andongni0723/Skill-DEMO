using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillDemo.Enemy;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public GameObject originPos;

    public float skillLenth = 0;
    public EffectDatails effect;

    /// <summary>
    /// Basic bullet move
    /// </summary>
    public void BulletMove()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        switch (other.tag)
        {
            case "Enemy":
                other.GetComponent<EnemyHP>().Damage(damage);
                break;
        }
    }

    public void OverSkillLenthDestroyObj()
    {
        if(Vector2.Distance(gameObject.transform.position, originPos.transform.position) > skillLenth)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
