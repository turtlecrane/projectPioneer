using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class player : MonoBehaviour
{
    //public bool CritPercen;

    Animator animator;
    public int atkDmg = 10;
    public int skillDmg;
    public float atkSpeed = 1;
    public bool attacked = false;
    MoveController moveController;
    
    public Transform skillEffectPos;//데미지가 표시될 위치
    public GameObject skillEffectprefep;//데미지 프리펩 
    
    void Start()
    {
        moveController = GameObject.FindWithTag("User").GetComponent<MoveController>();
        //CritPercen = Dods_ChanceMaker.GetThisChanceResult_Percentage(30); //30퍼 확률 설정 (크리티컬 용)  
        atkDmg = 10;//데미지
        skillDmg = atkDmg * 3; //스킬데미지
        animator = GetComponent<Animator>();
        //주의! SetAttackSpeed는 항상 animator = GetComponent<Animator>(); 아래에 있어야 합니다.
        //SetAttackSpeed(1.0f);
    }

    void Update()
    {
        if (moveController.isSkill)
        {
            var skillEffect = Instantiate<GameObject>(skillEffectprefep);//히트모션 생성 
            skillEffect.transform.position = skillEffectPos.position;
            
        }
    }

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }
}

public static class Dods_ChanceMaker
{
    public static bool GetThisChanceResult(float Chance)
    {
        if (Chance < 0.0000001f)
        {
            Chance = 0.0000001f;
        }

        bool Success = false;
        int RandAccuracy = 10000000;
        float RandHitRange = Chance * RandAccuracy;
        int Rand = UnityEngine.Random.Range(1, RandAccuracy + 1);
        if (Rand <= RandHitRange)
        {
            Success = true;
        }
        return Success;
    }

    public static bool GetThisChanceResult_Percentage(float Percentage_Chance)
    {
        if (Percentage_Chance < 0.0000001f)
        {
            Percentage_Chance = 0.0000001f;
        }

        Percentage_Chance = Percentage_Chance / 100;

        bool Success = false;
        int RandAccuracy = 10000000;
        float RandHitRange = Percentage_Chance * RandAccuracy;
        int Rand = UnityEngine.Random.Range(1, RandAccuracy + 1);
        if (Rand <= RandHitRange)
        {
            Success = true;
        }
        return Success;
    }
}