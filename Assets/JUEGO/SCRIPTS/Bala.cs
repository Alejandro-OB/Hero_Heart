using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    Rigidbody2D Rb2D;

    Vector2 Direccion;
    public float velocidadBala;
    public float dañoGenerado;
    public GameObject sonidoImpactoCuerpo;
    public GameObject sonidoImpactoEscudo;

    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rb2D.velocity = Direccion * velocidadBala;
    }

    public void EstablecerDireccion(Vector2 direccion)
    {
        Direccion = direccion;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HEROE Heroe = collision.GetComponent<HEROE>();
        if (collision.gameObject.name == "Heroe")
        {
            Destroy(gameObject);
            if (Heroe.bloquear == false)
            {
                Heroe.vidaActual -= dañoGenerado;
                Heroe.barraVida.value -= dañoGenerado;
                ProducirSonido(sonidoImpactoCuerpo, transform.position, 1f);
            }
            else if(Heroe.bloquear == true) ProducirSonido(sonidoImpactoEscudo, transform.position, 1f);
        }
    }

    private void ProducirSonido(GameObject prefabSonido, Vector2 posicion, float duracion)
    {
        Destroy(Instantiate(prefabSonido, posicion, Quaternion.identity), duracion);
    }
}
