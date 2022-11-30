using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    public GameObject bulletPrefab;

    private Vector3 hMoveDistance = new Vector3(0.05f,0,0);
    private Vector3 vMoveDistance = new Vector3(0, 0.15f, 0);

    private const float MAX_LEFT = -4f;
    private const float MAX_RIGHT = 4f;
    private const float MAX_MOVE_SPEED = 0.5f;
    private float moveTimer = 0.01f;
    private float moveTime = 0.005f;
    private float shootTimer = 3f;
    private const float shootTime = 3f;

    public ObjectPool objectPool;

    public GameObject motherShipPrefab;
    private Vector3 motherShipSpawnPos = new Vector3(3.72f, 7.45f, 0);
    private float motherShipTimer = 1f;
    private const float MOTHERSHIP_MIN = 15F;
    private const float MOTHERSHIP_MAX = 60F;


    public static List<GameObject> allAliens = new List<GameObject>();

    private bool movingRight;
    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(obj);
        }
        movingRight = true;
    }

    void Update()
    {
        if (moveTimer <= 0)
        {
            MoveEnemies();
        }
        if(shootTimer<= 0)
        {
            Shoot();
        }
        if (motherShipTimer <= 0)
        {
            SpawnMotherShip();
        }
        moveTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;
        motherShipTimer -= Time.deltaTime;
    }

    private void MoveEnemies()
    {
        int hitmax = 0;
        if(allAliens.Count > 0)
        {
            for (int i = 0; i < allAliens.Count; i++)
            {
                if (movingRight)
                {
                    allAliens[i].transform.position += hMoveDistance;
                }
                else
                {
                    allAliens[i].transform.position -= hMoveDistance;
                }
                if (allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x < MAX_LEFT)
                {
                    hitmax++;
                }
            }
            if (hitmax > 0)
            {
                for (int i = 0; i < allAliens.Count; i++)
                {
                    allAliens[i].transform.position -= vMoveDistance;
                }
                movingRight = !movingRight;
            }
            moveTimer = GetMoveSpeed();
        }
    }

    void Shoot()
    {
        Vector2 pos = allAliens[Random.Range(0, allAliens.Count)].transform.position;
        GameObject go = objectPool.GetPooledObject();
        go.transform.position = pos;
        shootTimer = shootTime;
    }

    void SpawnMotherShip()
    {
        Instantiate(motherShipPrefab, motherShipSpawnPos, Quaternion.identity);
        motherShipTimer = Random.Range(MOTHERSHIP_MIN, MOTHERSHIP_MAX);
    }


    float GetMoveSpeed()
    {
        float f = allAliens.Count * moveTime;
        if(f < MAX_MOVE_SPEED)
        {
            return MAX_MOVE_SPEED;
        }
        else
        {
            return f;
        }
    }
}
