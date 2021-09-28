using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jefe : MonoBehaviour
{
    private void OnDestroy()
    {
        BossUI.instance.BossDeActivator();
    }
}

