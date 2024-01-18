using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private Button BtnBoss;
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<GameObject> enemyList = new();
    
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private List<GameObject> bossList = new();

    public bool bossSpawned;
    
    [SerializeField] private float currentTime;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private float maxCount;
    [SerializeField] private float currentCount;

    protected override void Awake()
    {
        BtnBoss.onClick.AddListener(SpawnBoss);
    }

    private void Start()
    {
        StopCoroutine(nameof(CoSpawnEnemy));
        StartCoroutine(nameof(CoSpawnEnemy));
    }
    
    IEnumerator CoSpawnEnemy()
    {
        while (true)
        {
            if (bossSpawned == false)
            {
                if (enemyList.Count < maxCount)
                {
                    float randomTime = UnityEngine.Random.Range(minTime, maxTime);
                    yield return new WaitForSeconds(randomTime);
                    SpawnEnemy();
                }
            
                yield return null;
            }
            
            yield return null;
        }
    }

    private void SpawnEnemy()
    {
        GameObject instance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyList.Add(instance);
    }

    public void SpawnBoss()
    {
        bossSpawned = true;
        
        GameObject instance = Instantiate(bossPrefab, new Vector3(transform.position.x, 1, 0), Quaternion.identity);
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            Debug.Log("AAAAAAAAAA");
            Destroy(enemyList[i]);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }
}
