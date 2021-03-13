using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public bool generateMap = true;

    public MapGenerationAlgorithm generator;

    public GameObject testFloor;
    public GameObject tilePrefab;
    public GameObject wallPrefab;

    public GameObject torchPrefab;

    public Transform activeTiles;
    public Transform tilePool;

    public Transform activeWalls;
    public Transform wallPool;
    public ItemGeneration itemGenerator;
    public EnemyGeneration enemyGenerator;

    public GameObject torchGenerator;

    public int torchCoverArea;
    public int maxTorchesNearby;
    bool [,] hasTorch;

    public float scale = 2;

    public void CreateTile(Vector3 position) {
        if (tilePool.childCount > 0) {
            Transform tile = tilePool.GetChild(0);
            tile.gameObject.SetActive(true);
            tile.SetParent(activeTiles);
            tile.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            tile.localPosition = position;
        } else {
            GameObject tile = GameObject.Instantiate(tilePrefab, 
                                                     new Vector3(), 
                                                     Quaternion.identity);
            tile.transform.SetParent(activeTiles);
            tile.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            tile.transform.localPosition = position;
        }
    }

    public void DrawFloor() {
        tilePool.localScale = new Vector3(scale, scale, scale);
        activeTiles.localScale = new Vector3(scale, scale, scale);
        while (activeTiles.childCount > 0) {
            Transform child = activeTiles.GetChild(0);
            child.SetParent(tilePool);
            child.gameObject.SetActive(false);
        }
        activeTiles.position = scale*(new Vector3(-generator.width/2, 0, -generator.height/2));

        for (int i = 0; i < generator.tilePositions.Count; i++) {
            int x = generator.tilePositions[i].Item1;
            int y = generator.tilePositions[i].Item2;
            CreateTile(new Vector3(x, 0, y));
        }
    }

    public void DrawWalls() {
        wallPool.localScale = new Vector3(scale, scale, scale);
        activeWalls.localScale = new Vector3(scale, scale, scale);
        while (activeWalls.childCount > 0) {
            Transform child = activeWalls.GetChild(0);
            child.SetParent(wallPool);
            child.gameObject.SetActive(false);
        }
        activeWalls.position = scale*(new Vector3(-generator.width/2, 0, -generator.height/2));

        for (int i = 0; i < 4; i++) {
            DrawWallsInDirection(i);
        }
    }

    public void SpawnItems() {
        List<Tuple<int, int>> positions = generator.GenerateItemsStartPositions();
        itemGenerator.transform.localScale = new Vector3(scale, scale, scale);
        itemGenerator.transform.position = scale*(new Vector3(-generator.width/2, 0, -generator.height/2));
        for (int i = 0; i < positions.Count; i++) {
            GameObject item = itemGenerator.InstantiateItem();
            item.transform.SetParent(itemGenerator.transform);
            item.transform.localPosition = new Vector3(positions[i].Item1, 0.5f, positions[i].Item2);
        }
    }

    public void SpawnMonsters() {
        List<Tuple<int, int>> positions = generator.GenerateMonsterSpawnPositions();
        enemyGenerator.transform.localScale = new Vector3(scale, scale, scale);
        enemyGenerator.transform.position = scale*(new Vector3(-generator.width/2, 0, -generator.height/2));
        for (int i = 0; i < positions.Count; i++) {
            GameObject enemy = enemyGenerator.InstantiateEnemy();
            enemy.transform.SetParent(enemyGenerator.transform);
            enemy.transform.localPosition = new Vector3(positions[i].Item1, 0.5f, positions[i].Item2);
        }
    }

    public int CountTorchesNearby(Tuple<int, int> position) {
        int ans = 0;
        for (int i = position.Item1 - torchCoverArea; i <= position.Item1 + torchCoverArea; i++) {
            for (int j = position.Item2 - torchCoverArea; j <= position.Item2 + torchCoverArea; j++) {
                if (!generator.IsLimit(i, j) && hasTorch[i, j]) {
                    ans++;
                }
            }
        }
        return ans;
    }

    public void Illuminate() {
        List<Tuple<int, int>> positions = generator.GenerateIlluminationSpots();
        hasTorch = new bool[generator.width, generator.height];
        torchGenerator.transform.localScale = new Vector3(scale, scale, scale);
        torchGenerator.transform.position = scale*(new Vector3(-generator.width/2, 0, -generator.height/2));

        for (int i = 0; i < positions.Count; i++) {
            if (CountTorchesNearby(positions[i]) <= maxTorchesNearby) {
                hasTorch[positions[i].Item1, positions[i].Item2] = true;
                GameObject torch = Instantiate(torchPrefab, Vector3.zero, Quaternion.identity);
                torch.SetActive(true);
                torch.transform.SetParent(torchGenerator.transform);
                torch.transform.localPosition = new Vector3(positions[i].Item1, 0, positions[i].Item2);
                torch.transform.eulerAngles = new Vector3(90, 0, 0);
            }
        }

    }

    private void Start() {
        if (generateMap) {
            SpawnMap();
        } else {
            Instantiate(testFloor, new Vector3(), Quaternion.identity);
        }
    }


    void SpawnMap() {
        generator.GenerateMap();
        DrawFloor();
        DrawWalls();
        SpawnItems();
        SpawnMonsters();
        Illuminate();
    }

    private Vector3 GetCorrespondingRotationForWall(int d) {
        switch (d) {
            case 0: 
                return new Vector3(0, 0, 0);
            case 1: 
                return new Vector3(0, 180, 0);
            case 2: 
                return new Vector3(0, 90, 0);
            case 3: 
                return new Vector3(0, -90, 0);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    private void CreateWall(Vector3 position, Vector3 rotation) {
        if (wallPool.childCount > 0) {
            Transform wall = wallPool.GetChild(0);
            wall.gameObject.SetActive(true);
            wall.SetParent(activeWalls);
            wall.localScale = new Vector3(1f, 1f, 0.1f);
            wall.localPosition = position;
            wall.localEulerAngles = rotation;
        } else {
            GameObject wall = GameObject.Instantiate(wallPrefab, 
                                                     new Vector3(), 
                                                     Quaternion.identity);
            wall.transform.SetParent(activeWalls);
            wall.transform.localScale = new Vector3(1f, 1f, 0.1f);
            wall.transform.localPosition = position;
            wall.transform.localEulerAngles = rotation;
        }
    }

    private void DrawWallsInDirection(int d) {
        Tuple<int, int> dir = generator.GetDirection(d);
        for (int i = 0; i < generator.width; i++) {
            for (int j = 0; j < generator.height; j++) if (generator.grid[i, j] == true) {
                int nI = i + dir.Item1;
                int nJ = j + dir.Item2;
                if (generator.grid[nI, nJ] == false) {
                    CreateWall(new Vector3(nI - dir.Item1/2f, 0.5f, nJ - dir.Item2/2f), GetCorrespondingRotationForWall(d));
                }
            }
        }
    }
}