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
        private float AnimTimer = 0;
        private float playerAttackDamage;
        private bool callSetAndPlayDamageAnim = false;

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
            callSetAndPlayDamageAnim = true;

            StartCoroutine(DamageAnim(damage));
        }

        // private void SetAndPlayDamageAnim(float damage)
        // {
        //     AnimTimer = 0;
        //     damageText.gameObject.SetActive(true);
        //     damageText.text = "-" + damage.ToString();
        //     damageText.alpha = 255;
        //     StartCoroutine(DamageAnim(damage));

        // }

        private IEnumerator DamageAnim(float damage)
        {
            damageText.text = "-" + damage.ToString();
            damageText.gameObject.SetActive(true);

            yield return new WaitForSeconds(hpShowAnimTime);

            damageText.gameObject.SetActive(false);
        }
    }
}
