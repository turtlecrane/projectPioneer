using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Enemy : MonoBehaviour
{
    TMP_Text text;
    public bool CompareWeapon = false;
    public bool CompareSkill = false;
    public int SkillCritdamage;
    public int Skilldamage;
    public bool CritPercen;
    public int Cridamage;
    public int damage;
    public Transform DamagePoint;//데미지가 표시될 위치
    public GameObject Damage_prefep;//데미지 프리펩 
    private Transform HitEffectPoint;
    public GameObject HitEffect_prefep;

    enum States
    {
        idle = 1,
        move = 2,
        die = 3,
        atk = 4
    }

    public GameObject prefepHPbar;
    private Transform EnemyHPbarPos;
    RectTransform hpbar;
    public float height;
    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public int atkSpeed;
    string State = "Status";
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;
    public Animator enemyAnimator;
    AudioSource damagesource;
    player player;
    MoveController moveController;
    Image nowHpbar;

    // private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, int _atkSpeed)
    // {
    //     enemyName = _enemyName;
    //     maxHp = _maxHp;
    //     nowHp = _maxHp;
    //     atkDmg = _atkDmg;
    //     atkSpeed = _atkSpeed;
    // }

    void Start()
    {
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), false);
        
        damagesource = GameObject.Find("damageAudio").GetComponent<AudioSource>();

        player = GameObject.FindWithTag("Player").GetComponent<player>();
        moveController = GameObject.FindWithTag("User").GetComponent<MoveController>();
        EnemyHPbarPos = GameObject.FindWithTag("EnemyHP_Spawn").GetComponent<Transform>();

        DamagePoint = this.transform.Find("DamagePoint"); //적 오브젝트 하위에 있는 데미지 생성 포인트 위치 설저
        HitEffectPoint = this.transform.Find("HitEffectPoint");//적 오브젝트 하위에있는 히트모션생성포인트 위치 설정

        enemyAnimator = GetComponent<Animator>();

        hpbar = Instantiate(prefepHPbar, EnemyHPbarPos).GetComponent<RectTransform>();
        
        // if (tag.Equals("tutoEnemy"))
        // {
        //     SetEnemyStatus("Enemy", 100, 10, 1);
        // }
        
        nowHpbar = hpbar.transform.GetChild(0).GetComponent<Image>();
        text = Damage_prefep.GetComponent<TMP_Text>();
        // moveSpeed = 0.4f;
        // atkRange = 1.5f;
        // fieldOfVision = 5;
    }

    void Update()
    {
        
        Cridamage = player.atkDmg * 2;
        damage = player.atkDmg;
        Skilldamage = player.atkDmg * 3;
        SkillCritdamage = player.atkDmg * 4;
        CritPercen = Dods_ChanceMaker.GetThisChanceResult_Percentage(30); //30퍼 확률 설정 (크리티컬 용)  
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpbar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;

        if(moveController.isPotal == true) //플레이어가 포탈을 이동하면 체력바가 사라
        {
            Destroy(hpbar.gameObject);
        }

    }
    //스크립트가 붙어있는 오브젝트에 Trigger가 닿았을 때 실행되는 이벤트
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Debug.Log("CompareWeapon : " + CompareWeapon);
            var HitEffect = Instantiate<GameObject>(this.HitEffect_prefep);//히트모션 생성 
            HitEffect.transform.position = this.HitEffectPoint.position;

            damagesource.Play();
            if (CritPercen)
            {
                text.text = "<color=#ff4c44>" + Cridamage.ToString() + "! </color>";
                nowHp -= Cridamage;
            }
            else
            {
                text.text = damage.ToString();
                nowHp -= damage; 
            }
            player.attacked = false;
            
            var DamageEffect = Instantiate(this.Damage_prefep, DamagePoint).GetComponent<RectTransform>();
            DamageEffect.transform.position = this.DamagePoint.position;
        }
        if(col.CompareTag("skill"))
        {
            CompareSkill = true;
            // Debug.Log("스킬에 맞았음!");
            var HitEffect = Instantiate<GameObject>(this.HitEffect_prefep);//히트모션 생성 
            HitEffect.transform.position = this.HitEffectPoint.position;

            damagesource.Play();
            player.attacked = false;
            if (CritPercen)
            {
                text.text = "<color=#43c6cf>" + SkillCritdamage.ToString() + "!!! </color>";
                nowHp -= SkillCritdamage;
            }
            else
            {
                text.text = "<color=#3c7afc>" + Skilldamage.ToString() + "</color>";
                nowHp -= Skilldamage;
            }
            var DamageEffect = Instantiate(this.Damage_prefep, DamagePoint).GetComponent<RectTransform>();
            DamageEffect.transform.position = this.DamagePoint.position;
        }
        if (nowHp <= 0) // 적 사망
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), true);
            Invoke("DontIgnoreEnemy",0.3f);
            moveController.isKill = true;
            Destroy(hpbar.gameObject,0.3f);//체력바를 제거 
            enemyAnimator.SetInteger(State, (int)States.die);//사망모션으로 진입 
            Destroy(gameObject,0.3f);//몬스터 제거 
        }
    }
    
    // private void OnTriggerStay(Collider col)
    // {
    //     if (col.CompareTag("Player"))
    //     {
    //         if (CritPercen)
    //         {
    //             text.text = "<color=#ff4c44>" + Cridamage.ToString() + "! </color>";
    //         }
    //         else
    //         {
    //             text.text = damage.ToString();
    //         }
    //     }
    //     if (col.CompareTag("skill"))
    //     {
    //         if (CritPercen)
    //         {
    //             text.text = "<color=#43c6cf>" + SkillCritdamage.ToString() + "!!! </color>";
    //         }
    //         else
    //         {
    //             text.text = "<color=#3c7afc>" + Skilldamage.ToString() + "</color>";
    //         }
    //     }
    // }
    // private void OnTriggerExit(Collider col)
    // {
    //     if (col.CompareTag("Player"))
    //     {
    //         text.text = "";
    //     }
    //     if (col.CompareTag("skill"))
    //     {
    //         text.text = "";
    //     }
    // }
    

    void DontIgnoreEnemy()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), false);
    }

}
