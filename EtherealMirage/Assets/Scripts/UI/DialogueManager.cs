using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TalkManager talkManager;

    public GameObject DialogueBox;
    public TextMeshProUGUI Text;
    public GameObject scanObject;
    MenuController MenuController;
    public bool isAction;
    public int talkIndex;


    void Start()
    {
        MenuController = GameObject.Find("menu").GetComponent<MenuController>();
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjectData objectData = scanObject.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNpc);

        DialogueBox.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0; //이야기가 끝났으면 초기화하 
            return; //void함수에서 return은 강제종료 역할 
        }
        if (isNpc)
        {
            Text.text = talkData;
        }
        else
        {
            Text.text = talkData;
        }

        isAction = true;
        talkIndex++; //f를 누를때마다 토크인덱스가 늘어난

    }

    void Update()
    {
        
    }
}
