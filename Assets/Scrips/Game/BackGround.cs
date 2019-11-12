using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    
    private float scrollSpeed = 0.0f;
    Material myMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }
    
    // Update is called once per frame
    void Update()
    {
        
        float newOffsetX = myMaterial.mainTextureOffset.x + scrollSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(newOffsetX, 0);
        
        myMaterial.mainTextureOffset = newOffset;
        Debug.Log(scrollSpeed);
    }

    public void SetScrollSpeed(float s) { scrollSpeed = s; }

}
