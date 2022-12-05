using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    private int scoreVal;
    private const float MAX_LEFT = -5f, MIN_LEFT = 5f;
    private float speed = 5f;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x <= MAX_LEFT) 
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FriendlyBullet"))
        {
            UIManager.UpdateScore(scoreVal);
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
