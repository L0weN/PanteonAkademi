using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody2D rb2d;
    public float deathDelay = 5;
    
    
    void Start()
    {
        rb2d.velocity = transform.up * speed;
        StartCoroutine(DeathAfterDelay(deathDelay));
    }

    private IEnumerator DeathAfterDelay(float deathDelay)
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Projectile>() || collision.GetComponent<Enemy>() || collision.GetComponent<Player>())
            Destroy(gameObject);
    }
}
