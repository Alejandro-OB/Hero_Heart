using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoEstatico : MonoBehaviour
{
    public float Daño = 5;
    public GameObject sonidoHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HEROE Heroe = collision.GetComponent<HEROE>();
        if (collision.gameObject.name == "Heroe")
        {
            Heroe.vida -= Daño;
            Heroe.barraVida.value -= Daño;
            ProducirSonido(sonidoHit, transform.position, 1f);
        }
    }

    private void ProducirSonido(GameObject prefabSonido, Vector2 posicion, float duracion)
    {
        Destroy(Instantiate(prefabSonido, posicion, Quaternion.identity), duracion);
    }
}
