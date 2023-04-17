using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemObjEqualDestroy : MonoBehaviour
{
    static public SystemObjEqualDestroy instance; // 변수 추가
    private void Awake() 
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
