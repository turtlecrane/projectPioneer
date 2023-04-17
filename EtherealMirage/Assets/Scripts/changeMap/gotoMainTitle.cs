using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoMainTitle : MonoBehaviour
{
    public void SceneChange()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainTitle");
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
