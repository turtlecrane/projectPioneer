using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class damage : MonoBehaviour
{
    // public TextMeshPro text;
    TMP_Text text;
    Enemy enemy;
    public bool SkillDamage = false;
    public bool basicDamage = false;
    private void Awake()
    {
        //GameObject.FindWithTag("Enemy").
        
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
        text = enemy.Damage_prefep.GetComponent<TMP_Text>();
        
        // if (enemy.CompareSkill)//적이 스킬공격에 맞았을때
        // {
        //     Debug.Log(enemy.CompareSkill);
        //     if (enemy.CritPercen)
        //     {
        //         text.text = "<color=#43c6cf>" + enemy.SkillCritdamage.ToString() + "!!! </color>";
        //     }
        //     else
        //     {
        //         text.text = "<color=#3c7afc>" + enemy.Skilldamage.ToString() + "</color>";
        //     }
        //     // enemy.CompareSkill = false;
        // }
        // else//적이 기본공격에 맞았을때
        // {
        //     if (enemy.CritPercen)
        //     {
        //         text.text = "<color=#ff4c44>" + enemy.Cridamage.ToString() + "! </color>";
        //     }
        //     else
        //     {
        //         text.text = enemy.damage.ToString();
        //     }
        // }
        
    }

    void Update()
    {   
        Debug.Log("[[[[ SkillDamage : " + SkillDamage);
        Debug.Log("basicDamage : " + basicDamage);
        if (enemy.CompareSkill) // 스킬에 맞았을때.
        {
            text.text = "<color=#3c7afc>" + enemy.Skilldamage.ToString() + "</color>";
            SkillDamage = true;
            basicDamage = false;
            enemy.CompareSkill = false;
        }
        if (enemy.CompareWeapon) // 기본 공격에 맞았을때
        {
            text.text = enemy.damage.ToString();
            SkillDamage = false;
            basicDamage = true;
            enemy.CompareWeapon = false;
        }
        
        if (SkillDamage)
        {
            text.text = "<color=#3c7afc>" + enemy.Skilldamage.ToString() + "</color>";
        }
        if (basicDamage)
        {
            text.text = enemy.damage.ToString();
        }
        // else if(!SkillDamage)
        // {
        //     text.text = enemy.damage.ToString();
        // }
    }
}
