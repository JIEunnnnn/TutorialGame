using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private const float PI = 3.141592f;
    private const float Gravity = -9.888f;
    private const float TIMER = 5.0f;
    private const float SPEED = 10.0f;

    private float speed;                    //이동 시 속도
    private float stopWatch;
    private float characterY;

    private bool isQuestion;                //문제풀이 플래그
    private bool isJump;                    //점프 플래그
    private bool isFail;
    private bool isTimer;

    private int isChangeStage;  // 스테이지변경 플래그
    
    private int life;                       //목숨의 수
    private int currentStage;
    private int maxStage;
    
    private UIManeger uiManeger;
    private BackGround backGround;


    private Animator playerAnimator;

    private Vector3 jumpPosition;
    private Vector3 startPosition;
    
    private GameObject MainCameraObject;
    private GameObject backGroundObjec;

    private GameObject stageObject;

    private void Awake()
    {
        startPosition = transform.position;
        characterY = transform.position.y;
 
        playerAnimator = GetComponent<Animator>();
        
        uiManeger = GameObject.Find("UIManeger").GetComponent<UIManeger>();
        backGround = GameObject.Find("BackGround").GetComponent<BackGround>();
        backGroundObjec = GameObject.Find("BackGround");
        MainCameraObject = GameObject.Find("MainCamera");
        

    }
   
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        life = 3;
        currentStage = 1;
        maxStage = 3;
        
        stopWatch = TIMER;
        
        isQuestion = false;
        isJump = false;
        isFail = false;
        isChangeStage = 0 ;
        
        
        playerAnimator.SetBool("isBehaviour", true);
        backGround.SetScrollSpeed(0.2f);
        Debug.Log("Start");
    }
   
    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        MainCameraObject.transform.position = new Vector3(position.x + 15, MainCameraObject.transform.position.y, -10);
        backGroundObjec.transform.position = new Vector3(position.x + 15, backGroundObjec.transform.position.y);
        
        if (!isQuestion)
        {
            if (!isJump)
            {
                Vector3 direction = Vector3.right;
                transform.position += Time.deltaTime * speed * direction;
            }

        }
        else {

            if (isTimer)
            {
                stopWatch -= Time.deltaTime;
                uiManeger.SetTimeText();
            }
            if ((Input.GetKey(KeyCode.Q) || (uiManeger.RightFlag && uiManeger.ButtonClick)) && !isJump) {
                //정답
                Debug.Log("정답!");
                backGround.SetScrollSpeed(0.2f);
                //포물선 방향 및 힘 계산
                Vector3 v = GetVelocity(transform.position, jumpPosition);
                v.y += 3;
                Jump(v);
                //효과음 재생.
                isFail = false;
                isTimer = false;
                isQuestion = false;
                VolumeControl("Answer", 0.3f);
                PlaySound("Answer");
            }
            else if(((Input.GetKey(KeyCode.W) ) || (stopWatch < 0) || (!uiManeger.RightFlag && uiManeger.ButtonClick)) && !isJump)
            {
                //실패
                Debug.Log("틀렸습니다.!");
                isTimer = false;
                isQuestion = false;
                isFail = true;

                uiManeger.SetTimeText("");
                backGround.SetScrollSpeed(-0.2f);
                //방향 및 힘 지정
                Jump(new Vector3(-2, 5));
                
                
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject temp = other.gameObject;
        if (temp.CompareTag("Wall"))
        {
            //장애물 충돌시 음악 재생
            //생명력 감소
            ReduceLife();

            isFail = false;
            VolumeControl("Damage", 0.3f);
            PlaySound("Damage");
        }

        if (temp.CompareTag("QuestionSpot") && !isFail) {

            //문제 풀이 지점 도착
            Debug.Log("Spot");

            playerAnimator.SetBool("isBehaviour", false);
 
            isFail = true;
            isTimer = true;
            isQuestion = true;

            backGround.SetScrollSpeed(0.0f);
            jumpPosition = new Vector3(temp.transform.position.x + 8, temp.transform.position.y);
            uiManeger.SetTimeText();

            Debug.Log(other.gameObject.transform.position);
        }

        if (temp.tag == "Floor" && isJump == true) {
            //점프 이후 바닥 바닥 충돌 시
            isJump = false;
            playerAnimator.SetBool("isBehaviour", true);

            stopWatch = TIMER;
            uiManeger.SetTimeText("");
            backGround.SetScrollSpeed(0.2f);
            GetComponent<Rigidbody>().useGravity = false;
            transform.position = new Vector3(transform.position.x, characterY);
            SetVelocity(new Vector3(0,0,0));
        }

        if (temp.tag == "Finish")
        {
            speed = 0.0f;

            if (currentStage < maxStage)
            {
                string previousFname, CurrentFname;
                Vector3 stagePosition;
                GameObject stagePrefab;
                GameObject previousStage;

                if (currentStage > 1)
                {
                    previousFname = "Stage" + currentStage + "(Clone)";
                }
                else
                {
                    previousFname = "Stage" + currentStage;
                }

                
                previousStage = GameObject.Find(previousFname);
                stagePosition = previousStage.transform.position;
                
                currentStage++;

                CurrentFname = "Stage" + currentStage;
                
                stagePrefab = Resources.Load<GameObject>("Prefebs/" + CurrentFname);
                
                stageObject = Instantiate(stagePrefab);
            
                
                
                backGround = GameObject.Find("BackGround" + currentStage).GetComponent<BackGround>();
                backGroundObjec = GameObject.Find("BackGround" + currentStage);
                backGround.SetScrollSpeed(0.0f);
                Debug.Log(backGround);
                Debug.Log(backGround);
                Destroy(previousStage);

                stageObject.transform.position = stagePosition;
                transform.position = new Vector3(stageObject.transform.position.x - 69, transform.position.y, transform.position.z); ;
                

                playerAnimator.SetBool("isBehaviour", false);
                
                StartCoroutine(DelaySpeedChange(2.0f, SPEED));      
                StopCoroutine("DelaySpeedChange");

                isChangeStage++;
                
            }
            else if (currentStage == maxStage)
            {
                
                StartCoroutine(DelaySceneChange(.5f, "Scenes/GameClear"));
                
            }


        }

    }
    public int GetLife() { return life; }
    public void SetLife(int li) { life = li; }
    public float GetTime() { return stopWatch; }
    public float GetSpeed() { return speed; }   
    public void  SetSpeed(float s) { speed = s; }
    public bool GetIsQuestion() { return isQuestion; }
    public int ChangeStage() { return isChangeStage; }

   

    public void Jump(Vector3 v) {
        GetComponent<Rigidbody>().useGravity = true;
        Vector3 velocity = v;//new Vector3(-2, 5);
        isJump = true;
        playerAnimator.SetBool("isBehaviour", false);
        SetVelocity(velocity);
    }

    // 디그리 각도를 라디안으로 변환.
    public float DegreeToRadian(float degree) { return PI / 180 * degree; }
    //3차원 포물선 공식
    public Vector3 GetVelocity(Vector3 currentPos, Vector3 destinationPos)
    {
        float h = 3.5f;

        float displacementY = destinationPos.y - currentPos.y;
        Vector3 displacementXZ = new Vector3(destinationPos.x - currentPos.x, 0, destinationPos.z - currentPos.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * Gravity * h);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / Gravity) + Mathf.Sqrt(2 * (displacementY - h) / Gravity));

        return velocityXZ + velocityY;

    }
    //Rigidbody 방향 및 힘 지정
    public void SetVelocity(Vector3 velocity) {

        GetComponent<Rigidbody>().velocity = velocity;
    }
    //음악 재생
    public void PlaySound(string name) {

        GameObject.Find(name).GetComponent<AudioSource>().Play();

    }
    //음악 볼륨 조절
    public void VolumeControl(string name, float volume) {

        GameObject.Find(name).GetComponent<AudioSource>().volume = volume;
    }
    public void ReduceLife() {

        Sprite emptyLife = Resources.Load<Sprite>("Images/Heart_empty");
        GameObject.Find("Life_" + life.ToString()).GetComponent<Image>().sprite = emptyLife; 
        
        life--;
       
        
        if (life <= 0)
        { 
            StartCoroutine(DelaySceneChange(.5f, "Scenes/GameOver"));
            //SceneManager.LoadScene();
            speed = 0;
            StopCoroutine("DelaySceneChange");
        }

    }

    public IEnumerator DelaySceneChange(float delayTime, string path)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(path);
    }
    
    public IEnumerator DelaySpeedChange(float delayTime, float s)
    {
        yield return new WaitForSeconds(delayTime);
        this.speed = s;
        backGround.SetScrollSpeed(0.2f);
        playerAnimator.SetBool("isBehaviour", true);
    }

}
