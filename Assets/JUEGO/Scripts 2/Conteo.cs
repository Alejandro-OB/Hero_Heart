using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conteo : MonoBehaviour
{
    public GameObject Pared;
    public int Limite = 0;
    
 
    public static Conteo instance;
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

    public void DeActivator()
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
