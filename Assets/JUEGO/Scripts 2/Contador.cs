using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
    public GameObject Pared;
    public int Limite = 0;


    public static Contador instance;
    public int CantidadEnemigos = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Pared.SetActive(true);
    }

    public void DeActivator1()
    {
        CantidadEnemigos = CantidadEnemigos + 1;
        if (Pared != null)
        {
            if (CantidadEnemigos == Limite)
            {
                Pared.SetActive(false);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

}