using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        // 풀에서 가져오고 리스트에 저장
        GameObject instance = ObjectPoolManager.I.SpawnFromPool("Enemy", transform.position, quaternion.identity);
        enemyList.Add(instance);
    }

    public void SpawnBoss()
    {
        CameraFade.I.StartFade();
        bossSpawned = true;
        ShowBtnBoss(false);
        
        // 풀에서 가져오고 리스트에 저장
        GameObject instance = ObjectPoolManager.I.SpawnFromPool("Boss", transform.position + new Vector3(0, 0.5f, 0), quaternion.identity);
        bossList.Add(instance);
        
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            GameObject go = enemyList[i];
            go.GetComponentInChildren<Enemy>().ReturnToPool();
            enemyList.Remove(go);
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

    public void FailBoss()
    {
        DieBoss();
        foreach (var VARIABLE in bossList)
        {
            VARIABLE.GetComponentInChildren<Enemy>().ReturnToPool();
        }
        bossList.RemoveAll(x=> x);
    }
}
