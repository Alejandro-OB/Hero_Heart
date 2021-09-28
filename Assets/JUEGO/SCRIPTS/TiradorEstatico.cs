using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiradorEstatico : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;

    public Transform transformHeroe;
    float distanciaHeroe;
    public float zonaDisparo;

    float ultimoDisparo;
    public float frecuenciaDisparo;

    public GameObject HitFire;
    public GameObject balaPrefab;

    public float destruirBala;
    public float vida;

    public GameObject barraVida;

    public float dañoRecibido = .1f;

    float direccion;

    public int localScaleX, localScaleY, localScaleZ;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (transformHeroe == null) return;

        //Calcular distancia del Heroe
        distanciaHeroe = Mathf.Abs(transformHeroe.position.x - transform.position.x);
    

        if (distanciaHeroe < zonaDisparo)
        {
            if (Time.time > ultimoDisparo + frecuenciaDisparo)
            {
                Disparar();
                ultimoDisparo = Time.time;
                animator.SetTrigger("Disparar");
            }
            
        }

        if (transformHeroe.position.x > transform.position.x)
        {
            direccion = 1;
        }
        if (transformHeroe.position.x < transform.position.x)
        {
            direccion = -1;
        }
        transform.localScale = new Vector3(localScaleX * direccion, localScaleY, localScaleZ);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GolpeHeroe(Clone)")
        {
            vida -= 1;
            if (vida <= 0)
            {
                animator.SetBool("Muerto", true);
                Destroy(gameObject, 1);
            }
            if (barraVida.transform.localScale.x >= 0)
            {
                barraVida.transform.localScale = new Vector3(barraVida.transform.localScale.x - dañoRecibido, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
            }
        }
    }

    private void OnDestroy()
    {
        Conteo.instance.DeActivator();
    }
}
