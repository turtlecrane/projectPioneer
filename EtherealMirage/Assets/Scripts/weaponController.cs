using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class weaponController : MonoBehaviour
{
    Animator animator;//애니메이션 저장 변수
    SpriteRenderer spriteRenderer;
    MoveController moveController;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveController = GameObject.FindWithTag("User").GetComponent<MoveController>();
    }   

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("atk");
        }
        
        if(moveController.isSkill)
        {
            animator.SetTrigger("skill");
        }
        
        //큐 누르면 검기를 플레이어 하위에 생성해야한다.

        if (Input.GetButton("Horizontal"))
        {
            if(Input.GetAxisRaw("Horizontal") == -1)
            {
                animator.SetBool("isLeft", true);
            }
            else if(Input.GetAxisRaw("Horizontal") == 1)
            {
                animator.SetBool("isLeft", false);
            }
        }
    }
}
