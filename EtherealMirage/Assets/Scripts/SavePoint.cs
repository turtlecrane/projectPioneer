using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    //게임 저장 팝업이 뜨면 시간이 정지된다
    
    void Start()
    {
        
    }

    void Update()
    {
        Time.timeScale = 0;
    }

    private void LateUpdate()
    {
        
    }
}
