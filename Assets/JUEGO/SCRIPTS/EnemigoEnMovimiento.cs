using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoEnMovimiento : MonoBehaviour
{
    public float velocidad = 2;
    bool derecha;
    public GameObject PuntoInicio;
    public GameObject PuntoFin;
    public HEROE Heroe;
    public float daño = 5;
    Animator animator;
    public GameObject sonidoHit;


    void Start()
    {
        animator = GetComponent<Animator>();
        if (!derecha)
        {
            transform.position = PuntoInicio.transform.position;
        }
        else
        {
            transform.position = PuntoFin.transform.position;
        }
    }

    void Update()
    {
        if (!derecha)
        {
            transform.position = Vector3.MoveTowards(transform.position, PuntoFin.transform.position, velocidad * Time.deltaTime);
            if (transform.position == PuntoFin.transform.position) derecha = true;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (derecha)
        {
            transform.position = Vector3.MoveTowards(transform.position, PuntoInicio.transform.position, velocidad * Time.deltaTime);
            if (transform.position == PuntoInicio.transform.position) derecha = false;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Heroe == null) return;

        if (collision.gameObject.name == "Heroe")
        {
            Heroe.vidaActual -= daño;
            Heroe.barraVida.value -= daño;
            //Heroe.Rb2D.velocity = new Vector2(Heroe.BumpX, Hero.BumpY);
            Heroe.animator.SetTrigger("Hurt");
            ProducirSonido(sonidoHit, transform.position, 1f);
        }
        else
        {
            Heroe.animator.ResetTrigger("Hurt");
        }
    }

    private void ProducirSonido(GameObject prefabSonido, Vector2 posicion, float duracion)
    {
        Destroy(Instantiate(prefabSonido, posicion, Quaternion.identity), duracion);
    }
}
