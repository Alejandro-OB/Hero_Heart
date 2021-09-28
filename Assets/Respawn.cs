using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HEROE Heroe = collision.GetComponent<HEROE>();
        if (collision.gameObject.name == "Heroe")
        {
            Heroe.vida -= Heroe.vida;
            Heroe.barraVida.value -= Heroe.vida;
        }
    }
}
