using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject InstantiateEnemy() {
        int enemyType = Random.Range(0, 3);

        GameObject enemy =  Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        enemy.GetComponent<EnemyStats>().enemyType = (EnemyTypes) enemyType;
        enemy.SetActive(true);
        return enemy;
    }
}
