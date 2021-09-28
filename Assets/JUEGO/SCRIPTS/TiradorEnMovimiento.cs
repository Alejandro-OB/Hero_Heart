using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiradorEnMovimiento : MonoBehaviour
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
    public float entrarZonaActivaY, salirZonaActivaY, distanciaAtaqueY;

    public float transformScaleX, transformScaleY, transformScaleZ;

    public GameObject sonidoMuerte;

    public float vidaEnemigo = 10;
    public float dañoRecibido = 0.2f;

    public GameObject barraVidaEnemigo;

    public SpriteRenderer spriteRenderer;

    public GameObject HitFire;
    public GameObject balaPrefab;

    public float destruirBala;
    float ultimoDisparo;
    public float frecuenciaDisparo;

    public Transform transformBala;


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
        distanciaHeroeY = Mathf.Abs(transformHeroe.position.y - transform.position.y);

        switch (behaviour)
        {
            case Behaviour.pasivo:

                animator.speed = 1;

                Rb2D.velocity = new Vector2(velocidadEnemigo * direccion, Rb2D.velocity.y);

                //cambiar posición según limites
                if (transform.position.x < limiteMovimientoIzquierdo) direccion = 1;
                if (transform.position.x > limiteMovimientoDerecho) direccion = -1;

                if (distanciaHeroeX < entrarZonaActivaX && distanciaHeroeY < entrarZonaActivaY)
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

                if (distanciaHeroeX < distanciaAtaqueX && distanciaHeroeY < distanciaAtaqueY)
                {
                    behaviour = Behaviour.ataque;
                }

                break;

            case Behaviour.ataque:

                animator.speed = 1;
                //animator.SetTrigger("Disparar");

                if (Time.time > ultimoDisparo + frecuenciaDisparo)
                {
                    Disparar();
                    ultimoDisparo = Time.time;
                }

                //cambiar posición segun heroe
                if (transformHeroe.position.x > transform.position.x)
                {
                    direccion = 1;
                    transformBala.localScale = new Vector3(1, 1, 1); 
                }
                if (transformHeroe.position.x < transform.position.x)
                {
                    direccion = -1;
                    transformBala.localScale = new Vector3(-1, 1, 1);
                }

                if (distanciaHeroeX > distanciaAtaqueX)
                {
                    behaviour = Behaviour.pasivo;
                    //animator.ResetTrigger("Disparar");
                }

                break;

            default:
                break;
        }
        transform.localScale = new Vector3(transformScaleX * direccion, transformScaleY, transformScaleZ);
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

    public void Disparar()
    {
        Vector3 direccionDisparo;
        if (transform.localScale.x > 0 && spriteRenderer.flipX == false) direccionDisparo = Vector3.left;
        else
        {
            direccionDisparo = Vector3.right;
        }
        Vector3 posicionDisparo = new Vector3(HitFire.transform.position.x, HitFire.transform.position.y, 0);
        GameObject bala = Instantiate(balaPrefab, posicionDisparo, Quaternion.identity);
        bala.GetComponent<Bala>().EstablecerDireccion(direccionDisparo);
        Destroy(bala, destruirBala);
    }
}
