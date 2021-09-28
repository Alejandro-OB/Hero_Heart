using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HEROE : MonoBehaviour
{
    public float VelocidadHeroe = 2;

    Rigidbody2D Rb2D;
    SpriteRenderer spriteRenderer;
    public Animator animator;

    public GameObject cajaDeColisiones;
    public GameObject golpeHeroe;

    public GameObject sonidoEscudo;
    public GameObject sonidoEspada;
    public GameObject sonidoMuerte;

    public float vida = 100;
    public float vidaActual = 100;

    public bool bloquear;

    public Slider barraVida;

    public bool Muerto;

    public float direccionRueda;
    public float velocidadRueda = 6;

    public Image TelaNegra;
    float valorAlfaTelaNegra;

    public int localScaleX, localScaleY, localScaleZ;

    public float diferenciaSalud;

    public float fuerzaSalto;
    public float saltosMaximos;
    public LayerMask capaSuelo;

    private BoxCollider2D boxCollider;
    private float saltosRestantes;

    public GameObject JumpSound;

    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;

        if (Guardar.JuegoGuardado == true)
        {
            CargarInformacion();
        }

        if (Guardar.JuegoGuardado == false) vidaActual = vida;

       
    }

    void Update()
    {
        Vector3 posicionCajaDeSonido = new Vector3(cajaDeColisiones.transform.position.x, cajaDeColisiones.transform.position.y, 0);
        Muere();
        ControlTelaNegra();
        ProcesarSalto();

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Z))
        {
            crearCajaDeColisiones();
            animator.SetTrigger("Atacar");
            reproducirSonido(sonidoEspada, posicionCajaDeSonido, 1f);
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown("x"))
        {
            animator.SetTrigger("Bloquear");
            animator.SetBool("SostenerEscudo", true);
            bloquear = true;
            reproducirSonido(sonidoEscudo, transform.position, 1f);
        }
        else if (Input.GetMouseButtonUp(1) || Input.GetKeyUp("x"))
        {
            animator.SetBool("SostenerEscudo", false);
            bloquear = false;
        }

        if (transform.localScale.x > 0)
        {
            direccionRueda = 1;
        }
        else
        {
            direccionRueda = -1;
        }

        if (Input.GetKeyDown("r") || Input.GetKeyDown("c"))
        {
            animator.SetTrigger("Rodar");
            Rb2D.velocity = new Vector2(velocidadRueda * direccionRueda, Rb2D.velocity.y);
            //rolling = true;
        }

        animator.SetFloat("AirSpeedY", Rb2D.velocity.y);

        if (Input.GetKey(KeyCode.P)) CargarInformacion();
        if (Input.GetKey(KeyCode.O)) GuardarInformacion();

        diferenciaSalud = vida - vidaActual;

        

    }

    void FixedUpdate()
    {
        //MOVERSE
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            Rb2D.velocity = new Vector2(VelocidadHeroe, Rb2D.velocity.y);
            transform.localScale = new Vector3(localScaleX * 1, localScaleY, localScaleZ);
            animator.SetBool("correr", true);
            
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            Rb2D.velocity = new Vector2(-VelocidadHeroe, Rb2D.velocity.y);
            animator.SetBool("correr", true);
            transform.localScale = new Vector3(localScaleX * -1, localScaleY, localScaleZ);
        }
        else
        {
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
            animator.SetBool("correr", false);
        }
    }

    public void crearCajaDeColisiones()
    {
        if (GameObject.Find("GolpeHeroe(Clone)"))
        {
            return;
        }
        else
        {
            Vector3 posicionGolpe = new Vector3(cajaDeColisiones.transform.position.x, cajaDeColisiones.transform.position.y, 0);
            GameObject golpeTemporal = Instantiate(golpeHeroe, posicionGolpe, Quaternion.identity);
            Destroy(golpeTemporal, 1f);
        }
    }

    private void reproducirSonido(GameObject prefabSonido, Vector2 posicion, float duracion)
    {
        Destroy(Instantiate(prefabSonido, posicion, Quaternion.identity), duracion);
    }

    public void Muere()
    {
        if (vidaActual <= 0)
        {
            animator.SetBool("Muere", Muerto);
            if (GameObject.Find("SonidoMuerte(Clone)"))
            {
                return;
            }
            else
            {
                reproducirSonido(sonidoMuerte, transform.position, 1f);
            }
            Muerto = true;
            Destroy(gameObject, 2);
            startFadeOut();
            
        }
    }

    public void ControlTelaNegra()
    {
        float valorAlfa = Mathf.Lerp(TelaNegra.color.a, valorAlfaTelaNegra, .05f);
        TelaNegra.color = new Color(0, 0, 0, valorAlfa);

        if (valorAlfa > .9f && valorAlfaTelaNegra == 1) SceneManager.LoadScene(EscenaActual());
    }

    private int EscenaActual()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void startFadeOut()
    {
        valorAlfaTelaNegra = 1;
    }

    public void GuardarInformacion()
    {
        Guardar.informacionHeroe.vidaHeroe = vidaActual;
        Guardar.informacionHeroe.Posicion = transform.position;
        Guardar.informacionHeroe.barraVidaHeroe = barraVida.value;
        Guardar.JuegoGuardado = true;
    }

    public void CargarInformacion()
    {
        vidaActual = Guardar.informacionHeroe.vidaHeroe;
        transform.position = Guardar.informacionHeroe.Posicion;
        barraVida.value = Guardar.informacionHeroe.barraVidaHeroe;
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    void ProcesarSalto()
    {
        if (EstaEnSuelo())
        {
            saltosRestantes = saltosMaximos;
            animator.SetBool("Jump", false);
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("correr", false);
            animator.SetBool("Grounded", false);

        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Grounded", false);
            reproducirSonido(JumpSound, transform.position, 1f);


            if (saltosRestantes > 0)
            {
                saltosRestantes = saltosRestantes - 1;
                Rb2D.velocity = new Vector2(Rb2D.velocity.x, 0f);
                Rb2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            }
        }


    }

}
