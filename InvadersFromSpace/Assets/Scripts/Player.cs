using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ObjectPool objectPool;
    public ShipStats shipstats;
    private const float max_X = 4f;
    private const float min_X = -4f;

    private float speed = 4f;
    private float cooldown = 0.5f;
    private bool isShooting;

    private Vector2 offScreenPos = Vector2.zero;
    private Vector2 startPos = Vector2.zero;
    private float dirx;

    private void Start()
    {
        shipstats.currentHealth = shipstats.maxHealth;
        UIManager.UpdateHealthBar(shipstats.currentHealth);
    }
    void Update()
    {
#if UNITY_EDITOR    
        if (Input.GetKey(KeyCode.A) && transform.position.x > min_X)  
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < max_X)  
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isShooting) 
        {
            StartCoroutine(Shoot());
        }
#endif
        dirx = Input.acceleration.x;
        if (dirx <= -0.1f && transform.position.x > min_X)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (dirx >= 0.1f && transform.position.x < max_X)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }
    public void ShootButton()
    {
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot()
    {
        isShooting = true;
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject go = objectPool.GetPooledObject();
        go.transform.position = transform.position;
        yield return new WaitForSeconds(cooldown);
        isShooting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        shipstats.currentHealth--;
        UIManager.UpdateHealthBar(shipstats.currentHealth);
        if (shipstats.currentHealth <= 0) 
        {
            shipstats.currentLifes--;
            UIManager.UpdateLives(shipstats.currentLifes);
            if (shipstats.currentLifes <= 0)
            {
                Debug.Log("Game Over!");
            }
            else
            {
                StartCoroutine(Respawn());
                Debug.Log("Respawn");
            }
        }
        
    }

    public IEnumerator Respawn()
    {
        transform.position = offScreenPos;
        yield return new WaitForSeconds(2);
        shipstats.currentHealth = shipstats.maxHealth;
        transform.position = startPos;
        UIManager.UpdateHealthBar(shipstats.currentHealth);
    }
    
}
