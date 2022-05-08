using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SkillDemo.Skill
{
    public class test : MonoBehaviour
    {
        public Skill_SO skillDatail;

        private Image skillImage;
        private TextMeshProUGUI cooldownText;
        private TextMeshProUGUI skillName;
        private Joystick joystick;

        private bool isCooldown;
        private float timer;

        private void Awake()
        {
            
        }

        private void Update()
        {
            ColdDown();
        }

        public void ButtonUp()
        {
            isCooldown = true;
        }
        public void ColdDown()
        {
            if (isCooldown)
            {
                joystick.enabled = false;
                cooldownText.gameObject.SetActive(true);
                skillImage.color = new Color(255, 255, 255, 100);

                timer += Time.deltaTime;

                skillImage.fillAmount = timer / skillDatail.skillCooldown;
                cooldownText.text = (5 - timer).ToString("0");

                if (timer >= skillDatail.skillCooldown)
                {
                    isCooldown = false;
                    joystick.enabled = true;
                    timer = 0;
                    cooldownText.gameObject.SetActive(false);
                    skillImage.color = new Color(255, 255, 255, 255);
                }
            }
        }
    }
}
