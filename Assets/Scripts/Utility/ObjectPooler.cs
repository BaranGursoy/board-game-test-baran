using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> objectPoolDictionary;
    public List<GameObject> objectsToPool;
    public int initialPoolSize = 10;
    public static ObjectPooler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (GameObject obj in objectsToPool)
        {
            CreateObjectPool(obj.name);
        }
    }

    private void CreateObjectPool(string objectType)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(GetObjectPrefab(objectType), transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        objectPoolDictionary.Add(objectType, objectPool);
    }

    private GameObject GetObjectPrefab(string objectType)
    {
        foreach (GameObject obj in objectsToPool)
        {
            if (obj.name == objectType)
            {
                return obj;
            }
        }
        return null;
    }

    public GameObject SpawnFromPool(string objectType, Vector3 position, Quaternion rotation)
    {
        if (!objectPoolDictionary.ContainsKey(objectType))
        {
            Debug.LogWarning("Object type does not exist");
            return null;
        }

        Queue<GameObject> objectPool = objectPoolDictionary[objectType];

        if (objectPool.Count == 0)
        {
            GameObject obj = Instantiate(GetObjectPrefab(objectType), position, rotation);
            return obj;
        }
        else
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);

        string objectName = obj.name.Split('(')[0];
        
        Queue<GameObject> objectPool = objectPoolDictionary[objectName];
        objectPool.Enqueue(obj);
        obj.transform.SetParent(transform);
    }
}
