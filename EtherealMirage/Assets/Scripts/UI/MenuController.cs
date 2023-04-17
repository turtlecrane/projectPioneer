using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public bool isShow = false;
    Animator animator;//애니메이션 저장 변수
    DialogueManager DialogueManager;


    void Start()
    {
        isShow = false;
        animator = GetComponent<Animator>();
        DialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    public void FixedUpdate()
    {

    }


    void Update()
    {
        //메뉴 애니메이션
        //Debug.Log("isShow = " + isShow);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isShow)
            {
                isShow = false;
                
            }
            else 
            {
                isShow = true;
                
            }
        }

        if (isShow) //메뉴보이기
        {
            //Debug.Log("메뉴 나타남 !"); 
            animator.SetBool("isShow", true);
            Invoke("pueseTime", 0.6f);
        }
        else if (!isShow) //메뉴 들어가기
        {
            //Debug.Log("메뉴 들어감 ~ ");
            animator.SetBool("isShow", false);
            pueseTime();


        }
    }

    void pueseTime()//메뉴가 뜨면 시간을 멈추고 들어가면 재
    {
        if (isShow && !DialogueManager.isAction)
        {
            Time.timeScale = 0;
        }
        else if (isShow && DialogueManager.isAction)
        {
            Time.timeScale = 0;
        }
        else if (!isShow && DialogueManager.isAction)
        {
            Time.timeScale = 0;
        }
        else if (!isShow && !DialogueManager.isAction)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 1;
        }
    }


}
