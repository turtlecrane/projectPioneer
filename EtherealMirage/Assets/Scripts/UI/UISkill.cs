using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISkill : MonoBehaviour
{
    public Image Image;
    public TMP_Text textCoolTime;
    public Image imgFill;

    MoveController moveController;
    
    public void Init() //초기화 함수
    {
        this.textCoolTime.gameObject.SetActive(false);
        this.imgFill.fillAmount = 0;
    }

    void Start()
    {
        moveController = GameObject.FindWithTag("User").GetComponent<MoveController>();
        this.Image = this.GetComponent<Image>();
        textCoolTime.text = "";
    }

    private void Update()
    {
        if (moveController.isSkill)
        {
            StartCoroutine(CoolTime (moveController.skillCooltime));
            moveController.isSkill = false;
        }
    }
    
    IEnumerator CoolTime (float cool)
    {
        imgFill.fillAmount = 1;
 
        while (cool > 0.0f)
        {
            textCoolTime.gameObject.SetActive(true);
            cool -= Time.deltaTime;
            imgFill.fillAmount = (cool/moveController.skillCooltime);
            textCoolTime.text =  cool.ToString("0.0");
            yield return new WaitForFixedUpdate();
        }
        
        if (cool < 0.1f)
        {
            textCoolTime.text = "";
        }
    }
}