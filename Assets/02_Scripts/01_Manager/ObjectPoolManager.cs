using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.SetParent(this.transform); // Optional: Set the pool manager as parent for organized hierarchy
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    
    GameObject CreateNewObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        obj.transform.SetParent(this.transform); // Optional: Set the pool manager as parent for organized hierarchy
        return obj;
    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // 비활성화된 오브젝트 찾기
        GameObject objectToSpawn = null;
        foreach (var obj in poolDictionary[tag])
        {
            if (!obj.activeInHierarchy) // 비활성화된 오브젝트가 있다면
            {
                objectToSpawn = obj;
                break;
            }
        }

        // 비활성화된 오브젝트가 없다면 새로운 오브젝트 생성
        if (objectToSpawn == null)
        {
            Pool pool = pools.Find(p => p.tag == tag);
            if (pool != null)
            {
                objectToSpawn = CreateNewObject(pool.prefab);
                poolDictionary[tag].Enqueue(objectToSpawn); // 새로 생성된 오브젝트를 풀에 추가
            }
            else
            {
                Debug.LogWarning("No pool found with tag: " + tag);
                return null;
            }
        }
        else
        {
            // 비활성화된 오브젝트를 풀에서 제거
            poolDictionary[tag] = new Queue<GameObject>(poolDictionary[tag].Where(o => o != objectToSpawn));
        }

        // 오브젝트 활성화 및 위치와 회전 설정
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // IPooledObject 인터페이스가 있을 경우 OnObjectSpawn 호출
        
        IPooledObject pooledObj = objectToSpawn.GetComponentInChildren<IPooledObject>();
        if (pooledObj != null)
        {
            Debug.Log("!!!!!!!");
            pooledObj.OnObjectSpawn();
        }

        return objectToSpawn;
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        IPooledObject pooledObj = objectToReturn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectReturn(); // 오브젝트가 풀로 반환되기 전에 필요한 작업을 수행
        }

        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }
}