using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject follow;
    public Vector2 minCamPos, maxCamPos;
    public float soothTime;

    private Vector2 velocity;

    
    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
        if (follow == null) return;


        float posX = Mathf.SmoothDamp(transform.position.x,
            follow.transform.position.x, ref velocity.x, soothTime);

        float posY = Mathf.SmoothDamp(transform.position.y,
            follow.transform.position.y+3, ref velocity.y, soothTime);

        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
            transform.position.z);
    }
}
