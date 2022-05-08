using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SkillDemo.Skill
{
    public class SkillUI : MonoBehaviour
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
            skillImage = GetComponentInChildren<Image>();
            skillName = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            cooldownText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            joystick = GetComponentInChildren<FloatingJoystick>();

            skillImage.sprite = skillDatail.skillSprite;
            skillName.text = skillDatail.skillName;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isCooldown = true;
            }

            ColdDown();
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
