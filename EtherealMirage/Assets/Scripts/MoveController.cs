using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveController : MonoBehaviour
{
    public bool isSkill = false;
    public bool isSaveShow = false;
    public float skillCooltime;
    private float skillDelay;
    public GameObject coolTimeAction;
    
    public bool isPotal;
    public bool isLadder;
    public bool isPlatform;

    private float ver; //상하 이동을 관리합니다.
    public string currentMapName;

    public int GetEXP = 500;
    public GameObject EXPImage;
    public TextMeshProUGUI GetEXP_TEXT;


    public DialogueManager DialogueManager;
    GameObject scanObject;
    MenuController MenuController;

    public GameObject LevelUpAction;

    public bool isDamage = false;

    private int Level;
    public bool isKill = false;

    public Image nowEXPbar;
    public int maxEXP;
    public int nowEXP;

    public Image nowHpbar;
    public int maxHp;
    public int nowHp;
    private bool isDie = false;
    public bool isAlive = true;

    private Transform DashSpawnPoint;//대쉬스폰장소 
    public GameObject R_DashPrefab;
    public GameObject L_DashPrefab;
    
    public float movementSpeed; //캐릭터가 움직이는 기본 속력 설정 3.3

    public float speed; //3.3
    private bool isRun = false;

    public float jumpPower; //60
    private bool isJump = false;

    Vector2 movement = new Vector2(); //2차원 벡터 위치 표현하기
    public float direction = 0.0f;
    Vector3 dirVec = new Vector3();
    Rigidbody2D rigid2D; //리지드바디2D의 참조를 저장하는 변수
    SpriteRenderer spriteRenderer; //스프라이트 뒤집기 위해 쓰이는 렌더러

    Animator animator;//애니메이션 저장 변수
    string walkState = "isWalk";

    public AudioSource attacksource;
    public AudioSource Dashsource;

    //public TextMeshProUGUI HpParameters;
    public Text HpParameters;
    public Text LevelParameters;

    Enemy enemy;
    player player;
    

    //애니메이션 플레그 숫자
    enum States
    {
        walk = 1,
        run = 2,
        jump = 3,
        atk = 4,
        idle = 5,
        die = 6,
        damage = 7,
        LadderIdle = 8,
        LadderMove = 9,
        skill = 10
    }

    public static MoveController instance;

    void Start()
    {
        skillCooltime = 5f; //스킬 쿨타임
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            if (rigid2D == null)
                rigid2D = GetComponent<Rigidbody2D>();
            if (animator == null)
                animator = GetComponent<Animator>();
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Level = 0;
        GetEXP = 500;
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindWithTag("Player").GetComponent<player>();
        MenuController = GameObject.Find("menu").GetComponent<MenuController>();

        Time.timeScale = 1;
        movementSpeed = speed;
        rigid2D = GetComponent<Rigidbody2D>();//시작하면 해당 오브젝트의 리지드바디 컴포넌트를 가져온다
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        DashSpawnPoint = this.transform.Find("DashSpawnPoint");//플레이어 오브젝트 하위에있는 대쉬생성포인트 위치 설정
        maxHp = 1000;
        nowHp = 1000;
        maxEXP = 1000;
        nowEXP = 0;

        DontDestroyOnLoad();
    }

    public void FixedUpdate()//유니티가 일정한 간격으로 호출하는 메소드
    {                        //지속적으로 키를 입력함
        //Debug.Log("isDamage = " + isDamage);

        // isDamage = false;

        //좌 우 이동
        movement.x = Input.GetAxisRaw("Horizontal");
        
        rigid2D.velocity = new Vector2(movement.x * movementSpeed, rigid2D.velocity.y);

        if (Input.GetKey(KeyCode.LeftShift))//달리기 상태
        {
            isRun = true;
            //Debug.Log("isRun = true");
            movementSpeed = 8;
        }
        else
        {
            isRun = false;
            movementSpeed = speed;
        }

        //착지 관련
        if (rigid2D.velocity.y < -0.1)//내려가고(추락중) 있을때만 검사
        {
            //충돌감지 선 그리기  ( 시작,방향*길이,색깔 )
            Debug.DrawRay(rigid2D.position, Vector3.down * 2, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, Vector3.down, 2, LayerMask.GetMask("Platform"));
            //빔에 맞았는지 검사
            if (rayHit.collider!=null)
            {
                isJump = false;//바닥과 충돌되면 점프상태가 아니다.
                // Debug.Log(rayHit.collider.name + "과 충돌 됨 ! ");
            }
        }

        if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Q) )//공격하는중에는 확 멈춰진다.
        {
            movementSpeed = 3.3f;
            isRun = false;
        }

        //상호작용 관련
        Debug.DrawRay(rigid2D.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(rigid2D.position, dirVec, 0.7f, LayerMask.GetMask("Interaction"));

        if(rayhit.collider != null)
        {
            scanObject = rayhit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
         
        //사다리 관련
        if(isLadder)
        {
            ver = Input.GetAxis("Vertical");
            rigid2D.gravityScale = 0;
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, ver * 8f);
        }
        else
        {
            rigid2D.gravityScale = 4f;
            //점프
            Jump();
        }
        if (nowHp <= 0) //플레이어 사망
        {
            isDie = true;
        }


    }

    void Update()
    {
        Jump();
        skillDelay -= Time.deltaTime;
        // Debug.Log("skillDelay : " + skillDelay);
        if (skillDelay < 0) skillDelay = 0;

        if (skillDelay == 0 && Input.GetKeyDown(KeyCode.Q))
        {
            isSkill = true;
            skillDelay = skillCooltime;
        }
        else if (skillDelay != 0 && Input.GetKeyDown(KeyCode.Q))
        {
            isSkill = false;
            coolTimeAction.gameObject.SetActive(true);
            Invoke("coolTimeActionSetActive", 1.6f);
        }
        
        
        //상호작용 하기 
        if(Input.GetKeyDown(KeyCode.F) && scanObject != null)
        {
            if (scanObject.name == "SavePoint")
            {
                isSaveShow = true; //세이브포인트에서 F를 누르면 팝업생성
            }
            else
            {
                DialogueManager.Action(scanObject);
            }
        }

        if (DialogueManager.isAction == true && MenuController.isShow == false)
        {
            Time.timeScale = 0;
        }
        else if (DialogueManager.isAction == false && MenuController.isShow == true)
        {
            return;
        }
        else
        {
            Time.timeScale = 1;
        }



        //바라보고 있는 방향
        if (spriteRenderer.flipX)
        {
            dirVec = Vector3.left;
        }
        else if (!spriteRenderer.flipX)
        {
            dirVec = Vector3.right;
        }

        direction = dirVec.x;

        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
        HpParameters.text = string.Format("HP : {0}/{1}", (float)nowHp, (float)maxHp);
        LevelParameters.text = string.Format("Level : {0}", (int)Level);

        nowEXPbar.fillAmount = (float)nowEXP/ (float)maxEXP;

        //몬스터 죽였을때 경험치 획득
        if (isKill)
        {
            nowEXP += GetEXP;//경험치 획득량
            EXPImage.gameObject.SetActive(true);
            GetEXP_TEXT.text = "+ " + GetEXP;
            isKill = false;
            Invoke("EXPImage_SetActive", 1.5f);
            if (!GameObject.FindWithTag("Enemy"))
            {
                enemy.nowHp = enemy.maxHp;
            }
        }

        //현재 경험치와 맥스 경험치가 같아지면 레벨업 
        if (nowEXP >= maxEXP)
        {
            LevelUpAction.gameObject.SetActive(true);
            Invoke("LevelUpSetActive", 1.5f);
            Level += 1;//레벨 +1
            nowEXP = 0;//현재 경험치 초기화 
            maxEXP += 200; //최대경험치 200 증가
            player.atkDmg += 5; //공격력 5 증가
        }


        if (Input.GetButtonUp("Horizontal"))//키보드에서 손을 뗏을때 완전히 멈추게 	
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.normalized.x * 0.05f, rigid2D.velocity.y);
            //normalized = 벡터의 크기를 1로 만든 상태
        }

        //방향 전환
        if (Input.GetButton("Horizontal"))//수평으로 움직이는 값 받아오기
        {
            //기본값은 false
            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);
            //수평으로 값을 받아온 값이 -1(왼) 이면 뒤집어짐
        }
 
        //걷기 애니메이션
        if (movement.x > 0)//오른쪽으로 이동할/
        {
            animator.SetInteger(walkState, (int)States.walk);
        }
        else if (movement.x < 0)//왼쪽으로 이동할때
        {
            animator.SetInteger(walkState, (int)States.walk);
        }
        else if (movement.x == 0)//대기상태
        {
            animator.SetInteger(walkState, (int)States.idle);
        }

        //달리기 애니메이션 
        if ((movement.x > 0) && (isRun))//오른쪽으로 이동하고 달리기 상태이면
        {
            animator.SetInteger(walkState, (int)States.run);
        }
        else if ((movement.x < 0) && (isRun))//왼쪽으로 이동하고 달리기 상태이면
        {
            animator.SetInteger(walkState, (int)States.run);
        }

        //점프 애니메이션
        if((movement.x) == 0 && (isJump))//1.대기상태에 점프
        {
            animator.SetInteger(walkState, (int)States.jump);
        }
        if (movement.x > 0 && (isJump)) //2.걷는중에 점프
        {
            animator.SetInteger(walkState, (int)States.jump);
        }
        else if (movement.x < 0 && (isJump))
        {
            animator.SetInteger(walkState, (int)States.jump);
        }
        if ((isRun) && (isJump))//3.달리는중에 점프
        {
            animator.SetInteger(walkState, (int)States.jump);
        }
 
        //대쉬생성
        if(Input.GetKeyDown(KeyCode.LeftShift)&&(movement.x > 0)&&(!isJump)) //쉬프트를 눌렀으며, 오른쪽으로 걷는중이며, 점프중이 아닐때
        {
            Dashsource.Play();
            var DashEffect = Instantiate<GameObject>(this.R_DashPrefab);
            DashEffect.transform.position = this.DashSpawnPoint.position;
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift)&&(movement.x < 0)&&(!isJump)) //쉬프트를 눌렀으며, 왼쪽으로 걷는중이며, 점프중이 아닐때
        {
            Dashsource.Play();
            var DashEffect = Instantiate<GameObject>(this.L_DashPrefab);
            DashEffect.transform.position = this.DashSpawnPoint.position;
        }

        //공격모션
        //대기중에 공격 걷는중에 공격 달리는중에 공격 점프중에 공격은 안됨.
        if((movement.x) == 0 && Input.GetKeyDown(KeyCode.Z))
        {
            attacksource.Play();
            animator.SetInteger(walkState, (int)States.atk);
        }
        if (movement.x > 0 && Input.GetKeyDown(KeyCode.Z)) //2.걷는중에 공격
        {
            attacksource.Play();
            animator.SetInteger(walkState, (int)States.atk);
        }
        else if (movement.x < 0 && Input.GetKeyDown(KeyCode.Z))
        {
            attacksource.Play();
            animator.SetInteger(walkState, (int)States.atk);
        }
        if ((isRun) && Input.GetKeyDown(KeyCode.Z))//3.달리는중에 공격
        {
            attacksource.Play();
            animator.SetInteger(walkState, (int)States.atk);
        }
        
        //스킬모션
        // Debug.Log("isSkill : " + isSkill);
        if((movement.x) == 0 && isSkill)
        {
            attacksource.Play();
            animator.SetInteger(walkState, (int)States.skill);
        }
        if (movement.x > 0 && isSkill) //2.걷는중에 스킬
        {
            attacksource.Play();
            animator.SetInteger(walkState, (int)States.skill);
        }
        else if (movement.x < 0 && isSkill)
        {
            attacksource.Play();
            animator.SetInteger(walkState, (int)States.skill);
        }
        if ((isRun) && isSkill)//3.달리는중에 스킬
        {
            attacksource.Play();
            animator.SetInteger(walkState, (int)States.skill);
        }

        //데미지 모션
        if (isDamage)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.normalized.x * 0.05f, rigid2D.velocity.y);
            animator.SetInteger(walkState, (int)States.damage);
            isDamage = false;
        }

        //사다리 모션 
        if (isLadder)
        {
            Debug.DrawRay(rigid2D.position, Vector3.down * 1.3f, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, Vector3.down, 1.3f, LayerMask.GetMask("Platform"));
            if (rayHit.collider == null)
            {
                //Debug.Log("사다리에 있는데 바닥과 닿아있지 않음 ");
                animator.SetInteger(walkState, (int)States.LadderIdle);
                
            }
            if (ver != 0)
            {
                animator.SetInteger(walkState, (int)States.LadderMove);
            }
        }

        //사망모션
        if (isDie)
        {
            isAlive = false;
            //Invoke("pueseTime", 1.4f);
            animator.SetInteger(walkState, (int)States.die);
            gameObject.SetActive(false);
            //Destroy(gameObject, 1);//플레이어 제거
        }
    }


    // ------------------
    void coolTimeActionSetActive()
    {
        coolTimeAction.gameObject.SetActive(false);
    }

    void LevelUpSetActive()
    {
        LevelUpAction.gameObject.SetActive(false);
    }

    void EXPImage_SetActive()
    {
        EXPImage.gameObject.SetActive(false);
    }

    //사다리 관련
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
    
    //움직이는 플렛폼 관련
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("MovingPlatform"))//움직이는 발판에 플레이어가 접촉하면
        {
            Invoke("DontDestroyOnLoad",0.3f);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump)//무한점프 막기 - 스페이스를 누르는데 점프상태가 아닐때만 실행됨
        {
            isJump = true;
            rigid2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    public void DontDestroyOnLoad()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}