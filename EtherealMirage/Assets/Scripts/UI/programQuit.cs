using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class programQuit : MonoBehaviour
{
    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("------- [ 게임 종료됨 ] -------");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
