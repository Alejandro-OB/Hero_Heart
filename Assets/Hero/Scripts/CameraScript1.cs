using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript1 : MonoBehaviour
{
    public GameObject HeroKnight;
    void Update()
    {
        Vector3 position = transform.position;
        position.x = HeroKnight.transform.position.x;
        transform.position = position;
    }
}
