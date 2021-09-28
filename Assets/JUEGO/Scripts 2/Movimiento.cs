using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pruebapersonaje : MonoBehaviour
{
    // Variables de movimiento 
    public float runSpeed = 2f;

    public Slider lifeSlider;

    Rigidbody2D Rb2D;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public GameObject hitBox;
    public GameObject Hit;
    public GameObject hitBox2;
    public GameObject Hit2;

    public bool deadHero = false;

    

    // Variables de ataque
    private bool Attack1;
    private bool Attack2;

    public float BumpX, BumpY;

    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        checkLife();
        setAnimDead();

        // Ataque 1
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            Attack1 = true;
            if (Attack1)
            {
                CreateHitBox();
                animator.SetTrigger("Attacking1");
            }
        }

        // Ataque 2
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Attack2 = true;
            if (Attack2)
            {
                CreateHitBox();
                animator.SetTrigger("Attacking2");
            }
        }


        //// El personaje está en el suelo o no
        //if (CheckGround.isGrounded == false)
        //{
        //    animator.SetBool("Jump", true);
        //    animator.SetBool("Running", false);
        //}
        //if (CheckGround.isGrounded == true)
        //{
        //    animator.SetBool("Jump", false);
        //    //animator.SetBool("Falling", false);
        //}

        //if (spriteRenderer.flipX == true)
        //{
        //    if (CheckGround.isGrounded == true)
        //    {
        //        animator.SetBool("Jump", false);
        //        //animator.SetBool("Falling", false);
        //    }
        //}
    }
    private void FixedUpdate()
    {

        //Moverse 
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            Rb2D.velocity = new Vector2(runSpeed, Rb2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Running", true);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            Rb2D.velocity = new Vector2(-runSpeed, Rb2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Running", true);
        }
        else
        {
            Rb2D.velocity = new Vector2(0, Rb2D.velocity.y);
            animator.SetBool("Running", false);
        }
    }

    //crea objeto para matar a skeleton
    public void CreateHitBox()
    {
        if (GameObject.Find("Hit(Clone)"))
        {
            return;
        }
        else
        {
            Vector3 positionHit = new Vector3(hitBox.transform.position.x, hitBox.transform.position.y, 0);
            GameObject temphit = Instantiate(Hit, positionHit, Quaternion.identity);
            Destroy(temphit, 1f);
        }
        if (GameObject.Find("Hit2(Clone)"))
        {
            return;
        }
        else
        {
            Vector3 positionHit2 = new Vector3(hitBox2.transform.position.x, hitBox2.transform.position.y, 0);
            GameObject temphit2 = Instantiate(Hit2, positionHit2, Quaternion.identity);
            Destroy(temphit2, 1f);
        }
    }


    public float lifeHero;

    public float damageEnemy = 1;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hit1(Clone)")
        {
            lifeHero -= damageEnemy;
            lifeSlider.value -= damageEnemy;
        }
        if (collision.tag == "Enemy2")
        {
            Rb2D.velocity = new Vector2(BumpX, BumpY);
            lifeHero -= damageEnemy;
            lifeSlider.value -= damageEnemy;
        }
    }
    private void setAnimDead()
    {
        animator.SetBool("Dead", deadHero);
    }

    private void checkLife()
    {
        if (lifeHero <= 0)
        {
            deadHero = true;
            this.enabled = false;
            //Destroy(gameObject, 2);
        }
    }

}
