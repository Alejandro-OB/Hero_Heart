using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pociones : MonoBehaviour
{
    public float aumentarVida = 20;
    public GameObject sonidoPocion;
    

    public void DestruirPocion()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        HEROE Heroe = collision.GetComponent<HEROE>();

        if (collision.gameObject.name == "Heroe")
        {
            DestruirPocion();
            if (Heroe.diferenciaSalud >= aumentarVida)
            {
                Heroe.vidaActual += aumentarVida;
                Heroe.barraVida.value += aumentarVida;
                ProducirSonido(sonidoPocion, transform.position, 1f);
            }
            else
            {
                Heroe.vidaActual += Heroe.diferenciaSalud;
                Heroe.barraVida.value += Heroe.diferenciaSalud;
                ProducirSonido(sonidoPocion, transform.position, 1f);
            }

        }
    }

    private void ProducirSonido(GameObject prefabSonido, Vector2 posicion, float duracion)
    {
        Destroy(Instantiate(prefabSonido, posicion, Quaternion.identity), duracion);
    }

}
