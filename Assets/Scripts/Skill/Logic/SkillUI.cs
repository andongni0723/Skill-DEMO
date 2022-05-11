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
        public GameObject skillShowChild;
        public GameObject skillShowUI;



        private bool isCooldown;
        private float timer;

        private bool isSkillSettingDone = false;
        [SerializeField]
        private int skillStatus = 0; // (0: Nothing , 1: Mouse button Down  , 2: Excute skill  , 3: Mouse button Up )

        private

        //Excute Skill Var
        List<SkillActionDetails> skillActions;
        int index = 0;
        float excuteSkillTimer = 0;

        Quaternion skillShowUIRotation;



        private void OnEnable()
        {
            EventHandler.SetSkillDone += OnSetSkillDone;
            EventHandler.SaveSkillShowUIData += OnSaveSkillShowUIData;
        }

        private void OnDisable()
        {
            EventHandler.SetSkillDone -= OnSetSkillDone;
            EventHandler.SaveSkillShowUIData += OnSaveSkillShowUIData;
        }


        private void Awake()
        {
            skillImage = GetComponentInChildren<Image>();
            skillName = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            cooldownText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            joystick = GetComponentInChildren<FloatingJoystick>();
            skillActions = skillDatail.skillActions;

            skillImage.sprite = skillDatail.skillSprite;
            skillName.text = skillDatail.skillName;
        }

        private void Update()
        {
            if (isSkillSettingDone)
            {
                ColdDown();

                skillStatus = ChangeSkillStatus(skillStatus);
                //print(skillStatus);

            }
        }
        private void OnSetSkillDone()
        {
            skillShowUI = GameObject.FindGameObjectWithTag("SkillUI").transform.GetChild(skillDatail.skillNum - 1).gameObject;
            skillShowChild = skillShowUI.transform.GetChild(1).gameObject;

            isSkillSettingDone = true;

        }

        private void OnSaveSkillShowUIData()
        {
            skillShowUIRotation = skillShowUI.transform.rotation;
        }

        private void ColdDown()
        {
            if (skillStatus == 3)
            {
                joystick.enabled = false;
                cooldownText.gameObject.SetActive(true);
                skillImage.color = new Color(255, 255, 255, 100);

                timer += Time.deltaTime;

                skillImage.fillAmount = timer / skillDatail.skillCooldown;
                cooldownText.text = (skillDatail.skillCooldown - timer).ToString("0");

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
        /// <returns>status num (0: Nothing , 1: Mouse button Down  , 2: Excute skill  , 3: Mouse button Up )</returns>
        public int ChangeSkillStatus(int _status)
        {
            if (skillShowChild.activeInHierarchy == true)
                return 1;
            else if (skillShowChild.activeInHierarchy == false && _status == 1)
            {
                SetExcuteSkill();
                return 2;
            }
            else if (_status == 3 || _status == 2)
                return 3;
            else if (skillShowChild.activeInHierarchy == false && _status != 2)
                return 0;
            else
                return 1;


        }

        private void SetExcuteSkill()
        {
            index = 0;
            timer = 0;

            StartCoroutine(ExecuteSkill());
        }

        /// <summary>
        /// Excute Skill in SkillAction(SkillDetails_SO)
        /// </summary>
        /// <returns></returns>
        private IEnumerator ExecuteSkill()
        {
            // If the point skill doesn't have target enemy
            if (skillDatail.skillType == SkillType.Point)
            {
                PointUI pointUI = skillShowChild.GetComponent<PointUI>();

                if (pointUI.targetEnemy == null)
                {
                    skillStatus = 0;
                    yield return null;
                }
            }

            // Excute Action in Skill Action 
            for (int index = 0; index < skillActions.Count; index++) 
            {
                if (excuteSkillTimer == skillActions[index].startTime)
                {
                    print(excuteSkillTimer);
                    //FIXME: can't shoot at second time

                    switch (skillActions[index].actionType)
                    {
                        case ActionType.Effect:
                            if (skillActions[index].skillEffect == null) break;

                            break;
                        case ActionType.NewObject:
                            if (skillActions[index].skillVfxObj == null) break;

                            switch (skillDatail.skillType)
                            {
                                case SkillType.Shoot:                                  
                                    GameObject skillObj = Instantiate(skillActions[index].skillVfxObj, PlayerMove.Instance.transform.position, skillShowUIRotation);
                                    
                                    skillObj.GetComponentInChildren<SpriteRenderer>().size = new Vector2(skillDatail.shootVar.shootWidth, 1);
                                    skillObj.GetComponentInChildren<Bullet>().skillLenth = skillDatail.shootVar.shootHeight;
                                    break;

                                case SkillType.AOE:
                                    break;
                            }

                            break;
                    }
                }

                excuteSkillTimer += 0.25f;
                //yield return new WaitForSeconds(0.25f);
            }

            yield return null;
        }
    }
}
