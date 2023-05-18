using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform playerTarget;
    public float cameraDampening;
    Vector3 Cameraoffset;
    float lowY;
    
    Transform reset;
    // Start is called before the first frame update
    void Start()
    {
        Cameraoffset = transform.position - playerTarget.position;
        lowY = -20;
        //lowY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 cameraTargetPosition = playerTarget.position + Cameraoffset;
        transform.position = Vector3.Lerp(transform.position,cameraTargetPosition,cameraDampening*Time.deltaTime);

        if(transform.position.y < lowY)
        {
            transform.position = new Vector3 (transform.position.x, lowY, transform.position.z);
        }
    }
}
