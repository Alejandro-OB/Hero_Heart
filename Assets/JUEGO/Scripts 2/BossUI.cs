using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public GameObject muros;
    public GameObject CambioDeEscena;

    public static BossUI instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        muros.SetActive(false);
        CambioDeEscena.SetActive(false);
    }

    public void BossActivator()
    {
        if (muros != null) muros.SetActive(true);
        else return;

    }

    public void BossDeActivator()
    {
        if (muros != null) muros.SetActive(false);
        else return;
    }

    public void BossDeActivator1()
    {
        if (CambioDeEscena != null) CambioDeEscena.SetActive(true);
        else return;

    }
}
