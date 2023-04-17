using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    
    private Transform desPos;
    public float speed;
     void Start()
     {
         transform.position = startPos.position;
         desPos = endPos;
     }

     private void OnCollisionEnter2D(Collision2D col)
     {
         if (col.transform.CompareTag("User"))//움직이는 발판에 플레이어가 접촉하면
         {
             col.transform.SetParent(transform);//플레이어를 발판의 자식으로 설정한다.
         }
     }

     private void OnCollisionExit2D(Collision2D col)
     {
         if (col.transform.CompareTag("User"))//움직이는 발판에 플레이어가 접촉하면
         {
             col.transform.SetParent(null);
         }
     }

     void FixedUpdate()
    {
       transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime*speed);
       if (Vector2.Distance(transform.position, desPos.position) <= 0.05f)
       {
           if (desPos == endPos)
           {
               desPos = startPos;
           }
           else
           {
               desPos = endPos;
           }
       }
    }
}
