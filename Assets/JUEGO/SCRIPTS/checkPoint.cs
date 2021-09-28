using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    public GameObject sonidoCheckPoint;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        HEROE Heroe = collision.GetComponent<HEROE>();
        if (collision.gameObject.name == "Heroe")
        {
            Heroe.GuardarInformacion();
            producirSonido(sonidoCheckPoint, transform.position, 1f);
            Destroy(gameObject);
       
        }

    }

    private void producirSonido(GameObject prefabSonido, Vector2 position, float duration)
    {
        Destroy(Instantiate(prefabSonido, position, Quaternion.identity), duration);
    }
}
