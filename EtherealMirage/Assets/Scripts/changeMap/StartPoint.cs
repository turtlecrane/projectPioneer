using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartPoint : MonoBehaviour
{
    public string startPoint;
    private MoveController thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (thePlayer == null)
            thePlayer = GameObject.FindWithTag("User").GetComponent<MoveController>();
        if (startPoint == thePlayer.currentMapName)
        {
            thePlayer.isPotal = false;
            thePlayer.transform.position = transform.position;
        }
    }
}