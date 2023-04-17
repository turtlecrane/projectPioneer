using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake() {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject); // 이렇게 하면 다음 scene으로 넘어가도 오브젝트가 사라지지 않습니다.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
