using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightTrigger : MonoBehaviour {
    public AudioClip bossMusic;

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Weapon") {
            Vector3 position = GameObject.FindGameObjectWithTag("ReaperRoom").transform.position;
            GameObject.FindGameObjectWithTag("Player").transform.position = position;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().Teleport();
            GameObject.FindGameObjectWithTag("Reaper").GetComponent<Reaper>().enabled = true;

            AudioSource music = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<AudioSource>();
            music.clip = bossMusic;
            music.Play();
        }
    }
}
