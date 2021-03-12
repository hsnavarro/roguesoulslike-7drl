using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

public class MapGenerationAlgorithm : MonoBehaviour {

    int numberOfSteps;
    public List<Tuple<int, int>> tilePositions;
    public bool[,] grid;
    public int width = 200;
    public int height = 200;

    bool IsLimit(int i, int j) {
        return i == 0 || i == width - 1 || j == 0 || j == height - 1;
    }

    public Tuple<int, int> GetDirection(int i) {
        // 0-3: horizontal/vertical directions
        // 4-7: diagonals
        switch (i) {
            case 0:
                return new Tuple<int, int>(0, +1);
            case 1:
                return new Tuple<int, int>(0, -1);
            case 2:
                return new Tuple<int, int>(+1, 0);
            case 3:
                return new Tuple<int, int>(-1, 0);
            case 4:
                return new Tuple<int, int>(+1, +1);
            case 5:
                return new Tuple<int, int>(-1, -1);
            case 6:
                return new Tuple<int, int>(+1, -1);
            case 7:
                return new Tuple<int, int>(-1, +1);
            default:
                return new Tuple<int, int>(0, 0);
        }
    }

    public void UpdateTilesList() {
        tilePositions = new List<Tuple<int, int>>();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (grid[i, j] == true) {
                    tilePositions.Add(new Tuple<int, int>(i, j));
                }
            }
        }
    }

    int CountAdjacentWalls(int i, int j) {
        if (IsLimit(i, j)) {
            return -1;
        }
        int cnt = 0;
        for (int d = 0; d < 8; d++) {
            Tuple<int, int> direction = GetDirection(d);
            int nI = i + direction.Item1;
            int nJ = j + direction.Item2;
            if (grid[nI, nJ] == false || IsLimit(nI, nJ)) {
                cnt++;
            }
        }
        return cnt;
    }

    public void Smooth() {
        bool[,] gridCopy = new bool[width, height];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                gridCopy[i, j] = grid[i, j];
            }
        }

        for (int i = 1; i < width-1; i++) {
            for(int j = 1; j < height-1; j++) if (grid[i, j] == false) {
                int cnt = CountAdjacentWalls(i, j);
                if (cnt < 4) {
                    gridCopy[i, j] = true;
                }
            }
        }

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                grid[i, j] = gridCopy[i, j];
            }
        }
    }

    Tuple<int, int> GetGenerationStartPosition() {
        if (tilePositions.Count == 0) {
            return new Tuple<int, int>(width/2, height/2); 
        }

        int index = UnityEngine.Random.Range(0, tilePositions.Count-1);
        Tuple<int, int> start = tilePositions[index];
        return start;
    }

    void GenerationWalk() {
        Tuple<int, int> currentPosition = GetGenerationStartPosition();
        for (int i = 0; i < numberOfSteps; i++) {
            int curX = currentPosition.Item1;
            int curY = currentPosition.Item2;
            if (grid[curX, curY] == false) {
                grid[curX, curY] = true;
                tilePositions.Add(currentPosition);
            }

            List<int> dir = new List<int>{0, 1, 2, 3};
            dir = dir.OrderBy( x => Random.value ).ToList();
            for (int d = 0; d < 4; d++) {
                Tuple<int, int> randomDirection = GetDirection(dir[d]);
                int newX = curX + randomDirection.Item1;
                int newY = curY + randomDirection.Item2;
                if (!IsLimit(newX, newY)) {
                    currentPosition = new Tuple<int, int>(newX, newY);
                    break;
                }
            }
        }
    }

    public void Generate() {
        grid = new bool[width, height];
        tilePositions = new List<Tuple<int, int>>();

        for (int i = 0; i < 10; i++) {
            numberOfSteps = 2000;
            GenerationWalk();
        }
        for (int i = 0; i < 5; i++) {
            Smooth();
        }
        UpdateTilesList();
    }

    void Start() {
    }
}
