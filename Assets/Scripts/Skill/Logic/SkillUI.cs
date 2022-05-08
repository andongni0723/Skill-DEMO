using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SkillDemo.Skill
{
    public class SkillUI : Singleton<SkillUI>
    {
        public Skill_SO skillDatail;

        private Image skillImage;
        private TextMeshProUGUI cooldownText;
        private TextMeshProUGUI skillName;
        private Joystick joystick;
        public GameObject skillShow;



        private bool isCooldown;
        private float timer;

        private bool isSkillSettingDone = false;
        [SerializeField]
        private int skillStatus = 0; // 0: Nothing , 1: Mouse button Down  , 2: Mouse button Up
        private void OnEnable()
        {
            EventHandler.SetSkillDone += OnSetSkillDone;
        }

        private void OnDisable()
        {
            EventHandler.SetSkillDone -= OnSetSkillDone;
        }


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
            if (isSkillSettingDone)
            {
                ColdDown();

                skillStatus = ChangeSkillStatus(skillStatus);

            }
        }
        private void OnSetSkillDone()
        {
            skillShow = GameObject.FindGameObjectWithTag("SkillUI").transform.GetChild(skillDatail.skillNum - 1).GetChild(0).gameObject;
            isSkillSettingDone = true;

        }

        private void ColdDown()
        {
            if (skillStatus == 2)
            {
                joystick.enabled = false;
                cooldownText.gameObject.SetActive(true);
                skillImage.color = new Color(255, 255, 255, 100);

                timer += Time.deltaTime;

                skillImage.fillAmount = timer / skillDatail.skillCooldown;
                cooldownText.text = (5 - timer).ToString("0");

                if (timer >= skillDatail.skillCooldown)
                {
                    joystick.enabled = true;
                    timer = 0;
                    cooldownText.gameObject.SetActive(false);
                    skillImage.color = new Color(255, 255, 255, 255);

                    skillStatus = 0;
                }
            }
        }

        /// <summary>
        /// If skill UI status change, return the status num
        /// </summary>
        /// <param name="_status">Input current status num</param>
        /// <returns>status num (0: Nothing , 1: Mouse button Down  , 2: Mouse button Up )</returns>
        public int ChangeSkillStatus(int _status)
        {
            if (skillShow.activeInHierarchy == true)
                return 1;
            else if ((skillShow.activeInHierarchy == false && _status == 1) || (_status == 2))
                return 2;
            else if (skillShow.activeInHierarchy == false && _status != 2)
                return 0;
            else
                return 1;
        }
    }
}
