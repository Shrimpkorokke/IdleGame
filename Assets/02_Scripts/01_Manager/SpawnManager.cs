using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private Button btnBoss;
    
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
        btnBoss.onClick.AddListener(SpawnBoss);
        ShowBtnBoss(false);
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
            if (enemyList.Count < maxCount)
            {
                float randomTime = UnityEngine.Random.Range(minTime, maxTime);
                yield return new WaitForSeconds(randomTime);

                if (bossSpawned == false)
                {
                    SpawnEnemy();
                    yield return null;
                }
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
        ShowBtnBoss(false);
        
        GameObject instance = Instantiate(bossPrefab, new Vector3(transform.position.x, 1, 0), Quaternion.identity);
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            GameObject go = enemyList[i];
            enemyList.Remove(go);
            Destroy(go);
        }
    }

    public void DieBoss()
    {
        bossSpawned = false;
        ShowBtnBoss(false);
    }
    
    public void RemoveEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    public void ShowBtnBoss(bool show)
    {
        btnBoss.gameObject.SetActive(show);
    }
}
