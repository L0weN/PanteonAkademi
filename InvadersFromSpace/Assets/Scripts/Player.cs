using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ObjectPool objectPool;

    private const float max_X = 2.18f;
    private const float min_X = -2.18f;

    private float speed = 4f;
    private float cooldown = 0.5f;
    private bool isShooting;

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
    }
#endif

    private IEnumerator Shoot()
    {
        isShooting = true;
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject go = objectPool.GetPooledObject();
        go.transform.position = transform.position;
        yield return new WaitForSeconds(cooldown);
        isShooting = false;
    }

    
}
