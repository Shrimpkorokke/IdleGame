using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float currentTime;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private float maxCount;
    [SerializeField] private float currentCount;
    
    private void Start()
    {
        StopCoroutine(nameof(BBB));
        StartCoroutine(nameof(BBB));
    }
    IEnumerator BBB()
    {
        while (true)
        {
            if (currentCount < maxCount)
            {
                float randomTime = UnityEngine.Random.Range(minTime, maxTime);
                yield return new WaitForSeconds(randomTime);
                SpawnEnemy();
            }
            
            yield return null;
        }
    }

    private void SpawnEnemy()
    {
        GameObject instance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        currentCount++;
    }
}
