using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> pooledObjects;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize;
    void Awake()
    {
        pooledObjects= new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(objectPrefab);
            go.SetActive(false);
            pooledObjects.Enqueue(go);
        }
    }

    public GameObject GetPooledObject()
    {
        GameObject go = pooledObjects.Dequeue();
        go.SetActive(true);
        pooledObjects.Enqueue(go);
        return go;
    }
}
