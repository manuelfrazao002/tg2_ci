using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Image Below;
    public Image Cover;

    private bool isFlipping = false;
    private bool isFaceUp = false;

    public void Awake()
    {
        Below.gameObject.SetActive(true);
        Cover.gameObject.SetActive(true);
    }

    public void SetBelowImage(Sprite newImage)
    {
        Below.color = Color.white;
        Below.sprite = newImage;
    }

    public IEnumerator FlipCardCoroutine(float duration = 0.5f)
    {
        if (isFlipping) yield break;
        isFlipping = true;

        float halfDuration = duration / 2f;
        float time = 0f;

        Vector3 startScale = transform.localScale;
        Vector3 midScale = new Vector3(0f, 1f, 1f);
        Vector3 endScale = new Vector3(1f, 1f, 1f);

        while (time < halfDuration)
        {
            float t = time / halfDuration;
            transform.localScale = Vector3.Lerp(startScale, midScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = midScale;

        // Toggle cover visibility
        Cover.gameObject.SetActive(isFaceUp); // Show cover if it was face up

        isFaceUp = !isFaceUp;

        time = 0f;
        while (time < halfDuration)
        {
            float t = time / halfDuration;
            transform.localScale = Vector3.Lerp(midScale, endScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;

        isFlipping = false;
    }

    public bool IsFaceUp {
      get {return isFaceUp;}
    }
}