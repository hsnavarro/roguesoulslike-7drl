using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{

    private float delayTime = 1f;
    [SerializeField]
    private float speed = 100f;

    public void OnInput() {
        Application.Quit();
    }

    // Update is called once per frame
    private void Update() {
        if (Time.time > delayTime) transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
