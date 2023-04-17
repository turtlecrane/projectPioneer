using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroAI : MonoBehaviour
{
    public bool isAtk = false;
    public GameObject fireballPref;
    private Transform fireballPoint;
    AudioSource EnemyAttacksource;
    SpriteRenderer spriteRenderer;
    enum States
    {
        idle = 1,
        move = 2,
        die = 3,
        atk = 4
    }

    Transform target;
    float attackDelay;

    Enemy enemy;
    Animator enemyAnimator;
    void Start()
    {
        fireballPoint = this.transform.Find("FireballPoint");
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("User").GetComponent<Transform>();
        EnemyAttacksource = GameObject.Find("enemyAttackAudio").GetComponent<AudioSource>();
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        // 타겟과 자신의 거리를 확인
        float distance = Vector3.Distance(transform.position, target.position);

        // 공격 딜레이(쿨타임)가 0일 때, 시야 범위안에 들어올 때
        if (attackDelay == 0 && distance <= enemy.fieldOfVision)
        {
            FaceTarget();

            // 공격 범위안에 들어올 때 공격
            if (distance <= enemy.atkRange)
            {
                AttackTarget();
            }
            else// 공격 애니메이션 실행 중이 아닐 때 추적
            {
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    MoveToTarget();
                }
            }
        }
        else// 시야 범위 밖에 있을 때 Idle 애니메이션으로 전환
        {
            //enemyAnimator.SetBool("moving", false);
            enemyAnimator.SetInteger("Status", (int)States.idle);
        }
    }

    void MoveToTarget()//몬스터 움직이/
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * enemy.moveSpeed * Time.deltaTime);
        //enemyAnimator.SetBool("moving", true);
        enemyAnimator.SetInteger("Status", (int)States.move);
    }

    void FaceTarget()//몬스터 바라보/
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            spriteRenderer.flipX =true;
            //transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            spriteRenderer.flipX = false;
            //transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void AttackTarget()//몬스터 공격 
    {
        
        isAtk = true;
        EnemyAttacksource.Play();
        
        enemyAnimator.SetInteger("Status", (int)States.atk);
        
        if (isAtk)
        {
            var FireballInstan = Instantiate<GameObject>(this.fireballPref);//생성 
            FireballInstan.transform.position = this.fireballPoint.position;
            isAtk = false;
        }
        
        attackDelay = enemy.atkSpeed; // 딜레이 충전
    }
}

