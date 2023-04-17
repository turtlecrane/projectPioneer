using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour 
{
    public string transferMapName;
    private MoveController thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (thePlayer == null)
            thePlayer = FindObjectOfType<MoveController>();
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("User"))
        {
            //만약 화살표 윗키를 누른다면{
            float ver = Input.GetAxis("Vertical");
            //Debug.Log(ver);
            if (ver > 0)
            {
                thePlayer.isPotal = true;
                thePlayer.currentMapName = transferMapName;
                SceneManager.LoadScene(transferMapName);
            }

        }
    }
}