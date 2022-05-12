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

        public Slider skillLoadingSlider;



        private bool isCooldown;
        private float timer;

        private bool isSkillSettingDone = false;
        [SerializeField]
        private int skillStatus = 0; // (0: Nothing , 1: Mouse button Down  , 2: Excute skill  , 3: Mouse button Up )

        private

        //Excute Skill Var
        List<SkillActionDetails> skillActions;
        float excuteSkillTimer = 0;
        float excuteSkillTimerAddTime = 0.1f;

        Quaternion skillShowUIRotation;
        Vector3 skillShowUIPosition;



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


        private new void Awake()
        {
            skillImage = GetComponentInChildren<Image>();
            skillName = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            cooldownText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            joystick = GetComponentInChildren<FloatingJoystick>();
            skillLoadingSlider = PlayerMove.Instance.gameObject.GetComponentInChildren<Slider>();
            
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

            skillLoadingSlider.gameObject.SetActive(false);
            isSkillSettingDone = true;

        }

        private void OnSaveSkillShowUIData(Quaternion rotation, Vector3 pos)
        {
            skillShowUIRotation = rotation;
            skillShowUIPosition = pos;
            
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
            StopCoroutine(ExecuteSkill());
            excuteSkillTimer = 0;
            skillLoadingSlider.gameObject.SetActive(false);
            skillLoadingSlider.value = 0;

            StartCoroutine(ExecuteSkill());
        }

        /// <summary>
        /// Excute Skill in SkillAction(SkillDetails_SO)
        /// </summary>
        /// <returns></returns>
        private IEnumerator ExecuteSkill()
        {
            float skillLoagingSliderSaveTime = 0;

            // If the point skill show UI doesn't have target enemy
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
                // If the 'script skill time' not equal the 'skill action time', the timer will add times and let script begin to here
            StartExcute: 

                //print("Timer: " + excuteSkillTimer);
                if(skillDatail.skillActions[index].isSkillLoadingSliderOpen)
                {
                    skillLoadingSlider.gameObject.SetActive(true);
                    skillLoagingSliderSaveTime = excuteSkillTimer;
                    skillLoadingSlider.value = skillLoagingSliderSaveTime / skillActions[index].startTime;
                }

                if (Mathf.Approximately(excuteSkillTimer, skillActions[index].startTime))
                {

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
                                    //print("1");
                                    GameObject skillShootObj = Instantiate(skillActions[index].skillVfxObj, PlayerMove.Instance.transform.position, skillShowUIRotation);

                                    skillShootObj.GetComponentInChildren<SpriteRenderer>().size = new Vector2(skillDatail.shootVar.shootWidth, skillDatail.shootVar.shootObjHeight);
                                    skillShootObj.GetComponentInChildren<Bullet>().skillLenth = skillDatail.shootVar.shootHeight;
                                    skillShootObj.GetComponentInChildren<Bullet>().damage = skillDatail.skillActions[index].skillDamage;
                                    skillShootObj.GetComponentInChildren<BoxCollider2D>().size = new Vector2(skillDatail.shootVar.shootWidth, skillDatail.shootVar.shootObjHeight);                               
                                    break;

                                case SkillType.AOE:
                                    GameObject skillAoeObj = Instantiate(skillActions[index].skillVfxObj, skillShowUIPosition, Quaternion.identity);
                                    
                                    skillAoeObj.GetComponentInChildren<SpriteRenderer>().size = new Vector2(skillDatail.aoeVar.aoeRadius, skillDatail.aoeVar.aoeRadius);
                                    skillAoeObj.GetComponentInChildren<AoeBomb>().damage = skillDatail.skillActions[index].skillDamage;
                                    skillAoeObj.GetComponentInChildren<CircleCollider2D>().radius = skillDatail.aoeVar.aoeRadius / 2;

                                    break;
                                case SkillType.Point:
                                    break;
                            }

                            break;
                    }

                    excuteSkillTimer += excuteSkillTimerAddTime;
                    

                    yield return new WaitForSeconds(excuteSkillTimerAddTime);

                }
                else // the element start time isn't now excute time
                {
                    excuteSkillTimer += excuteSkillTimerAddTime;     

                    yield return new WaitForSeconds(excuteSkillTimerAddTime);
                    //print("2");
                    goto StartExcute;
                }

                skillLoadingSlider.gameObject.SetActive(false);
            }

            yield return null;
        }
    }
}
