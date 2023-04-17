using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    Rigidbody2D rigid2D;
    MoveController moveController; //플레이어 바라보는 방향 알기위함
    public float movementSpeed = 70; //검기가 날아가는 속도
    SpriteRenderer spriteRenderer; 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveController = GameObject.FindWithTag("User").GetComponent<MoveController>();
        rigid2D = GetComponent<Rigidbody2D>();
    }
    
    
    void Update()
    {
        if (moveController.direction > 0)//플레이어의 바라보는 방향을 가져온다.
        {
            spriteRenderer.flipX = false;
            rigid2D.velocity = new Vector2(15 * movementSpeed, rigid2D.velocity.y); //검기가 날아감
        }
        else if (moveController.direction < 0)
        {
            spriteRenderer.flipX = true;//플레이어가 바라보는 방향으로 뒤집어준다.
            rigid2D.velocity = new Vector2(-15 * movementSpeed, rigid2D.velocity.y);
        }

        Destroy(this.gameObject, 0.6f);
    }
}
