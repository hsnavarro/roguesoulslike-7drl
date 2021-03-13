using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public GameObject InstantiateEnemy(int type) {
        GameObject enemy =  Instantiate(enemyPrefabs[type], Vector3.zero, Quaternion.identity);
        enemy.SetActive(true);
        return enemy;
    }
}
