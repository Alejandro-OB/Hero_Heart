using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejoras : MonoBehaviour
{
    public float aumentarVelocidad = 2;
    public float aumentarFuerzaSalto = 2;
    public GameObject sonidoPocion;
    public GameObject pocion;
    public HEROE heroe;
    public float aumentarVida;




    public void DestruirPocion()
    {
        Destroy(gameObject);
    }

  


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.name == "Heroe")
        {
            DestruirPocion();
            ProducirSonido(sonidoPocion, transform.position, 1f);

            heroe.VelocidadHeroe += aumentarVelocidad;
            heroe.fuerzaSalto += aumentarFuerzaSalto;

            if (heroe.diferenciaSalud >= aumentarVida)
            {
                heroe.vidaActual += aumentarVida;
                heroe.barraVida.value += aumentarVida;
            }
            else
            {
                heroe.vidaActual += heroe.diferenciaSalud;
                heroe.barraVida.value += heroe.diferenciaSalud;
            }

        }

        
    }

    private void ProducirSonido(GameObject prefabSonido, Vector2 posicion, float duracion)
    {
        Destroy(Instantiate(prefabSonido, posicion, Quaternion.identity), duracion);
    }

}
