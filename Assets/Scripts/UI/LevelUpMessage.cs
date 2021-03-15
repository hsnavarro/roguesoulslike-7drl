using System.Collections;
using UnityEngine;

public class LevelUpMessage : MonoBehaviour {
    public float startY = 100f;
    public float endY = 130f;
    public float fadeDuration = 0.5f;
    public float stayDuration = 1.5f;

    private CanvasGroup group;
    private RectTransform transf;

    void Start() {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0f;

        transf = GetComponent<RectTransform>();
    }

    public void Show() {
        StartCoroutine(AlphaCoroutine());
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator AlphaCoroutine() {
        group.alpha = 0f;

        // Fade in
        float startTime = Time.time;
        while (true) {
            yield return new WaitForEndOfFrame();
            float currentTime = Time.time;
            if (currentTime - startTime >= fadeDuration) break;
            group.alpha = (currentTime - startTime) / fadeDuration;
        }

        group.alpha = 1f;

        //
        yield return new WaitForSeconds(stayDuration);

        // Fade out
        startTime = Time.time;
        while (true) {
            yield return new WaitForEndOfFrame();
            float currentTime = Time.time;
            if (currentTime - startTime >= fadeDuration) break;
            group.alpha = 1f - ((currentTime - startTime) / fadeDuration);
        }

        group.alpha = 0f;
    }

    IEnumerator MoveCoroutine() {
        var pos = transf.localPosition;
        pos.y = startY;
        transf.localPosition = pos;

        float totalDuration = 2 * fadeDuration + stayDuration;
        float startTime = Time.time;
        float deltaY = endY - startY;
        while (true) {
            yield return new WaitForEndOfFrame();
            float delta = Time.time - startTime;
            if (delta >= totalDuration) break;
            pos.y = deltaY * (delta / totalDuration) + startY;
            transf.localPosition = pos;
        }

        pos.y = endY;
        transf.localPosition = pos;
    }
}
