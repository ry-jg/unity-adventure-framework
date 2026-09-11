using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

// Ryan Jung - WinScreen
// Call Show() from the final keypad's OnUnlocked event.
// Fades in a win panel, waits, then reloads the scene.
public class WinScreen : MonoBehaviour
{
    public GameObject winPanel;
    public TMP_Text winText;
    public float holdDuration = 5.0f;

    public void Show()
    {
        // Show the win panel and start the restart coroutine
        winPanel.SetActive(true);
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(holdDuration);

        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}