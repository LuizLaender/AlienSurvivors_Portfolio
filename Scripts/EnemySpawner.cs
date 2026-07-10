using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<Sprite> enemySprites;
    [SerializeField] GameObject enemy;

    float speed = 2;
    float maxSpeed = 4;

    float health = 1;
    float maxHealth = 3;

    float reward = 50;
    float maxReward = 250;

    float bulletSpeed = 6;
    float maxBulletSpeed = 9;

    float spawnInterval = 0.9f;

    int level;
    int levelIndex;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            yield return StartCoroutine(InstantiateEnemy(enemy));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator InstantiateEnemy(GameObject enemy)
    {
        if(levelIndex % 10 == 0 && levelIndex != 0)
        {
            level++;
            spawnInterval = Mathf.Clamp(spawnInterval - 0.1f, 0.64f, spawnInterval);
        }
        levelIndex++;

        int randomSpawnPoint = Random.Range(0, spawnPoints.Count);

        var enemyInstance = Instantiate(enemy,
                                        spawnPoints[randomSpawnPoint].transform.position,
                                        Quaternion.identity,
                                        transform);

        enemyInstance.GetComponentInChildren<SpriteRenderer>().sprite = SpriteIterator();
        enemyInstance.GetComponent<Pathfinder>().SetMoveSpeed(LinearGrowth(speed,maxSpeed,level,1));
        enemyInstance.GetComponent<EnemyHealth>().SetStats(LinearGrowth(health,maxHealth,level,1), LinearGrowth(reward,maxReward,level,25));
        enemyInstance.GetComponent<EnemyShooter>().SetBulletSpeed(LinearGrowth(bulletSpeed,maxBulletSpeed,level,1));

        yield return new WaitForSeconds(0);
    }

    Sprite SpriteIterator()
    {
        switch(Mathf.Clamp(level, 0, 4))
        {
            case 0:
                return enemySprites[0];
            case 1:
                return enemySprites[1];
            case 2:
                return enemySprites[2];
            case 3:
                return enemySprites[3];
            case 4:
                return enemySprites[4];
            default:
                return null;
        }
    }

    float LinearGrowth(float startingValue, float max, int level, float increment)
    {
        float result = level * startingValue + increment;

        if(level == 0)
        {
            return startingValue;
        }
        else
        {
            return Mathf.Min(result, max);
        }
    }
}
