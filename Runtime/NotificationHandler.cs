using UnityEngine;
using TMPro;
using System.Collections;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;
    public TMP_Text notificationText;

    public float holdDuration = 2.5f;
    public float fadeDuration = 0.4f;

    private Coroutine current;

    void Awake()
    {
        Instance = this;
        notificationText.alpha = 0f;
    }

    public void Show(string message)
    {
        // If a notification is already being displayed, stop it and start the new one
        if (current != null)
            StopCoroutine(current);
        current = StartCoroutine(DisplayRoutine(message));
    }

    IEnumerator DisplayRoutine(string message)
    {
        // Set the message and fade in
        notificationText.text = message;
        yield return Fade(0f, 1f, fadeDuration);
        yield return new WaitForSeconds(holdDuration);
        yield return Fade(1f, 0f, fadeDuration);
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        // Lerp the alpha value of the text from 'from' to 'to' over the specified duration
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            notificationText.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        notificationText.alpha = to;
    }
}
