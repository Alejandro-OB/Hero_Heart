using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpe : MonoBehaviour
{
    public float daño = 4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HEROE Heroe = collision.GetComponent<HEROE>();
        if (collision.gameObject.name == "Heroe" && Heroe.bloquear == false)
        {
            
            Heroe.vidaActual -= daño;
            Heroe.barraVida.value -= daño;
            
        }
    }
}
