using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour {

    [SerializeField]
    private float inputBlockTime;
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private float speed = 100f;

    public void OnInput() {
        if (Time.time > inputBlockTime) Application.Quit();
    }

    // Update is called once per frame
    private void Update() {
        if (Time.time > delayTime) transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
