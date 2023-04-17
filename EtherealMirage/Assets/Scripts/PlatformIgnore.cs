using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    public Collider2D platformCollider;
    private void OnTriggerStay2D(Collider2D collision)//사다리에 있을때 
    {
        if (collision.CompareTag("User")) //사다리에 있는게 유저 혹은 플레이어면 
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, true); //플렛폼을 무시해
        }
        //if (collision.CompareTag("Player")) //사다리에 있는게 유저 혹은 플레이어면 
        //{
        //    Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, true); //플렛폼을 무시해
        //} 
        
    }
    private void OnTriggerExit2D(Collider2D collision)//사다리에서 나갈때 
    {
        if (collision.CompareTag("User"))//나간 태그가 유저와 플레이어면
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, false); //플렛폼 무시하지마 
        }
        //if (collision.CompareTag("Player"))//나간 태그가 유저와 플레이어면
        //{
        //    Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, false); //플렛폼 무시하지마
        //}
    }


}
