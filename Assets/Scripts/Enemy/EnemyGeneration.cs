using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemyPrefab;

    public GameObject InstantiateEnemy() {
        int enemyType = Random.Range(0, 3);

        GameObject enemy =  Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        enemy.GetComponent<EnemyStats>().enemyType = (EnemyTypes) enemyType;
        enemy.SetActive(true);
        return enemy;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
