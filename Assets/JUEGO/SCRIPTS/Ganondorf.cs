using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganondorf : MonoBehaviour
{
    Rigidbody2D Rb2D;
    Animator animator;

    float limiteMovimientoIzquierdo, limiteMovimientoDerecho;

    enum Behaviour { pasivo, persecucion, ataque }
    Behaviour behaviour = Behaviour.pasivo;

    public float velocidadEnemigo = 2f;
    int direccion = 1;

    float distanciaHeroeX, distanciaHeroeY;

    public Transform transformHeroe;

    public float entrarZonaActivaX, salirZonaActivaX, distanciaAtaqueX;
  

    public float transformScaleX, transformScaleY, transformScaleZ;

    public GameObject cajaDeColisionEnemigo;
    public GameObject golpeEnemigo;

    float ultimoGolpe;

    public GameObject sonidoAtaque;
    public GameObject sonidoMuerte;

    public float vidaEnemigo = 10;
    public float dañoRecibido = 0.2f;

    public GameObject barraVidaEnemigo;


    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();

        //Limites de Movimiento
        limiteMovimientoIzquierdo = transform.position.x - GetComponent<CircleCollider2D>().radius;
        limiteMovimientoDerecho = transform.position.x + GetComponent<CircleCollider2D>().radius;
    }


    void Update()
    {
        if (transformHeroe == null) return;

        MuerteEnemigo();

        //calcular distancia del heroe
        distanciaHeroeX = Mathf.Abs(transformHeroe.position.x - transform.position.x);
      

        switch (behaviour)
        {
            case Behaviour.pasivo:

                animator.speed = 1;

                Rb2D.velocity = new Vector2(velocidadEnemigo * direccion, Rb2D.velocity.y);

                //cambiar posición según limites
                if (transform.position.x < limiteMovimientoIzquierdo) direccion = 1;
                if (transform.position.x > limiteMovimientoDerecho) direccion = -1;

                if (distanciaHeroeX < entrarZonaActivaX)
                {
                    behaviour = Behaviour.persecucion;
                }

                break;

            case Behaviour.persecucion:

                animator.speed = 1.5f;

                Rb2D.velocity = new Vector2(velocidadEnemigo * 1.5f * direccion, Rb2D.velocity.y);

                //cambiar posición segun heroe
                if (transformHeroe.position.x > transform.position.x) direccion = 1;
                if (transformHeroe.position.x < transform.position.x) direccion = -1;

                if (distanciaHeroeX > salirZonaActivaX) behaviour = Behaviour.pasivo;

                if (distanciaHeroeX < distanciaAtaqueX)
                {
                    behaviour = Behaviour.ataque;
                }

                break;

            case Behaviour.ataque:

                animator.speed = 1;
                animator.SetTrigger("Atacar");

                if (Time.time > ultimoGolpe + 1)
                {
                    CrearCajaDeColisiones();
                    ultimoGolpe = Time.time;
                }

                //cambiar posición segun heroe
                if (transformHeroe.position.x > transform.position.x) direccion = 1;
                if (transformHeroe.position.x < transform.position.x) direccion = -1;

                if (distanciaHeroeX > distanciaAtaqueX)
                {
                    behaviour = Behaviour.pasivo;
                    animator.ResetTrigger("Atacar");
                }

                break;

            default:
                break;
        }
        transform.localScale = new Vector3(transformScaleX * direccion, transformScaleY, transformScaleZ);
    }

    public void CrearCajaDeColisiones()
    {
        Vector3 posicionColision = new Vector3(cajaDeColisionEnemigo.transform.position.x, cajaDeColisionEnemigo.transform.position.y, 0);
        GameObject golpeTemporal = Instantiate(golpeEnemigo, posicionColision, Quaternion.identity);
        Destroy(golpeTemporal, 1f);
    }

    public void SonidoAtaque()
    {
        Destroy(Instantiate(sonidoAtaque, transform.position, Quaternion.identity), 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GolpeHeroe(Clone)")
        {
            vidaEnemigo -= 2;

            if (barraVidaEnemigo.transform.localScale.x >= 0)
            {
                barraVidaEnemigo.transform.localScale = new Vector3(barraVidaEnemigo.transform.localScale.x - dañoRecibido, barraVidaEnemigo.transform.localScale.y, barraVidaEnemigo.transform.localScale.z);
            }

        }
    }

    private void MuerteEnemigo()
    {
        if (vidaEnemigo <= 0)
        {
            animator.SetBool("Muerto", true);
            this.enabled = false;
            Destroy(gameObject, 1);
            ReproducirSonidos(sonidoMuerte, transform.position, 1f);
        }
    }

    public void ReproducirSonidos(GameObject prefabSonido, Vector2 posicion, float duracion)
    {
        Destroy(Instantiate(prefabSonido, transform.position, Quaternion.identity), 1f);
    }

    private void OnDestroy()
    {
        Conteo.instance.DeActivator();
        Contador.instance.DeActivator1();
    }
}
