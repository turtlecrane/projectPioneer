using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //어떤 대사가 들어가는지 저장을 하는 스크립트 입니다 

    Dictionary<int, string[]> talkData; //아이디 int 와 대사 string 배[

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        Generatedata();
    }


    void Generatedata()
    {
        talkData.Add(1000, new string[] { "안녕하세요! \n 앞으로 가면 마을이 있습니다.", "이 앞은 위험하니 각별히 조심하세요!" });
        talkData.Add(2000, new string[] { "저는 그래픽이 다릅니다. \n 곧 바꿔질 운명이죠. ", "주인이 이렇게 말을 많이 시키는걸 보면, 분명 다른 속샘이 있을것 같지만.", "전 그렇게 속좁은 사람이 아니기 때문에 그냥 넘어가려고 합니다", "언젠가는 쓰일 날이 오겠죠 뭐. 하하" });
        talkData.Add(3000, new string[] {"마을에 도착했구나? \n 천천히 둘러봐. 여기까지 오느라 수고했어. \n 왼쪽으로 가면 던전으로 향하는 포탈이 있어." , "강력한 마물들이 살고있다고해. \n 어디에서 왔는지는 모르지만 ..." , "조심하는게 좋을거야. \n 행운을 빌어."});
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }

}
