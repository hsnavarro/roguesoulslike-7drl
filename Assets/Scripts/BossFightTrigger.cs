using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Weapon") {
            SceneManager.LoadScene("Reaper");
        }
    }
}
