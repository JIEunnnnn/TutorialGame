using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManeger : MonoBehaviour
{
    // 버튼셋팅1개, 화면문제 셋팅1개 랑 풀이과정 ㅇㅇ 

    // Start is called before the first frame update
    private Player scriptPlayer;
    private Text lifeText, ProblemText;
    private Text stopWatch;
    private Image ProblemIMG;
    public Button  Btn1, Btn2, Btn3 ;
    private Text BtnTxt1, BtnTxt2, BtnTxt3; 

    private GameObject ProblemSpot ;
   


    private bool ProblemTag;
    public bool RightFlag; // 정답확인용 플래그
    public bool ButtonClick; // 버튼클릭확인용 플래그  => 클릭하면 true로 변경

    public int answer;
    public char answerEnglish; 

    public int CurrentStage;
    public int MaxStage;
    public int stage;

    private void Awake()
    {
        lifeText = GameObject.Find("Life").GetComponent<Text>();
        scriptPlayer = GameObject.Find("Player").GetComponent<Player>();

        stopWatch = GameObject.Find("Time").GetComponent<Text>();
        ProblemIMG = GameObject.Find("ProblemIMG").GetComponent<Image>();


        Btn1 = GameObject.Find("Btn1").GetComponent<Button>();
        Btn2 = GameObject.Find("Btn2").GetComponent<Button>();
        Btn3 = GameObject.Find("Btn3").GetComponent<Button>();

        BtnTxt1 = GameObject.Find("BtnTxt1").GetComponent<Text>();
        BtnTxt2 = GameObject.Find("BtnTxt2").GetComponent<Text>();
        BtnTxt3 = GameObject.Find("BtnTxt3").GetComponent<Text>();

        ProblemText = GameObject.Find("ProblemText").GetComponent<Text>();
    }

    void Start()
    {

        // 문제 이미지, 버튼, 텍스트 
        SetLifeText();


        ProblemText.gameObject.SetActive(false); 
        ProblemIMG.gameObject.SetActive(false);
        //ProblemIMG.GetComponent<Image>().enabled = false;
        // 숨기기

        Btn1.gameObject.SetActive(false);
        Btn2.gameObject.SetActive(false);
        Btn3.gameObject.SetActive(false);
        BtnTxt1.gameObject.SetActive(false);
        BtnTxt2.gameObject.SetActive(false);
        BtnTxt3.gameObject.SetActive(false);

        ProblemTag = true;
        ButtonClick = false;

        stage = 0;
        CurrentStage = 1;
        MaxStage = 2; 


        // ProblemSpot = GameObject.Find("QuestionSpot").GetComponent<Gam>();



    }

    public void SetLifeText()
    {
        lifeText.text = "Life : " + scriptPlayer.GetLife().ToString();
    }
    public Text GetLifeText() { return lifeText; }

    public void SetTimeText()
    {
        stopWatch.text = "Time : " + scriptPlayer.GetTime().ToString("F2");
    }
    public void SetTimeText(string s)
    {
        stopWatch.text = s;
    }


    void Update()
    {

        if (scriptPlayer.GetIsQuestion() == true && ProblemTag == true )    
        {
                ProblemTag = false;

               ProblemText.gameObject.SetActive(true);
                ProblemIMG.gameObject.SetActive(true);
          
                Btn1.gameObject.SetActive(true);
                Btn2.gameObject.SetActive(true);
                Btn3.gameObject.SetActive(true);
                BtnTxt1.gameObject.SetActive(true);
                BtnTxt2.gameObject.SetActive(true);
                BtnTxt3.gameObject.SetActive(true);

            if(scriptPlayer.ChangeStage() == 0)
            {
             PlayProblem(ProblemTag);
            }
            else if(scriptPlayer.ChangeStage() == 1)
            {
             PlayEnglish(ProblemTag);
            }
            else if(scriptPlayer.ChangeStage() == 2)
            {

            }


        /*    if (GameObject.FindWithTag("Finish"))

            {
                if(CurrentStage < MaxStage)
                {
                    stage += 1;
                }
               
            }

    */
          //  var = GameObject.FindWithTag("Finish").GetComponent<UIManeger>().PlayEnglish();




        } else if (scriptPlayer.GetIsQuestion() != true && ProblemTag == false)
        {

            ProblemText.gameObject.SetActive(false);
            ProblemIMG.gameObject.SetActive(false);
           

            Btn1.gameObject.SetActive(false);
            Btn2.gameObject.SetActive(false);
            Btn3.gameObject.SetActive(false);
            BtnTxt1.gameObject.SetActive(false);
            BtnTxt2.gameObject.SetActive(false);
            BtnTxt3.gameObject.SetActive(false);

            ProblemTag = true ;
            ButtonClick = false;
            
        }
       
    

    }

  

    public void PlayProblem(bool p)
    {

        if (ProblemTag == false && scriptPlayer.GetIsQuestion() == true ) // problem = false 일때 
        {

            int a1 = Random.Range(0, 10);
            int a2 = Random.Range(0, 10);

            int err1 = a1 * a2; 
            int err2 = a1 - a2; 

            int num = Random.Range(1, 4); // 배치순서 ㅇㅇ 
            Debug.Log(num);

            ProblemText.text = a1 + " + " + a2 + " = ?";
            answer = a1 + a2;

            if(err1 == answer)
            {
                err1 += Random.Range(2, 10);
            }
            else if(err2 == answer)
            {
                err2 += Random.Range(2, 10); 
            }else if(err1 == err2)
            {
                err2 += Random.Range(2, 10);
            }


            if (num == 1) // 1번이 정답
            {
                BtnTxt1.text = answer.ToString();
                BtnTxt2.text = err1.ToString();
                BtnTxt3.text = err2.ToString();

                Debug.Log("답이1번"+answer);

                ProblemTag = false;
                //CheckFirst();
                //  CheckFirst(answer); 
                //CheckProblem(0, answer, err1, err2); // 답이 0번 


            }
            else if(num == 2) // 2번이 정답
            {
                BtnTxt1.text = err2.ToString();
                BtnTxt2.text = answer.ToString();
                BtnTxt3.text = err1.ToString();
                Debug.Log("답이2번"+answer);

                ProblemTag = false;
                //CheckSecond();
                //PlayProblemYes(1, answer, err1, err2);
                // CheckSecond(answer); 
                // CheckProblem(1, err2, answer, err1); // 답이 1번 

            }
            else if(num == 3)// 3번이 정답
            {
                BtnTxt1.text = err1.ToString();
                BtnTxt2.text = err2.ToString();
                BtnTxt3.text = answer.ToString();
                Debug.Log("답이3번"+answer);

                ProblemTag = false;
                //CheckThird();
                // CheckThird(answer);
                //PlayProblemYes(2, answer, err1, err2);
                // CheckProblem(2, err1, err2, answer);
            }

            // PlayProblemYes(answer);  
            // txt 말고 변수값으로 받아서!! 


        }

    }

    public void PlayEnglish(bool p) // 영어배열
    {
        if (ProblemTag == false && scriptPlayer.GetIsQuestion() == true) // problem = false 일때 
        {
            char[] eng = new char[26] { 'A','B','C','D','E','F','G','H','I','J','K',
                                'L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };


            int a1 = Random.Range(0, 24); // 랜덤하게 문자 순서 나타내기 

            int a2 = Random.Range(0, 24); // 오답 나타낼때 순서 ㅇㅇ 

            char err1 = eng[a2];
            char err2 = eng[Random.Range(0, 24)];

            int num = Random.Range(1, 4); // 배치순서 ㅇㅇ 
            Debug.Log(num);

            ProblemText.text = eng[a1 - 1] + " [  ] " + eng[a1 + 1];
            answerEnglish = eng[a1];

            if (err1 == answerEnglish)
            {
                err1 = eng[Random.Range(0, 24)];
            }
            else if (err2 == answerEnglish)
            {
                err2 = eng[Random.Range(0, 24)];
            }
            else if (err1 == err2)
            {
                err2 = eng[Random.Range(0, 24)];
            }


            if (num == 1) // 1번이 정답
            {
                BtnTxt1.text = answerEnglish.ToString();
                BtnTxt2.text = err1.ToString();
                BtnTxt3.text = err2.ToString();

                Debug.Log("답이1번" + answerEnglish);

                ProblemTag = false;
                //CheckFirst();
                //  CheckFirst(answer); 
                //CheckProblem(0, answer, err1, err2); // 답이 0번 


            }
            else if (num == 2) // 2번이 정답
            {
                BtnTxt1.text = err2.ToString();
                BtnTxt2.text = answerEnglish.ToString();
                BtnTxt3.text = err1.ToString();
                Debug.Log("답이2번" + answerEnglish);

                ProblemTag = false;
                //CheckSecond();
                //PlayProblemYes(1, answer, err1, err2);
                // CheckSecond(answer); 
                // CheckProblem(1, err2, answer, err1); // 답이 1번 

            }
            else if (num == 3)// 3번이 정답
            {
                BtnTxt1.text = err1.ToString();
                BtnTxt2.text = err2.ToString();
                BtnTxt3.text = answerEnglish.ToString();
                Debug.Log("답이3번" + answerEnglish);

                ProblemTag = false;
                //CheckThird();
                // CheckThird(answer);
                //PlayProblemYes(2, answer, err1, err2);
                // CheckProblem(2, err1, err2, answer);
            }

            // PlayProblemYes(answer);  
            // txt 말고 변수값으로 받아서!! 


        }
    }


    public void PlayKorea(bool p)
    {
        if (ProblemTag == false && scriptPlayer.GetIsQuestion() == true) // problem = false 일때 
        {
            char[] con = new char[14]
            {
                'ㄱ','ㄴ','ㄷ','ㄹ','ㅁ','ㅂ','ㅅ','ㅇ','ㅈ','ㅊ',
                'ㅋ','ㅌ','ㅍ','ㅎ'
            };
            char[] vow = new char[14]
            {
                'ㅏ', 'ㅓ', 'ㅗ','ㅜ','ㅑ','ㅕ','ㅛ','ㅠ','ㅐ','ㅔ',
                'ㅒ','ㅖ','ㅡ','ㅣ'
            };



        }


    }

    public void CheckFirst()
    {
        if(scriptPlayer.ChangeStage() == 0)
        {
            int answ = answer;

            if (BtnTxt1.text == answ.ToString())
            {
                Debug.Log("1번이정답입니다!!");
                RightFlag = true;
                ButtonClick = true;


            }// 선택한 버튼의 텍스트값...


            else
            {
                Debug.Log("틀렸습니다!!");
                RightFlag = false;
                ButtonClick = true;

            }
        }else if(scriptPlayer.ChangeStage() == 1)
        {
            char answ = answerEnglish;

            if (BtnTxt1.text == answ.ToString())
            {
                Debug.Log("1번이정답입니다!!");
                RightFlag = true;
                ButtonClick = true;


            }// 선택한 버튼의 텍스트값...


            else
            {
                Debug.Log("틀렸습니다!!");
                RightFlag = false;
                ButtonClick = true;

            }

        }
        
            
    }

    public void CheckSecond()
    {

        if(scriptPlayer.ChangeStage() == 0)
        {
            int answ = answer;

            if (BtnTxt2.text == answ.ToString())
            {
                Debug.Log("2번이정답입니다");
                RightFlag = true;
                ButtonClick = true;

            }


            else
            {
                Debug.Log("틀렸습니다!!");
                RightFlag = false;
                ButtonClick = true;

            }
        }else if(scriptPlayer.ChangeStage() == 1)
        {
            char answ = answerEnglish;

            if (BtnTxt2.text == answ.ToString())
            {
                Debug.Log("2번이정답입니다");
                RightFlag = true;
                ButtonClick = true;

            }


            else
            {
                Debug.Log("틀렸습니다!!");
                RightFlag = false;
                ButtonClick = true;

            }
        }
      
    }

    public void CheckThird()
    {

        if(scriptPlayer.ChangeStage() == 0)
        {
            int answ = answer;


            if (BtnTxt3.text == answ.ToString())
            {
                Debug.Log("3번이정답입니다");
                RightFlag = true;
                ButtonClick = true;
            }

            else
            {

                Debug.Log("틀렸습니다!!");
                RightFlag = false;
                ButtonClick = true;
            }
        }else if(scriptPlayer.ChangeStage() == 1)
        {
            char answ = answerEnglish;

            if (BtnTxt3.text == answ.ToString())
            {
                Debug.Log("3번이정답입니다");
                RightFlag = true;
                ButtonClick = true;
            }

            else
            {

                Debug.Log("틀렸습니다!!");
                RightFlag = false;
                ButtonClick = true;
            }

        }
       



    }

   


}


