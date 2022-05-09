using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkillDemo.Skill
{
    public class SkillManager : Singleton<SkillManager>
    {

        private List<SkillUI> skills = new List<SkillUI>();

        public GameObject playerSkillUI;

        private void Start()
        {
            InitSkillUI();
            playerSkillUI = GameObject.FindGameObjectWithTag("SkillUI").gameObject;

            GetSkillDataAndLoad();
        }

        public void InitSkillUI()
        {
            for (int i = 1; i < 4; i++)
            {
                skills.Add(GameObject.FindGameObjectWithTag("Skill" + i).GetComponent<SkillUI>());
            }
        }

        private void GetSkillDataAndLoad()
        {
            foreach (var item in skills)
            {
                switch (item.skillDatail.skillType)
                {
                    //WORKFLOW: Add the all skill type after all player skill UI is done

                    case SkillType.Shoot:
                        GameObject shoot = (GameObject)Instantiate(PrefabManager.Instance.skillShoot, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity, playerSkillUI.transform);
                        ShootUI shootUI = shoot.GetComponent<ShootUI>();

                        shootUI.height = item.skillDatail.shootVar.shootHeight;
                        shootUI.width= item.skillDatail.shootVar.shootWidth;
                        shootUI.skillNum = skills.IndexOf(item) + 1;
                        shootUI.isSetDone = true;
                        break;

                    case SkillType.AOE:
                        GameObject aoe = (GameObject)Instantiate(PrefabManager.Instance.skillAOE, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity, playerSkillUI.transform);
                        AOEUI aoeUI = aoe.GetComponent<AOEUI>();

                        aoeUI.skillMaxRadius = item.skillDatail.aoeVar.aoeSkillMaxRadius;
                        aoeUI.aoeRadius = item.skillDatail.aoeVar.aoeRadius;
                        aoeUI.skillNum = skills.IndexOf(item) + 1;
                        aoeUI.isSetDone = true;
                        break;

                    case SkillType.Point:
                        GameObject point = (GameObject)Instantiate(PrefabManager.Instance.skillPoint, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity, playerSkillUI.transform);
                        PointUI pointUI = point.GetComponent<PointUI>();

                        pointUI.skillMaxRadius = item.skillDatail.pointVar.pointSkillMaxRadius; 
                        pointUI.skillNum = skills.IndexOf(item) + 1;
                        pointUI.isSetDone = true;             
                        break;
                }
            }

            EventHandler.CallSetSkillDone();
        }
    }

}
