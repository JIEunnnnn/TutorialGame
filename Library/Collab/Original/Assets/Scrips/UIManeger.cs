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
    private Button  Btn1, Btn2, Btn3 ;
    private Text BtnTxt1, BtnTxt2, BtnTxt3; 

    private GameObject ProblemSpot ;

    private bool Problem;
    
    void Start()
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
        

        Problem = true; 

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

        if (scriptPlayer.GetIsQuestion() == true )    
        { 
            

                ProblemText.gameObject.SetActive(true);
                ProblemIMG.gameObject.SetActive(true);
                //ProblemIMG.GetComponent<Image>().enabled = false;
                // 나타내기 

                Btn1.gameObject.SetActive(true);
                Btn2.gameObject.SetActive(true);
                Btn3.gameObject.SetActive(true);
                BtnTxt1.gameObject.SetActive(true);
                BtnTxt2.gameObject.SetActive(true);
                BtnTxt3.gameObject.SetActive(true);

                PlayProblem(Problem);
               // get이 받고 set이 설정 
               //Problem = false;
           

        }
        else {

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
            Problem = true ; 

        }
       
     
  



    }

    public void PlayProblem(bool p)
    {
        if (Problem)
        {

            int a1 = Random.Range(0, 10);
            int a2 = Random.Range(0, 10);

            int answer = a1 + a2;
            int err1 = a1 * a2; 
            int err2 = Random.Range(0, 10);

            int num = Random.Range(0, 3); // 배치순서 ㅇㅇ 

            ProblemText.text = a1 + " + " + a2 + " = ?";
            
            if(num == 0)
            {
                BtnTxt1.text = answer.ToString();
                BtnTxt2.text = err1.ToString();
                BtnTxt3.text = err2.ToString();
                PlayProblemYes(0, answer, err1, err2);


            }
            else if(num == 1)
            {
                BtnTxt1.text = err2.ToString();
                BtnTxt2.text = answer.ToString();
                BtnTxt3.text = err1.ToString();
                PlayProblemYes(1, err2, answer, err1);

            }
            else
            {
                BtnTxt1.text = err1.ToString();
                BtnTxt2.text = err2.ToString();
                BtnTxt3.text = answer.ToString();
                PlayProblemYes(2, err1, err2, answer);
            }

          // PlayProblemYes(answer);  // txt 말고 변수값으로 받아서!! 

            Problem = false; 



        }
        else
        {

        }
    }
    public void PlayProblemYes(int num, int a, int b, int c) // 맞는 순서 answer, err1, err2 
    {
     //   string name = EventSystem.current.currentSelectedGameObject.name;

    //       Debug.Log(name);


      /*  if (Input.GetMouseButton(0))

        {

            Debug.Log(EventSystem.current.currentSelectedGameObject.name);

        } */

        if (num == 0 ) // num이 0일경우 a의 답이 옳다. 나머지 입력할경우 무조건 틀림 다시시도 필요
        {
            if()
            Debug.Log("1번이 정답입니다");
            

        }
        else if (num == 1)
        {

            Debug.Log("2번이 정답입니다");
        }
        else if (num == 2 )
        {

            Debug.Log("3번이 정답입니다");
        }

    

    } //정답인지아닌지 get 함수하나 추가하기ㅇㅇ! 


}


