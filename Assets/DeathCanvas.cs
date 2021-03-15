using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathCanvas : MonoBehaviour {
    [SerializeField]
    private GameObject continueText;
    [SerializeField]
    private GameObject normalText;

    public void Show(float duration) {
        gameObject.SetActive(true);
        //normalText.SetActive(true);
        StartCoroutine(ShowCoroutine(normalText.GetComponent<Text>(), 1f));
    }

    public void ShowContinue() {
        //continueText.SetActive(true);
        StartCoroutine(ShowCoroutine(continueText.GetComponent<Text>(), 0.5f));
    }

    IEnumerator ShowCoroutine(Text text, float duration) {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);

        // fade out
        float startTime = Time.time;
        while (true) {
            yield return new WaitForEndOfFrame();
            float currentTime = Time.time;
            if (currentTime - startTime >= duration) break;

            float alpha = (currentTime - startTime) / duration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }
}
