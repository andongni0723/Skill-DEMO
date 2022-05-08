using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillDemo.Skill
{
    public class SkillManager : MonoBehaviour
    {

        private List<SkillUI> skills = new List<SkillUI>();

        private GameObject playerSkillUI;

        private void Start()
        {
            InitSkillUI();
            playerSkillUI = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;

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
                        Instantiate(PrefabManager.Instance.skillShoot, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity, playerSkillUI.transform);
                        break;
                }
            }
        }
    }

}
