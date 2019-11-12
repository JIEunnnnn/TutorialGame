using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FontBlinkOver : MonoBehaviour
{
    // Start is called before the first frame update
    Text flashingText;

    void Start()
    {
        flashingText = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText()
    {
        Debug.Log("코루틴 시작");
        while (true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(.5f);
            flashingText.text = "다시 시작하려면 화면을 클릭해 주세요.";
            yield return new WaitForSeconds(.5f);
        }
    }
}
