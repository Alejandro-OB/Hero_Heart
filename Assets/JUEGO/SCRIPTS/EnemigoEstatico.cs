using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoEstatico : MonoBehaviour
{
    public float Da�o = 5;
    public GameObject sonidoHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HEROE Heroe = collision.GetComponent<HEROE>();
        if (collision.gameObject.name == "Heroe")
        {
            Heroe.vida -= Da�o;
            Heroe.barraVida.value -= Da�o;
            ProducirSonido(sonidoHit, transform.position, 1f);
        }
    }

    private void ProducirSonido(GameObject prefabSonido, Vector2 posicion, float duracion)
    {
        Destroy(Instantiate(prefabSonido, posicion, Quaternion.identity), duracion);
    }
}
