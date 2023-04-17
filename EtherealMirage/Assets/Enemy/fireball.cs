using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    private Transform target;
    private Enemy Necro;
    private float speed = 150f;
    private float waitTime;
    Rigidbody2D rigid2D;
    
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("User").GetComponent<Transform>();
        Necro = GameObject.FindWithTag("Necro").GetComponent<Enemy>();
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        // rigid2D.velocity = new Vector2(target.position.x * speed, target.position.x);
    }

    void Update()
    {
        waitTime += Time.deltaTime;
        //1초 동안 타겟 방향으로 전진
        if (waitTime < 1f)
        {
            speed = Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed*10);
        }
        else if (waitTime > 4.99f)
        {
            waitTime = 0;
        }
        else
        {
            rigid2D.velocity = new Vector2(target.transform.position.x * speed*30, target.transform.position.y * speed*30);
        }
        
        
        Destroy(this.gameObject, 5);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("User"))
        {
            target.GetComponent<MoveController>().nowHp -= Necro.atkDmg;
            target.GetComponent<MoveController>().isDamage = true;
            Destroy(this.gameObject);
        }
    }
}
