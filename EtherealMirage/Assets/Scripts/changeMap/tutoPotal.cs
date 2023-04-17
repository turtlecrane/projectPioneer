using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutoPotal : MonoBehaviour
{
    public GameObject EnterTutoKey;
    public string transferMapName;
    private MoveController thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (thePlayer == null)
            thePlayer = FindObjectOfType<MoveController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("User"))
        {
            //Debug.Log("포탈에 닿았다 ");
            //윗방향키 화살표 이미지 활성화
            EnterTutoKey.gameObject.SetActive(true);
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("User"))
        {
            //만약 화살표 윗키를 누른다면{
            float ver = Input.GetAxis("Vertical");
            //Debug.Log(ver);
            if(ver > 0)
            {
                thePlayer.isPotal = true;
                thePlayer.currentMapName = transferMapName;
                SceneManager.LoadScene(transferMapName);
            }
            
        }
    }
        private void OnTriggerExit2D(Collider2D collision)//사다리에서 나갈때 
    {
        if (collision.CompareTag("User"))
        {
            EnterTutoKey.gameObject.SetActive(false);
        }
    }
}