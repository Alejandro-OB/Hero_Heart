using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject HeroKnight;
    private float LastAttack;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = HeroKnight.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        float distance = Mathf.Abs(HeroKnight.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastAttack + 0.25f)
        {
            Attack();
            LastAttack = Time.time;
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
    }
}
