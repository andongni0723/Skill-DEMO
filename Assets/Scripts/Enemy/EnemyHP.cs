using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SkillDemo.Enemy
{
    public class EnemyHP : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI damageText;

        private Animator anim;

        [SerializeField]
        private float hp = 100;

        [SerializeField]
        private float hpShowAnimTime = 2;
        private float playerAttackDamage;

        private void Awake()
        {
            damageText.gameObject.SetActive(false);
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            HpNumToSliderUI();
        }

        private void HpNumToSliderUI()
        {
            if (hp <= 0)
            {
                Destroy(gameObject);
            }

            slider.value = hp / 100;
        }

        public void Damage(float damage)
        {
            playerAttackDamage = damage;
            hp -= playerAttackDamage;

            StartCoroutine(DamageAnim(damage));
        }

        private IEnumerator DamageAnim(float damage)
        {
            damageText.text = "-" + damage.ToString();
            damageText.gameObject.SetActive(true);

            yield return new WaitForSeconds(hpShowAnimTime);

            damageText.gameObject.SetActive(false);
        }
    }
}
