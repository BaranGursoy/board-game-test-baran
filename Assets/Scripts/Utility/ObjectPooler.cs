using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public PrefabType prefabType;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<PrefabType, Queue<GameObject>> objectPoolDictionary;
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

        objectPoolDictionary = new Dictionary<PrefabType, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            CreateObjectPool(pool);
        }
    }

    private void CreateObjectPool(Pool pool)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab, transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        objectPoolDictionary.Add(pool.prefabType, objectPool);
    }

    public GameObject SpawnFromPool(PrefabType prefabType, Vector3 position, Quaternion rotation)
    {
        if (!objectPoolDictionary.ContainsKey(prefabType))
        {
            Debug.LogWarning("Prefab type does not exist");
            return null;
        }

        Queue<GameObject> objectPool = objectPoolDictionary[prefabType];

        if (objectPool.Count == 0)
        {
            GameObject obj = Instantiate(GetObjectPrefab(prefabType), position, rotation);
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

    private GameObject GetObjectPrefab(PrefabType prefabType)
    {
        foreach (Pool pool in pools)
        {
            if (pool.prefabType == prefabType)
            {
                return pool.prefab;
            }
        }
        return null;
    }

    public void ReturnToPool(GameObject obj, PrefabType prefabType)
    {
        obj.SetActive(false);

        if (!objectPoolDictionary.ContainsKey(prefabType))
        {
            Debug.LogWarning("Object doesn't match any prefab type in the pool");
            Destroy(obj);
            return;
        }

        Queue<GameObject> objectPool = objectPoolDictionary[prefabType];
        objectPool.Enqueue(obj);
        obj.transform.SetParent(transform);
    }
}

public enum PrefabType
{
    CurrencySprite,
    Dice
}
