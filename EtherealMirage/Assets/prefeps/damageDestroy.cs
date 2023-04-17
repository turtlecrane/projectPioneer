using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 0.5f); //0.5초 뒤 사라짐
    }
}
