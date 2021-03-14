using System.Collections;
using UnityEngine;

public class DeathCanvas : MonoBehaviour {
    public GameObject continueText;

    public void Show(float duration) {
        gameObject.SetActive(true);
        StartCoroutine(ShowCoroutine(GetComponent<CanvasGroup>(), 1f));
    }

    public void ShowContinue() {
        continueText.SetActive(true);
        StartCoroutine(ShowCoroutine(continueText.GetComponent<CanvasGroup>(), 0.5f));
    }

    IEnumerator ShowCoroutine(CanvasGroup group, float duration) {
        group.alpha = 0f;

        // fade out
        float startTime = Time.time;
        while (true) {
            yield return new WaitForEndOfFrame();
            float currentTime = Time.time;
            if (currentTime - startTime >= duration) break;

            float alpha = (currentTime - startTime) / duration;
            group.alpha = alpha;
        }

        group.alpha = 1f;
    }
}
