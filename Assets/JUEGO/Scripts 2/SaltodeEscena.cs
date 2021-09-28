using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaltodeEscena : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Wait());
  
            SceneManager.LoadScene(sceneName);
            Guardar.JuegoGuardado = false;
        }

    }

    IEnumerator Wait() 
    {
        
        yield return new WaitForSeconds(100f);
        
    }
}
