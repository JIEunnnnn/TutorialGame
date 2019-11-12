using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManeger : MonoBehaviour
{
    // 버튼셋팅1개, 화면문제 셋팅1개 랑 풀이과정 ㅇㅇ 

    // Start is called before the first frame update
    private Player scriptPlayer;
    private Text lifeText, ProblemText;
    private Text stopWatch;
    private Image ProblemIMG;
    private Button ProblemBtn;

    private GameObject ProblemSpot ;  
    
    void Start()
    {
        lifeText = GameObject.Find("Life").GetComponent<Text>();
        scriptPlayer = GameObject.Find("Player").GetComponent<Player>();
        stopWatch = GameObject.Find("Time").GetComponent<Text>();
        ProblemIMG = GameObject.Find("ProblemIMG").GetComponent<Image>();
        ProblemBtn = GameObject.Find("ProblemBtn").GetComponent<Button>();
        ProblemText = GameObject.Find("ProblemText").GetComponent<Text>();
        // 문제 이미지, 버튼, 텍스트 
        SetLifeText();


        ProblemIMG.gameObject.SetActive(false);
        //ProblemIMG.GetComponent<Image>().enabled = false;
        ProblemBtn.gameObject.SetActive(false);  // 숨기기

        
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
        //ProblemSpot = GameObject.Find("QuestionSpot").GetComponent<GameObject>();


        
    }

}
