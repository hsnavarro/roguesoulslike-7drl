using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour {

    [SerializeField]
    private float inputBlockTime = 25f;
    [SerializeField]
    private float delayTime = 1f;
    [SerializeField]
    private float speed = 70f;

    private float startTime;

    public void OnInput() {
        if (Time.time - startTime > inputBlockTime) Application.Quit();
    }

    private void Start() {
        startTime = Time.time;
    }

    // Update is called once per frame
    private void Update() {
        if (Time.time - startTime > delayTime) transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
