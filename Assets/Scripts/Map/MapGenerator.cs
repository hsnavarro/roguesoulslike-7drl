using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapGenerationAlgorithm generator;

    void Start() {
        generator.Generate();
    }

    // Update is called once per frame
    void Update() {}
}