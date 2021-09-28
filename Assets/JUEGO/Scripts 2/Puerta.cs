using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Puerta : MonoBehaviour
{
    private void OnDestroy()
    {
        Conteo.instance.DeActivator();
    }
}

