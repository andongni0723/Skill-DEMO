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

        [SerializeField]
        private float hp = 100;

        private float hpShowAnimTime = 2;
        private float AnimTimer = 0;
        private float playerAttackDamage;
        private bool callSetAndPlayDamageAnim = false;

        private void Awake()
        {
            damageText.gameObject.SetActive(false);
        }

        private void Update()
        {
            HpNumToSliderUI();
            
            if(callSetAndPlayDamageAnim)
            {
                SetAndPlayDamageAnim(playerAttackDamage);
                callSetAndPlayDamageAnim = false;
            }
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
        }

        private void SetAndPlayDamageAnim(float damage)
        {
            AnimTimer = 0;
            damageText.gameObject.SetActive(true);
            damageText.text = "-" + damage.ToString();
            damageText.alpha = 255;
            StartCoroutine(DamageAnim(damage));

        }

        private IEnumerator DamageAnim(float damage)
        {
            if (AnimTimer >= hpShowAnimTime)
            {
                damageText.gameObject.SetActive(false);
                print("OVER");
                yield return null;
            }
            print(damageText.alpha);
            damageText.alpha -= ( 0.1f / hpShowAnimTime);
            //print(damageText.alpha);

            AnimTimer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
