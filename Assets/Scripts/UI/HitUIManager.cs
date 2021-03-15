using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HitUIManager : MonoBehaviour {
  Image hitCanvasBackgroundImage;

  private Color red = new Color(0.509434f, 0f, 0f, 0.08627451f);
  private Color nullColor = new Color(0f, 0f, 0f, 0f);

  public void OnPlayerHit() {
    StartCoroutine(changeBackground());
  }

  private void Start() {
    hitCanvasBackgroundImage = gameObject.GetComponent<Image>();
  }

  private IEnumerator changeBackground() {
    hitCanvasBackgroundImage.color = red;

    yield return new WaitForSeconds(0.2f); 

    hitCanvasBackgroundImage.color = nullColor;
  }
}
