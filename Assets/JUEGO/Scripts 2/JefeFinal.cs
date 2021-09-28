using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinal : MonoBehaviour
{
    private void OnDestroy()
    {
        BossUI.instance.BossDeActivator1();
    }
}
