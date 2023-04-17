using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupActivate : MonoBehaviour
{
    public GameObject GameOverPopup;
    public GameObject SaveCheckPopup;
    public bool saveshow = false;
    
    public MoveController player;
    private bool Alive;

    static public popupActivate instance; // 변수 추가
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
        Alive = true;
    }

    public void FixedUpdate()
    {
        if (!player.isAlive)//플레이어가 죽으
        {
            //Debug.Log("player.isAlive = " + player.isAlive);
            Alive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Alive)
        {
            GameOverPopup.SetActive(true);
        }

        if (player.isSaveShow)//게임저장하려고하면 팝업을 띄우기
        {
            SaveCheckPopup.SetActive(true);
            player.isSaveShow = false;
        }
    }
}
