using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameClear : MonoBehaviour
{
 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(DelaySceneChange(.5f, "Scenes/Start"));
            StopCoroutine("DelaySceneChange");
        }
    }
    public IEnumerator DelaySceneChange(float delayTime, string path)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(path);
    }
}
