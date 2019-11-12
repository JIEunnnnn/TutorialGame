using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLife : MonoBehaviour
{
    private Player playerObject;
    
    private float lifeY;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player").GetComponent<Player>();
        lifeY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = transform.position.x + Time.deltaTime * playerObject.GetSpeed();
//        Vector3 lifePosition = new Vector3(playerTransform.position.x - 2, lifeY);
//        transform.position = lifePosition;
        transform.position = new Vector3(newX, lifeY);
    }
}
